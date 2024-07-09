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
using System.Threading;

namespace controle_de_estoque1
{
    public partial class FrmEstoque : Form
    {
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
    }
}
