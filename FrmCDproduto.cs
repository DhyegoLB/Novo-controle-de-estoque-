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
    public partial class FrmCDproduto : Form
    {
        private MySqlConnection conexao;
        string data_source = "server=localhost;Database=controle_estoque;Uid=root;pwd='123456'"; //fazer conexão com o banco de dados.
        private readonly byte[] chave = GerarChave256Bits(); // onde a chave fixa sera gerada.


        Thread nt;

        public FrmCDproduto()
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
            listView1.Columns.Add("codigo", 100);
            listView1.Columns.Add("nome", 150);
            listView1.Columns.Add("fornecedor", 150);
            listView1.Columns.Add("fabricante", 150);
            listView1.Columns.Add("quantidade_minima", 150);
        }


        private void FrmCDproduto_Load(object sender, EventArgs e)
        {
            //criado por aperto de butão errado.
        }

        private void button3_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (conexao = new MySqlConnection(data_source))
                {
                    string sql = "INSERT INTO cadastro_pd( codigo, fabricante, fornecedor, nome, quantidade_minima)" +
                                           "VALUES (@codigo, @fabricante, @fornecedor, @nome, @quantidade_minima )";

                    using (MySqlCommand comando = new MySqlCommand(sql, conexao))
                    {
                        comando.Parameters.AddWithValue("@codigo", txtCodigo.Text);
                        comando.Parameters.AddWithValue("@fabricante", txtFabricante.Text);
                        comando.Parameters.AddWithValue("@fornecedor", txtFornecedor.Text);
                        comando.Parameters.AddWithValue("@nome", txtNome.Text);
                        comando.Parameters.AddWithValue("@quantidade_minima", txtQuantidadeMinima.Text);


                        conexao.Open();
                        comando.ExecuteNonQuery();
                    }
                    MessageBox.Show("Gravação dos Dados Inseridos com Sucesso!");
                    txtCodigo.Clear();
                    txtFabricante.Clear();
                    txtFornecedor.Clear();
                    txtNome.Clear();
                    txtQuantidadeMinima.Clear();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro:{ex.Message}\n{ex.StackTrace}");
            }
        }
        private static byte[] GerarChave256Bits()
        {
            //Aqui estamos gerando uma chave fixa
            byte[] chave = Encoding.UTF8.GetBytes("12345678901234567890123456789012");
            return chave;
        }

        private void butPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conexao = new MySqlConnection(data_source))
                {
                    string query = "%" + txtPesquisa.Text + "%";
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
            if (listView1.SelectedItems.Count > 0) // Selecionando do listView o que já está cadastrado no sistema pelo SQL
            {
                ListViewItem item = listView1.SelectedItems[0];
                txtCodigo.Text = item.SubItems[1].Text;
                txtNome.Text = item.SubItems[2].Text;
                txtFabricante.Text = item.SubItems[3].Text;
                txtFornecedor.Text = item.SubItems[4].Text;
                txtQuantidadeMinima.Text = item.SubItems[5].Text;
            }
        }


        private void butAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                using (conexao = new MySqlConnection(data_source))
                {
                    string sql = "UPDATE cadastro_pd SET codigo = @codigo, nome = @nome, fabricante = @fabricante, fornecedor = @fornecedor, quantidade_minima = @quantidade_minima  WHERE id = @id";

                    using (MySqlCommand comando = new MySqlCommand(sql, conexao))
                    {

                        comando.Parameters.AddWithValue("@id", listView1.SelectedItems[0].SubItems[0].Text); // Pegar o ID do item selecionado
                        comando.Parameters.AddWithValue("@codigo", txtCodigo.Text);
                        comando.Parameters.AddWithValue("@nome", txtNome.Text);
                        comando.Parameters.AddWithValue("@fabricante", txtFabricante.Text);
                        comando.Parameters.AddWithValue("@fornecedor", txtFornecedor.Text);
                        comando.Parameters.AddWithValue("@quantidade_minima", txtQuantidadeMinima.Text);
                       

                        conexao.Open();
                        comando.ExecuteNonQuery();
                    }

                    MessageBox.Show("Dados atualizados com sucesso");
                    txtCodigo.Clear();
                    txtNome.Clear();
                    txtFabricante.Clear();
                    txtQuantidadeMinima.Clear();
                    txtFornecedor.Clear();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}\n{ex.StackTrace}");
            }               //ATUALIZAR O CADASTRO
        }
    }
}