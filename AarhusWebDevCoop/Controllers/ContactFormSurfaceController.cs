using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using AarhusWebDevCoop.ViewModels;
using System.Net.Mail;
using Umbraco.Core.Models;

namespace AarhusWebDevCoop.Controllers {
    public class ContactFormSurfaceController : SurfaceController {
        // GET: ContactFormSurface
        public ActionResult Index() {
            ContactForm cform = new ContactForm();
            return PartialView("ContactForm", cform);
        }

        [HttpPost]
        public ActionResult HandleFormSubmit(ContactForm model) {
            if (!ModelState.IsValid) {
                return CurrentUmbracoPage();
            }

            IContent comment = Services.ContentService.CreateContent(model.Subject, CurrentPage.Id, "Comment");
            comment.SetValue("Commentname", model.Name);
            comment.SetValue("email", model.Email);
            comment.SetValue("subject", model.Subject);
            comment.SetValue("message", model.Message);

            //save 
            Services.ContentService.Save(comment);
            // save and publish
            // Services.ContentService.SaveAndPublishStatus(comment);

            MailMessage message = new MailMessage();
            message.To.Add("terkeljc@outlook.com");
            message.Subject = model.Subject;
            message.From = new MailAddress(model.Email, model.Name);
            message.Body = model.Message + "\n my email is: " + model.Email;

            using (SmtpClient smtp = new SmtpClient()) {
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Host = "smtp-mail.outlook.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("terkeljc@outlook.com", "Dansetrold23");

                smtp.Send(message);
                TempData["success"] = true;
            }
            
                return RedirectToCurrentUmbracoPage();
        }
    }
}