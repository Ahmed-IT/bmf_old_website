using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Events
{
    public partial class M_EventRound : BaseEntity
    {
        #region Foreign Keys

        [ForeignKey(nameof(EventCategory))]
        [Display(Name = "Event Category", Prompt = "Event Category")]
        [Required]
        [JsonProperty(PropertyName = nameof(ID_EventCategory))]
        public long ID_EventCategory { get; set; }

        #endregion

        #region Properties

        [Display(Name = "Round Number", Prompt = "Round Number")]
        [Required, MaxLength(10)]
        [JsonProperty(PropertyName = nameof(Round_Number))]
        public string Round_Number { get; set; }

        [Display(Name = "Start Date & Time", Prompt = "Start Date & Time")]
        [Required]
        [JsonProperty(PropertyName = nameof(DateTime_Start))]
        public DateTime DateTime_Start { get; set; }

        [Display(Name = "End Date & Time", Prompt = "End Date & Time")]
        [Required]
        [JsonProperty(PropertyName = nameof(DateTime_End))]
        public DateTime DateTime_End { get; set; }

        [Display(Name = "Add To Calendar", Prompt = "Add To Calendar")]
        [Required]
        [JsonProperty(PropertyName = nameof(AddTo_Calendar))]
        public bool AddTo_Calendar { get; set; }

        [Display(Name = "Add To Countdown", Prompt = "Add To Countdown")]
        [Required]
        [JsonProperty(PropertyName = nameof(AddTo_Countdown))]
        public bool AddTo_Countdown { get; set; }

        #endregion

        #region Relationships

        [InverseProperty(nameof(M_EventCategory.EventRounds))]
        public M_EventCategory EventCategory { get; set; }

        [InverseProperty(nameof(M_EventRoundSession.EventRound))]
        public ICollection<M_EventRoundSession> EventRoundSessions { get; set; }

        #endregion
    }
}
