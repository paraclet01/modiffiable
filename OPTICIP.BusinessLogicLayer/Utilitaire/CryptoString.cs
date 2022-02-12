using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace OPTICIP.BusinessLogicLayer.Utilitaire
{
    public class CryptoString
    {
        private SymmetricAlgorithm CryptoAlgorithme { get; set; }

        public CryptoString(byte[] aCle, byte[] aVecteurInitialisation)
        {
            this.CryptoAlgorithme = new RijndaelManaged();
            this.CryptoAlgorithme.Key = aCle;
            this.CryptoAlgorithme.IV = aVecteurInitialisation;
            this.CryptoAlgorithme.Mode = CipherMode.CBC;
            this.CryptoAlgorithme.Padding = PaddingMode.Zeros;
        }

        public string Crypter(string aTexte)
        {
            // Variables locales.
            byte[] oTab;
            ICryptoTransform oEncryptor;
            string sResult;

            // Initialisation.
            oTab = Encoding.UTF8.GetBytes(aTexte);
            oEncryptor = this.CryptoAlgorithme.CreateEncryptor();

            // Cryptage.
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, oEncryptor, CryptoStreamMode.Write))
            {
                cs.Write(oTab, 0, oTab.Length);
                cs.FlushFinalBlock();
                oTab = ms.ToArray();
                sResult = Convert.ToBase64String(oTab);
            }

            // Retour.
            return sResult;
        }

        public string Decrypter(string aTexte)
        {
            

            try
            {
                // Variables locales.
                byte[] oTab;
                ICryptoTransform oDecryptor;
                string sResult;
                int iNombreBytes;

                // Initialisation.
                oTab = Convert.FromBase64String(aTexte);
                oDecryptor = this.CryptoAlgorithme.CreateDecryptor();

                // Décryptage.
                using (MemoryStream ms = new MemoryStream(oTab))
                using (CryptoStream cs = new CryptoStream(ms, oDecryptor, CryptoStreamMode.Read))
                {
                    oTab = new byte[oTab.Length];
                    iNombreBytes = cs.Read(oTab, 0, oTab.Length);
                    sResult = Encoding.UTF8.GetString(oTab, 0, iNombreBytes);
                }

                // Retour.
                return sResult;
            }
            catch (Exception ex)
            {

                return  ex.Message;
            }
        }

        public static void GetCleEtVecteurInitialisation(string aMotDePasse, out Byte[] aCle, out Byte[] aVecteurInitialisation)
        {
            // Variables locales.
            Rfc2898DeriveBytes rfcDb;

            // Initialisation.
            rfcDb = new Rfc2898DeriveBytes(aMotDePasse, 16);

            // Génération.
            aCle = rfcDb.GetBytes(16);
            aVecteurInitialisation = rfcDb.GetBytes(16);
        }

    }
}
