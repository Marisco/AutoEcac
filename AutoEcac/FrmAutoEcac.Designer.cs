namespace AutoEcac
{
    partial class frmAutoEcac
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAutoEcac));
			this.btnOk = new System.Windows.Forms.Button();
			this.grpDatas = new System.Windows.Forms.GroupBox();
			this.cbxperiodo = new System.Windows.Forms.ComboBox();
			this.lblDtFinal = new System.Windows.Forms.Label();
			this.lblDtInicial = new System.Windows.Forms.Label();
			this.dtpFinal = new System.Windows.Forms.DateTimePicker();
			this.dtpInicial = new System.Windows.Forms.DateTimePicker();
			this.grpServicos = new System.Windows.Forms.GroupBox();
			this.cbxServico = new System.Windows.Forms.ComboBox();
			this.grpPerfil = new System.Windows.Forms.GroupBox();
			this.lblCnpj = new System.Windows.Forms.Label();
			this.edtCNPJ = new System.Windows.Forms.TextBox();
			this.btnFechar = new System.Windows.Forms.Button();
			this.grbDI = new System.Windows.Forms.GroupBox();
			this.btnAdicionar = new System.Windows.Forms.Button();
			this.dgvNrDelacaracao = new System.Windows.Forms.DataGridView();
			this.nrDeclaracao = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.rdbCpfUsuario = new System.Windows.Forms.RadioButton();
			this.rdbCpfImportador = new System.Windows.Forms.RadioButton();
			this.rdbCnpjImportador = new System.Windows.Forms.RadioButton();
			this.rdbNrDeclaracao = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.edtNrConsultaDI = new System.Windows.Forms.TextBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.rbConsultaDI = new System.Windows.Forms.RadioButton();
			this.rbConsultaLI = new System.Windows.Forms.RadioButton();
			this.gbExtrato = new System.Windows.Forms.GroupBox();
			this.cbxBancoDados = new System.Windows.Forms.CheckBox();
			this.btnServico = new System.Windows.Forms.Button();
			this.tempo = new System.Windows.Forms.Timer(this.components);
			this.grpDatas.SuspendLayout();
			this.grpServicos.SuspendLayout();
			this.grpPerfil.SuspendLayout();
			this.grbDI.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvNrDelacaracao)).BeginInit();
			this.panel3.SuspendLayout();
			this.gbExtrato.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.BackColor = System.Drawing.Color.Green;
			this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOk.ForeColor = System.Drawing.Color.White;
			this.btnOk.Location = new System.Drawing.Point(239, 683);
			this.btnOk.Margin = new System.Windows.Forms.Padding(4);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(100, 28);
			this.btnOk.TabIndex = 3;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = false;
			this.btnOk.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// grpDatas
			// 
			this.grpDatas.Controls.Add(this.cbxperiodo);
			this.grpDatas.Controls.Add(this.lblDtFinal);
			this.grpDatas.Controls.Add(this.lblDtInicial);
			this.grpDatas.Controls.Add(this.dtpFinal);
			this.grpDatas.Controls.Add(this.dtpInicial);
			this.grpDatas.Location = new System.Drawing.Point(13, 552);
			this.grpDatas.Margin = new System.Windows.Forms.Padding(4);
			this.grpDatas.Name = "grpDatas";
			this.grpDatas.Padding = new System.Windows.Forms.Padding(4);
			this.grpDatas.Size = new System.Drawing.Size(336, 123);
			this.grpDatas.TabIndex = 14;
			this.grpDatas.TabStop = false;
			this.grpDatas.Text = "Datas / Periodicidade:";
			// 
			// cbxperiodo
			// 
			this.cbxperiodo.FormattingEnabled = true;
			this.cbxperiodo.Items.AddRange(new object[] {
            "Selecione o Período para Armazenamento",
            "Diário",
            "Semanal",
            "Mensal",
            "Trimestral",
            "Semestral",
            "Anual"});
			this.cbxperiodo.Location = new System.Drawing.Point(8, 79);
			this.cbxperiodo.Margin = new System.Windows.Forms.Padding(4);
			this.cbxperiodo.Name = "cbxperiodo";
			this.cbxperiodo.Size = new System.Drawing.Size(317, 24);
			this.cbxperiodo.TabIndex = 18;
			// 
			// lblDtFinal
			// 
			this.lblDtFinal.AutoSize = true;
			this.lblDtFinal.Location = new System.Drawing.Point(189, 20);
			this.lblDtFinal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDtFinal.Name = "lblDtFinal";
			this.lblDtFinal.Size = new System.Drawing.Size(38, 17);
			this.lblDtFinal.TabIndex = 17;
			this.lblDtFinal.Text = "Final";
			// 
			// lblDtInicial
			// 
			this.lblDtInicial.AutoSize = true;
			this.lblDtInicial.Location = new System.Drawing.Point(8, 21);
			this.lblDtInicial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDtInicial.Name = "lblDtInicial";
			this.lblDtInicial.Size = new System.Drawing.Size(43, 17);
			this.lblDtInicial.TabIndex = 16;
			this.lblDtInicial.Text = "Inicial";
			// 
			// dtpFinal
			// 
			this.dtpFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpFinal.Location = new System.Drawing.Point(193, 41);
			this.dtpFinal.Margin = new System.Windows.Forms.Padding(4);
			this.dtpFinal.Name = "dtpFinal";
			this.dtpFinal.Size = new System.Drawing.Size(132, 22);
			this.dtpFinal.TabIndex = 15;
			this.dtpFinal.Value = new System.DateTime(2018, 8, 14, 0, 0, 0, 0);
			// 
			// dtpInicial
			// 
			this.dtpInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpInicial.Location = new System.Drawing.Point(13, 41);
			this.dtpInicial.Margin = new System.Windows.Forms.Padding(4);
			this.dtpInicial.Name = "dtpInicial";
			this.dtpInicial.Size = new System.Drawing.Size(132, 22);
			this.dtpInicial.TabIndex = 14;
			this.dtpInicial.Value = new System.DateTime(2018, 8, 14, 0, 0, 0, 0);
			// 
			// grpServicos
			// 
			this.grpServicos.Controls.Add(this.cbxServico);
			this.grpServicos.Location = new System.Drawing.Point(12, 105);
			this.grpServicos.Margin = new System.Windows.Forms.Padding(4);
			this.grpServicos.Name = "grpServicos";
			this.grpServicos.Padding = new System.Windows.Forms.Padding(4);
			this.grpServicos.Size = new System.Drawing.Size(336, 68);
			this.grpServicos.TabIndex = 17;
			this.grpServicos.TabStop = false;
			this.grpServicos.Text = "Serviço:";
			// 
			// cbxServico
			// 
			this.cbxServico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
			this.cbxServico.Enabled = false;
			this.cbxServico.FormattingEnabled = true;
			this.cbxServico.Items.AddRange(new object[] {
            "Selecione um serviço AutoEcac",
            "Consutar Comprovante de Pagamento - Darf",
            "Consultar  Extrato"});
			this.cbxServico.Location = new System.Drawing.Point(8, 25);
			this.cbxServico.Margin = new System.Windows.Forms.Padding(4);
			this.cbxServico.Name = "cbxServico";
			this.cbxServico.Size = new System.Drawing.Size(317, 25);
			this.cbxServico.TabIndex = 13;
			this.cbxServico.SelectedIndexChanged += new System.EventHandler(this.cbxServico_SelectedIndexChanged);
			// 
			// grpPerfil
			// 
			this.grpPerfil.BackColor = System.Drawing.Color.White;
			this.grpPerfil.Controls.Add(this.lblCnpj);
			this.grpPerfil.Controls.Add(this.edtCNPJ);
			this.grpPerfil.Location = new System.Drawing.Point(12, 180);
			this.grpPerfil.Margin = new System.Windows.Forms.Padding(4);
			this.grpPerfil.Name = "grpPerfil";
			this.grpPerfil.Padding = new System.Windows.Forms.Padding(4);
			this.grpPerfil.Size = new System.Drawing.Size(336, 68);
			this.grpPerfil.TabIndex = 18;
			this.grpPerfil.TabStop = false;
			this.grpPerfil.Text = "e-CAC - Alteração do Perfil:";
			// 
			// lblCnpj
			// 
			this.lblCnpj.AutoSize = true;
			this.lblCnpj.Location = new System.Drawing.Point(9, 27);
			this.lblCnpj.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblCnpj.Name = "lblCnpj";
			this.lblCnpj.Size = new System.Drawing.Size(162, 17);
			this.lblCnpj.TabIndex = 21;
			this.lblCnpj.Text = "CNPJ (sem formatação):";
			// 
			// edtCNPJ
			// 
			this.edtCNPJ.Location = new System.Drawing.Point(188, 23);
			this.edtCNPJ.Margin = new System.Windows.Forms.Padding(4);
			this.edtCNPJ.Name = "edtCNPJ";
			this.edtCNPJ.Size = new System.Drawing.Size(137, 22);
			this.edtCNPJ.TabIndex = 20;
			// 
			// btnFechar
			// 
			this.btnFechar.BackColor = System.Drawing.Color.Red;
			this.btnFechar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnFechar.ForeColor = System.Drawing.Color.White;
			this.btnFechar.Location = new System.Drawing.Point(20, 683);
			this.btnFechar.Margin = new System.Windows.Forms.Padding(4);
			this.btnFechar.Name = "btnFechar";
			this.btnFechar.Size = new System.Drawing.Size(100, 28);
			this.btnFechar.TabIndex = 19;
			this.btnFechar.Text = "Fechar";
			this.btnFechar.UseVisualStyleBackColor = false;
			this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
			// 
			// grbDI
			// 
			this.grbDI.Controls.Add(this.btnAdicionar);
			this.grbDI.Controls.Add(this.dgvNrDelacaracao);
			this.grbDI.Controls.Add(this.rdbCpfUsuario);
			this.grbDI.Controls.Add(this.rdbCpfImportador);
			this.grbDI.Controls.Add(this.rdbCnpjImportador);
			this.grbDI.Controls.Add(this.rdbNrDeclaracao);
			this.grbDI.Controls.Add(this.label1);
			this.grbDI.Controls.Add(this.edtNrConsultaDI);
			this.grbDI.Location = new System.Drawing.Point(12, 298);
			this.grbDI.Margin = new System.Windows.Forms.Padding(4);
			this.grbDI.Name = "grbDI";
			this.grbDI.Padding = new System.Windows.Forms.Padding(4);
			this.grbDI.Size = new System.Drawing.Size(336, 246);
			this.grbDI.TabIndex = 22;
			this.grbDI.TabStop = false;
			this.grbDI.Text = "Consulta:";
			// 
			// btnAdicionar
			// 
			this.btnAdicionar.BackColor = System.Drawing.Color.Green;
			this.btnAdicionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAdicionar.ForeColor = System.Drawing.Color.White;
			this.btnAdicionar.Image = global::AutoEcac.Properties.Resources.arrowDown;
			this.btnAdicionar.Location = new System.Drawing.Point(295, 110);
			this.btnAdicionar.Margin = new System.Windows.Forms.Padding(4);
			this.btnAdicionar.Name = "btnAdicionar";
			this.btnAdicionar.Size = new System.Drawing.Size(31, 27);
			this.btnAdicionar.TabIndex = 27;
			this.btnAdicionar.UseVisualStyleBackColor = false;
			this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
			// 
			// dgvNrDelacaracao
			// 
			this.dgvNrDelacaracao.AllowUserToAddRows = false;
			this.dgvNrDelacaracao.AllowUserToDeleteRows = false;
			this.dgvNrDelacaracao.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgvNrDelacaracao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvNrDelacaracao.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nrDeclaracao});
			this.dgvNrDelacaracao.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dgvNrDelacaracao.Enabled = false;
			this.dgvNrDelacaracao.Location = new System.Drawing.Point(12, 144);
			this.dgvNrDelacaracao.Margin = new System.Windows.Forms.Padding(4);
			this.dgvNrDelacaracao.Name = "dgvNrDelacaracao";
			this.dgvNrDelacaracao.Size = new System.Drawing.Size(309, 85);
			this.dgvNrDelacaracao.TabIndex = 26;
			// 
			// nrDeclaracao
			// 
			this.nrDeclaracao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.nrDeclaracao.HeaderText = "Número da Declaração";
			this.nrDeclaracao.Name = "nrDeclaracao";
			// 
			// rdbCpfUsuario
			// 
			this.rdbCpfUsuario.AutoSize = true;
			this.rdbCpfUsuario.Location = new System.Drawing.Point(172, 52);
			this.rdbCpfUsuario.Margin = new System.Windows.Forms.Padding(4);
			this.rdbCpfUsuario.Name = "rdbCpfUsuario";
			this.rdbCpfUsuario.Size = new System.Drawing.Size(132, 21);
			this.rdbCpfUsuario.TabIndex = 25;
			this.rdbCpfUsuario.Text = "CPF do Usuário.";
			this.rdbCpfUsuario.UseVisualStyleBackColor = true;
			this.rdbCpfUsuario.Click += new System.EventHandler(this.rdbNrDeclaracao_Click);
			// 
			// rdbCpfImportador
			// 
			this.rdbCpfImportador.AutoSize = true;
			this.rdbCpfImportador.Location = new System.Drawing.Point(172, 23);
			this.rdbCpfImportador.Margin = new System.Windows.Forms.Padding(4);
			this.rdbCpfImportador.Name = "rdbCpfImportador";
			this.rdbCpfImportador.Size = new System.Drawing.Size(151, 21);
			this.rdbCpfImportador.TabIndex = 24;
			this.rdbCpfImportador.Text = "CPF do Importador.";
			this.rdbCpfImportador.UseVisualStyleBackColor = true;
			this.rdbCpfImportador.Click += new System.EventHandler(this.rdbNrDeclaracao_Click);
			// 
			// rdbCnpjImportador
			// 
			this.rdbCnpjImportador.AutoSize = true;
			this.rdbCnpjImportador.Location = new System.Drawing.Point(13, 52);
			this.rdbCnpjImportador.Margin = new System.Windows.Forms.Padding(4);
			this.rdbCnpjImportador.Name = "rdbCnpjImportador";
			this.rdbCnpjImportador.Size = new System.Drawing.Size(160, 21);
			this.rdbCnpjImportador.TabIndex = 23;
			this.rdbCnpjImportador.Text = "CNPJ do importador.";
			this.rdbCnpjImportador.UseVisualStyleBackColor = true;
			this.rdbCnpjImportador.Click += new System.EventHandler(this.rdbNrDeclaracao_Click);
			// 
			// rdbNrDeclaracao
			// 
			this.rdbNrDeclaracao.AutoSize = true;
			this.rdbNrDeclaracao.Checked = true;
			this.rdbNrDeclaracao.Location = new System.Drawing.Point(13, 23);
			this.rdbNrDeclaracao.Margin = new System.Windows.Forms.Padding(4);
			this.rdbNrDeclaracao.Name = "rdbNrDeclaracao";
			this.rdbNrDeclaracao.Size = new System.Drawing.Size(144, 21);
			this.rdbNrDeclaracao.TabIndex = 22;
			this.rdbNrDeclaracao.Text = "Nº da Declaração:";
			this.rdbNrDeclaracao.UseVisualStyleBackColor = true;
			this.rdbNrDeclaracao.Click += new System.EventHandler(this.rdbNrDeclaracao_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 116);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(142, 17);
			this.label1.TabIndex = 21;
			this.label1.Text = "Nº (sem formatação):";
			// 
			// edtNrConsultaDI
			// 
			this.edtNrConsultaDI.Location = new System.Drawing.Point(156, 112);
			this.edtNrConsultaDI.Margin = new System.Windows.Forms.Padding(4);
			this.edtNrConsultaDI.Name = "edtNrConsultaDI";
			this.edtNrConsultaDI.Size = new System.Drawing.Size(132, 22);
			this.edtNrConsultaDI.TabIndex = 20;
			this.edtNrConsultaDI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtNrConsultaDI_KeyPress);
			// 
			// panel3
			// 
			this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
			this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel3.Controls.Add(this.panel4);
			this.panel3.Location = new System.Drawing.Point(191, 7);
			this.panel3.Margin = new System.Windows.Forms.Padding(4);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(157, 89);
			this.panel3.TabIndex = 21;
			this.panel3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel3_MouseClick);
			// 
			// panel4
			// 
			this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
			this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel4.Location = new System.Drawing.Point(164, 2);
			this.panel4.Margin = new System.Windows.Forms.Padding(4);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(170, 89);
			this.panel4.TabIndex = 21;
			// 
			// panel1
			// 
			this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
			this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Location = new System.Drawing.Point(12, 7);
			this.panel1.Margin = new System.Windows.Forms.Padding(4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(170, 89);
			this.panel1.TabIndex = 20;
			this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
			// 
			// rbConsultaDI
			// 
			this.rbConsultaDI.AutoSize = true;
			this.rbConsultaDI.Checked = true;
			this.rbConsultaDI.Location = new System.Drawing.Point(13, 25);
			this.rbConsultaDI.Name = "rbConsultaDI";
			this.rbConsultaDI.Size = new System.Drawing.Size(42, 21);
			this.rbConsultaDI.TabIndex = 23;
			this.rbConsultaDI.TabStop = true;
			this.rbConsultaDI.Text = "DI";
			this.rbConsultaDI.UseVisualStyleBackColor = true;
			// 
			// rbConsultaLI
			// 
			this.rbConsultaLI.AutoSize = true;
			this.rbConsultaLI.Location = new System.Drawing.Point(86, 25);
			this.rbConsultaLI.Name = "rbConsultaLI";
			this.rbConsultaLI.Size = new System.Drawing.Size(40, 21);
			this.rbConsultaLI.TabIndex = 24;
			this.rbConsultaLI.Text = "LI";
			this.rbConsultaLI.UseVisualStyleBackColor = true;
			// 
			// gbExtrato
			// 
			this.gbExtrato.Controls.Add(this.cbxBancoDados);
			this.gbExtrato.Controls.Add(this.rbConsultaDI);
			this.gbExtrato.Controls.Add(this.rbConsultaLI);
			this.gbExtrato.Location = new System.Drawing.Point(12, 249);
			this.gbExtrato.Name = "gbExtrato";
			this.gbExtrato.Size = new System.Drawing.Size(336, 52);
			this.gbExtrato.TabIndex = 25;
			this.gbExtrato.TabStop = false;
			this.gbExtrato.Text = "Extrato :: Tipo de Consulta";
			// 
			// cbxBancoDados
			// 
			this.cbxBancoDados.AutoSize = true;
			this.cbxBancoDados.ForeColor = System.Drawing.Color.Red;
			this.cbxBancoDados.Location = new System.Drawing.Point(156, 25);
			this.cbxBancoDados.Name = "cbxBancoDados";
			this.cbxBancoDados.Size = new System.Drawing.Size(135, 21);
			this.cbxBancoDados.TabIndex = 25;
			this.cbxBancoDados.Text = "Banco de Dados";
			this.cbxBancoDados.UseVisualStyleBackColor = true;
			this.cbxBancoDados.Click += new System.EventHandler(this.cbxBancoDados_Click);
			// 
			// btnServico
			// 
			this.btnServico.BackColor = System.Drawing.Color.Chocolate;
			this.btnServico.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnServico.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnServico.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnServico.ForeColor = System.Drawing.Color.White;
			this.btnServico.Location = new System.Drawing.Point(128, 683);
			this.btnServico.Margin = new System.Windows.Forms.Padding(4);
			this.btnServico.Name = "btnServico";
			this.btnServico.Size = new System.Drawing.Size(100, 28);
			this.btnServico.TabIndex = 26;
			this.btnServico.Text = "Serviço";
			this.btnServico.UseVisualStyleBackColor = false;
			this.btnServico.Click += new System.EventHandler(this.btnServico_Click);
			// 
			// tempo
			// 
			this.tempo.Interval = 60000;
			this.tempo.Tick += new System.EventHandler(this.tempo_Tick);
			// 
			// frmAutoEcac
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(360, 740);
			this.Controls.Add(this.btnServico);
			this.Controls.Add(this.gbExtrato);
			this.Controls.Add(this.grbDI);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.btnFechar);
			this.Controls.Add(this.grpServicos);
			this.Controls.Add(this.grpDatas);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.grpPerfil);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "frmAutoEcac";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Auto-Ecac";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.grpDatas.ResumeLayout(false);
			this.grpDatas.PerformLayout();
			this.grpServicos.ResumeLayout(false);
			this.grpPerfil.ResumeLayout(false);
			this.grpPerfil.PerformLayout();
			this.grbDI.ResumeLayout(false);
			this.grbDI.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvNrDelacaracao)).EndInit();
			this.panel3.ResumeLayout(false);
			this.gbExtrato.ResumeLayout(false);
			this.gbExtrato.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox grpDatas;
        private System.Windows.Forms.ComboBox cbxperiodo;
        private System.Windows.Forms.Label lblDtFinal;
        private System.Windows.Forms.Label lblDtInicial;
        private System.Windows.Forms.DateTimePicker dtpFinal;
        private System.Windows.Forms.DateTimePicker dtpInicial;
        private System.Windows.Forms.GroupBox grpServicos;
        private System.Windows.Forms.ComboBox cbxServico;
        private System.Windows.Forms.GroupBox grpPerfil;
        private System.Windows.Forms.Label lblCnpj;
        private System.Windows.Forms.TextBox edtCNPJ;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox grbDI;
        private System.Windows.Forms.DataGridView dgvNrDelacaracao;
        private System.Windows.Forms.DataGridViewTextBoxColumn nrDeclaracao;
        private System.Windows.Forms.RadioButton rdbCpfUsuario;
        private System.Windows.Forms.RadioButton rdbCpfImportador;
        private System.Windows.Forms.RadioButton rdbCnpjImportador;
        private System.Windows.Forms.RadioButton rdbNrDeclaracao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edtNrConsultaDI;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.GroupBox gbExtrato;
        private System.Windows.Forms.RadioButton rbConsultaDI;
        private System.Windows.Forms.RadioButton rbConsultaLI;
		private System.Windows.Forms.CheckBox cbxBancoDados;
		private System.Windows.Forms.Button btnServico;
		private System.Windows.Forms.Timer tempo;
	}
}

