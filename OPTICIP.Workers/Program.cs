using System;
using System.Net.Http;

namespace OPTICIP.Workers
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();

            var resultLettreAve =  client.GetAsync("http://localhost:53955/api/ReportingApi/GenererLettreAvertissement");
            //http://api.opticip.com:8090/api/ReportingApi/GenererLettreAvertissement
            if (resultLettreAve.Result.IsSuccessStatusCode)
            {
                Console.WriteLine("Génération des lettres d'avertissement OK!");
            }
            var resultLettreInjonction = client.GetAsync("http://localhost:53955/api/ReportingApi/GenererLettreInjonction");
            //http://api.opticip.com:8090/api/ReportingApi/GenererLettreInjonction
            if (resultLettreAve.Result.IsSuccessStatusCode)
            {
                Console.WriteLine("Génération des lettres d'injonction OK!");
            }

            Console.Read();
        }
    }
}
