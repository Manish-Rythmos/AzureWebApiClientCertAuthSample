using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Http;

namespace WebApiWithClientCertAuth.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            ProcessClientCertificate pCert = new ProcessClientCertificate();
            System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;
            List<string> lst = new List<string>();
            foreach (var header in headers)
            {
                if (headers.Contains(header.Key))
                {
                    string token = headers.GetValues(header.Key).First();
                    if (!string.IsNullOrEmpty(token))
                    {
                        lst.Add(header.Key + " : " + token);
                    }
                 }

                else
                {
                    lst.Add(header.Key + " : No value ");
                }
            }

            if (headers.Contains("X-ARR-ClientCert"))
            {
                string token = headers.GetValues("X-ARR-ClientCert").First();
                X509Certificate2 cert = pCert.GetClientCertificateFromHeader(token);
                return new string[] { cert.Thumbprint, cert.Issuer };
            }

            return lst.ToArray<string>(); //new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
