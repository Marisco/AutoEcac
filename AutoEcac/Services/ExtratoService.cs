using AutoEcac.DAL;
using AutoEcac.Services;
using AutoEcac.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using Newtonsoft.Json;
using AutoEcac.model;
using OpenQA.Selenium.Support.UI;

namespace AutoEcac.Servicos
{
    public class ExtratoService : BaseService
    {
        private string _NmArquivoNovo;
        private IWebDriver _browser;
        private roboEntities _db;
        private KestraaUpload _kestraaUpload;

        public ExtratoService(IWebDriver browser, roboEntities db)
        {
            _browser = browser;
            _db = db;
            _kestraaUpload = new KestraaUpload();
        }

        public ExtratoService()
        {
        }

        public void NavegarURLExtratoDeclaracaoDI()
        {
            _browser.Navigate().GoToUrl(URL_EXTRATO_DECLARACAO_DI);
        }

        public void NavegarURLExtratoDeclaracaoLI()
        {
            _browser.Navigate().GoToUrl(URL_EXTRATO_DECLARACAO_LI);
        }


        public void AtualizarLI(string pCaminhoComnpleto, ref tsiscomexweb_robo pRegistro)
        {
            string orgaoAnuente = "";
            int codigoSituacaoAnuencia = 0;
            int inRodando = 0;

            XmlDocument xmlLiCompleta = new XmlDocument();
            xmlLiCompleta.Load(@pCaminhoComnpleto);
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmlLiCompleta.NameTable);
            nsMgr.AddNamespace("li", "http://www.serpro.gov.br/liweb/schema/ResultadoConsultaLoteLiWeb.html");
            XmlNode listaAnuencias = xmlLiCompleta.SelectSingleNode("/li:resposta-consulta-li/li:lista-li-completa/li:li-completa/li:Grupo-LI-Anuencias/li:lista-anuencias", nsMgr);

            foreach (XmlNode anuencia in listaAnuencias)
            {
                foreach (XmlNode atributo in anuencia.ChildNodes)
                {

                    if (atributo.Name == "orgao-anuente")
                    {
                        if (string.IsNullOrEmpty(orgaoAnuente))
                        {

                            orgaoAnuente = "{ orgaoAnuente : [{ " + atributo.InnerXml + ": ";
                        }
                        else
                        {
                            orgaoAnuente = orgaoAnuente + " { " + atributo.InnerXml + ": ";
                        }
                    };

                    if (atributo.Name == "codigo-situacao-anuencia")
                    {

                        codigoSituacaoAnuencia = int.Parse(atributo.InnerXml);
                        switch (codigoSituacaoAnuencia)
                        {
                            case (int)InSituacao.CANCELADA:
                                orgaoAnuente = orgaoAnuente + "CANCELADA },";
                                break;
                            case (int)InSituacao.DEFERIDA:
                                orgaoAnuente = orgaoAnuente + "DEFERIDA },";
                                break;
                            case (int)InSituacao.DEFERIDA_JUDICIALMENTE:
                                orgaoAnuente = orgaoAnuente + "DEFERIDA_JUDICIALMENTE },";
                                break;
                            case (int)InSituacao.DEFERIDA_JUDICIALMENTE_RESERVADA:
                                orgaoAnuente = orgaoAnuente + "DEFERIDA_JUDICIALMENTE_RESERVADA },";
                                break;
                            case (int)InSituacao.DEFERIDA_JUDICIALMENTE_VINCULADA_DI:
                                orgaoAnuente = orgaoAnuente + "DEFERIDA_JUDICIALMENTE_VINCULADA_DI },";
                                break;
                            case (int)InSituacao.DEFERIDA_RESERVADA:
                                orgaoAnuente = orgaoAnuente + "DEFERIDA_RESERVADA },";
                                break;
                            case (int)InSituacao.DEFERIDA_VINCULADA_DI:
                                orgaoAnuente = orgaoAnuente + "DEFERIDA_VINCULADA_DI },";
                                break;
                            case (int)InSituacao.DESEMBARAÇADA:
                                orgaoAnuente = orgaoAnuente + "DESEMBARAÇADA },";
                                break;
                            case (int)InSituacao.VENCIDA:
                                orgaoAnuente = orgaoAnuente + "VENCIDA },";
                                break;
                            case (int)InSituacao.INDEFERIDA:
                                orgaoAnuente = orgaoAnuente + "INDEFERIDA },";
                                break;
                            case (int)InSituacao.EMBARQUE_AUTORIZADO:
                                orgaoAnuente = orgaoAnuente + "EMBARQUE_AUTORIZADO },";
                                inRodando++;
                                break;
                            case (int)InSituacao.EM_ANALISE:
                                orgaoAnuente = orgaoAnuente + "EM_ANALISE },";
                                inRodando++;
                                break;
                            case (int)InSituacao.EM_EXIGENCIA:
                                orgaoAnuente = orgaoAnuente + "EM_EXIGENCIA },";
                                inRodando++;
                                break;
                            case (int)InSituacao.PARA_ANALISE:
                                orgaoAnuente = orgaoAnuente + "PARA_ANALISE },";
                                inRodando++;
                                break;
                        }

                    };

                }

            };

            orgaoAnuente = orgaoAnuente + "]}";


            string OrgaoAnuenteBase = pRegistro.tp_anuencia;
            string OrgaoAnuenteAtual = orgaoAnuente;

