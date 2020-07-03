using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShowPhones.Models;
using System.Threading.Tasks;
using System.Net.Http;


namespace MVC.Controllers
{
    public class PhoneController : Controller
    {
        // GET: Phones
        //
        public ActionResult Index()
        {
            IEnumerable<PhoneView> phones = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:64189/api/");
                //HTTP GET
                var responseTask = client.GetAsync("phone");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<PhoneView>>();
                    readTask.Wait();

                    phones = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    phones = Enumerable.Empty<PhoneView>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(phones);
        }
    }

    }
}