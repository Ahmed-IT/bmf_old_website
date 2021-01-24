using System.Collections.Generic;

namespace BMF.Shared.Models.KatrikFile
{
    public partial class KatrikFileResponse
    {
        public string error { get; set; }
        public List<string> initialPreview { get; set; }
        public List<KatrikFilePreviewConfig> initialPreviewConfig { get; set; }
        public bool append { get; set; }
    }
}
