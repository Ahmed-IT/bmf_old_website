using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Models
{
    public partial class BaseEntity
    {
        #region Properties

        public long ID { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedOnUtc { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? DeletedOnUtc { get; set; }

        public string DeletedBy { get; set; }

        public byte[] RowVersion { get; set; }

        #endregion
    }
}