            pRegistro.tp_anuencia = OrgaoAnuenteAtual;
            pRegistro.in_rodando = pRegistro.tp_acao.ToLower() == "lote" && inRodando > 0 ? 1 : 0;
            pRegistro.xml_comando = xmlLiCompleta.OuterXml;
            pRegistro.nr_tentativas = pRegistro.nr_tentativas + 1;

            if ((pRegistro.tp_acao.ToLower() == "consulta" && inRodando > 0) || (pRegistro.tp_acao.ToLower() == "lote" && OrgaoAnuenteBase != OrgaoAnuenteAtual && inRodando > 0))
            {
                tsiscomexweb_robo novoRegistro = new tsiscomexweb_robo
                {
                    nr_processo = pRegistro.nr_processo,
                    nr_registro = pRegistro.nr_registro,
                    nr_seq = pRegistro.nr_seq,
                    tp_consulta = pRegistro.tp_consulta,
                    tp_acao = "lote",
                    dt_agendamento = DateTime.Now.AddMinutes(30),
                    dt_realizacao = DateTime.Now,
                    dt_registro_di = pRegistro.dt_registro_di,
                    nr_tentativas = 0,
                    tx_erro = "",
                    cd_usuario = pRegistro.cd_usuario,
                    cpf_certificado = pRegistro.cpf_certificado,
                    in_rodando = 1,
                    in_desembaraco = 0,
                    tp_anuencia = orgaoAnuente,
                    in_situacao = 0,
                    xml_comando = xmlLiCompleta.OuterXml,
                    xml_retorno = "",
                    pdf_comprovante = null,
                    pdf_extrato = null

                };
                _db.tsiscomexweb_robo.Add(novoRegistro);
            }
            else
            {
                
                pRegistro.in_desembaraco = OrgaoAnuenteAtual.Contains("DESEMBARAÇADA") ? 1 : 0;
                pRegistro.xml_retorno = xmlLiCompleta.OuterXml;
                
            }


        }

        public void ConsultarLI(Periodo pPeriodo, TipoConsultaExtrato pTipoConsultaExtrato, List<string> pNrConsulta, DateTime pDtInicial, DateTime pDtFinal)
        {
            try
            {
                base._periodo = pPeriodo;
                foreach (DateTime data in LoopNoPeriodo(pDtInicial, pDtFinal))
                {

                    Thread.Sleep(2000);

                    foreach (string numero in pNrConsulta)
                    {
                        int numeroLi = int.Parse(numero);
                        tsiscomexweb_robo registro = _db.tsiscomexweb_robo.Where(reg => reg.nr_registro == numeroLi && reg.tp_acao.ToLower() == "consulta").First();
                        Thread.Sleep(2000);
                        base._consultaAtual = numero;

                        switch ((int)pTipoConsultaExtrato)
                        {
                            case (int)TipoConsultaExtrato.NumeroDeclaracao:
                                _browser.FindElement(By.Name("numeroLI")).SendKeys(Keys.Delete);
                                Thread.Sleep(500);
                                _browser.FindElement(By.Name("numeroLI")).SendKeys(numero);
                                break;
                            case (int)TipoConsultaExtrato.CnpjImportador:
                                _browser.FindElement(By.Name("numeroLI")).SendKeys(Keys.Delete);
                                Thread.Sleep(500);
                                _browser.FindElement(By.Name("cnpjImportador")).SendKeys(numero);
                                break;
                            case (int)TipoConsultaExtrato.CpfImportador:
                                _browser.FindElement(By.Name("numeroLI")).SendKeys(Keys.Delete);
                                Thread.Sleep(500);
                                _browser.FindElement(By.Name("cpfImportador")).SendKeys(numero);
                                break;
                        }

                        Thread.Sleep(1000);

                        if (pTipoConsultaExtrato != TipoConsultaExtrato.NumeroDeclaracao)
                        {
                            var dtInicial = _dtInicial.ToShortDateString().Trim().Replace("/", "");
                            var dtFinal = _dtFinal.ToShortDateString().Trim().Replace("/", "");

                            var objDataInicial = _browser.FindElement(By.Name("dataInicial"));
                            var objDataFinal = _browser.FindElement(By.Name("dataFinal"));

                            Thread.Sleep(1000);
                            objDataInicial.Clear();
                            objDataInicial.SendKeys(dtInicial);
                            Thread.Sleep(1000);

                            objDataFinal.Clear();
                            objDataFinal.SendKeys(dtFinal);
                            Thread.Sleep(1000);
                        }

                        _browser.FindElement(By.Name("enviar")).Click();
                        Thread.Sleep(1000);

                        if (pTipoConsultaExtrato != TipoConsultaExtrato.NumeroDeclaracao)
                        {
                            List<string> vListaLI = getListaLI();
                            Thread.Sleep(2000);
                            ConsultarLI(pPeriodo, 0, vListaLI, DateTime.Now, DateTime.Now);
                        }

                        try
                        {


                            _browser.FindElement(By.Id("imprimir")).Click();
                            Thread.Sleep(2000);
                            _NmArquivoPadrao = "ExtratoLI.pdf";
                            _NmArquivoNovo = numero + "_extrato.pdf";
                            SalvarArquivosBaixados(_NmArquivoNovo, this.DiretorioCompleto);
                            Thread.Sleep(1000);

                            var arquivoPdf = System.IO.File.ReadAllBytes(this.DiretorioCompleto + "\\" + _NmArquivoNovo);
                            registro.pdf_extrato = arquivoPdf;

                            //_browser.Navigate().Back();
                            //Thread.Sleep(1000);
                            //_browser.Navigate().Back();
                            Thread.Sleep(1000);

                            _browser.Navigate().GoToUrl("https://www1c.siscomex.receita.fazenda.gov.br/li_web-7/liweb_menu_li_consultar_lote_li.do");
                            Thread.Sleep(2000);

                            XmlDocument consultaPorLi = new XmlDocument();
                            consultaPorLi.Load(@"C:\AutoEcac\Arquivos\ConlustaLotePadrao\consultaPorLi.xml");
                            consultaPorLi.FirstChild.Value = "version=\"1.0\" encoding=\"UTF-8\"";
                            consultaPorLi.GetElementsByTagName("li").Item(0).InnerText = numero;
                            string fileName = "C:\\AutoEcac\\Arquivos\\ConlustaLotePadrao\\consultaPorLi.xml";
                            string textToAdd = consultaPorLi.InnerXml;
                            FileStream fs = null;
                            try
                            {
                                fs = new FileStream(fileName, FileMode.Truncate);
                                using (StreamWriter writer = new StreamWriter(fs, new UTF8Encoding(false)))
                                {
                                    writer.Write(textToAdd);
                                }
                            }
                            finally
                            {
                                if (fs != null)
                                    fs.Dispose();
                            }

                            _browser.FindElement(By.Id("arquivo")).Click();
                            Thread.Sleep(2000);
                            System.Windows.Forms.SendKeys.SendWait(@"C:\AutoEcac\Arquivos\ConlustaLotePadrao\consultaPorLi.xml");
                            Thread.Sleep(2000);
                            System.Windows.Forms.SendKeys.SendWait(@"{Enter}");


                            Thread.Sleep(1000);
                            _browser.FindElement(By.Id("enviarArquivo")).Click();
                            Thread.Sleep(2000);
                            _NmArquivoPadrao = "CONSULTA.XXXXXX.xml";
                            _NmArquivoNovo = numero + "_consulta.xml";
                            SalvarArquivosBaixados(_NmArquivoNovo, this.DiretorioCompleto);
                            Thread.Sleep(1000);

                            NavegarURLExtratoDeclaracaoLI();

                            Thread.Sleep(1000);                            
                            AtualizarLI(this.DiretorioCompleto + "\\" + _NmArquivoNovo, ref registro);
                            _db.SaveChanges();

                        }
                        catch (Exception e)
                        {
                            string msg = DateTime.Now.ToString() + " Erro: Consulta Atual: " + _consultaAtual + " Consulta Anterior: " + _consultaAnterior + " Msg: " + e.Message;
                            registro.tx_erro = msg;
                            registro.in_rodando = 1;
                            _db.SaveChanges();
                            LogarErros(msg);
                            Thread.Sleep(1000);
                            try
                            {
                                _browser.SwitchTo().Alert().Accept();

                            }
                            catch (Exception)
                            {
                                msg = DateTime.Now.ToString() + " Erro: Consulta Atual: " + _consultaAtual + " Consulta Anterior: " + _consultaAnterior + " Msg: " + e.Message;
                                registro.tx_erro = msg;
                                registro.in_rodando = 1;
                                _db.SaveChanges();
                                LogarErros(msg);
                            }
                        }

                        base._consultaAnterior = _consultaAtual;
                    }

                }

                pNrConsulta.Clear();
                _browser.Quit();
            }
            catch (Exception e)
            {
                
                LogarErros(DateTime.Now.ToString() + " Consulta Antual: " + _consultaAtual + " Consulta Anterior: " + _consultaAnterior + "Msg: " + e.Message);

            }
        }


