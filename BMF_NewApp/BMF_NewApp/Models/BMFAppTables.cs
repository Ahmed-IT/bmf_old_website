using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BMF_NewApp.Models
{
    public class BMFAppTables
    {
        [Table("TblUsersAccount")]
        public class TblUsersAccount : IEntity
        {
            [PrimaryKey]
            public int Id { get; set; }

            public string CPR { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public bool LoggedIn { get; set; }
            public bool IsAdmin { get; set; }
        }

    }
}
