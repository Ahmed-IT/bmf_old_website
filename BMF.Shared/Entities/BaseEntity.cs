using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BMF.Shared.Entities
{
    public abstract class BaseEntity
    {
        #region Fields

        [NotMapped]
        [Display(Name = "Is Active?")]
        [JsonProperty(PropertyName = nameof(Display_IsActive))]
        public string Display_IsActive => IsActive ? "Yes" : "No";

        #endregion

        #region Properties

        [Key]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty(PropertyName = nameof(ID))]
        public long ID { get; set; }

        [Display(Name = "Is Active?")]
        [JsonProperty(PropertyName = nameof(IsActive))]
        public bool IsActive { get; set; }

        [Column(TypeName = "DATETIME2(7)")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Created On")]
        [JsonProperty(PropertyName = nameof(CreatedOnUtc))]
        public DateTime CreatedOnUtc { get; set; }

        [Display(Name = "Created By")]
        [JsonProperty(PropertyName = nameof(CreatedBy))]
        public string CreatedBy { get; set; }

        [Column(TypeName = "DATETIME2(7)")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Updated On")]
        [JsonProperty(PropertyName = nameof(UpdatedOnUtc))]
        public DateTime UpdatedOnUtc { get; set; }

        [Display(Name = "Updated By")]
        [JsonProperty(PropertyName = nameof(UpdatedBy))]
        public string UpdatedBy { get; set; }

        [Column(TypeName = "DATETIME2(7)")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Deleted On")]
        [JsonProperty(PropertyName = nameof(DeletedOnUtc))]
        public DateTime? DeletedOnUtc { get; set; }

        [Display(Name = "Deleted By")]
        [JsonProperty(PropertyName = nameof(DeletedBy))]
        [AllowNull]
        public string DeletedBy { get; set; }

        [Timestamp]
        [ConcurrencyCheck]
        [Display(Name = "Row Version")]
        [JsonProperty(PropertyName = nameof(RowVersion))]
        public byte[] RowVersion { get; set; }

        #endregion

        #region Foreign Keys

        #endregion

        #region Relationships

        #endregion
    }
}
