using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRS.Models;

namespace PRS.Controllers
{
    public class PrisonerTransactionController : PRSBaseController
    {
        private void PopulateDropDowns(PrisonerTransaction transaction)
        {
            ViewBag.TransactionType = new SelectList(Enum.GetValues(typeof(TransactionType)), transaction.TransactionType);
        }

        //
        // GET: /PrisonerTransaction/

        public ActionResult Index()
        {
            var prisonerproperties = db.PrisonerTransactions.Include(p => p.Prisoner).Where(o => o.PrisonerId == PrisonerId);
            return View(prisonerproperties.ToList());
        }

        //
        // GET: /PrisonerTransaction/Details/5

        public ActionResult Details(int id = 0)
        {
            PrisonerTransaction transaction = db.PrisonerTransactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //
        // GET: /PrisonerTransaction/Create

        public ActionResult Create()
        {
            var transaction = new PrisonerTransaction();
            PopulateDropDowns(transaction);
            return View(transaction);
        }

        //
        // POST: /PrisonerTransaction/Create

        [HttpPost]
        public ActionResult Create(PrisonerTransaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.PrisonerTransactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ValidationError();
            PopulateDropDowns(transaction);
            return View(transaction);
        }

        //
        // GET: /PrisonerTransaction/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PrisonerTransaction transaction = db.PrisonerTransactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            PopulateDropDowns(transaction);
            return View(transaction);
        }

        //
        // POST: /PrisonerTransaction/Edit/5

        [HttpPost]
        public ActionResult Edit(PrisonerTransaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ValidationError();
            PopulateDropDowns(transaction);
            return View(transaction);
        }

        //
        // GET: /PrisonerTransaction/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PrisonerTransaction transaction = db.PrisonerTransactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //
        // POST: /PrisonerTransaction/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PrisonerTransaction transaction = db.PrisonerTransactions.Find(id);
            db.PrisonerTransactions.Remove(transaction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}