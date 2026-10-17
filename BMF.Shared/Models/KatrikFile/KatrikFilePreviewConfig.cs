using System.Collections.Generic;

namespace BMF.Shared.Models.KatrikFile
{
    public partial class KatrikFilePreviewConfig
    {
        public string caption { get; set; }
        public string filename { get; set; }
        public string url { get; set; } // 'http://localhost/avatar/delete', // server delete action 
        public string downloadUrl { get; set; } // 'http://localhost/avatar/delete', // server delete action 
        public long key { get; set; }
        public Dictionary<string, object> extra { get; set; }
    }
}
