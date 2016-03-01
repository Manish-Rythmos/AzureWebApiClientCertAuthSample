using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApiWithClientCertAuth.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            ProcessClientCertificate pCert = new ProcessClientCertificate();
            NameValueCollection headers = base.Request.Headers;
            var certHeader = headers["X-ARR-ClientCert"];
            if (!String.IsNullOrEmpty(certHeader))
            {
                ViewBag.Title = pCert.GetClientCertificateFromHeader(certHeader);
            }
            {
                ViewBag.Title = "Home Page";
            }
            return View();
        }
    }
}
