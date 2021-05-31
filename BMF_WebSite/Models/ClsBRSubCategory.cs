using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMF_WebSite.Models
{
    public class ClsBRSubCategory
    {
        public long ID { get; set; }
        public long ID_Category { get; set; }
        public string Title { get; set; }
        public List<ClsBulletins> Bulletins { get; set; }
        public List<ClsRulesRegs> RulesRegs { get; set; }
    }
}