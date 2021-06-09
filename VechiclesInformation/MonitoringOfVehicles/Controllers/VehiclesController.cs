using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MonitoringOfVehicles.Models;

namespace MonitoringOfVehicles.Controllers
{

    public class VehiclesController : Controller
    {

        // GET: Vehicles
        public ActionResult VehiclesList()
        {
            IEnumerable<VehicleDetails> vehicles = null;
            using (var client = new HttpClient())
            {
                string apiUrl= System.Configuration.ConfigurationManager.AppSettings["ApiUrl"];
                //client.BaseAddress = new Uri("https://localhost:44370/api/");
                client.BaseAddress = new Uri(apiUrl);
                string userName = System.Configuration.ConfigurationManager.AppSettings["UserName"];
                string password = System.Configuration.ConfigurationManager.AppSettings["Password"];
                string plainCredential = userName + ":" + password;
                // Encode with base64
                var plainTextBytes = Encoding.UTF8.GetBytes(plainCredential);
                string encodedCredential = Convert.ToBase64String(plainTextBytes);
                // Create authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedCredential);
                //HTTP GET
                var responseTask = client.GetAsync("Vehicle");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<VehicleDetails>>();
                    readTask.Wait();

                    vehicles = readTask.Result;
                }
                else //web api sent error response 
                {
                    vehicles = Enumerable.Empty<VehicleDetails>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(1);
            var timer = new System.Threading.Timer((e) =>
            {
                SyncStatus();
            }, null, startTimeSpan, periodTimeSpan);
            return View(vehicles);
        }

        private void SyncStatus()
        {
            Task.Factory.StartNew(() =>
            {
                using (var client = new HttpClient())
                {
                    string apiUrl = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"];
                    //client.BaseAddress = new Uri("https://localhost:44370/api/");
                    client.BaseAddress = new Uri(apiUrl);
                    string userName = System.Configuration.ConfigurationManager.AppSettings["UserName"];
                    string password = System.Configuration.ConfigurationManager.AppSettings["Password"];
                    string plainCredential = userName + ":" + password;
                    // Encode with base64
                    var plainTextBytes = Encoding.UTF8.GetBytes(plainCredential);
                    string encodedCredential = Convert.ToBase64String(plainTextBytes);
                    // Create authorization header
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedCredential);                    
                    var responseTask = client.GetAsync($"Vehicle/sync");
                    responseTask.Wait();
                }
            });
        }

        //Get Vehicles by CustomerName
        public ActionResult GetVehiclesByCustomerName(string customerName)
        {
            if (!String.IsNullOrEmpty(customerName))
            {
                VehicleDetails VehiclesOwnedByCustomer = null;
                using (var client = new HttpClient())
                {
                    string apiUrl = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"];
                    //client.BaseAddress = new Uri("https://localhost:44370/api/");
                    client.BaseAddress = new Uri(apiUrl);

                    string userName = System.Configuration.ConfigurationManager.AppSettings["UserName"];
                    string password = System.Configuration.ConfigurationManager.AppSettings["Password"];
                    string plainCredential = userName + ":" + password;
                    // Encode with base64
                    var plainTextBytes = Encoding.UTF8.GetBytes(plainCredential);
                    string encodedCredential = Convert.ToBase64String(plainTextBytes);
                    // Create authorization header
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedCredential);
                    //HTTP GET
                    var responseTask = client.GetAsync($"Vehicle/name/{customerName}");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<VehicleDetails>();
                        readTask.Wait();

                        VehiclesOwnedByCustomer = readTask.Result;
                    }
                }
                return View(VehiclesOwnedByCustomer);
            }
            else
            {
                return View();
            }
        }

        //Get Vehicles by status
        public ActionResult GetVehiclesByStatus(bool vehicleStatus = false)
        {
            IEnumerable<VehicleDetails> VehiclesByStatus = null;
            using (var client = new HttpClient())
            {
                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"];
                //client.BaseAddress = new Uri("https://localhost:44370/api/");
                client.BaseAddress = new Uri(apiUrl);
                string userName = System.Configuration.ConfigurationManager.AppSettings["UserName"];
                string password = System.Configuration.ConfigurationManager.AppSettings["Password"];
                string plainCredential = userName + ":" + password;
                // Encode with base64
                var plainTextBytes = Encoding.UTF8.GetBytes(plainCredential);
                string encodedCredential = Convert.ToBase64String(plainTextBytes);
                // Create authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedCredential);
                //HTTP GET
                var responseTask = client.GetAsync($"Vehicle/status/{vehicleStatus}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<VehicleDetails>>();
                    readTask.Wait();

                    VehiclesByStatus = readTask.Result;
                }
            }
            return View(VehiclesByStatus);

        }
    }
}