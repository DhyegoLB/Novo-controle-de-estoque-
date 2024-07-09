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

namespace controle_de_estoque1
{
    public partial class FrmMenu : Form
    {
        Thread nt;
        public FrmMenu()
        {
            InitializeComponent();
        }
        DateTime time;
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblData_Click(object sender, EventArgs e)
        {

        }

        private void lblHora_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time = DateTime.Now;
            lblData.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblHora.Text = time.ToLongTimeString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            nt = new Thread(novoForm1);
            nt.SetApartmentState(ApartmentState.STA);
            nt.Start();
        }
        private void novoForm1()
        {
            Application.Run(new Form1());
        }

        private void cdtProduto_Click(object sender, EventArgs e)
        {

            this.Close();
            nt = new Thread(novoFrmCDproduto);
            nt.SetApartmentState(ApartmentState.STA);
            nt.Start();
        }
        private void novoFrmCDproduto()
        {
            Application.Run(new FrmCDproduto());
        }

        private void cdtUsuario_Click(object sender, EventArgs e)
        {
            this.Close();
            nt = new Thread(novoFrmCDfuncionario);
            nt.SetApartmentState (ApartmentState.STA);
            nt.Start();
        }
        private void novoFrmCDfuncionario()
        {
            Application.Run(new FrmCDfuncionario());
        }

        private void btnEstoque_Click(object sender, EventArgs e)
        {

            this.Close();
            nt = new Thread(novoFrmEstoque);
            nt.SetApartmentState(ApartmentState.STA);
            nt.Start();
        }
        private void novoFrmEstoque()
        {
            Application.Run(new FrmEstoque());
        }

        private void saida_Click(object sender, EventArgs e)
        {
            this.Close();
            nt = new Thread(novoFrmSaidaproduto);
            nt.SetApartmentState(ApartmentState.STA);
            nt.Start();
        }
        private void novoFrmSaidaproduto()
        {
            Application.Run(new FrmSaidaproduto());
        }

        private void entrada_Click(object sender, EventArgs e)
        {
            this.Close();
            nt = new Thread(novoFrmEntradaproduto);
            nt.SetApartmentState(ApartmentState.STA);
            nt.Start();
        }
        private void novoFrmEntradaproduto()
        {
            Application.Run(new FrmEntradaproduto());
        }

        private void Relatorio_Click(object sender, EventArgs e)
        {
            this.Close();
            nt = new Thread(novoFrmRelatorio);
            nt.SetApartmentState(ApartmentState.STA);
            nt.Start();
        }
        private void novoFrmRelatorio()
        {
            Application.Run(new FrmRelatorio());
        }

        private void rgtProduto_Click(object sender, EventArgs e)
        {

            this.Close();
            nt = new Thread(novoFrmEstoque1);
            nt.SetApartmentState(ApartmentState.STA);
            nt.Start();
        }
        private void novoFrmEstoque1()
        {
            Application.Run(new FrmEstoque());
        }

        private void Backup_Click(object sender, EventArgs e)
        {
            this.Close();
            nt = new Thread(novofrmBackup);
            nt.SetApartmentState(ApartmentState.STA);
            nt.Start();
        }

        private void novofrmBackup()
        {
            Application.Run(new FrmBackup());
        }
    }
}
