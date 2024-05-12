using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Windows.Forms;
using InternshipManagementSystem.Models;

namespace InternshipManagementSystem.Controllers
{
    public class tbl_applicationController : Controller
    {
       
        private db_internshipEntities1 db = new db_internshipEntities1();

        public ActionResult Report(string username)
        {
            ViewBag.username = username;    
            return View();
        }
        [HttpPost]
        public ActionResult Report(string departmentid,HttpPostedFileBase file)
        {
            try
            {
                 
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path);
                    
                    tbl_report tbl = new tbl_report();
                    tbl.r_studennumber = ViewBag.username;
                    tbl.r_departmentid = Convert.ToInt32(departmentid);
                    tbl.r_reportloc = _path;
                    tbl.r_date = System.DateTime.Now;
                    db.tbl_report.Add(tbl);
                    db.SaveChanges();
                    
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return View();
            }
            catch (Exception)
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
            
        }
        public ActionResult Index()
        {
            return View(db.tbl_application.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_application tbl_application = db.tbl_application.Find(id);
            if (tbl_application == null)
            {
                return HttpNotFound();
            }
            return View(tbl_application);
        }
        public ActionResult Create(string username)
        {
            ViewBag.username = username;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ap_applicationid,ap_internshiptype,ap_companyname,ap_companyaddress,ap_companyphonenumber,ap_companymail,ap_startingdate,ap_enddate,ap_workingday,ap_documentbycompany,ap_managername,ap_managerlastname,ap_managermail,ap_managertitle,ap_status,ap_studentdepartment")] tbl_application tbl_application)
        {
            int startyear = tbl_application.ap_startingdate.Year;
            int startmonth = tbl_application.ap_startingdate.Month;
            int startday = tbl_application.ap_startingdate.Day;
            int endyear = tbl_application.ap_enddate.Year;
            int endmonth = tbl_application.ap_enddate.Month;
            int endday = tbl_application.ap_enddate.Day;
            int result = 0;
            result = result + ((endyear - startyear)*365);
            result = result + ((endmonth - startmonth) * 30);
            result = result + (endday  -startday);
            tbl_application.ap_workingday = (byte)result;
            if (ModelState.IsValid)
            {
                db.tbl_application.Add(tbl_application);
                db.SaveChanges();
                MessageBox.Show("Your application has been received");
            }

            return View();//We can check the database about that the student has any application or not! but we didn't bc we have just only 3 username and password and we were trying lots of times to know program is working or not
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_application tbl_application = db.tbl_application.Find(id);
            if (tbl_application == null)
            {
                return HttpNotFound();
            }
            return View(tbl_application);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ap_applicationid,ap_internshiptype,ap_companyname,ap_companyaddress,ap_companyphonenumber,ap_companymail,ap_startingdate,ap_enddate,ap_workingday,ap_documentbycompany,ap_managername,ap_managerlastname,ap_managermail,ap_managertitle,ap_status")] tbl_application tbl_application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_application);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_application tbl_application = db.tbl_application.Find(id);
            if (tbl_application == null)
            {
                return HttpNotFound();
            }
            return View(tbl_application);
        }  
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_application tbl_application = db.tbl_application.Find(id);
            db.tbl_application.Remove(tbl_application);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Error()
        {
           
            return View();
        }
    }
}
