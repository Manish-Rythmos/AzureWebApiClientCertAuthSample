using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace WebApiWithClientCertAuth
{
    public class ProcessClientCertificate
    {
       


        public X509Certificate2 GetClientCertificateFromHeader(string  certHeader)
        {
            
            X509Certificate2 certificate = null;
            if (!String.IsNullOrEmpty(certHeader))
                {

                    byte[] clientCertBytes = Convert.FromBase64String(certHeader);
                    certificate = new X509Certificate2(clientCertBytes);
                  
                }

                return certificate;
        }

        }
    }
 