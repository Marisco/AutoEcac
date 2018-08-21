namespace AutoEcac
{
    partial class frmCodReceita
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
            this.lblCdReceita = new System.Windows.Forms.Label();
            this.edtCdReceita = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblCdReceita
            // 
            this.lblCdReceita.AutoSize = true;
            this.lblCdReceita.Location = new System.Drawing.Point(12, 9);
            this.lblCdReceita.Name = "lblCdReceita";
            this.lblCdReceita.Size = new System.Drawing.Size(98, 13);
            this.lblCdReceita.TabIndex = 22;
            this.lblCdReceita.Text = "Código de Receita:";
            // 
            // edtCdReceita
            // 
            this.edtCdReceita.Location = new System.Drawing.Point(15, 25);
            this.edtCdReceita.Name = "edtCdReceita";
            this.edtCdReceita.Size = new System.Drawing.Size(100, 20);
            this.edtCdReceita.TabIndex = 21;
            this.edtCdReceita.KeyDown += new System.Windows.Forms.KeyEventHandler(this.edtCdReceita_KeyDown);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.Green;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(121, 24);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(37, 23);
            this.btnOk.TabIndex = 24;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmCodReceita
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(176, 65);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblCdReceita);
            this.Controls.Add(this.edtCdReceita);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmCodReceita";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Auto-Ecac";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCdReceita;
        private System.Windows.Forms.TextBox edtCdReceita;
        private System.Windows.Forms.Button btnOk;
    }
}