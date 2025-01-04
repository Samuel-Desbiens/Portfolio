using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace EFCS
{
    
    public class StringTable
    {
        static StringTable instance = null;
        Dictionary<string, string> fr = new Dictionary<string, string>();
        Dictionary<string, string> en = new Dictionary<string, string>();

        /// <summary>
        /// Constructeur de la string table en singleton.
        /// </summary>
        /// <returns></returns>
        static public StringTable GetInstance()
        {
            if (instance == null)
                instance = new StringTable();
            return instance;
        }
        /// <summary>
        /// Vas lire le fichier et en retirer les information pertinente pour l'affichage de HUD.
        /// </summary>
        /// <param name="text">Le fichier a lire.</param>
        /// <returns>Un code pour annoncer une érreur dans le fichier ou son bon fonctionnement</returns>
        public ErrorCode Parse(string text)
        {
            string[] textSplit;
            string[] textLineSplit = Regex.Split(text,"\r\n" );
            if (textLineSplit.Length == 3)
            {
                string[] splitID1 = textLineSplit[0].Split('=');
                string[] splitID2 = textLineSplit[1].Split('=');
                string[] splitID3 = textLineSplit[2].Split('=');

                if(splitID1.Length == 2 && splitID2.Length == 2 && splitID3.Length == 2 && splitID1[0] !=""
                    && splitID1[1] != "" && splitID2[0] != "" && splitID2[1] != "" && splitID3[0] !="" && splitID3[1] !="")
                {
                    string[] splitValue1 = splitID1[1].Split('-');
                    string[] splitValue2 = splitID2[1].Split('-');
                    string[] splitValue3 = splitID3[1].Split('-');

                    if (splitValue1.Length == 2 && splitValue2.Length == 2 && splitValue3.Length == 2)
                    {
                        textSplit = new string[9] { splitID1[0], splitValue1[0], splitValue1[1], splitID2[0], splitValue2[0], splitValue2[1],splitID3[0],splitValue3[0],splitValue3[1] };
                        fr.Add(textSplit[0], textSplit[1]);
                        fr.Add(textSplit[3], textSplit[4]);
                        fr.Add(textSplit[6], textSplit[7]);
                        en.Add(textSplit[0], textSplit[2]);
                        en.Add(textSplit[3], textSplit[5]);
                        en.Add(textSplit[6], textSplit[8]);
                    }
                    else
                    {
                        return ErrorCode.MISSING_FIELD;

                    }
                }
                else
                {
                    return ErrorCode.MISSING_FIELD;
                }
            }
            else
            {
                return ErrorCode.BAD_FILE_FORMAT;
            }
            return ErrorCode.OK;
        }
        /// <summary>
        /// Donne l'information des dictionnaires au autre class.
        /// </summary>
        /// <param name="lang">La langue choisie par l'utilisateur.</param>
        /// <param name="key">L'ID de la clé du dictionnaire pour aller chercher la bonne information.</param>
        /// <returns>Un string contenant l'information rechercher des dictionnaire.</returns>
        public string GetValue(Language lang,string key)
        {
            if(lang == Language.English)
            {
                return en[key];
            }
            else if(lang == Language.French)
            {
                return fr[key];
            }
            else
            {
                return "Error 404 Language not found";
            }
        }
        /// <summary>
        /// Méthode pour les tests unitaires pour éffacer l'instance du singleton.
        /// </summary>
        public void Clean()
        {
            instance = null;
        }

            
    }

}




   