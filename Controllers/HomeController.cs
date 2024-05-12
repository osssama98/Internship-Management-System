using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Drawing;
using InternshipManagementSystem.Models;




namespace InternshipManagementSystem.Controllers
{
    
    public class HomeController : Controller
    {
        private db_internshipEntities1 db = new db_internshipEntities1();

       
        WebBrowser browser;
        string act;
        string username1 = "Unknown User";

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            username1 = username;
            Thread thread = new Thread(delegate ()
            {
                string url = "https://orion.iku.edu.tr".Trim();

                using (browser = new WebBrowser())
                {
                    browser.ScrollBarsEnabled = false;
                    browser.AllowNavigation = true;
                    browser.Navigate("https://orion.iku.edu.tr");
                    browser.Width = 1024;
                    browser.Height = 768;
                    browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(DocumentCompleted);
                    while (browser.ReadyState != WebBrowserReadyState.Complete)
                    {
                        System.Windows.Forms.Application.DoEvents();
                    }
                  

                }
               
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            void DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
            {

                WebBrowser browser = sender as WebBrowser;
                HtmlElementCollection doc = browser.Document.All;
                foreach (HtmlElement element in doc)
                {
                    if (element.GetAttribute("id") == "logonuidfield")
                    {
                        browser.Document.GetElementById("logonuidfield").SetAttribute("value", username);
                        MessageBox.Show("The username is setting on orion...");

                    }
                }
                foreach (HtmlElement element in doc)
                {
                    if (element.GetAttribute("id") == "logonpassfield")
                    {
                        browser.Document.GetElementById("logonpassfield").SetAttribute("value", password);
                        MessageBox.Show("The password is setting on orion...");

                    }
                }
                foreach (HtmlElement element in doc)
                {
                    if (element.GetAttribute("className") == "button")
                    {
                        element.InvokeMember("click");
                        
                        
                        MessageBox.Show("The information is submitting...");
                        MessageBox.Show(browser.Url.ToString());
                        MessageBox.Show(browser.Document.Cookie.ToString());
                        if (browser.Document.Cookie.ToString().Length>45)
                        {
                            act = "Success";
                           
                            MessageBox.Show("Login Succesfully... Username:"+username1);
                            browser.Stop();     
                        }
                        else
                        {
                            browser.Stop();
                        }

                    }
                }

            }
            if (act == "Success") 
            {
                return RedirectToAction("Mainpage", "Home", new {username=username1});
               
            }
            else
            {
                MessageBox.Show("Username or password is wrong!");
                return View();
            }

           

        }


        public ActionResult Mainpage(string username)
        {
            ViewBag.username = username;
            var info1 = db.tbl_user.FirstOrDefault(x => x.u_username == username);
            if (info1 != null)
            {
                var info2 = db.tbl_academician.FirstOrDefault(x => x.a_userid == info1.u_userid);
                if (info2 != null)
                {
                    ViewBag.departmentid = info2.a_departmentid;
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Userlogin()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Userlogin(tbl_user user)
        {
            var infouser = db.tbl_user.FirstOrDefault(x => x.u_username == user.u_username && x.u_password == user.u_password);
            if (infouser != null)
            {
                var infouser2 = db.tbl_academician.FirstOrDefault(x => x.a_userid == infouser.u_userid);
                if(infouser2 != null)
                {
                    return RedirectToAction("Usermain", "Home", new { username = infouser.u_username,departmentid=infouser2.a_departmentid  } );
                    
                    
                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("Giriş Başarısız işlemleri");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Applications(int departmentid,string username)
        {
            ViewBag.username = username;
            ViewBag.departmentid = departmentid;
            return View(db.tbl_application.ToList().Where(x=>x.ap_studentdepartment==departmentid));
        }
        [HttpPost]
        public ActionResult Applications(string appid)
        {
            int id=Convert.ToInt32(appid);
            var acceptedapp = db.tbl_application.FirstOrDefault(x => x.ap_applicationid == id);
            if (acceptedapp != null)
            {
                acceptedapp.ap_status = true;
                tbl_internship newinternship = new tbl_internship();
                newinternship.i_companyname = acceptedapp.ap_companyname;
                newinternship.i_companyaddress= acceptedapp.ap_companyaddress;
                newinternship.i_companymail = acceptedapp.ap_companymail;
                newinternship.i_companyphonenumber = acceptedapp.ap_companyphonenumber;
                newinternship.i_documentbycompany=acceptedapp.ap_documentbycompany;
                newinternship.i_enddate = acceptedapp.ap_enddate;
                newinternship.i_internshiptype = acceptedapp.ap_internshiptype;
                newinternship.i_managerlastname= acceptedapp.ap_managerlastname;
                newinternship.i_managermail= acceptedapp.ap_managermail;
                newinternship.i_managername= acceptedapp.ap_managername;
                newinternship.i_managertitle= acceptedapp.ap_managertitle;
                newinternship.i_startingdate= acceptedapp.ap_startingdate;
                newinternship.i_studentdepartment= acceptedapp.ap_studentdepartment;
                newinternship.i_workingday= acceptedapp.ap_workingday;
                db.tbl_internship.Add(newinternship);
                db.SaveChanges();
                MessageBox.Show("Application is accepted...");
            }
            else
            {
                MessageBox.Show("Error");
            }
            return View(db.tbl_application.ToList().Where(x => x.ap_studentdepartment == acceptedapp.ap_studentdepartment));
        }
        public ActionResult Usermain(string username,string departmentid)
        {
            ViewBag.username = username;
            ViewBag.departmentid = departmentid;
            return View();
        }
        //public ActionResult Usermain(string username)
        //{
        //    ViewBag.username = username;
        //    var info1 = db.tbl_user.FirstOrDefault(x => x.u_username == username);
        //    if(info1 != null)
        //    {
        //        var info2=db.tbl_academician.FirstOrDefault(y => y.a_userid == info1.u_userid);
        //        if(info2 != null)
        //        {
        //            ViewBag.departmentid = info2.a_departmentid;
        //        }
        //    }

        //    return View();
        //}

        [HttpGet]
        public ActionResult Reports(int departmentid)
        {
            ViewBag.departmentid = departmentid;
            return View(db.tbl_report.ToList().Where(x=>x.r_departmentid==departmentid));
        }

        [HttpPost]
        public ActionResult Reports(string reportloc,int departmentid)
        {
            
            System.Diagnostics.Process.Start(reportloc);
            return View(db.tbl_report.ToList().Where(x => x.r_departmentid == departmentid));
        }

       



    }
}