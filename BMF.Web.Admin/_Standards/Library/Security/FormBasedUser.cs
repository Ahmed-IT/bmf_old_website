using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace _Standards.Library.Security
{
    public class FormBasedUser
    {
        #region Properties

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        #endregion

        #region Constructors

        public FormBasedUser()
        {

        }

        #endregion
    }
}