        public void ConsultarLILote(Periodo pPeriodo, TipoConsultaExtrato pTipoConsultaExtrato, List<string> pNrConsulta, DateTime pDtInicial, DateTime pDtFinal, int pQtdEmLote)
        {
            try
            {
                base._periodo = pPeriodo;
                foreach (DateTime data in LoopNoPeriodo(pDtInicial, pDtFinal))
                {

                    Thread.Sleep(2000);

                    int ContaLote = 0;
                    XmlDocument consultaPorLote = new XmlDocument();
                    consultaPorLote.Load(@"C:\AutoEcac\Arquivos\ConlustaLotePadrao\consultaPorLole.xml");
                    consultaPorLote.FirstChild.Value = "version=\"1.0\" encoding=\"UTF-8\"";
                    XmlNode node = consultaPorLote.ChildNodes[1].ChildNodes[1];
                    node.RemoveAll();

                    string ultimoRegistro = pNrConsulta.Last();

                    foreach (string numero in pNrConsulta)
                    {
                        base._consultaAtual = numero;
                        ContaLote++;
                        XmlElement elem = consultaPorLote.CreateElement("li", "http://www.serpro.gov.br/liweb/schema/ConsultaLoteLiWeb.html");
                        elem.InnerText = numero;
                        node.AppendChild(elem);


                        if (ContaLote < pQtdEmLote && numero != ultimoRegistro)
                        {
                            continue;
                        }

                        try
                        {
                            ContaLote = 0;
                            string fileName = "C:\\AutoEcac\\Arquivos\\ConlustaLotePadrao\\consultaPorLole.xml";
                            string textToAdd = consultaPorLote.InnerXml;
                            FileStream fs = null;
                            try
                            {
                                fs = new FileStream(fileName, FileMode.Truncate);
                                using (StreamWriter writer = new StreamWriter(fs, new UTF8Encoding(false)))
                                {
                                    writer.Write(textToAdd);
                                }
                            }
                            finally
                            {
                                if (fs != null)
                                    fs.Dispose();
                            }



                            _browser.Navigate().GoToUrl("https://www1c.siscomex.receita.fazenda.gov.br/li_web-7/liweb_menu_li_consultar_lote_li.do");
                            Thread.Sleep(2000);
                            _browser.FindElement(By.Id("arquivo")).Click();
                            Thread.Sleep(2000);
                            System.Windows.Forms.SendKeys.SendWait(@"C:\AutoEcac\Arquivos\ConlustaLotePadrao\consultaPorLole.xml");
                            Thread.Sleep(2000);
                            System.Windows.Forms.SendKeys.SendWait(@"{Enter}");
                            Thread.Sleep(1000);
                            _browser.FindElement(By.Id("enviarArquivo")).Click();
                            Thread.Sleep(2000);

                            node.RemoveAll();

                            _NmArquivoPadrao = "CONSULTA.XXXXXX.xml";
                            _NmArquivoNovo = "consulta.lote.xml";
                            SalvarArquivosBaixados(_NmArquivoNovo, this.DiretorioCompleto);
                            TratarXMLConsulta(this.DiretorioCompleto + "\\" + _NmArquivoNovo);
                            _db.SaveChanges();
                            Thread.Sleep(1000);
                            NavegarURLExtratoDeclaracaoLI();
                            Thread.Sleep(1000);

                        }
                        catch (Exception e)
                        {
                            string msg = DateTime.Now.ToString() + " Erro: Consulta Atual: " + _consultaAtual + " Consulta Anterior: " + _consultaAnterior + " Msg: " + e.Message;
                            _db.SaveChanges();                            
                            LogarErros(msg);
                            Thread.Sleep(1000);
                            try
                            {
                                _browser.SwitchTo().Alert().Accept();

                            }
                            catch (Exception)
                            {
                                msg = DateTime.Now.ToString() + " Erro: Consulta Atual: " + _consultaAtual + " Consulta Anterior: " + _consultaAnterior + " Msg: " + e.Message;
                                _db.SaveChanges();
                                LogarErros(msg);
                            }
                        }

                        base._consultaAnterior = _consultaAtual;
                    }

                }

                pNrConsulta.Clear();
                _browser.Quit();
            }
            catch (Exception e)
            {

                LogarErros(DateTime.Now.ToString() + " Consulta Antual: " + _consultaAtual + " Consulta Anterior: " + _consultaAnterior + "Msg: " + e.Message);

            }
        }

