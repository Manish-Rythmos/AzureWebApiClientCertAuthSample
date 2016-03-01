using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace AppNetApi_Client
{
    class Program
    {
     
        static void Main()
        {
            CallHttpsApi();
            
        }

        static X509Certificate2 GetCertByThumbprint(string thumbprint)
        {
            X509Store store = null;
            X509Certificate2 cert = null;
            try
            {
                store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
                store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);

                cert = store.Certificates.Find(
                                    X509FindType.FindByThumbprint,
                                    thumbprint,
                                    true
                                    )[0];

            }
            finally
            {
                if (store != null) store.Close();
            }

            return cert;
        }
        static X509Certificate2 GetCertFromFile(string certPath, string certPassword)
        {

            X509Certificate2 cert = new X509Certificate2();
            cert.Import(certPath, certPassword, X509KeyStorageFlags.PersistKeySet);
            
            return cert;
        }
        static void CallHttpsApi()
        {

            Console.WriteLine("Enter full path to certificate file (.pfx)");
            string certFile = Console.ReadLine();
            Console.WriteLine("Enter password for the certificate");
            string certPassword = Console.ReadLine();
            Console.WriteLine();

            string baseWebUrl = "webapiwithclientcertauth.azurewebsites.net";
            string url = string.Format("https://{0}/api/values",baseWebUrl);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.ClientCertificates.Add(GetCertFromFile(certFile,certPassword));
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            using (var readStream = new StreamReader(resp.GetResponseStream()))
            {
                Console.WriteLine(readStream.ReadToEnd());
            }

            Console.ReadLine();

        }
        
    }
}
