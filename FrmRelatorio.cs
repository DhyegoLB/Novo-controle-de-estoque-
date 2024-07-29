using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using MySql.Data.MySqlClient;

namespace controle_de_estoque1
{
    public partial class FrmRelatorio : Form
    {

        private MySqlConnection conexao;
        private string data_source = "server=localhost;Database=controle_estoque;Uid=root;pwd='123456'"; // Fazer conexão com o banco de dados
        private readonly byte[] chave = GerarChave256Bits();

        Thread nt;
        public FrmRelatorio()
        {
            InitializeComponent();
            ConfigurarListView();
        }

        private void ConfigurarListView() // Configurando o ListView para o cadastro já registrado
        {
            listView1.View = View.Details;
            listView1.LabelEdit = true;
            listView1.AllowColumnReorder = true;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("id", 50);
            listView1.Columns.Add("codigo", 100);
            listView1.Columns.Add("nome", 150);
            listView1.Columns.Add("fornecedor", 150);
            listView1.Columns.Add("fabricante", 150);
            listView1.Columns.Add("quantidade_minima", 150);

            listView2.View = View.Details;
            listView2.LabelEdit = true;
            listView2.AllowColumnReorder = true;
            listView2.FullRowSelect = true;
            listView2.Columns.Add("id", 50);
            listView2.Columns.Add("nome", 100);
            listView2.Columns.Add("cpf", 50);
            listView2.Columns.Add("rg", 50);
            listView2.Columns.Add("contato", 50);
            listView2.Columns.Add("email", 100);
            listView2.Columns.Add("pis", 50);
            listView2.Columns.Add("cep", 50);
            listView2.Columns.Add("bairro", 50);
            listView2.Columns.Add("carteira_trabalho", 100);
            listView2.Columns.Add("genero", 50);
            listView2.Columns.Add("estado_civil", 50);
            listView2.Columns.Add("uf", 20);
            listView2.Columns.Add("nacionalidade", 100);
            listView2.Columns.Add("admissao", 100);
            listView2.Columns.Add("senha", 255);
        }

        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //isserido errado só para lembrar
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

        private void butGerar1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conexao = new MySqlConnection(data_source))
                {
                    string query = "%" + txtGerar1.Text + "%";
                    string sql = "SELECT id, codigo, nome, fabricante, fornecedor, quantidade_minima " +
                                 "FROM cadastro_pd WHERE nome LIKE @query";

                    conexao.Open();
                    using (var comando = new MySqlCommand(sql, conexao))
                    {
                        comando.Parameters.AddWithValue("@query", query);
                        using (var reader = comando.ExecuteReader())
                        {
                            listView1.Items.Clear(); // Limpar o listView antes de adicionar os itens

                            if (!reader.HasRows)
                            {
                                MessageBox.Show("Nenhum registro encontrado");
                                return;
                            }

                            while (reader.Read())
                            {
                                string id = reader.IsDBNull(0) ? string.Empty : reader.GetInt32(0).ToString();
                                string codigo = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                                string nome = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                                string fabricante = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                                string fornecedor = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                                string quantidade_minima = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);

                                string[] row = { id, codigo, nome, fabricante, fornecedor, quantidade_minima };

                                var linha_listview = new ListViewItem(row);
                                listView1.Items.Add(linha_listview);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            // listView1 de maneira errada...
        }

        private static byte[] GerarChave256Bits()
        {
            // Gerando uma chave fixa
            return Encoding.UTF8.GetBytes("12345678901234567890123456789012");
        }

        private void butGerar2_Click(object sender, EventArgs e)
        {
            try
            {
                using (conexao = new MySqlConnection(data_source))
                {
                    string query = "%" + txtGerar2.Text + "%";
                    string sql = "SELECT id, nome, cpf, rg, contato, email, pis, cep, bairro, carteira_trabalho, genero, estado_civil, uf, nacionalidade, admissao, senha " +
                                 "FROM funcionario WHERE nome LIKE @query";

                    conexao.Open();
                    MySqlCommand comando = new MySqlCommand(sql, conexao);
                    comando.Parameters.AddWithValue("@query", query);
                    MySqlDataReader reader = comando.ExecuteReader();

                    listView2.Items.Clear(); // limpar o listView antes de adicionar o item

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


                        string[] row = { id, nome, cpf, rg, contato, email, pis, cep, bairro, carteira_trabalho, genero, estado_civil, uf, nacionalidade, admissao, senha, };

                        var linha_listview = new ListViewItem(row);
                        listView2.Items.Add(linha_listview);
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Erro:{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
    }

