using AutoEcac.Services;
using AutoEcac.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AutoEcac.Servicos
{
    public class DARFService : BaseService
    {
        private IWebDriver _browser;

        public DARFService(IWebDriver browser)
        {
            _browser = browser;
        }

        public void ConsultarDARF(Periodo periodo, string CdReceita, DateTime dInicial, DateTime dFinal)
        {
            base._periodo = periodo;

            _browser.FindElement(By.Id("txtPesquisar")).SendKeys("consulta comprovante de pagamento - darf");
            _browser.FindElement(By.Id("txtPesquisar")).Submit();
            _browser.FindElement(By.PartialLinkText("DARF")).Click();

            foreach (DateTime data in LoopNoPeriodo(dInicial, dFinal))
            {
                Thread.Sleep(2000);
                try
                {
                    _browser.SwitchTo().Frame(("frmApp"));
                }
                catch (Exception)
                {
                    // throw;
                }
                _browser.FindElement(By.Id("campoDataArrecadacaoInicial")).Clear();
                _browser.FindElement(By.Id("campoDataArrecadacaoFinal")).Clear();
                _browser.FindElement(By.Id("campoNumeroDocumento")).Clear();
                _browser.FindElement(By.Id("campoCodReceita")).Clear();

                SelectElement dropdown = new SelectElement(_browser.FindElement(By.Id("campoTipoDocumento")));
                dropdown.Options[1].Click();
                Thread.Sleep(1000);

                _browser.FindElement(By.Id("campoDataArrecadacaoInicial")).SendKeys(_dtInicial.ToShortDateString());
                Thread.Sleep(1000);

                _browser.FindElement(By.Id("campoDataArrecadacaoFinal")).SendKeys(_dtFinal.ToShortDateString());
                Thread.Sleep(1000);
                _browser.FindElement(By.Id("campoCodReceita")).SendKeys(CdReceita);
                Thread.Sleep(1000);

                _browser.FindElement(By.Name("botaoConsultar")).Click();

                try
                {
                    Thread.Sleep(2000);

                    try
                    {
                        // fBrowser.FindElement(By.Name("CheckBoxTodos")).Click();

                    }
                    catch (Exception)
                    {

                        // se não existe o botão fazer loop no checkbox;
                    }

                    //Thread.Sleep(3000);

                    try
                    {
                        _browser.SwitchTo().Frame(("frmApp"));
                    }
                    catch (Exception)
                    {

                        // throw;
                    }
                    IWebElement tableElement = _browser.FindElement(By.Id("listagemDARF"));
                    Thread.Sleep(2000);
                    IList<IWebElement> trCollection = tableElement.FindElements(By.TagName("tr"));
                    IList<IWebElement> tdCollection;
                    Actions builder = new Actions(_browser);
                    string dtArreecadacao = "";

                    foreach (IWebElement element in trCollection)
                    {
                        try
                        {
                            // fBrowser.FindElement(By.Id("FIELDSET1"))).Click().Build().Perform();  
                            tdCollection = element.FindElements(By.TagName("td"));
                            builder.MoveToElement(tdCollection[0]).Build().Perform();
                            tdCollection[0].Click();
                            tdCollection[1].Click();
                            if (tdCollection.Count() > 0)
                            {
                                Thread.Sleep(3000);
                                dtArreecadacao = tdCollection[5].Text.Trim().Replace("/", "");
                                dtArreecadacao = dtArreecadacao.Substring(4, 4).ToString() + dtArreecadacao.Substring(2, 2).ToString() + dtArreecadacao.Substring(0, 2).ToString();

                                SalvarArquivosBaixados(dtArreecadacao + "_" + tdCollection[2].Text.Trim() + ".pdf",
                                                       this.DiretorioCompleto + @"\" + _dtInicial.ToShortDateString().Replace("/", "") + "_" + _dtFinal.ToShortDateString().Replace("/", ""));

                                Thread.Sleep(2000);
                            }
                        }
                        catch (Exception)
                        {
                            // throw;
                        }
                    }

                    try
                    {
                        _browser.Navigate().Back();
                        _browser.SwitchTo().Frame(("frmApp"));
                        _browser.FindElement(By.Id("campoDataArrecadacaoInicial")).Click();
                    }
                    catch (Exception)
                    {

                        _browser.Navigate().Back();
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
            }
        }

        public void AbrirBrowser()
        {
            _browser.Navigate().GoToUrl(URL_DARF_LOGIN);
            _browser.FindElement(By.Id("caixa-login-certificado")).Click();
        }
    }
}
