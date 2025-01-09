using ConsumeAPI_Front_End.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ConsumeAPI_Front_End.Controllers
{
    public class StudentController : Controller
    {
        private static readonly string Url = "https://localhost:7070/api/Student";
        private Uri BaseUrl = new Uri(Url);
        private readonly HttpClient client;

        public StudentController()
        {
            client = new HttpClient();
            client.BaseAddress = BaseUrl;
        }



        // GET: StudentController
        public ActionResult Index()
        {
            HttpResponseMessage response = client.GetAsync(BaseUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                List<Student> student = JsonConvert.DeserializeObject<List<Student>>(json);
                return View(student);


            }

            return View(null);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            string json = JsonConvert.SerializeObject(student);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage respone = client.PostAsync(BaseUrl, content).Result;
            if (respone.IsSuccessStatusCode)
            {
                TempData["message"] = "Student added successfully";
                TempData["messageType"] = "success";
                return RedirectToAction(nameof(Index));
            }
            TempData["message"] = "Student not added successfully";
            TempData["messageType"] = "error";
            return RedirectToAction(nameof(Index));
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {  HttpResponseMessage res=client.GetAsync($"{BaseUrl}/{id}").Result;
        if (res.IsSuccessStatusCode)
            {
              string json= res.Content.ReadAsStringAsync().Result;
              Student student= JsonConvert.DeserializeObject<Student>(json);
                return View(student);
            }

            TempData["message"] = "Student not found";
            TempData["messageType"] = "error";
            return RedirectToAction(nameof(Index));
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            string json = JsonConvert.SerializeObject(student);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage res = client.PutAsync($"{BaseUrl}/{student.Id}", content).Result;
            if(res.IsSuccessStatusCode)
            {
                TempData["message"] = "Student Record Updated";
                TempData["messageType"] = "Success";
                return RedirectToAction(nameof(Index));
            }
            TempData["message"] = "Student not found";
            TempData["messageType"] = "error";
            return RedirectToAction(nameof(Index));
        }



        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = client.DeleteAsync($"{BaseUrl}/{id}").Result;
            {
                if (response.IsSuccessStatusCode)
                {
                    TempData["message"] = "Student Deleted successfully";
                    TempData["messageType"] = "success";
                    return RedirectToAction(nameof(Index));
                }

                TempData["message"] = "Student not Delete";
                TempData["messageType"] = "error";
                return RedirectToAction(nameof(Index));

            }
        }
    }
}
