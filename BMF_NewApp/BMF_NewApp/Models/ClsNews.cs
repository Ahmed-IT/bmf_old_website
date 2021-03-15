using System;
using System.Collections.Generic;
using System.Text;

namespace BMF_NewApp.Models
{
    public class ClsNews
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime NewsTimestampUtc { get; set; }
        public string Image_URL { get; set; }
        public string Content { get; set; }
        public string ContentCard
        {
            get
            {
                return Content.Substring(0, Content.Length > 150 ? 150 : Content.Length) + " ...";
            }
        }
    }
}
