using AutoEcac.Services;
using AutoEcac.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace AutoEcac.Servicos
{
    public class ExtratoService : BaseService
    {
        private string _NmArquivoNovo;
        private IWebDriver _browser;

        public ExtratoService(IWebDriver browser)
        {
            _browser = browser;
        }

        public void NavegarURLExtratoDeclaracaoDI()
        {
            _browser.Navigate().GoToUrl(URL_EXTRATO_DECLARACAO_DI);
        }

        public void NavegarURLExtratoDeclaracaoLI()
        {
            _browser.Navigate().GoToUrl(URL_EXTRATO_DECLARACAO_LI);
        }

        public void ConsultarLI(Periodo pPeriodo, TipoConsultaExtrato pTipoConsultaExtrato, List<string> pNrConsulta, DateTime pDtInicial, DateTime pDtFinal)
        {
            base._periodo = pPeriodo;

            foreach (DateTime data in LoopNoPeriodo(pDtInicial, pDtFinal))
            {
                Thread.Sleep(1000);

                foreach (string numero in pNrConsulta)
                {
                    switch ((int)pTipoConsultaExtrato)
                    {
                        case (int)TipoConsultaExtrato.NumeroDeclaracao:
                            _browser.FindElement(By.Name("numeroLI")).Clear();
                            Thread.Sleep(1000);
                            _browser.FindElement(By.Name("numeroLI")).SendKeys(numero);
                            break;
                        case (int)TipoConsultaExtrato.CnpjImportador:
                            _browser.FindElement(By.Name("cnpjImportador")).Clear();
                            _browser.FindElement(By.Name("cnpjImportador")).Click();
                            _browser.FindElement(By.Name("cnpjImportador")).SendKeys(numero);
                            break;
                        case (int)TipoConsultaExtrato.CpfImportador:
                            _browser.FindElement(By.Name("cpfImportador")).Clear();
                            _browser.FindElement(By.Name("cpfImportador")).Click();
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

                        _browser.Navigate().Back();
                        Thread.Sleep(1000);
                        _browser.Navigate().Back();

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

                }
            }
            pNrConsulta.Clear();
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
            return vListaLi;
        }

        public void ConsultarDI(Periodo pPeriodo, TipoConsultaExtrato pTipoConsultaExtrato, List<string> pNrConsulta, DateTime pDtInicial, DateTime pDtFinal)
        {
            base._periodo = pPeriodo;
            foreach (DateTime data in LoopNoPeriodo(pDtInicial, pDtFinal))
            {

                Thread.Sleep(2000);               

                foreach (string numero in pNrConsulta)
                {
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
                        Thread.Sleep(1000);


                        _browser.FindElement(By.Id("btnRegistrarDI")).Click();
                        Thread.Sleep(2000);
                        _NmArquivoPadrao = "Extrato.pdf";
                        _NmArquivoNovo = numero + "_extrato.pdf";
                        SalvarArquivosBaixados(_NmArquivoNovo, this.DiretorioCompleto);
                        Thread.Sleep(1000);


                        if (gerarXmlAcompanhamento(numero))
                        {
                            _browser.FindElement(By.Id("btnComprovanteDI")).Click();
                            Thread.Sleep(1000);
                            _browser.SwitchTo().Window(_browser.WindowHandles.Last());
                            _browser.FindElement(By.XPath("//input[contains(@value,'Emitir')]")).Click();
                            Thread.Sleep(2000);
                            _NmArquivoPadrao = "COMPROVANTE_DI.pdf";
                            _NmArquivoNovo = numero + "_comprovante.pdf";
                            SalvarArquivosBaixados(_NmArquivoNovo, this.DiretorioCompleto);
                            _browser.Close();
                            _browser.SwitchTo().Window(_browser.WindowHandles.First());


                        }
                        _browser.Navigate().Back();
                        Thread.Sleep(1000);
                        _browser.Navigate().Back();

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

                }               
            }
            pNrConsulta.Clear();
        }

        private Boolean gerarXmlAcompanhamento(string pNumerorDi)
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
            _browser.Navigate().GoToUrl(URL_EXTRATO_LOGIN);
            _browser.FindElement(By.XPath("//img[contains(@src,'certificado')]")).Click();
        }
    }
}
