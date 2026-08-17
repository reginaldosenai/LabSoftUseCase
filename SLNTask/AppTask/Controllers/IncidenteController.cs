using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppTask.Controllers
{
    public class IncidenteController : Controller
    {
        // GET: IncidenteController
        public ActionResult Index()
        {
            return View();
        }

        // GET: IncidenteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IncidenteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncidenteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IncidenteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IncidenteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IncidenteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IncidenteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
