using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Runtime.ConstrainedExecution;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;
using static Mysqlx.Expect.Open.Types.Condition.Types;

namespace controle_de_estoque1
{
    public partial class FrmCDfuncionario : Form
    {
        Thread nt;

        private MySqlConnection conexao;
        string data_source = "server=localhost;Database=controle_estoque;Uid=root;pwd='123456'"; //fazer conexão com o banco de dados
        private readonly byte[] chave = GerarChave256Bits(); //criacão de criptografia
        public FrmCDfuncionario()
        {
            InitializeComponent();
            ConfigurarlistView();
        }

        private void ConfigurarlistView() //configurando o listeview para o cadastro ja registrado.
        {
            listView1.View = View.Details;
            listView1.LabelEdit = true;
            listView1.AllowColumnReorder = true;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("id", 50);
            listView1.Columns.Add("nome", 100);
            listView1.Columns.Add("cpf", 50);
            listView1.Columns.Add("rg", 50);
            listView1.Columns.Add("contato", 50);
            listView1.Columns.Add("email", 100);
            listView1.Columns.Add("pis", 50);
            listView1.Columns.Add("cep", 50);
            listView1.Columns.Add("bairro", 50);
            listView1.Columns.Add("carteira_trabalho", 100);
            listView1.Columns.Add("genero", 50);
            listView1.Columns.Add("estado_civil", 50);
            listView1.Columns.Add("uf", 20);
            listView1.Columns.Add("nacionalidade", 100);
            listView1.Columns.Add("admissao", 100);
            listView1.Columns.Add("senha", 255);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Close();
            nt = new Thread(novofrmMenu);
            nt.SetApartmentState(ApartmentState.STA);
            nt.Start();
        }

