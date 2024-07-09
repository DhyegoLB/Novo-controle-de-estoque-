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
    public partial class FrmEntradaproduto : Form
    {
        Thread nt;
        public FrmEntradaproduto()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
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
    }
}
