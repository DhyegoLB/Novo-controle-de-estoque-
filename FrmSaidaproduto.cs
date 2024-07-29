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
    public partial class FrmSaidaproduto : Form
    {
        private MySqlConnection conexao;
        string data_source = "server=localhost;Database=controle_estoque;Uid=root;pws'123456'"; //fazer conexão com o banco de dados.
        private readonly byte[] chave = GerarChave256Bits(); // onde a chave fixa sera gerada.


        Thread nt;
        public FrmSaidaproduto()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            nt = new Thread(novofrmEstoque);
            nt.SetApartmentState(ApartmentState.STA);
            nt.Start();
        }

        private void novofrmEstoque()
        {
            Application.Run(new FrmEstoque());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (conexao = new MySqlConnection(data_source))
                {
                    string sql = "INSERT INTO saida( codigo_produto, descricao, quantidade_estoque)" +
                                           "VALUES ( @codigo_produto,@descricao, @quantidade_estoque)";

                    using (MySqlCommand comando = new MySqlCommand(sql, conexao))
                    {
                        comando.Parameters.AddWithValue("@codigo_produto", txtCodigoProduto.Text);
                        comando.Parameters.AddWithValue("@quantidade_estoque", txtQuantidadeEstoque.Text);
                        comando.Parameters.AddWithValue("@descricao", txtDescricao.Text);

                        conexao.Open();
                        comando.ExecuteNonQuery();
                    }
                    MessageBox.Show("Gravação dos Dados Inseridos com Sucesso!");
                    txtDescricao.Clear();
                    txtCodigoProduto.Clear();
                    txtQuantidadeEstoque.Clear();


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
    }
    }

