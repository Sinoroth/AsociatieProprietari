using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AsociatieProprietari.Models;
using Microsoft.AspNet.Identity;

namespace AsociatieProprietari.Controllers
{
    [Authorize(Roles = "User")]
    public class FlatController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Flat
        public ActionResult Index()
        {
            return View(db.FlatModels.ToList());
        }

        // GET: Flat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlatModels flatModels = db.FlatModels.Find(id);
            if (flatModels == null)
            {
                return HttpNotFound();
            }
            return View(flatModels);
        }

        // GET: Flat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Flat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Street,Address,ApartmentNumber,FullAddress,NumberOfPersons,NumberOfRooms")] FlatModels flatModels)
        {
            if (ModelState.IsValid)
            {
                db.FlatModels.Add(flatModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(flatModels);
        }

        // GET: Flat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlatModels flatModels = db.FlatModels.Find(id);
            if (flatModels == null)
            {
                return HttpNotFound();
            }
            return View(flatModels);
        }

        // POST: Flat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Street,Address,ApartmentNumber,FullAddress,NumberOfPersons,NumberOfRooms")] FlatModels flatModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flatModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flatModels);
        }

        // GET: Flat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlatModels flatModels = db.FlatModels.Find(id);
            if (flatModels == null)
            {
                return HttpNotFound();
            }
            return View(flatModels);
        }

        // POST: Flat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FlatModels flatModels = db.FlatModels.Find(id);
            db.FlatModels.Remove(flatModels);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: InvoiceModels/Create
        public ActionResult AddInvoice(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }

        // POST: InvoiceModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddInvoice([Bind(Include = "Id,FlatId,Value,InvoiceDate")] InvoiceModels invoiceModels)
        {
            if (ModelState.IsValid)
            {
                invoiceModels.Paid = false;
                db.InvoiceModels.Add(invoiceModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(invoiceModels);
        }

        // GET: PaymentsModels/Create
        public ActionResult AddPayment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }

        // POST: PaymentsModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPayment([Bind(Include = "Id,FlatId,Value")] PaymentsModels paymentsModels)
        {
            if (ModelState.IsValid)
            {
                paymentsModels.PaymentDate = DateTime.Now;
                db.PaymentsModels.Add(paymentsModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paymentsModels);
        }

        public ActionResult NewMonthInvoices()
        {

            var Flats = db.FlatModels.ToList();

            float totalSuppliers = db.ContratModels.Sum(s => s.Value);

            float totalSalaries = db.EmployeeModels.Sum(s => s.Salary);

            float numberOfFlats = db.FlatModels.Count();

            float rate = 0.5F;

            foreach (var item in Flats)
            {
                float InvoiceAmount = ((totalSuppliers + totalSalaries) / numberOfFlats) * item.NumberOfRooms * (item.NumberOfPersons * rate);

                var newInvoice = new InvoiceModels();

                newInvoice.FlatId = item.Id;
                newInvoice.InvoiceDate = DateTime.Now;
                newInvoice.Value = InvoiceAmount;
                newInvoice.Paid = false;

                db.InvoiceModels.Add(newInvoice);
                db.SaveChanges();
            }

            string redirectUrl = "/Flat/Index";

            return Redirect(redirectUrl);
        }



        public ActionResult Situation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var InvoicePerFlat = db.InvoiceModels.Where(s => s.FlatId == id).ToList();
            var PaymentsPerFlat = db.PaymentsModels.Where(s => s.FlatId == id).ToList();

            float totalInvoices = 0;
            float totalPayments = 0;

            foreach (var item in InvoicePerFlat)
            {
                totalInvoices = totalInvoices + item.Value;
            }

            foreach (var item in PaymentsPerFlat)
            {
                totalPayments = totalPayments + item.Value;
            }

            var model = new SituationViewModels()
            {
                Invoices = InvoicePerFlat,

                Payments = PaymentsPerFlat,

                TotalDebt = totalInvoices,

                TotalPayments = totalPayments,

                TotalRemaining = totalInvoices - totalPayments

            };

            return View(model);
        }

        public ActionResult WaterConsumption(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(db.WaterConsumptionModels.Where(s => s.FlatId ==  id));
        }

        // GET: WaterConsumptionModels/Create
        public ActionResult AddWaterConsumption(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }

        // POST: WaterConsumptionModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddWaterConsumption([Bind(Include = "Id,FlatId,HotCubeMeters,ColdCubeMeters,Month")] WaterConsumptionModels waterConsumptionModels)
        {
            if (ModelState.IsValid)
            {
                db.WaterConsumptionModels.Add(waterConsumptionModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(waterConsumptionModels);
        }

        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // !!! SCHIMBA PATHUL LA EXECUTABILUL DE PYTHON !!!
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        private string run_cmd(string cmd, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo(); 
            start.FileName = @"C:\Users\PC\AppData\Local\Programs\Python\Python36-32\python.exe";  // SCHIMBA PATHUL LA EXECUTABILUL DE PYTHON
            start.CreateNoWindow = true;
            start.Arguments = string.Format("{0} {1}", cmd, args);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    //Console.Write(result);
                    process.WaitForExit();
                    return result;
                }
            }
        }

        public FileResult GenerateSummaryRepor(string id)
        {
            var filePath = Server.MapPath("~/ReportingFolder/") + "\\Summary_Rerpot.xlsx";

            if (System.IO.File.Exists(filePath))
            {
             //   System.IO.File.Delete(filePath);
            }

            string fullScriptPath = Server.MapPath("~/ReportsPrograms/") + "\\summary.py";

            var textResult = run_cmd(fullScriptPath, id);

            byte[] fileBytes = System.IO.File.ReadAllBytes(@filePath);
            string fileName = "Summary_Rerpot.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
