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
using AutoEcac.DAL;
using System.Linq;
using AutoEcac.Services;
using Microsoft.Win32;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.IO;
using AutoEcac.model;
using System.Globalization;
using System.Drawing;

namespace AutoEcac
{
    public partial class frmAutoEcac : Form
    {
        private ExtratoService _extratoService;
        private DARFService _darfService;
        private string fCdReceita;
        protected IWebDriver Browser;
        protected roboEntities db;
        ExtratoService _extratoRodando = new ExtratoService();


        public frmAutoEcac()
        {
            InitializeComponent();
            //btnServico.Enabled = false;
            //cbxBancoDados.Checked = false;
            //Browser = ConfigurarBrowser();
            LimpaServicos();
        }

        private void LimpaServicos()
        {
            _extratoService = null;
            _darfService = null;
            db = null;
        }

        private void IniciarOperacao()
        {
            LimpaServicos();

            if (TipoServicoSelecionado == TipoServico.DARF)
            {
                _darfService = new DARFService(Browser)
                {
                    _tipoServicoSelecionado = TipoServico.DARF,
                    _CNPJ = edtCNPJ.Text.Trim()
                };

                _darfService.AbrirBrowser();

            }
            else if (TipoServicoSelecionado == TipoServico.EXTRATO)
            {
                db = new roboEntities();
                _extratoService = new ExtratoService(Browser, db)
                {
                    _tipoServicoSelecionado = TipoServico.EXTRATO,
                    _tipoExtratoSelecionado = TipoExtratoSelecionado,
                    _CNPJ = edtCNPJ.Text.Trim()
                };
                //_extratoService.AbrirBrowser();
            }
            else
            {
                MessageBox.Show("Serviço não implementado.");
                return;
            }

            Thread.Sleep(2000);
        }

        private void FinalizarOperacao()
        {
            LimpaServicos();
            Browser.Quit();
            Browser.Dispose();
            Browser = null;
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
            //options.AddArguments($"user-data-dir=C:/Users/{Environment.UserName}/AppData/Local/Google/Chrome/User Data/Default");
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("download.directory_upgrade", true);
            options.AddUserProfilePreference("safebrowsing.enabled", true);
            //options.LeaveBrowserRunning = true;
            //options.AcceptInsecureCertificates = true;
            return new ChromeDriver(service, options);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            //Browser = ConfigurarBrowser();
            cbxServico.SelectedIndex = 0;
            cbxperiodo.SelectedIndex = 0;
            rdbNrDeclaracao.PerformClick();
            rbConsultaDI.Checked = true;
            rbConsultaLI_Click(sender, e);

        }
        #region Eventos Clicks

