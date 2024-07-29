using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.IO;
using System.Threading;

namespace controle_de_estoque1
{
    public partial class Form1 : Form
    {
        Thread nt; //Emergencia para voltar ao menu

        private MySqlConnection conexao;
        string data_source = "Server=localhost;Database=controle_estoque;Uid=root;pwd='123456'"; //fazer conexão com o banco de dados
        private readonly byte[] chave = GerarChave256Bits(); //criacão de criptografia
        private readonly byte[] iv = GerarIV();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {



            string usuario = txtUsuario.Text;
        string senha = txtSenha.Text;
            

            try
            {
                using (conexao = new MySqlConnection(data_source))
                {
                    string sql = "SELECT senha FROM funcionario WHERE nome = @nome";
                    MySqlCommand comando = new MySqlCommand(sql, conexao);
                    comando.Parameters.AddWithValue("@nome", usuario);

                    conexao.Open();
                    object resultado = comando.ExecuteScalar();

                    if (resultado != null)
                    {
                        string senhaCriptografada = resultado.ToString();
                        string senhaDescriptografada = DescriptografarSenhaAES(senhaCriptografada);

                        if (senhaDescriptografada == senha)
                        {
                            FrmMenu frmMenu = new FrmMenu();
                            frmMenu.Show(); //botão de Emergencia para voltar ao menu
                            this.Hide();    
                        }
                        else
                        {
                            MessageBox.Show("Usuario ou senha incorretos");
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}\n{ex.StackTrace}");
            }


        }
        private string DescriptografarSenhaAES(string senhaCriptografada)
        {
            
            try
            {
                byte[] encryptoBytesWitchIV = Convert.FromBase64String(senhaCriptografada);
                byte[] iv = new byte[16];
                byte[] encryptoBytes = new byte[encryptoBytesWitchIV.Length - iv.Length];

                //Extrair o IV dos primeirs 16 bytes da senha criptografada
                Buffer.BlockCopy(encryptoBytesWitchIV, 0, iv, 0, iv.Length);
                //Extrair os bytes restantes criptografados
                Buffer.BlockCopy(encryptoBytesWitchIV, iv.Length, encryptoBytes, 0, encryptoBytes.Length);

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = chave;
                    aesAlg.IV = iv;

                    ICryptoTransform descryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(encryptoBytes))
                    {
                        using (CryptoStream csDescrypt = new CryptoStream(msDecrypt, descryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDescrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Erro na descriptografia:{ex.StackTrace}");
                return null;
            }

        }


        private static byte[] GerarChave256Bits()
        {
            //Chave de 32 bytes (256 bits)
            string chaveString = "12345678901234567890123456789012";
            return Encoding.UTF8.GetBytes(chaveString);
        }

        private static byte[] GerarIV()
        {
            //IV de 16 bytes
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] iv = new byte[16];
                rng.GetBytes(iv);
                return iv;
            }
            
        }

    }
}
