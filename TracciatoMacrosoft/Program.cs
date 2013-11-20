using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TracciatoMacrosoft
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = "C:\\pippo";
            foreach (string sFile in Directory.GetFiles(path))
            {
                string result;
                string contenutoDelFileDiTesto;
                StreamReader sr = null;
                sr = new StreamReader(sFile);
                contenutoDelFileDiTesto = sr.ReadToEnd();
                sr.Close();

                result = InserisciTracciatoFTP(contenutoDelFileDiTesto);
                File.WriteAllText(sFile, result);
            }
        }


        public static string InserisciTracciatoFTP(string strTesto)
        {
            string rn = Environment.NewLine;
            char[] RN = rn.ToCharArray();
            string[] stringhe = strTesto.Split(RN);

            int NLung = stringhe[0].Length;

            string strEsito = "T" + DateTime.Now.ToString("yyyyMMddHHMMss");
            if ((NLung - strEsito.Length + 1) >= 0)
            {
                strEsito += new string(' ', NLung - strEsito.Length + 1) + "X" + Environment.NewLine;
            }
            else
            {
                strEsito += "X" + Environment.NewLine;
            }

            NLung = strEsito.Length - 2;

            int NLinee = 2; //Sono già incluse l'header e il footer
            int NLungAtt = 0;

            for (int i = 0; i < stringhe.Length; i++)
            {
                if (stringhe[i] != "")
                {
                    NLungAtt = ("D" + stringhe[i]).Length;

                    //strEsito += "D" + stringhe[i];
                    //strEsito += new string(' ', NLungAtt - NLung) + "X" + Environment.NewLine;
                    strEsito += "D" + stringhe[i] + "X" + Environment.NewLine;
                    NLinee++;
                }
            }

            NLungAtt = ("Z" + NLinee.ToString("000000000000")).Length;
            strEsito += "Z" + NLinee.ToString("000000000000");
            strEsito += new string(' ', NLung - NLungAtt - 1) + "X" + Environment.NewLine;

            return strEsito;

        }
    }
}
