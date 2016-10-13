﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AarhusWebDevCoop.ViewModels {
    public class MessageBoardForm {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = " ugyldig email addresse")]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        public MessageBoardForm() { }
    }
}