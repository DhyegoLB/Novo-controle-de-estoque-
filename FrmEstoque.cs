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
        string data_source = "server=localhost;Database=controle_estoque;Uid=root;"; //fazer conexão com o banco de dados
        private readonly byte[] chave = GerarChave256Bits();

        Thread nt;
        public FrmEstoque()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            nt = new Thread(novofrmSaidaproduto);
            nt.SetApartmentState(ApartmentState.STA);
            nt.Start();
        }
        private void novofrmSaidaproduto()
        {
            Application.Run(new FrmSaidaproduto());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            nt = new Thread(novofrmEntradaproduto);
            nt.SetApartmentState(ApartmentState.STA);
            nt.Start();
        }
        private void novofrmEntradaproduto()
        {
            Application.Run(new FrmEntradaproduto());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (conexao = new MySqlConnection(data_source))
                {
                    string sql = "INSERT INTO estoque( nome,codigo_produto,quantidade)" +
                                           "VALUES ( @nome, @codigo_produto, @quantidade)";

                    using (MySqlCommand comando = new MySqlCommand(sql, conexao))
                    {
                        comando.Parameters.AddWithValue("@nome", txtNome.Text);
                        comando.Parameters.AddWithValue("@codigo_produto", txtCodigoProduto.Text);
                        comando.Parameters.AddWithValue("@quantidade", txtQuantidade.Text);


                        conexao.Open();
                        comando.ExecuteNonQuery();
                    }
                        MessageBox.Show("Dados inseridos com sucesso!");
                        txtNome.Clear();
                        txtCodigoProduto.Clear();
                        txtQuantidade.Clear();
                    

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

