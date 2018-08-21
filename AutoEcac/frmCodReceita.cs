using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoEcac
{
    public partial class frmCodReceita : Form
    {
        public string fCdReceita;
        public frmCodReceita()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            fCdReceita = edtCdReceita.Text;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
                
        }

        private void edtCdReceita_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk.PerformClick();
            }
        }
    }
}
