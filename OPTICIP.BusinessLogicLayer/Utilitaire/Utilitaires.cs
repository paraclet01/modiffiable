using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace OPTICIP.BusinessLogicLayer.Utilitaire
{
    public static class Utilitaires
    {


        public static string Crypter(this string MotPasse)
        {
            //Byte[] oCle, oVecteurInitialisation;
            //CryptoString oCryptoString;

            //// Initialisation.
            //CryptoString.GetCleEtVecteurInitialisation("OPTICIP.API@PARACLET", out oCle, out oVecteurInitialisation);
            //oCryptoString = new CryptoString(oCle, oVecteurInitialisation);

            //// Cryptage.
            //return  oCryptoString.Crypter(MotPasse);


            return Convert.ToBase64String(Encoding.Default.GetBytes(MotPasse));
        }

        public static string Decrypter(this string MotPasse)
        {
            //Byte[] oCle, oVecteurInitialisation;
            //CryptoString oCryptoString;

            //// Initialisation.
            //CryptoString.GetCleEtVecteurInitialisation("OPTICIP.API@PARACLET", out oCle, out oVecteurInitialisation);
            //oCryptoString = new CryptoString(oCle, oVecteurInitialisation);

            //// Décryptage.
            //return  oCryptoString.Decrypter(MotPasse);


            return Encoding.Default.GetString(Convert.FromBase64String(MotPasse));
        }


        public static T CastObject<T, U>(U obj)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
        }
    }
}