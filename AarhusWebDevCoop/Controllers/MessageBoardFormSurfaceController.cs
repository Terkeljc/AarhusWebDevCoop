using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using AarhusWebDevCoop.ViewModels;
using System.Net.Mail;
using Umbraco.Core.Models;

namespace AarhusWebDevCoop.Controllers
{
    public class MessageBoardFormSurfaceController : SurfaceController
    {
        // GET: MessageBoardFormSurface
        public ActionResult Index()
        {
            MessageBoardForm mbform = new MessageBoardForm();
            return PartialView("MessageBoardForm", mbform);
        }

        [HttpPost]
        public ActionResult HandleFormSubmit( MessageBoardForm model) {
            if (!ModelState.IsValid) {
                return CurrentUmbracoPage();
            }
            IContent message = Services.ContentService.CreateContent(model.Subject, CurrentPage.Id, "Messages");
            message.SetValue("Messagename", model.Name);
            message.SetValue("Email", model.Email);
            message.SetValue("Subject", model.Subject);
            message.SetValue("Message", model.Message);


            // save and publish
            Services.ContentService.SaveAndPublishWithStatus(message);

                return RedirectToCurrentUmbracoPage();
        }
    }
}