using AutoEcac.Servicos;
using AutoEcac.Utils;
using MySql.Data.MySqlClient;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Threading;
using System.Windows.Forms;

namespace AutoEcac
{
    public partial class frmAutoEcac : Form
    {
        private ExtratoService _extratoService;
        private DARFService _darfService;

        private string fCdReceita;

        protected IWebDriver Browser;

        public frmAutoEcac()
        {
            InitializeComponent();
			btnServico.Enabled = false;
			cbxBancoDados.Checked = false;

            //conectarBanco();

            Browser = ConfigurarBrowser();

            LimpaServicos();
        }

        private void conectarBanco()
        {
            string connStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

            using (MySqlConnection mysql = new MySqlConnection(connStr))
            {
                mysql.Open();
                MySqlCommand command = new MySqlCommand("Select * from TSISCOMEXWEB_ROBO where tp_consulta = 'DI' and tp_acao = 'consulta' and in_rodando = 1", mysql);
                MySqlDataReader rdr = command.ExecuteReader();

				dgvNrDelacaracao.Rows.Clear();

				
                while (rdr.Read())
                {
                    //Console.WriteLine(rdr.GetString("tp_consulta") + " -- " + rdr[1] + " --- " + rdr[2] + "----" + rdr[0]);

					dgvNrDelacaracao.Rows.Add(rdr.GetString("nr_registro_di"));




				}
                mysql.Close();
            }

        }

        private void LimpaServicos()
        {
            _extratoService = null;
            _darfService = null;
        }

        private void IniciarOperacao()
        {
            LimpaServicos();

            if (TipoServicoSelecionado == TipoServico.DARF)
            {
                _darfService = new DARFService(Browser);
                _darfService._tipoServicoSelecionado = TipoServico.DARF;
                _darfService._CNPJ = edtCNPJ.Text.Trim();

                _darfService.AbrirBrowser();

            }
            else if (TipoServicoSelecionado == TipoServico.EXTRATO)
            {
                _extratoService = new ExtratoService(Browser);
                _extratoService._tipoServicoSelecionado = TipoServico.EXTRATO;
                _extratoService._tipoExtratoSelecionado = TipoExtratoSelecionado;
                _extratoService._CNPJ = edtCNPJ.Text.Trim();

                _extratoService.AbrirBrowser();
            }
            else
            {
                MessageBox.Show("Serviço não implementado.");
                return;
            }

            Thread.Sleep(2000);
        }

