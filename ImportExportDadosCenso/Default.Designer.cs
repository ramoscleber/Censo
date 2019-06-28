namespace ImportExportDadosCenso
{
    partial class Default
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Default));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnAtualizarNomeDataNasc = new System.Windows.Forms.Button();
            this.pCarregando = new System.Windows.Forms.PictureBox();
            this.btnGerarArquivoCSV = new System.Windows.Forms.Button();
            this.pbVinculoAlunoCurso = new System.Windows.Forms.PictureBox();
            this.lblTotalVinculosAluno = new System.Windows.Forms.Label();
            this.pbRegistro = new System.Windows.Forms.PictureBox();
            this.pbAluno = new System.Windows.Forms.PictureBox();
            this.lblTotalRegistros = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gvArquivosCsv = new System.Windows.Forms.DataGridView();
            this.btnInserirRegistro = new System.Windows.Forms.Button();
            this.lblTotalLinhasRegistroAluno = new System.Windows.Forms.Label();
            this.btnAbrirArquivo = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pCarregando)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVinculoAlunoCurso)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRegistro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAluno)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvArquivosCsv)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(953, 500);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnAtualizarNomeDataNasc);
            this.tabPage1.Controls.Add(this.pCarregando);
            this.tabPage1.Controls.Add(this.btnGerarArquivoCSV);
            this.tabPage1.Controls.Add(this.pbVinculoAlunoCurso);
            this.tabPage1.Controls.Add(this.lblTotalVinculosAluno);
            this.tabPage1.Controls.Add(this.pbRegistro);
            this.tabPage1.Controls.Add(this.pbAluno);
            this.tabPage1.Controls.Add(this.lblTotalRegistros);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.gvArquivosCsv);
            this.tabPage1.Controls.Add(this.btnInserirRegistro);
            this.tabPage1.Controls.Add(this.lblTotalLinhasRegistroAluno);
            this.tabPage1.Controls.Add(this.btnAbrirArquivo);
            this.tabPage1.Controls.Add(this.txtPath);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(945, 467);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Exibição do arquivo";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnAtualizarNomeDataNasc
            // 
            this.btnAtualizarNomeDataNasc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAtualizarNomeDataNasc.Enabled = false;
            this.btnAtualizarNomeDataNasc.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtualizarNomeDataNasc.Location = new System.Drawing.Point(641, 434);
            this.btnAtualizarNomeDataNasc.Name = "btnAtualizarNomeDataNasc";
            this.btnAtualizarNomeDataNasc.Size = new System.Drawing.Size(166, 27);
            this.btnAtualizarNomeDataNasc.TabIndex = 51;
            this.btnAtualizarNomeDataNasc.Text = "Atualizar Nome-Data";
            this.btnAtualizarNomeDataNasc.UseVisualStyleBackColor = true;
            this.btnAtualizarNomeDataNasc.Click += new System.EventHandler(this.btnAtualizarNomeDataNasc_Click);
            // 
            // pCarregando
            // 
            this.pCarregando.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pCarregando.Image = ((System.Drawing.Image)(resources.GetObject("pCarregando.Image")));
            this.pCarregando.Location = new System.Drawing.Point(327, 179);
            this.pCarregando.Name = "pCarregando";
            this.pCarregando.Size = new System.Drawing.Size(291, 125);
            this.pCarregando.TabIndex = 50;
            this.pCarregando.TabStop = false;
            this.pCarregando.Visible = false;
            // 
            // btnGerarArquivoCSV
            // 
            this.btnGerarArquivoCSV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGerarArquivoCSV.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGerarArquivoCSV.Image = ((System.Drawing.Image)(resources.GetObject("btnGerarArquivoCSV.Image")));
            this.btnGerarArquivoCSV.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGerarArquivoCSV.Location = new System.Drawing.Point(666, 14);
            this.btnGerarArquivoCSV.Name = "btnGerarArquivoCSV";
            this.btnGerarArquivoCSV.Size = new System.Drawing.Size(273, 27);
            this.btnGerarArquivoCSV.TabIndex = 49;
            this.btnGerarArquivoCSV.Text = "Criar arquivo para importação de dados";
            this.btnGerarArquivoCSV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGerarArquivoCSV.UseVisualStyleBackColor = true;
            this.btnGerarArquivoCSV.Click += new System.EventHandler(this.btnGerarArquivoCSV_Click_1);
            // 
            // pbVinculoAlunoCurso
            // 
            this.pbVinculoAlunoCurso.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbVinculoAlunoCurso.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbVinculoAlunoCurso.BackgroundImage")));
            this.pbVinculoAlunoCurso.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbVinculoAlunoCurso.Location = new System.Drawing.Point(585, 429);
            this.pbVinculoAlunoCurso.Name = "pbVinculoAlunoCurso";
            this.pbVinculoAlunoCurso.Size = new System.Drawing.Size(33, 34);
            this.pbVinculoAlunoCurso.TabIndex = 45;
            this.pbVinculoAlunoCurso.TabStop = false;
            this.pbVinculoAlunoCurso.Visible = false;
            // 
            // lblTotalVinculosAluno
            // 
            this.lblTotalVinculosAluno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalVinculosAluno.AutoSize = true;
            this.lblTotalVinculosAluno.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalVinculosAluno.ForeColor = System.Drawing.Color.Green;
            this.lblTotalVinculosAluno.Location = new System.Drawing.Point(621, 442);
            this.lblTotalVinculosAluno.Name = "lblTotalVinculosAluno";
            this.lblTotalVinculosAluno.Size = new System.Drawing.Size(20, 18);
            this.lblTotalVinculosAluno.TabIndex = 44;
            this.lblTotalVinculosAluno.Text = "...";
            this.lblTotalVinculosAluno.Visible = false;
            // 
            // pbRegistro
            // 
            this.pbRegistro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbRegistro.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbRegistro.BackgroundImage")));
            this.pbRegistro.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbRegistro.Location = new System.Drawing.Point(6, 427);
            this.pbRegistro.Name = "pbRegistro";
            this.pbRegistro.Size = new System.Drawing.Size(33, 33);
            this.pbRegistro.TabIndex = 43;
            this.pbRegistro.TabStop = false;
            this.pbRegistro.Visible = false;
            // 
            // pbAluno
            // 
            this.pbAluno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbAluno.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbAluno.BackgroundImage")));
            this.pbAluno.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbAluno.Location = new System.Drawing.Point(296, 429);
            this.pbAluno.Name = "pbAluno";
            this.pbAluno.Size = new System.Drawing.Size(33, 34);
            this.pbAluno.TabIndex = 42;
            this.pbAluno.TabStop = false;
            this.pbAluno.Visible = false;
            // 
            // lblTotalRegistros
            // 
            this.lblTotalRegistros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalRegistros.AutoSize = true;
            this.lblTotalRegistros.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRegistros.ForeColor = System.Drawing.Color.Green;
            this.lblTotalRegistros.Location = new System.Drawing.Point(332, 442);
            this.lblTotalRegistros.Name = "lblTotalRegistros";
            this.lblTotalRegistros.Size = new System.Drawing.Size(20, 18);
            this.lblTotalRegistros.TabIndex = 41;
            this.lblTotalRegistros.Text = "...";
            this.lblTotalRegistros.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 21);
            this.label1.TabIndex = 40;
            this.label1.Text = "Exibindo dados do arquivo CSV";
            // 
            // gvArquivosCsv
            // 
            this.gvArquivosCsv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvArquivosCsv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvArquivosCsv.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.gvArquivosCsv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvArquivosCsv.Location = new System.Drawing.Point(6, 73);
            this.gvArquivosCsv.Name = "gvArquivosCsv";
            this.gvArquivosCsv.Size = new System.Drawing.Size(933, 341);
            this.gvArquivosCsv.TabIndex = 39;
            // 
            // btnInserirRegistro
            // 
            this.btnInserirRegistro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInserirRegistro.Enabled = false;
            this.btnInserirRegistro.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInserirRegistro.Location = new System.Drawing.Point(813, 434);
            this.btnInserirRegistro.Name = "btnInserirRegistro";
            this.btnInserirRegistro.Size = new System.Drawing.Size(126, 27);
            this.btnInserirRegistro.TabIndex = 38;
            this.btnInserirRegistro.Text = "Inserir Registro";
            this.btnInserirRegistro.UseVisualStyleBackColor = true;
            this.btnInserirRegistro.Click += new System.EventHandler(this.btnInserirRegistro_Click);
            // 
            // lblTotalLinhasRegistroAluno
            // 
            this.lblTotalLinhasRegistroAluno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalLinhasRegistroAluno.AutoSize = true;
            this.lblTotalLinhasRegistroAluno.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalLinhasRegistroAluno.ForeColor = System.Drawing.Color.Green;
            this.lblTotalLinhasRegistroAluno.Location = new System.Drawing.Point(41, 439);
            this.lblTotalLinhasRegistroAluno.Name = "lblTotalLinhasRegistroAluno";
            this.lblTotalLinhasRegistroAluno.Size = new System.Drawing.Size(20, 18);
            this.lblTotalLinhasRegistroAluno.TabIndex = 37;
            this.lblTotalLinhasRegistroAluno.Text = "...";
            this.lblTotalLinhasRegistroAluno.Visible = false;
            // 
            // btnAbrirArquivo
            // 
            this.btnAbrirArquivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbrirArquivo.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbrirArquivo.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrirArquivo.Image")));
            this.btnAbrirArquivo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAbrirArquivo.Location = new System.Drawing.Point(444, 14);
            this.btnAbrirArquivo.Name = "btnAbrirArquivo";
            this.btnAbrirArquivo.Size = new System.Drawing.Size(183, 27);
            this.btnAbrirArquivo.TabIndex = 36;
            this.btnAbrirArquivo.Text = "Abrir arquivo existente";
            this.btnAbrirArquivo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAbrirArquivo.UseVisualStyleBackColor = true;
            this.btnAbrirArquivo.Click += new System.EventHandler(this.btnAbrirArquivo_Click);
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPath.Location = new System.Drawing.Point(6, 14);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(423, 25);
            this.txtPath.TabIndex = 35;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 526);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(977, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(243, 17);
            this.toolStripStatusLabel1.Text = "Sistema pra importação e exportação CENSO";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(71, 17);
            this.toolStripStatusLabel2.Text = "Versão: 2017";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(59, 17);
            this.toolStripStatusLabel3.Text = "Dev: Maia";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(67, 17);
            this.toolStripStatusLabel4.Text = "Versão beta";
            // 
            // Default
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 548);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Default";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Importador-Exportador [CENSO]";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Default_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pCarregando)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVinculoAlunoCurso)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRegistro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAluno)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvArquivosCsv)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gvArquivosCsv;
        private System.Windows.Forms.Button btnInserirRegistro;
        private System.Windows.Forms.Label lblTotalLinhasRegistroAluno;
        private System.Windows.Forms.Button btnAbrirArquivo;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.Label lblTotalRegistros;
        private System.Windows.Forms.PictureBox pbAluno;
        private System.Windows.Forms.PictureBox pbRegistro;
        private System.Windows.Forms.PictureBox pbVinculoAlunoCurso;
        private System.Windows.Forms.Label lblTotalVinculosAluno;
        private System.Windows.Forms.Button btnGerarArquivoCSV;
        public System.Windows.Forms.PictureBox pCarregando;
        private System.Windows.Forms.Button btnAtualizarNomeDataNasc;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
    }
}

