using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;

namespace AutoEcac.Utils
{
    #region Enums

    public enum TipoServico
    {
        NENHUM = 0,
        DARF = 1,
        EXTRATO = 2
    };

    public enum TipoExtrato
    {
        DI = 0,
        LI = 1
    }

    public enum TipoConsultaExtrato
    {
        NumeroDeclaracao = 0,
        CnpjImportador = 1,
        CpfImportador = 2,
        CpfUsuario = 3
    }

    public enum Periodo
    {
        NENHUM = 0,
        DIARIO = 1,
        SEMANAL = 2,
        MENSAL = 3,
        TRIMESTRAL = 4,
        SEMESTRAL = 5,
        ANUAL = 6
    }

    public enum InSituacao
    {
            TODAS = 0,                                
			PARA_ANALISE = 1,                          
			EM_ANALISE =  2,                           
			EM_EXIGENCIA = 3,                          
			INDEFERIDA = 4,                            
			DEFERIDA= 5,                               
			DEFERIDA_JUDICIALMENTE = 6,                
			DEFERIDA_VINCULADA_DI = 7,               
			DEFERIDA_JUDICIALMENTE_VINCULADA_DI = 8, 
			DEFERIDA_RESERVADA = 9,                    
			DEFERIDA_JUDICIALMENTE_RESERVADA = 10,     
			EMBARQUE_AUTORIZADO = 15,                  
			VENCIDA = 16,                              
			DESEMBARAÇADA = 17,
			CANCELADA = 18
    }

    #endregion

    public class Helper
    {
        public static string NormalizarTexto(string pTexto)
        {

            var chars = from c in pTexto.Normalize(NormalizationForm.FormD).ToCharArray()
                        let uc = CharUnicodeInfo.GetUnicodeCategory(c)
                        where uc != UnicodeCategory.NonSpacingMark
                        select c;

            return new string(chars.ToArray()).Normalize(NormalizationForm.FormC);

        }
    }
    
}

