﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MailingService.Models;
using BusinessLogic;
using MailingService.Models.ViewModel;

namespace MailingService.Controllers
{
    public class EmailsController : Controller
    {
        private MailingServiceContext db = new MailingServiceContext();

        // GET: Emails
        public ActionResult Index()
        {
            return View();
        }

        



        // POST: Emails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Email,Subject,Message")] EmailViewModel form)
        {

            Mail mail = new Mail();
            mail.SendSimpleMessage(form.Email, form.Subject, form.Message);
            //if (ModelState.IsValid)
            //{
            //    //db.Emails.Add(email);
            //    //db.SaveChanges();
            //    //return RedirectToAction("Index");

            //}

            return View();
        }

    }
}