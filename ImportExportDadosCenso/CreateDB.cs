using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ImportExportDadosCenso
{
    public partial class CreateDB : Form
    {
        public CreateDB()
        {
            InitializeComponent();
        }



        #region Montando a base de dados
        protected bool CriouTabela = false;
        private string vbCrLf;

        #endregion

        private void CreateDB_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }
        public void gerar()
        {
            string sql = "IF EXISTS (" +
            "SELECT * " +
            "FROM master.dbo.sysdatabases " +
            "WHERE Name =  'TesteDB' )" + vbCrLf +
            "DROP DATABASE  TesteDB" + vbCrLf +
            "CREATE DATABASE TesteDB";

        }

    }
}
