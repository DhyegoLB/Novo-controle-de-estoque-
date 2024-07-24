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
        string data_source = "server=localhost;Database=controle_estoque;Uid=root;"; //fazer conexão com o banco de dados.
        private readonly byte[] chave = GerarChave256Bits(); // onde a chave fixa sera gerada.


        Thread nt;

        public FrmCDproduto()
        {
            InitializeComponent();
        }

        private void FrmCDproduto_Load(object sender, EventArgs e)
        {

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
    }
    }