        private void TratarXMLConsulta(string pCaminhoCompleto)
        {
            tsiscomexweb_robo registro = new tsiscomexweb_robo();
            try
            {
                XmlDocument xmlLiCompleta = new XmlDocument();
                xmlLiCompleta.Load(@pCaminhoCompleto);
                XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmlLiCompleta.NameTable);
                nsMgr.AddNamespace("li", "http://www.serpro.gov.br/liweb/schema/ResultadoConsultaLoteLiWeb.html");
                XmlNode listaLiCompleta = xmlLiCompleta.SelectSingleNode("/li:resposta-consulta-li/li:lista-li-completa", nsMgr);
                XmlDocument novoXmlLiCompleta = new XmlDocument();
                novoXmlLiCompleta.InnerXml = xmlLiCompleta.OuterXml;


                


                foreach (XmlNode liCompleta in listaLiCompleta)
                {
                    XmlNode xmlnumeroLi = liCompleta.ChildNodes[0].FirstChild;
                    string numero = xmlnumeroLi.InnerText.Replace("/", "").Replace("-", "").Trim();
                    int numeroLi = int.Parse(numero);
                    registro = _db.tsiscomexweb_robo.Where(reg => reg.nr_registro == numeroLi && reg.tp_acao.ToLower() == "lote").OrderByDescending(reg => reg.nr_sequencia).First();

                    novoXmlLiCompleta.ChildNodes[1].ChildNodes[2].RemoveAll();
                    novoXmlLiCompleta.ChildNodes[1].ChildNodes[2].InnerXml = liCompleta.OuterXml;

                    string fileName = this.DiretorioCompleto + "\\" + numero + "_consulta.xml";
                    string textToAdd = novoXmlLiCompleta.OuterXml;
                    FileStream fs = null;
                    try
                    {
                        fs = new FileStream(fileName, FileMode.Create);
                        using (StreamWriter writer = new StreamWriter(fs, new UTF8Encoding(false)))
                        {
                            writer.Write(textToAdd);
                        }
                    }
                    finally
                    {
                        if (fs != null)
                            fs.Dispose();
                    }

                    AtualizarLI(fileName, ref registro);

                }
            }
            catch (Exception e)
            {
                registro.tx_erro = e.Message;
                registro.in_rodando = 1;
                LogarErros(DateTime.Now.ToString() + " Consulta Antual: " + _consultaAtual + " Consulta Anterior: " + _consultaAnterior + "Msg: " + e.Message);
                _db.SaveChanges();
            }
        }


        private List<string> getListaLI()
        {
            string vNrLI = "";
            List<string> vListaLi = new List<string>();

            try
            {
                IWebElement tableElement = _browser.FindElement(By.Id("tabela"));
                Thread.Sleep(2000);
                IList<IWebElement> trCollection = tableElement.FindElements(By.TagName("tr"));
                IList<IWebElement> tdCollection;
                Actions builder = new Actions(_browser);

                foreach (IWebElement element in trCollection)
                {
                    try
                    {
                        tdCollection = element.FindElements(By.TagName("td"));

                        if (tdCollection.Count() > 1)
                        {
                            vNrLI = tdCollection[0].Text.Trim().Replace("/", "").Replace("-", "");
                            vListaLi.Add(vNrLI);
                        }
                    }
                    catch (Exception e)
                    {
                        LogarErros(DateTime.Now.ToString() + " Consulta Antual: " + _consultaAtual + " Consulta Anterior: " + _consultaAnterior + "Msg: " + e.Message);
                    }
                }

                _browser.Navigate().Back();
                Thread.Sleep(1000);

            }
            catch (Exception e)
            {
                Thread.Sleep(1000);
                try
                {
                    LogarErros(DateTime.Now.ToString() + " Consulta Antual: " + _consultaAtual + " Consulta Anterior: " + _consultaAnterior + "Msg: " + e.Message);
                    _browser.SwitchTo().Alert().Accept();

                }
                catch (Exception ex)
                {
                    LogarErros(DateTime.Now.ToString() + " Consulta Antual: " + _consultaAtual + " Consulta Anterior: " + _consultaAnterior + "Msg: " + ex.Message);
                }
            }
            return vListaLi;
        }

        public void ConsultarDI(Periodo pPeriodo, TipoConsultaExtrato pTipoConsultaExtrato, List<string> pNrConsulta, DateTime pDtInicial, DateTime pDtFinal)
        {
            try
            {

                base._periodo = pPeriodo;
                foreach (DateTime data in LoopNoPeriodo(pDtInicial, pDtFinal))
                {
                    Thread.Sleep(2000);

                    foreach (string numero in pNrConsulta)
                    {
                        base._consultaAtual = numero;
                        int nrRegistroDi = Int32.Parse(numero);
                        tsiscomexweb_robo registro = _db.tsiscomexweb_robo.OrderByDescending(reg => reg.nr_sequencia).Where(reg => reg.nr_registro == nrRegistroDi).OrderByDescending(reg => reg.nr_sequencia).First();

                        switch ((int)pTipoConsultaExtrato)
                        {
                            case (int)TipoConsultaExtrato.NumeroDeclaracao:
                                _browser.FindElement(By.Name("nrDeclaracao")).Clear();
                                _browser.FindElement(By.Name("nrDeclaracao")).SendKeys(numero);
                                break;
                            case (int)TipoConsultaExtrato.CnpjImportador:
                                _browser.FindElement(By.Name("cnpjImport")).Clear();
                                _browser.FindElement(By.Name("cnpjImport")).SendKeys(numero);
                                break;
                            case (int)TipoConsultaExtrato.CpfImportador:
                                _browser.FindElement(By.Name("cpfImport")).Clear();
                                _browser.FindElement(By.Name("cpfImport")).SendKeys(numero);
                                break;
                            case (int)TipoConsultaExtrato.CpfUsuario:
                                _browser.FindElement(By.Name("cpfUsuario")).Clear();
                                _browser.FindElement(By.Name("cpfUsuario")).SendKeys(numero);
                                break;
                        }

                        Thread.Sleep(2000);

                        if (pTipoConsultaExtrato != TipoConsultaExtrato.NumeroDeclaracao)
                        {
                            _browser.FindElement(By.Name("inicioConsulta")).Clear();
                            _browser.FindElement(By.Name("fimConsulta")).Clear();

                            _browser.FindElement(By.Name("inicioConsulta")).SendKeys(_dtInicial.ToShortDateString().Trim().Replace("/", ""));
                            Thread.Sleep(1000);

                            _browser.FindElement(By.Name("fimConsulta")).SendKeys(_dtFinal.ToShortDateString().Trim().Replace("/", ""));
                            Thread.Sleep(2000);
                        }

                        _browser.FindElement(By.Name("enviar")).Click();
                        Thread.Sleep(2000);

                        if (pTipoConsultaExtrato != TipoConsultaExtrato.NumeroDeclaracao)
                        {
                            List<string> vListaDI = getListaDI();
                            Thread.Sleep(2000);
                            ConsultarDI(pPeriodo, 0, vListaDI, DateTime.Now, DateTime.Now);
                        }


                        try
                        {
                            _browser.FindElement(By.Id("consultarXmlDi")).Click();
                            Thread.Sleep(2000);
                            _NmArquivoPadrao = numero + ".xml";
                            _NmArquivoNovo = numero + "_declaracao.xml";
                            SalvarArquivosBaixados(_NmArquivoNovo, this.DiretorioCompleto);
                            Thread.Sleep(2000);

                            var arquivo = System.IO.File.ReadAllText(this.DiretorioCompleto + "\\" + _NmArquivoNovo);

                            registro.xml_retorno = arquivo;

                            var arquivoXml = System.IO.File.ReadAllBytes(this.DiretorioCompleto + "\\" + _NmArquivoNovo);
                            KestraaUploadRequest uploadRequest = new KestraaUploadRequest(_NmArquivoNovo, arquivoXml, "Import Declaration - DI",
                                numero,
                                DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                                "99999999");

                            _kestraaUpload.enviarArquivosws(uploadRequest, registro.nr_registro);

                            //System.IO.File.Delete(this.DiretorioCompleto + "\\" + _NmArquivoNovo);

                            _browser.FindElement(By.Id("btnRegistrarDI")).Click();
                            Thread.Sleep(2000);
                            _NmArquivoPadrao = "Extrato.pdf";
                            _NmArquivoNovo = numero + "_extrato.pdf";
                            SalvarArquivosBaixados(_NmArquivoNovo, this.DiretorioCompleto);
                            Thread.Sleep(2000);

                            var arquivoPdf = System.IO.File.ReadAllBytes(this.DiretorioCompleto + "\\" + _NmArquivoNovo);
                            registro.pdf_extrato = arquivoPdf;

                            uploadRequest = new KestraaUploadRequest(_NmArquivoNovo, arquivoPdf, "Other",
                                numero,
                                DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                                "99999999");

                            _kestraaUpload.enviarArquivosws(uploadRequest, registro.nr_processo);
                            //System.IO.File.Delete(this.DiretorioCompleto + "\\" + _NmArquivoNovo);

                            if (gerarXmlAcompanhamento(numero, ref registro))
                            {
                                _browser.FindElement(By.Id("btnComprovanteDI")).Click();
                                Thread.Sleep(1000);
                                _browser.SwitchTo().Window(_browser.WindowHandles.Last());
                                _browser.FindElement(By.XPath("//input[contains(@value,'Emitir')]")).Click();
                                Thread.Sleep(2000);
                                _NmArquivoPadrao = "COMPROVANTE_DI.pdf";
                                _NmArquivoNovo = numero + "_comprovante.pdf";
                                SalvarArquivosBaixados(_NmArquivoNovo, this.DiretorioCompleto);
                                Thread.Sleep(2000);

                                arquivoPdf = System.IO.File.ReadAllBytes(this.DiretorioCompleto + "\\" + _NmArquivoNovo);
                                registro.pdf_comprovante = arquivoPdf;

                                uploadRequest = new KestraaUploadRequest(_NmArquivoNovo, arquivoPdf, "Other",
                                    numero,
                                    DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                                    "99999999");

                                _kestraaUpload.enviarArquivosws(uploadRequest, registro.nr_processo);
                                //System.IO.File.Delete(this.DiretorioCompleto + "\\" + _NmArquivoNovo);


                                _browser.Close();
                                _browser.SwitchTo().Window(_browser.WindowHandles.First());

                            }
                            //_browser.Navigate().Back();
                            Thread.Sleep(1000);
                            //_browser.Navigate().Back();
                            NavegarURLExtratoDeclaracaoDI();

                        }
                        catch (Exception e)
                        {
                            string msg = DateTime.Now.ToString() + " Consulta Antual: " + _consultaAtual + " Consulta Anterior: " + _consultaAnterior + "Msg: " + e.Message;
                            registro.tx_erro = msg;
                            registro.in_rodando = 1;
                            _db.SaveChanges();
                            LogarErros(msg);

                            Thread.Sleep(1000);
                            try
                            {
                                _browser.SwitchTo().Alert().Accept();

                            }
                            catch (Exception)
                            {

                                // throw;
                            }
                        }

                        base._consultaAnterior = base._consultaAtual;
                    }
                }
            }
            catch (Exception e)
            {
                LogarErros(DateTime.Now.ToString() + " Consulta Antual: " + _consultaAtual + " Consulta Anterior: " + _consultaAnterior + "Msg: " + e.Message);

            }
            pNrConsulta.Clear();
            _db.SaveChanges();
            _browser.Quit();
        }

        private Boolean gerarXmlAcompanhamento(string pNumerorDi, ref tsiscomexweb_robo pRegistro)
        {
            Boolean bEmitirComnprovante = false;

            XmlDocument vXmlAcompanhamento = new XmlDocument();
            XmlNode docNode = vXmlAcompanhamento.CreateXmlDeclaration("1.0", "UTF-8", null);
            vXmlAcompanhamento.AppendChild(docNode);
            XmlNode DI = vXmlAcompanhamento.CreateElement("DI");
            vXmlAcompanhamento.AppendChild(DI);

            _browser.FindElement(By.Id("btnAcompahamentoDI")).Click();
            Thread.Sleep(2000);
            _browser.SwitchTo().Window(_browser.WindowHandles.Last());

            IWebElement tabelaSituacao = _browser.FindElement(By.Id("tabela"));
            Thread.Sleep(2000);
            IList<IWebElement> trCollection = tabelaSituacao.FindElements(By.TagName("tr"));
            IList<IWebElement> tdCollection;

            foreach (IWebElement element in trCollection)
            {
                try
                {
                    tdCollection = element.FindElements(By.TagName("td"));

                    if (tdCollection.Count() > 1)
                    {
                        tdCollection[0].Click();

                        XmlNode DIChild = vXmlAcompanhamento.CreateElement("identificador-di");
                        DIChild.InnerText = tdCollection[0].Text.Trim();
                        DI.AppendChild(DIChild);

                        bEmitirComnprovante = tdCollection[1].Text.Trim() == "DECLARACAO DESEMBARACADA";

                        /*
                        if (!bEmitirComnprovante)
                        {
                            Console.WriteLine(tdCollection[1].Text);
                        }
                        */

                        for (int i = 1; i < 10; i++)
                        {
                            try
                            {
                                IWebElement tabelasDinamicas = _browser.FindElement(By.Id("TABLE_" + i));

                                string vNmTag = Helper.NormalizarTexto(tabelasDinamicas.FindElement(By.XPath("..")).Text.ToString().Trim());
                                vNmTag = vNmTag.Substring(0, vNmTag.IndexOf(" ")).Trim().ToLower();

                                IList<IWebElement> trCollection2 = tabelasDinamicas.FindElements(By.TagName("tr"));
                                IList<IWebElement> tdCollection2;
                                foreach (IWebElement element2 in trCollection2)
                                {
                                    tdCollection2 = element2.FindElements(By.TagName("td"));
                                    try
                                    {
                                        if (tdCollection2.Count() > 1)
                                        {
                                            vNmTag = tdCollection2[0].Text.Trim();
                                            DIChild = vXmlAcompanhamento.CreateElement(vNmTag.Replace(":", "").Replace(" ", "-").ToLower());
                                            DIChild.InnerText = tdCollection2[tdCollection2.Count() - 1].Text.Trim();
                                            DI.AppendChild(DIChild);
                                        }
                                        else
                                        {
                                            DIChild = vXmlAcompanhamento.CreateElement(vNmTag);
                                            DIChild.InnerText = tdCollection2[0].Text.Trim();
                                            DI.AppendChild(DIChild);

                                        }

                                    }
                                    catch (Exception)
                                    {

                                        // throw;
                                    }

                                }
                            }
                            catch (Exception)
                            {

                                //throw;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    // throw;
                }
            }

            XmlTextWriter writer = new XmlTextWriter(this.DiretorioCompleto + "\\" + pNumerorDi + "_situacao.xml", null);
            vXmlAcompanhamento.Save(writer);
            writer.Close();

            Thread.Sleep(2000);

            if (!bEmitirComnprovante)
            {
                var arquivo = System.IO.File.ReadAllText(this.DiretorioCompleto + "\\" + pNumerorDi + "_situacao.xml");
                pRegistro.xml_comando = arquivo;
                pRegistro.in_desembaraco = 0;
                pRegistro.in_rodando = 0;

                var arquivoXml = System.IO.File.ReadAllBytes(this.DiretorioCompleto + "\\" + pNumerorDi + "_situacao.xml");
                KestraaUploadRequest uploadRequest = new KestraaUploadRequest(pNumerorDi + "_situacao.xml", arquivoXml, "Other",
                            pNumerorDi,
                            DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                            "99999999");

                _kestraaUpload.enviarArquivosws(uploadRequest, pRegistro.nr_processo);
                //System.IO.File.Delete(this.DiretorioCompleto + "\\" + pNumerorDi + "_situacao.xml");

                if (pRegistro.tp_acao != "acompanha")
                {
                    tsiscomexweb_robo novoRegistro = new tsiscomexweb_robo
                    {
                        nr_processo = pRegistro.nr_processo,
                        nr_registro = pRegistro.nr_registro,
                        nr_seq = pRegistro.nr_seq,
                        tp_consulta = pRegistro.tp_consulta,
                        tp_acao = "acompanha",
                        dt_agendamento = DateTime.Now.AddMinutes(30),
                        dt_realizacao = DateTime.Now,
                        dt_registro_di = pRegistro.dt_registro_di,
                        nr_tentativas = 1,
                        tx_erro = "",
                        cd_usuario = pRegistro.cd_usuario,
                        cpf_certificado = pRegistro.cpf_certificado,
                        in_rodando = 1,
                        in_desembaraco = 0,
                        tp_anuencia = pRegistro.tp_anuencia,
                        in_situacao = 0,
                        xml_comando = "",
                        xml_retorno = "",
                        pdf_comprovante = null,
                        pdf_extrato = null
                    };
                    _db.tsiscomexweb_robo.Add(novoRegistro);

                }
                else
                {
                    pRegistro.dt_realizacao = DateTime.Now;
                    pRegistro.dt_agendamento = DateTime.Now.AddMinutes(30);
                    pRegistro.nr_tentativas = pRegistro.nr_tentativas++;
                }
            }
            else
            {
                var arquivo = System.IO.File.ReadAllText(this.DiretorioCompleto + "\\" + pNumerorDi + "_situacao.xml");
                pRegistro.xml_comando = arquivo;
                pRegistro.dt_realizacao = DateTime.Now;
                pRegistro.in_desembaraco = 1;
                pRegistro.in_rodando = 0;

                var arquivoXml = System.IO.File.ReadAllBytes(this.DiretorioCompleto + "\\" + pNumerorDi + "_situacao.xml");
                KestraaUploadRequest uploadRequest = new KestraaUploadRequest(pNumerorDi + "_situacao.xml", arquivoXml, "xml",
                            "NUMERODI - XML Comando",
                            DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                            "99999999");

                _kestraaUpload.enviarArquivosws(uploadRequest, pRegistro.nr_processo);
                //System.IO.File.Delete(this.DiretorioCompleto + "\\" + pNumerorDi + "_situacao.xml");
            }

            _browser.Close();
            _browser.SwitchTo().Window(_browser.WindowHandles.First());

            return bEmitirComnprovante;
        }

        public void ConsultarExtrato(string cnpj, bool bConsolidarXML, DateTime dInicial, DateTime dFinal)
        {
            foreach (DateTime data in LoopNoPeriodo(dInicial, dFinal))
            {
                Thread.Sleep(1000);

                _browser.FindElement(By.Name("cnpjImport")).SendKeys(cnpj);
                Thread.Sleep(1000);

                _browser.FindElement(By.Name("inicioConsulta")).SendKeys(_dtInicial.ToShortDateString().Trim().Replace("/", ""));
                Thread.Sleep(1000);

                _browser.FindElement(By.Name("fimConsulta")).SendKeys(_dtFinal.ToShortDateString().Trim().Replace("/", ""));
                Thread.Sleep(1000);

                _browser.FindElement(By.Name("enviar")).Click();
                Thread.Sleep(1000);

                try
                {
                    string dtConsultaInicial = _dtInicial.ToShortDateString().Replace("/", "");
                    string dtConsultaFinal = _dtFinal.ToShortDateString().Replace("/", "");

                    dtConsultaInicial = dtConsultaInicial.Substring(4, 4).ToString() + dtConsultaInicial.Substring(2, 2).ToString() + dtConsultaInicial.Substring(0, 2).ToString();
                    dtConsultaFinal = dtConsultaFinal.Substring(4, 4).ToString() + dtConsultaFinal.Substring(2, 2).ToString() + dtConsultaFinal.Substring(0, 2).ToString();

                    _NmArquivoNovo = "consultaDI_" + dtConsultaInicial + "_" + dtConsultaFinal;

                    if (bConsolidarXML)
                    {
                        Thread.Sleep(2000);
                        try
                        {
                            _browser.FindElement(By.Id("ckTodos")).Click();
                            Thread.Sleep(5000);
                            _browser.FindElement(By.Name("gerarXml")).Click();
                            _NmArquivoPadrao = "consultaDI" + DateTime.Now.ToString("yyyyMMddHHmm") + ".xml";
                            Thread.Sleep(2000);
                            SalvarArquivosBaixados(_NmArquivoNovo + ".xml", this.DiretorioCompleto);
                            Thread.Sleep(1000);

                        }
                        catch (Exception)
                        {
                            getTabelaExtrato();
                        }

                    }
                    else
                    {
                        getTabelaExtrato();

                    }
                }
                catch (Exception)
                {
                    Thread.Sleep(1000);
                    try
                    {
                        _browser.SwitchTo().Alert().Accept();

                    }
                    catch (Exception)
                    {

                        // throw;
                    }
                }
                Thread.Sleep(1000);
                _browser.Navigate().Back();
            }
        }

        private List<string> getListaDI()
        {
            string vNrDI = "";
            List<string> vListaDi = new List<string>();
            //string vCnpjImportador = "";
            //string vDtRegistro = "";
            //string vCpfRegistro = "";

            try
            {

                IWebElement tableElement = _browser.FindElement(By.Id("tabListaDI"));
                Thread.Sleep(2000);
                IList<IWebElement> trCollection = tableElement.FindElements(By.TagName("tr"));
                IList<IWebElement> tdCollection;
                Actions builder = new Actions(_browser);

                foreach (IWebElement element in trCollection)
                {
                    try
                    {
                        tdCollection = element.FindElements(By.TagName("td"));

                        if (tdCollection.Count() > 1)
                        {
                            //Thread.Sleep(1000);
                            vNrDI = tdCollection[0].Text.Trim().Replace("/", "").Replace("-", "");
                            // Pode-se depois Salvar  todos os dados da tabela para usar posteriormente
                            //vCnpjImportador = tdCollection[2].Text.Trim().Replace("/", "").Replace(".", "");
                            //vDtRegistro = tdCollection[3].Text.Trim().Replace("/", "");
                            //vCpfRegistro = tdCollection[4].Text.Trim().Replace("/", "").Replace("-", "").Replace("-", "").Replace(".", "");
                            vListaDi.Add(vNrDI);

                        }

                    }
                    catch (Exception)
                    {
                        // throw;
                    }

                }

                _browser.Navigate().Back();
                Thread.Sleep(1000);

            }
            catch (Exception)
            {

                Thread.Sleep(1000);
                try
                {
                    _browser.SwitchTo().Alert().Accept();

                }
                catch (Exception)
                {
                    // throw;
                }
            }
            return vListaDi;
        }

        private void getTabelaExtrato()
        {
            string dtRegistro = "";

            IWebElement tableElement = _browser.FindElement(By.Id("tabListaDI"));
            Thread.Sleep(2000);
            IList<IWebElement> trCollection = tableElement.FindElements(By.TagName("tr"));
            IList<IWebElement> tdCollection;
            Actions builder = new Actions(_browser);


            foreach (IWebElement element in trCollection)
            {
                try
                {
                    tdCollection = element.FindElements(By.TagName("td"));
                    builder.MoveToElement(tdCollection[0]).Build().Perform();

                    if (tdCollection.Count() > 1)
                    {
                        Thread.Sleep(1000);
                        tdCollection[6].Click();
                        dtRegistro = tdCollection[3].Text.Trim().Replace("/", "");
                        dtRegistro = dtRegistro.Substring(4, 4).ToString() + dtRegistro.Substring(2, 2).ToString() + dtRegistro.Substring(0, 2).ToString();
                        _browser.FindElement(By.Name("gerarXml")).Click();
                        _NmArquivoPadrao = tdCollection[0].Text.Trim().Replace("/", "").Replace("-", "") + ".xml";
                        Thread.Sleep(2000);
                        SalvarArquivosBaixados(_NmArquivoNovo + "_" + _NmArquivoPadrao, this.DiretorioCompleto);
                        Thread.Sleep(1000);
                        tdCollection[6].Click();
                    }

                }
                catch (Exception)
                {
                    // throw;
                }

            }
        }

        public void AbrirBrowser()
        {
            try
            {
                _browser.Navigate().GoToUrl(URL_EXTRATO_LOGIN);
                _browser.FindElement(By.XPath("//img[contains(@src,'certificado')]")).Click();

            }
            catch (Exception e)
            {
                LogarErros(DateTime.Now.ToString() + " Erro no login a tela não estava em foco ou demorou muito para carregar Msg:" + e.Message);

            }


        }

        public void SetarCertificado(int qtdSetas)
        {
            Thread.Sleep(30000);
            for (int i = 0; i <= qtdSetas; i++)
            {
                System.Windows.Forms.SendKeys.SendWait(@"{DOWN}");
            }

            System.Windows.Forms.SendKeys.SendWait(@"{Enter}");
        }
    }
}
