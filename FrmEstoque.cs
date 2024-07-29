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

namespace controle_de_estoque1
{
    public partial class FrmEstoque : Form
    {
        private MySqlConnection conexao;
        private string data_source = "server=localhost;Database=controle_estoque;Uid=root;pwd='123456'"; // Fazer conexão com o banco de dados
        private readonly byte[] chave = GerarChave256Bits();

        public FrmEstoque()
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
            listView1.Columns.Add("ID", 50);
            listView1.Columns.Add("Código Produto", 100);
            listView1.Columns.Add("Nome", 100);
            listView1.Columns.Add("Descrição", 100);
            listView1.Columns.Add("Quantidade Entrada", 100);
            listView1.Columns.Add("Quantidade Saída", 100);
            listView1.Columns.Add("Quantidade", 50);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            var nt = new Thread(novofrmMenu);
            nt.SetApartmentState(ApartmentState.STA);
            nt.Start();
        }

        private void novofrmMenu()
        {
            Application.Run(new FrmMenu());
        }

        private void button3_Click(object sender, EventArgs e) // Atualizar
        {
            try
            {
                using (conexao = new MySqlConnection(data_source))
                {
                    string sql = "UPDATE estoque SET codigo_produto = @codigo_produto, nome = @nome, descricao = @descricao, quantidade_entrada = @quantidade_entrada, quantidade_saida = @quantidade_saida, quantidade = @quantidade  WHERE id = @id";

                    using (MySqlCommand comando = new MySqlCommand(sql, conexao))
                    {

                        comando.Parameters.AddWithValue("@id", listView1.SelectedItems[0].SubItems[0].Text); // Pegar o ID do item selecionado
                        comando.Parameters.AddWithValue("@codigo_produto", txtCodigoProduto.Text);
                        comando.Parameters.AddWithValue("@nome", txtNome.Text);
                        comando.Parameters.AddWithValue("@descricao", txtDescricao.Text);
                        comando.Parameters.AddWithValue("@quantidade_entrada", txtQuantidadeEntrada.Text);
                        comando.Parameters.AddWithValue("@quantidade_saida", txtQuantidadeSaida.Text);
                        comando.Parameters.AddWithValue("@quantidade", txtQuantidade.Text);


                        conexao.Open();
                        comando.ExecuteNonQuery();
                    }

                    MessageBox.Show("Dados atualizados com sucesso");
                    txtCodigoProduto.Clear();
                    txtNome.Clear();
                    txtDescricao.Clear();
                    txtQuantidadeEntrada.Clear();
                    txtQuantidadeSaida.Clear();
                    txtQuantidade.Clear();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}\n{ex.StackTrace}");
            }               //ATUALIZAR O CADASTRO
        }
    

        private void button4_Click(object sender, EventArgs e) // Pesquisar
        {
            try
            {
                using (var conexao = new MySqlConnection(data_source))
                {
                    string query = "%" + txtPesquisar.Text + "%";
                    string sql = "SELECT id, codigo_produto, nome, descricao, quantidade_entrada, quantidade_saida, quantidade " +
                                 "FROM estoque WHERE nome LIKE @query"; 

                    conexao.Open();
                    using (var comando = new MySqlCommand(sql, conexao))
                    {
                        comando.Parameters.AddWithValue("@query", query);
                        using (var reader = comando.ExecuteReader())
                        {
                            listView1.Items.Clear(); // Limpar o ListView antes de adicionar os itens

                            if (!reader.HasRows)
                            {
                                MessageBox.Show("Nenhum registro encontrado");
                                return;
                            }

                            while (reader.Read())
                            {
                                string id = reader.IsDBNull(0) ? string.Empty : reader.GetInt32(0).ToString();
                                string codigo_produto = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                                string nome = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                                string descricao = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                                string quantidade_entrada = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                                string quantidade_saida = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                                string quantidade = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);

                                string[] row = { id, codigo_produto, nome, descricao, quantidade_entrada, quantidade_saida, quantidade };

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

        private void button1_Click(object sender, EventArgs e) // Inserir
        {
            try
            {
                using (conexao = new MySqlConnection(data_source))
                {
                    string sql = "INSERT INTO estoque(codigo_produto, nome, descricao, quantidade_entrada, quantidade_saida, quantidade) " +
                                 "VALUES (@codigo_produto, @nome, @descricao, @quantidade_entrada, @quantidade_saida, @quantidade)";

                    using (var comando = new MySqlCommand(sql, conexao))
                    {
                        // Adicionando parâmetros à consulta
                        comando.Parameters.AddWithValue("@codigo_produto", txtCodigoProduto.Text);
                        comando.Parameters.AddWithValue("@nome", txtNome.Text);
                        comando.Parameters.AddWithValue("@descricao", txtDescricao.Text);
                        comando.Parameters.AddWithValue("@quantidade_entrada", txtQuantidadeEntrada.Text);
                        comando.Parameters.AddWithValue("@quantidade_saida", txtQuantidadeSaida.Text);
                        comando.Parameters.AddWithValue("@quantidade", txtQuantidade.Text);

                        // Abrindo a conexão e executando a consulta
                        conexao.Open();
                        comando.ExecuteNonQuery();
                    }

                    // Mensagem de sucesso e limpeza dos campos
                    MessageBox.Show("Dados inseridos com sucesso!");
                    LimparCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private void LimparCampos()
        {
            txtCodigoProduto.Clear();
            txtNome.Clear();
            txtDescricao.Clear();
            txtQuantidadeEntrada.Clear();
            txtQuantidadeSaida.Clear();
            txtQuantidade.Clear();
        }

        private static byte[] GerarChave256Bits()
        {
            // Gerando uma chave fixa
            return Encoding.UTF8.GetBytes("12345678901234567890123456789012");
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0) // Selecionando do ListView o que já está cadastrado no sistema pelo SQL
            {
                ListViewItem item = listView1.SelectedItems[0];
                txtCodigoProduto.Text = item.SubItems[1].Text;
                txtNome.Text = item.SubItems[2].Text;
                txtDescricao.Text = item.SubItems[3].Text;
                txtQuantidadeEntrada.Text = item.SubItems[4].Text;
                txtQuantidadeSaida.Text = item.SubItems[5].Text;
                txtQuantidade.Text = item.SubItems[6].Text;
            }
        }
    }
}