        private void btnOK_Click(object sender, EventArgs e)
        {
            Browser = ConfigurarBrowser();
            IniciarOperacao();

            if (TipoServicoSelecionado == TipoServico.DARF)
            {
                var vFrmCodReceita = new frmCodReceita();

                if (vFrmCodReceita.ShowDialog(this) == DialogResult.OK)
                {
                    this.fCdReceita = vFrmCodReceita.fCdReceita;
                }

                vFrmCodReceita.Dispose();

                Thread.Sleep(2000);
                Browser = ConfigurarBrowser();
                IniciarOperacao();

                Periodo periodo = PeriodoSelecionado;
                Thread t = new Thread(() => _darfService.ConsultarDARF(periodo, this.fCdReceita, dtpInicial.Value, dtpFinal.Value));
                t.Start();

            }
            else if (TipoServicoSelecionado == TipoServico.EXTRATO)
            {


                List<string> vListaNrConsulta = new List<string>();
                List<string> vListaCpf = new List<string>();
                List<string> vListaDiLi = new List<string>();

                if (TipoExtratoSelecionado == TipoExtrato.DI)
                {
                    if (TipoConsultaExtratoSelecionado == TipoConsultaExtrato.NumeroDeclaracao)
                    {
                        if (cbxBancoDados.Checked)
                        {
                            foreach (var row in db.tsiscomexweb_robo.Where(reg => reg.tp_consulta == "DI" && reg.in_rodando == 1 && reg.in_desembaraco == 0))
                            {
                                row.in_rodando = 0;

                                if (row.tp_acao == "consulta" || (row.tp_acao == "acompanha" && row.dt_agendamento <= DateTime.Now))
                                {
                                    vListaNrConsulta.Add(row.nr_registro.ToString().Trim() + "; " + row.cpf_certificado.ToString().Trim());

                                    if (!vListaCpf.Contains(row.cpf_certificado.ToString().Trim()))
                                    {
                                        vListaCpf.Add(row.cpf_certificado.ToString().Trim());
                                    }

                                }
                            }
                            db.SaveChanges();
                        }
                        else
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

                    }
                    if (vListaCpf.Count() > 0)
                    {
                        var store = new X509Store(StoreLocation.CurrentUser);
                        store.Open(OpenFlags.ReadOnly);
                        var certificados = store.Certificates.OfType<X509Certificate2>().Where(x => x.Issuer.Contains("Secretaria da Receita Federal do Brasil")).OrderBy(x => x.Subject);

                        int qtdSetas = 0;
                        foreach (string cpf in vListaCpf)
                        {
                            int i = 0;
                            foreach (var certificado in certificados)
                            {
                                if (certificado.FriendlyName.Contains(cpf))
                                {
                                    qtdSetas = i++;

                                }
                                i++;
                            }

                            Thread tSeta = new Thread(() => _extratoService.SetarCertificado(qtdSetas));
                            tSeta.Start();
                            _extratoService.AbrirBrowser();
                            _extratoService.NavegarURLExtratoDeclaracaoDI();
                            Thread.Sleep(2000);
                            foreach (string di in vListaNrConsulta)
                            {
                                if (cpf == di.ToString().Split(';')[1].Trim())
                                {
                                    vListaDiLi.Add(di.ToString().Split(';')[0]);
                                }
                            }
                            Periodo periodo = PeriodoSelecionado;
                            Thread t = new Thread(() => _extratoService.ConsultarDI(periodo, TipoConsultaExtratoSelecionado, vListaDiLi, dtpInicial.Value, dtpFinal.Value));
                            t.Start();
                        }
                    }
                    else
                    {
                        FinalizarOperacao();
                    }

                    
                }

                if (TipoExtratoSelecionado == TipoExtrato.LI_LOTE)
                {
                    if (TipoConsultaExtratoSelecionado == TipoConsultaExtrato.NumeroDeclaracao)
                    {
                        if (cbxBancoDados.Checked)
                        {
                            var selecaoLI = db.tsiscomexweb_robo.Where(reg => reg.tp_consulta.ToLower() == "li" && reg.tp_acao.ToLower() == "lote" && reg.in_rodando == 1).OrderBy(reg => reg.nr_registro);

                            foreach (var row in selecaoLI)
                            {
                                row.in_rodando = 0;

                                vListaNrConsulta.Add(row.nr_registro.ToString().Trim() + "; " + row.cpf_certificado.ToString().Trim());

                                if (!vListaCpf.Contains(row.cpf_certificado.ToString().Trim()))
                                {
                                    vListaCpf.Add(row.cpf_certificado.ToString().Trim());
                                }
                            }
                            db.SaveChanges();
                        }

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
                    if (vListaCpf.Count() > 0)
                    {

                        var store = new X509Store(StoreLocation.CurrentUser);
                        store.Open(OpenFlags.ReadOnly);
                        var certificados = store.Certificates.OfType<X509Certificate2>().Where(x => x.Issuer.Contains("Secretaria da Receita Federal do Brasil")).OrderBy(x => x.Subject);

                        int qtdSetas = 0;

                        foreach (string cpf in vListaCpf)
                        {
                            int i = 0;
                            foreach (var certificado in certificados)
                            {
                                if (certificado.FriendlyName.Contains(cpf))
                                {
                                    qtdSetas = i++;

                                }
                                i++;
                            }

                            Thread tSeta = new Thread(() => _extratoService.SetarCertificado(qtdSetas));
                            tSeta.Start();
                            _extratoService.AbrirBrowser();
                            _extratoService.NavegarURLExtratoDeclaracaoLI();
                            Thread.Sleep(2000);
                            foreach (string di in vListaNrConsulta)
                            {
                                if (cpf == di.ToString().Split(';')[1].Trim())
                                {
                                    vListaDiLi.Add(di.ToString().Split(';')[0]);
                                }
                            }
                            Periodo periodo = PeriodoSelecionado;
                            Thread t = new Thread(() => _extratoService.ConsultarLILote(periodo, TipoConsultaExtratoSelecionado, vListaDiLi, dtpInicial.Value, dtpFinal.Value, Convert.ToInt32(qtdArqLote.Value)));
                            t.Start();
                        }
                    }
                    else
                    {
                        FinalizarOperacao();
                    }

                }


                if (TipoExtratoSelecionado == TipoExtrato.LI)
                {
                    if (TipoConsultaExtratoSelecionado == TipoConsultaExtrato.NumeroDeclaracao)
                    {
                        if (cbxBancoDados.Checked)
                        {
                            var selecaoLI = db.tsiscomexweb_robo.Where(reg => reg.tp_consulta.ToLower() == "li" && reg.tp_acao.ToLower() == "consulta" && reg.in_rodando == 1).OrderBy(reg => reg.nr_registro);

                            foreach (var row in selecaoLI)
                            {
                                //row.in_rodando = 0;

                                vListaNrConsulta.Add(row.nr_registro.ToString().Trim() + "; " + row.cpf_certificado.ToString().Trim());

                                if (!vListaCpf.Contains(row.cpf_certificado.ToString().Trim()))
                                {
                                    vListaCpf.Add(row.cpf_certificado.ToString().Trim());
                                }
                            }
                            //db.SaveChanges();
                        }

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
                    if (vListaCpf.Count() > 0)
                    {

                        var store = new X509Store(StoreLocation.CurrentUser);
                        store.Open(OpenFlags.ReadOnly);
                        var certificados = store.Certificates.OfType<X509Certificate2>().Where(x => x.Issuer.Contains("Secretaria da Receita Federal do Brasil")).OrderBy(x => x.Subject);

                        int qtdSetas = 0;

                        foreach (string cpf in vListaCpf)
                        {
                            int i = 0;
                            foreach (var certificado in certificados)
                            {
                                if (certificado.FriendlyName.Contains(cpf))
                                {
                                    qtdSetas = i++;

                                }
                                i++;
                            }

                            Thread tSeta = new Thread(() => _extratoService.SetarCertificado(qtdSetas));
                            tSeta.Start();
                            _extratoService.AbrirBrowser();
                            Thread.Sleep(2000);
                            _extratoService.NavegarURLExtratoDeclaracaoLI();
                            Thread.Sleep(2000);
                            foreach (string di in vListaNrConsulta)
                            {
                                if (cpf == di.ToString().Split(';')[1].Trim())
                                {
                                    vListaDiLi.Add(di.ToString().Split(';')[0]);
                                }
                            }
                            Periodo periodo = PeriodoSelecionado;                           
                            _extratoService.ConsultarLI(periodo, TipoConsultaExtratoSelecionado, vListaDiLi, dtpInicial.Value, dtpFinal.Value);                            
                        }
                    }                    
                     FinalizarOperacao();                    

                }
            }

            Thread.Sleep(2000);

            dgvNrDelacaracao.Rows.Clear();

            if (!cbxBancoDados.Checked)
            {
                MessageBox.Show("Consulta realizada com sucesso!", "Auto-Ecac", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //}

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

            //IniciarOperacao();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
                return rbConsultaDI.Checked == true ? TipoExtrato.DI : rbConsultaLI.Checked ? TipoExtrato.LI : TipoExtrato.LI_LOTE;
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

        public string URL_EXTRATO_LOGIN { get; private set; }
        public string URL_EXTRATO_DECLARACAO_LI { get; private set; }

        #endregion



        private void cbxBancoDados_Click(object sender, EventArgs e)
        {
            if (cbxBancoDados.Enabled)
            {
                //rbConsultaDI.Checked = true;
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
            if (Helper.ChecarConexaoInternet()) { 
            if (rbConsultaLI.Checked)
            {                
                tempoLI.Enabled = true;
                tempoLI.Interval = rdbMinutos.Checked ? (int)TimeSpan.FromMinutes(Convert.ToInt32(numericUpDown1.Value)).TotalMilliseconds : (int)TimeSpan.FromHours(Convert.ToInt32(numericUpDown1.Value)).TotalMilliseconds;
                tempoLI_Tick(sender, null);

            }
            else if (rbConsultaDI.Checked)
            {
                tempoDI.Enabled = true;
                tempoDI.Interval = rdbMinutos.Checked ? (int)TimeSpan.FromMinutes(Convert.ToInt32(numericUpDown1.Value)).TotalMilliseconds : (int)TimeSpan.FromHours(Convert.ToInt32(numericUpDown1.Value)).TotalMilliseconds;
                tempoDI_Tick(sender, null);
            }
            else
            {
                tempoLiLote.Enabled = true;
                tempoLI.Interval = (int)TimeSpan.FromMinutes(1).TotalMilliseconds;
                tempoLiLote_Tick(sender, null);

            }
            }
            else
            {
                MessageBox.Show("Sem conexão com a Internet!");                

            }

        }



        private void tempoDI_Tick(object sender, EventArgs e)
        {
            //rbConsultaDI.Checked = true;
            cbxBancoDados.Checked = true;
            panel3_MouseClick(sender, null);
            btnOK_Click(sender, e);

        }

        private void tempoLiLote_Tick(object sender, EventArgs e)
        {
            cbxBancoDados.Checked = true;
            panel3_MouseClick(sender, null);

            foreach (DataGridViewRow row in dgvHoraLI.Rows)
            {
                if (row.Cells[0].Value.ToString() == DateTime.Now.ToShortTimeString().ToString())
                {
                    btnOK_Click(sender, e);
                }
            }
        }

        private void tempoLI_Tick(object sender, EventArgs e)
        {

            if (Browser == null)
            {
                cbxBancoDados.Checked = true;
                panel3_MouseClick(sender, null);
                btnOK_Click(sender, e);

            }
            
        }

        private void rbConsultaLI_Click(object sender, EventArgs e)
        {
            grbPeriodocidadeLI.Enabled = rbConsultaLILote.Checked;
            dtpLi.Enabled =  rbConsultaLILote.Checked;
            btnAddTempo.Enabled = rbConsultaLILote.Checked;


            dgvHoraLI.Enabled = rbConsultaLILote.Checked;
            dgvHoraLI.BackgroundColor = rbConsultaLILote.Checked ? Color.LightYellow : Color.LightGray;

            qtdArqLote.Enabled = rbConsultaLILote.Checked;
            lblArqLote.Enabled = rbConsultaLILote.Checked;

            grbTempo.Enabled = rbConsultaDI.Checked || rbConsultaLI.Checked;
            rdbHora.Enabled = rbConsultaDI.Checked || rbConsultaLI.Checked;
            rdbMinutos.Enabled = rbConsultaDI.Checked || rbConsultaLI.Checked;

        }

        private void btnAddTempo_Click(object sender, EventArgs e)
        {
            dgvHoraLI.Rows.Add(dtpLi.Value.ToShortTimeString().ToString());

        }

        
    }
}
