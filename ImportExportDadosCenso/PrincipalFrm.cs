using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportExportDadosCenso
{
    public partial class PrincipalFrm : Form
    {
        public PrincipalFrm()
        {
            InitializeComponent();
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            CreateDB tabela = new CreateDB();
            this.Parent = this.Parent;
            tabela.ShowDialog();
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            Default arquivo = new Default();
            this.Parent = this.Parent;
            arquivo.ShowDialog();
        }
    }
}
