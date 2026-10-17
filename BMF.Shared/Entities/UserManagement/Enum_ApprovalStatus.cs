using System.ComponentModel.DataAnnotations;

namespace BMF.Shared.Entities.UserManagement
{
    public enum Enum_ApprovalStatus
    {
        [Display(Name = "Pending Approval", Prompt = "Pending Approval")]
        PendingApproval = 0,

        [Display(Name = "Approved", Prompt = "Approved")]
        Approved = 10,
        
        [Display(Name = "Rejected", Prompt = "Rejected")]
        Rejected = 20,
    }
}