        private void novofrmMenu()
        {
            Application.Run(new FrmMenu());
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                using (conexao = new MySqlConnection(data_source))
                {
                    string sql = "INSERT INTO funcionario(  nome, cpf, contato, rg, bairro, email, pis, cep, carteira_trabalho, genero, estado_civil, uf, nacionalidade, senha)" +
                                           "VALUES  (  @nome, @cpf, @contato, @rg, @bairro,  @email, @pis, @cep, @carteira_trabalho, @genero, @estado_civil, @uf, @nacionalidade, @senha)";

                    using (MySqlCommand comando = new MySqlCommand(sql, conexao))
                    {
                        string senha = txtSenha.Text;

                        if (!ValidarSenha(senha)) //tem que atender os criterios da senha
                        {
                            MessageBox.Show("A senha não atende aos criterios de segurança.");
                            MessageBox.Show("Pelo menos 8 caracteres de comprimento.\nPelo menos uma letra maiuscula (A-Z).\nPelo menos uma letra minuscula (a-z).\nPelo menos um digito númerico (1-9).\nPelo menos um caractere especial (!, @, #, $, %, etc.\nNão conter caractere repetidos consecutivos.");
                            return;
                        }

                        string senhaCriptografada = CriptografarSenhaAES(senha);


                        comando.Parameters.AddWithValue("@nome", txtNome.Text);
                        comando.Parameters.AddWithValue("@cpf", txtCpf.Text);
                        comando.Parameters.AddWithValue("@contato", txtContato.Text);
                        comando.Parameters.AddWithValue("@rg", txtRg.Text);
                        comando.Parameters.AddWithValue("@bairro", txtBairro.Text);
                        comando.Parameters.AddWithValue("@email", txtEmail.Text);
                        comando.Parameters.AddWithValue("@cep", txtCep.Text); 
                        comando.Parameters.AddWithValue("@pis", txtPis.Text);
                        comando.Parameters.AddWithValue("@carteira_trabalho", txtCarteiraTrabalho.Text);
                        comando.Parameters.AddWithValue("@genero", txtGenero.Text);
                        comando.Parameters.AddWithValue("@estado_civil", txtEstadoCivil.Text);
                        comando.Parameters.AddWithValue("@uf", txtUf.Text);
                        comando.Parameters.AddWithValue("@nacionalidade", txtNacionalidade.Text);
                        comando.Parameters.AddWithValue("@senha", senhaCriptografada);

                        conexao.Open();
                        comando.ExecuteNonQuery();

                    }
                    MessageBox.Show("Dados inseridos com sucesso!");
                    txtNome.Clear();
                    txtCpf.Clear();
                    txtPis.Clear();
                    txtCarteiraTrabalho.Clear();
                    txtGenero.Clear();
                    txtEstadoCivil.Clear();
                    txtUf.Clear();
                    txtNacionalidade.Clear();
                    txtAdmissao.Clear();
                    txtEmail.Clear();
                    txtContato.Clear();
                    txtBairro.Clear();
                    txtRg.Clear();
                    txtCep.Clear();
                   

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro:{ex.Message}\n{ex.StackTrace}");
            }
        }
        private string CriptografarSenhaAES(string senha)
        {
                if (chave.Length != 32)
                    throw new ArgumentException("A chave deve ter exatamente 32 bytes)");

                byte[] iv = GerarIV();

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = chave;
                    aesAlg.IV = iv;

                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(senha);
                            }
                            byte[] encryptedContent = msEncrypt.ToArray();
                            byte[] result = new byte[iv.Length + encryptedContent.Length];
                            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                            Buffer.BlockCopy(encryptedContent, 0, result, iv.Length, encryptedContent.Length);
                            return Convert.ToBase64String(result);


                        }
                    }


                }
            }
            private static byte[] GerarIV()
            {
                using (var rng = new RNGCryptoServiceProvider())
                {
                    byte[] iv = new byte[16];
                    rng.GetBytes(iv);
                    return iv;
                }
            }
            private static byte[] GerarChave256Bits()
            {
                //Aqui estamos gerando uma chave fixa
                byte[] chave = Encoding.UTF8.GetBytes("12345678901234567890123456789012");
                return chave;
            }

            private bool ValidarSenha(string senha)
            {
                if (senha.Length < 8)
                {
                    return false;
                }

                if (!Regex.IsMatch(senha, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$"))
                {
                    return false;
                }

                for (int i = 0; i < senha.Length - 1; i++)
                {
                    if (senha[i] == senha[i + 1])
                    {
                        return false;
                    }
                }

                return true;

            }

        private void label17_Click(object sender, EventArgs e)
        {
            //lebel criada errada
        }

        private void butPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                using (conexao = new MySqlConnection(data_source))
                {
                    string query = "%" + txtPesquisa.Text + "%";
                    string sql = "SELECT id, nome, cpf, rg, contato, email, pis, cep, bairro, carteira_trabalho, genero, estado_civil, uf, nacionalidade, admissao, senha " +
                                 "FROM funcionario WHERE nome LIKE @query";

                    conexao.Open();
                    MySqlCommand comando = new MySqlCommand(sql, conexao);
                    comando.Parameters.AddWithValue("@query", query);
                    MySqlDataReader reader = comando.ExecuteReader();

                    listView1.Items.Clear(); // limpar o listView antes de adicionar o item

                    if (!reader.HasRows)
                    {
                        MessageBox.Show("Nenhum registro encontrado");
                        return;
                    }

                    while (reader.Read())
                    {
                        // LER OS DADOS DO ARMAZENAMENTO ou ARMAZENADOS
                        string id = reader.IsDBNull(0) ? string.Empty : reader.GetInt32(0).ToString();
                        string nome = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        string cpf = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        string rg = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        string contato = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        string email = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        string pis = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        string cep = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        string bairro = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        string carteira_trabalho = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        string genero = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        string estado_civil = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                        string uf = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                        string nacionalidade = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                        string admissao = reader.IsDBNull(14) ? string.Empty : reader.GetDateTime(14).ToString("yyyy-MM-dd HH:mm:ss");
                        string senha = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);

                        string[] row = { id, nome, cpf, rg, contato, email, pis, cep, bairro, carteira_trabalho, genero, estado_civil, uf, nacionalidade, admissao, senha };

                        var linha_listview = new ListViewItem(row);
                        listView1.Items.Add(linha_listview);
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Erro:{ex.Message}\n{ex.StackTrace}");
            }
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // erro ao introduzir o partar do botao.
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0) //selecionando do listview o que ja esta cadastrado no sistema pelo sql
            {
                ListViewItem item = listView1.SelectedItems[0];
                txtNome.Text = item.SubItems[1].Text;
                txtCpf.Text = item.SubItems[2].Text;
                txtContato.Text = item.SubItems[3].Text;
                txtRg.Text = item.SubItems[4].Text;
                txtBairro.Text = item.SubItems[5].Text;
                txtEmail.Text = item.SubItems[6].Text;
                txtPis.Text = item.SubItems[7].Text;
                txtCep.Text = item.SubItems[8].Text;
                txtCarteiraTrabalho.Text = item.SubItems[9].Text;
                txtGenero.Text = item.SubItems[10].Text;
                txtEstadoCivil.Text = item.SubItems[11].Text;
                txtUf.Text = item.SubItems[12].Text;
                txtNacionalidade.Text = item.SubItems[13].Text;
                txtAdmissao.Text = item.SubItems[14].Text;
                txtSenha.Text = item.SubItems[15].Text;
                
            }
        }

        private void txtPesquisa_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // erro ao introduzir o partar do botao.
        }

        private void butAtualizar_Click(object sender, EventArgs e)
        {

        }
    }
    }
    

    


    

