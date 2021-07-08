using System;
using System.Collections.Generic;
using System.Text;

namespace BMF_NewApp.Models
{
    public class ClsBulletinCategory
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public List<ClsBulletinSubCategory> SubCategory { get; set; }
    }

    public class ClsBulletinSubCategory
    {
        public int ID { get; set; }
        public int ID_Category { get; set; }
        public string Title { get; set; }
        public List<ClsBulletins> Bulletins { get; set; }
        public int SectionHeight { get; set; }
    }

    public class ClsBulletins
    {
        public int ID { get; set; }
        public int ID_SubCategory { get; set; }
        public string Title { get; set; }
        public string PDF_Url { get; set; }
    }
}
