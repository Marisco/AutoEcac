using AutoEcac.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace AutoEcac.Services
{
    public abstract class BaseService
    {
        //CNPJ
        public string _CNPJ;

        public TipoServico _tipoServicoSelecionado;
        public TipoExtrato _tipoExtratoSelecionado { get; set; }

        //Configuração de Arquivos
        protected string _NmArquivoPadrao;

        //Parâmetros de Pesquisa
        protected Periodo _periodo;
        protected DateTime _dtInicial;
        protected DateTime _dtFinal;

        protected IEnumerable<DateTime> LoopNoPeriodo(DateTime pDtInicial, DateTime pDtFinal)
        {
            int PeriodoSelecionado = (int)_periodo;

            switch (PeriodoSelecionado)
            {
                case (int)Periodo.DIARIO:
                    for (var dia = pDtInicial.Date; dia.Date <= pDtFinal.Date; dia = dia.AddDays(1))
                    {
                        _dtInicial = dia;
                        _dtFinal = dia;
                        yield return dia;
                    }
                    break;
                case (int)Periodo.SEMANAL:
                    for (var semana = pDtInicial.Date; semana.Date <= pDtFinal.Date; semana = semana.AddDays(1))
                    {
                        if (semana.DayOfWeek == DayOfWeek.Sunday)
                        {
                            _dtInicial = semana.AddDays(-7);
                            _dtFinal = semana.AddDays(-1);
                            yield return semana;
                        }

                    }
                    break;
                case (int)Periodo.MENSAL:
                    for (var mes = pDtInicial.Date; mes.Date <= pDtFinal.Date; mes = mes.AddDays(1))
                    {
                        if (mes.Day == 1)
                        {
                            _dtInicial = mes;
                            _dtFinal = mes.AddMonths(1).AddDays(-1);
                            yield return mes;
                        }
                    }
                    break;
                case (int)Periodo.TRIMESTRAL:
                    for (var trimestre = pDtInicial.Date; trimestre.Date <= pDtFinal.Date; trimestre = trimestre.AddDays(1))
                    {
                        if ((trimestre.Month % 3 == 0))
                        {
                            if (trimestre.Day == 1)
                            {
                                _dtInicial = trimestre.AddMonths(-2);
                                _dtFinal = trimestre.AddMonths(1).AddDays(-1);
                                yield return trimestre;
                            }
                        }
                    }
                    break;
                case (int)Periodo.SEMESTRAL:
                    for (var semestre = pDtInicial.Date; semestre.Date <= pDtFinal.Date; semestre = semestre.AddDays(1))
                        if ((semestre.Month % 6 == 0))
                        {
                            if (semestre.Day == 1)
                            {
                                _dtInicial = semestre.AddMonths(-5);
                                _dtFinal = semestre.AddMonths(1).AddDays(-1);
                                yield return semestre;
                            }
                        }
                    break;
                case (int)Periodo.ANUAL:
                    for (var ano = pDtInicial.Date; ano.Date <= pDtFinal.Date; ano = ano.AddDays(1))
                        if ((ano.Month == 12))
                        {
                            if (ano.Day == 1)
                            {
                                _dtInicial = ano.AddMonths(-11);
                                _dtFinal = ano.AddMonths(1).AddDays(-1);
                                yield return ano;
                            }
                        }
                    break;
            }
        }

        protected void SalvarArquivosBaixados(string pNmArquivoNovo, string pDiretorio)
        {
            if(_tipoServicoSelecionado == TipoServico.DARF)
            {
                _NmArquivoPadrao = "PagtoWebImpREST.pdf";
            }

            if (!System.IO.Directory.Exists(pDiretorio))
            {
                System.IO.Directory.CreateDirectory(pDiretorio);
            }
            if (System.IO.File.Exists(pDiretorio + @"\" + pNmArquivoNovo))
            {
                System.IO.File.Delete(pDiretorio + @"\" + pNmArquivoNovo);
            }
            if (!System.IO.File.Exists(pDiretorio + @"\" + "LogErro.txt"))
            {
                System.IO.File.Create(pDiretorio + @"\" + "LogErro.txt");
            }

            var vStopWach = new Stopwatch();
            vStopWach.Start();

            while ((System.IO.Directory.GetFiles(this.PathDownload).Where(s => s.Contains(_NmArquivoPadrao.Substring(0, _NmArquivoPadrao.Length - 8))).Count() == 0) && (vStopWach.Elapsed.TotalSeconds < 10))
            {
                // enquanto não baixar o arquivo não faz nada;
            }

            if (vStopWach.Elapsed.TotalSeconds >= 10)
            {
                File.AppendAllLines(pDiretorio + @"\" + "LogErro.txt", new[] { "Erro: o arquivo " + _NmArquivoPadrao + " não foi encontrado na pasta " + this.PathDownload });
            }

            else
            {
                var vArquivos = System.IO.Directory.GetFiles(this.PathDownload).Where(s => s.Contains(_NmArquivoPadrao.Substring(0, _NmArquivoPadrao.Length - 8)));

                foreach (var arq in vArquivos)
                {
                    _NmArquivoPadrao = arq.ToString();
                }

                // fNmArquivoPadrao = vArquivo.FirstOrDefault();
                Thread.Sleep(2000);
                string destFile = System.IO.Path.Combine(pDiretorio, pNmArquivoNovo);
                System.IO.File.Move(_NmArquivoPadrao, destFile);

            }
        }

        public string PathDownload
        {
            get
            {
                string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                return Path.Combine(pathUser, "Downloads");
            }
        }

        public string DiretorioCompleto
        {
            get
            {
                if (_tipoServicoSelecionado == TipoServico.DARF)
                    return string.Format(@"{0}\{1}\{2}", PATH_ROOT, _CNPJ, "DARF");
                else
                    return string.Format(@"{0}\{1}\{2}\{3}", PATH_ROOT, _CNPJ, "EXTRATO", _tipoExtratoSelecionado.ToString());
            }
        }

        public string NomePasta
        {
            get { return _tipoServicoSelecionado == TipoServico.DARF ? "DARF" : "EXTRATO"; }
        }

        #region APP_Configs

        protected string PATH_ROOT
        {
            get { return ConfigurationManager.AppSettings["PATH_ROOT"]; }
        }
        protected string URL_DARF_LOGIN
        {
            get { return ConfigurationManager.AppSettings["URL_DARF_LOGIN"]; }
        }
        protected string URL_EXTRATO_LOGIN
        {
            get { return ConfigurationManager.AppSettings["URL_EXTRATO_LOGIN"]; }
        }
        protected string URL_EXTRATO_DECLARACAO_DI
        {
            get { return ConfigurationManager.AppSettings["URL_EXTRATO_DECLARACAO_DI"]; }
        }
        protected string URL_EXTRATO_DECLARACAO_LI
        {
            get { return ConfigurationManager.AppSettings["URL_EXTRATO_DECLARACAO_LI"]; }
        }

        #endregion
    }
}
