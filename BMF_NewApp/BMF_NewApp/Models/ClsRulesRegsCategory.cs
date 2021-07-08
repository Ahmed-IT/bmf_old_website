using System;
using System.Collections.Generic;
using System.Text;

namespace BMF_NewApp.Models
{
    public class ClsRulesRegsCategory
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public List<ClsRulesRegsSubCategory> SubCategory { get; set; }
    }

    public class ClsRulesRegsSubCategory
    {
        public int ID { get; set; }
        public int ID_Category { get; set; }
        public string Title { get; set; }
        public int SectionHeight { get; set; }
        public List<ClsRulesRegs> RulesRegs { get; set; }
    }

    public class ClsRulesRegs
    {
        public int ID { get; set; }
        public int ID_SubCategory { get; set; }
        public string Title { get; set; }
        public string PDF_Url { get; set; }
    }
}