        private bool AlterarPerfil()
        {
            if (TipoServicoSelecionado == TipoServico.DARF && !string.IsNullOrEmpty(edtCNPJ.Text))
            {
                Browser.FindElement(By.Id("btnPerfil")).Click();
                Thread.Sleep(2000);

                Browser.FindElement(By.Id("txtNIPapel2")).SendKeys(edtCNPJ.Text);
                Browser.FindElement(By.XPath("//input[contains(@onclick,'formPJ')]")).Click();
                Thread.Sleep(2000);

                if (Browser.FindElement(By.ClassName("erro")).Text.Contains("ATENÇÃO"))
                {
                    MessageBox.Show("Seu Browser irá reiniciar. Verifique sua procuração eletrônica!", "Auto-Ecac", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Browser.FindElement(By.ClassName("ui-icon-closethick")).Click();
                    Browser.FindElement(By.Id("sairSeguranca")).Click();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (TipoServicoSelecionado == TipoServico.EXTRATO && !rdbNrDeclaracao.Checked && string.IsNullOrEmpty(edtNrConsultaDI.Text.Trim()))
            {
                MessageBox.Show("Campo Nº Obrigatório", "Auto-Siscomex Importacao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private Boolean ValidarCampos()
        {
            if ((TipoServicoSelecionado == TipoServico.EXTRATO) && ((dtpInicial.Value == null) || (dtpFinal.Value == null)) && !rdbNrDeclaracao.Checked)
            {
                MessageBox.Show("Data inicial e Final de consulta obrigatória.", "Auto-Siscomex", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (TipoServicoSelecionado == TipoServico.NENHUM)
            {
                MessageBox.Show("Selecione um serviço AutoEcac", "Auto-Ecac", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (PeriodoSelecionado == Periodo.NENHUM)
            {
                MessageBox.Show("Selecione a periodicidade de consulta e armazenamento", "Auto-Ecac", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private ChromeDriver ConfigurarBrowser()
        {
            var service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            var options = new ChromeOptions();
            options.AddArguments("disable-infobars");
            options.AddArgument("--safebrowsing-disable-download-protection");
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("download.directory_upgrade", true);
            options.AddUserProfilePreference("safebrowsing.enabled", true);

            return new ChromeDriver(service, options);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbxServico.SelectedIndex = 0;
            cbxperiodo.SelectedIndex = 0;
            rdbNrDeclaracao.PerformClick();
        }

		private void GravarnoBanco(string nrdeclaracoes)
		{

			if (nrdeclaracoes.Length > 0)
			{
				nrdeclaracoes = nrdeclaracoes.Substring(0, nrdeclaracoes.Length - 1);
			}


			string connStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

			using (MySqlConnection mysql = new MySqlConnection(connStr))
			{
				mysql.Open();
				MySqlCommand command = new MySqlCommand("Update TSISCOMEXWEB_ROBO set in_rodando=0 where tp_consulta = 'DI' and tp_acao = 'consulta' and nr_registro_di in(" + nrdeclaracoes + ")", mysql);

				command.ExecuteNonQuery();


				mysql.Close();
			}
		}
			#region Eventos Clicks

			private void btnOK_Click(object sender, EventArgs e)
        {
            Boolean PerfilValido = AlterarPerfil();

            if (!PerfilValido)
            {
                Browser.Quit();
                Thread.Sleep(2000);
                Browser.Dispose();
                Thread.Sleep(2000);

                IniciarOperacao();
            }

            if (PerfilValido && ValidarCampos())
            {
                Thread.Sleep(2000);

                if (TipoServicoSelecionado == TipoServico.DARF)
                {
                    var vFrmCodReceita = new frmCodReceita();

                    if (vFrmCodReceita.ShowDialog(this) == DialogResult.OK)
                    {
                        this.fCdReceita = vFrmCodReceita.fCdReceita;
                    }

                    vFrmCodReceita.Dispose();

                    _darfService.ConsultarDARF(PeriodoSelecionado, this.fCdReceita, dtpInicial.Value, dtpFinal.Value);

                }
                else if (TipoServicoSelecionado == TipoServico.EXTRATO)
                {
                    List<string> vListaNrConsulta = new List<string>();

                    if (TipoExtratoSelecionado == TipoExtrato.DI)
                    {
                        _extratoService.NavegarURLExtratoDeclaracaoDI();

                        Thread.Sleep(2000);

                        if (TipoConsultaExtratoSelecionado == TipoConsultaExtrato.NumeroDeclaracao)
                        {
                            if (!string.IsNullOrEmpty(edtNrConsultaDI.Text.Trim()))
                            {

                                vListaNrConsulta.Add(edtNrConsultaDI.Text.Trim());

                            }

                            if (dgvNrDelacaracao.Rows.Count > 0)
                            {
                                foreach (DataGridViewRow numero in dgvNrDelacaracao.Rows)
                                {
                                    vListaNrConsulta.Add(numero.Cells[0].Value.ToString());
                                }
                            }
                        }
                        else
                        {
                            vListaNrConsulta.Add(edtNrConsultaDI.Text.Trim());
                        }

                        _extratoService.ConsultarDI(PeriodoSelecionado, TipoConsultaExtratoSelecionado, vListaNrConsulta, dtpInicial.Value, dtpFinal.Value);
                    }

                    if(TipoExtratoSelecionado == TipoExtrato.LI)
                    {
                        _extratoService.NavegarURLExtratoDeclaracaoLI();

                        Thread.Sleep(2000);

                        if (TipoConsultaExtratoSelecionado == TipoConsultaExtrato.NumeroDeclaracao)
                        {
                            if (!string.IsNullOrEmpty(edtNrConsultaDI.Text.Trim()))
                            {
                                vListaNrConsulta.Add(edtNrConsultaDI.Text.Trim());
                            }

                            if (dgvNrDelacaracao.Rows.Count > 0)
                            {
                                foreach (DataGridViewRow numero in dgvNrDelacaracao.Rows)
                                {
                                    vListaNrConsulta.Add(numero.Cells[0].Value.ToString());
                                }
                            }
                        }
                        else
                        {
                            vListaNrConsulta.Add(edtNrConsultaDI.Text.Trim());
                        }

                        _extratoService.ConsultarLI(PeriodoSelecionado, TipoConsultaExtratoSelecionado, vListaNrConsulta, dtpInicial.Value, dtpFinal.Value);
                    }
                }

                Thread.Sleep(2000);
                Browser.Navigate().Back();

				//antes de limpar fazer update no banco
                
				string nrdeclaracoes = string.Empty;

				for (int i = 0; i < dgvNrDelacaracao.RowCount; i++)
				{
					nrdeclaracoes += "'" + dgvNrDelacaracao.Rows[i].Cells[0].Value + "',";
				}

				if (!string.IsNullOrEmpty(nrdeclaracoes))
				{
					GravarnoBanco(nrdeclaracoes);

				}

				dgvNrDelacaracao.Rows.Clear();

				if (!cbxBancoDados.Checked)
				{
					MessageBox.Show("Consulta realizada com sucesso!", "Auto-Ecac", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            dgvNrDelacaracao.Rows.Add(edtNrConsultaDI.Text);
            edtNrConsultaDI.Clear();
        }

        private void rdbNrDeclaracao_Click(object sender, EventArgs e)
        {
            dgvNrDelacaracao.Enabled = rdbNrDeclaracao.Checked;
            btnAdicionar.Enabled = rdbNrDeclaracao.Checked;

            grpDatas.Enabled = TipoConsultaExtratoSelecionado != TipoConsultaExtrato.NumeroDeclaracao;

            if (!grpDatas.Enabled)
            {
                dtpInicial.Value = DateTime.Now;
                dtpFinal.Value = DateTime.Now;
                cbxperiodo.SelectedIndex = (int)Periodo.DIARIO;
                grpDatas.Enabled = false;
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            cbxServico.SelectedIndex = (int)TipoServico.DARF;
            grbDI.Enabled = false; //desabilita grid Consulta DI
            gbExtrato.Enabled = false;
            grpPerfil.Enabled = true; //Habilita grid e-CAC - Alteração de Perfil

            IniciarOperacao();
        }

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            cbxServico.SelectedIndex = (int)TipoServico.EXTRATO;
            grbDI.Enabled = true; //habilita grid Consulta DI
            gbExtrato.Enabled = true;
            grpPerfil.Enabled = false; // desabilita grid e-CAC - Alteração de Perfil

            IniciarOperacao();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Outros Eventos

        private void cbxServico_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxperiodo.Items.Clear();
            cbxperiodo.Items.Add("Selecione o Período para Armazenamento");
            cbxperiodo.Items.Add("Diário");
            cbxperiodo.Items.Add("Semanal");
            cbxperiodo.Items.Add("Mensal");

            if (TipoServicoSelecionado == TipoServico.DARF)
            {
                cbxperiodo.Items.Add("Trimestral");
                cbxperiodo.Items.Add("Semestral");
                cbxperiodo.Items.Add("Anual");
            }
            cbxperiodo.SelectedIndex = (int)Periodo.DIARIO;
        }

        private void edtNrConsultaDI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            if ((System.Windows.Forms.Keys)e.KeyChar == System.Windows.Forms.Keys.Enter)
            {
                btnAdicionar.PerformClick();
            }
        }

        #endregion

        #region Gets

        public TipoExtrato TipoExtratoSelecionado
        {
            get
            {
                return rbConsultaDI.Checked == true ? TipoExtrato.DI : TipoExtrato.LI;
            }
        }

        public TipoConsultaExtrato TipoConsultaExtratoSelecionado
        {
            get
            {
                if (rdbNrDeclaracao.Checked)
                {
                    return TipoConsultaExtrato.NumeroDeclaracao;
                }
                else if (rdbCnpjImportador.Checked)
                {
                    return TipoConsultaExtrato.CnpjImportador;
                }
                else if (rdbCpfImportador.Checked)
                {
                    return TipoConsultaExtrato.CpfImportador;
                }
                else
                {
                    return TipoConsultaExtrato.CpfUsuario;
                }
            }
        }

        public TipoServico TipoServicoSelecionado
        {
            get { return (TipoServico)cbxServico.SelectedIndex; }
        }

        public Periodo PeriodoSelecionado
        {
            get { return (Periodo)cbxperiodo.SelectedIndex; }
        }

		#endregion

		

		private void cbxBancoDados_Click(object sender, EventArgs e)
		{
			if (cbxBancoDados.Enabled)
			{
				rbConsultaDI.Checked = true;
				rdbNrDeclaracao.Checked = true;

				dgvNrDelacaracao.Enabled = true; // rdbNrDeclaracao.Checked;
				
				btnAdicionar.Enabled = false;
				edtNrConsultaDI.Enabled = false;
				btnServico.Enabled = true;

				grpDatas.Enabled = false;

				if (!grpDatas.Enabled)
				{
					dtpInicial.Value = DateTime.Now;
					dtpFinal.Value = DateTime.Now;
					cbxperiodo.SelectedIndex = (int)Periodo.DIARIO;
					grpDatas.Enabled = false;
				}
			}
			else
			{
				dgvNrDelacaracao.Enabled = true; // rdbNrDeclaracao.Checked;
				btnAdicionar.Enabled = true;
				edtNrConsultaDI.Enabled = true;
				btnServico.Enabled = false;


				grpDatas.Enabled = true;

				if (!grpDatas.Enabled)
				{
					dtpInicial.Value = DateTime.Now;
					dtpFinal.Value = DateTime.Now;
					cbxperiodo.SelectedIndex = (int)Periodo.DIARIO;
					grpDatas.Enabled = false;
				}




			}
		}

		private void btnServico_Click(object sender, EventArgs e)
		{
			tempo.Enabled = true;
			
		}

		private void tempo_Tick(object sender, EventArgs e)
		{
			conectarBanco();
			btnOK_Click(sender,e);
			
		}

		
	}
}
