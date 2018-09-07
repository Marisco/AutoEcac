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

    public class CustomXmlTextWriter : XmlTextWriter
    {
        public CustomXmlTextWriter(string filename)
            : base(filename, Encoding.UTF8)
        {

        }

        public override void WriteStartDocument()
        {
            
            WriteRaw("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        }

        public override void WriteEndDocument()
        {
        }
    }
}

