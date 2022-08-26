using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace API.Infra
{
    public static class Helper
    {
        public static string GenerateSlug(string str)
        {
            str = RemoveAccents(str).ToLower();
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = Regex.Replace(str, @"\s+", " ").Trim();
            str = Regex.Replace(str, @"\s", "-");

            return str;
        }

        public static string RemoveAccents(string texto)
        {
            StringBuilder resultado = new StringBuilder();
            var arrayDoTexto = texto.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letra in arrayDoTexto)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letra) != UnicodeCategory.NonSpacingMark)
                    resultado.Append(letra);
            }

            return resultado.ToString();
        }
    }
}
