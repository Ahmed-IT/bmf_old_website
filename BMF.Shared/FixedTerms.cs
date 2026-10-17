using Newtonsoft.Json;
using System;

namespace BMF.Shared
{
    public static class FixedTerms
    {
        #region Application Properties

        public static string Application_Name => Environment.GetEnvironmentVariable("Application_Name");
        public static string Application_Code => Environment.GetEnvironmentVariable("Application_Code");
        public static string Application_Website => Environment.GetEnvironmentVariable("Application_Website");
        public static string Application_WebsiteLink => Environment.GetEnvironmentVariable("Application_WebsiteLink");

        #endregion

        #region Success

        public static string Title_Success_Save => "Succesfully saved changes.";
        public static string Title_Success_Delete => "Succesfully deleted record(s).";
        public static string Title_Success_Add => "Succesfully added new record(s).";

        #endregion

        #region Warning

        public static string Title_Warning_Save => "Some changes could not be saved successfully.";

        #endregion

        #region Error

        public static string Title_Error_ErrorOccurred => "Error Occurred.";
        public static string Message_Error_RefreshPage => "Please try refreshing the page.";
        public static string Message_Error_NoAccess => "Unauthorized access within the system.";
        public static string Message_Error_Navigation => "Please make sure you navigated to the requested page correctly.";
        public static string Message_Error_GeneralAdd => "Failed to add new record(s). Please try again.";
        public static string Message_Error_GeneralDelete => "Failed to delete record(s). Please try again.";
        public static string Message_Error_GeneralSave => "Failed to save changes. Please try again.";
        public static string Message_Error_LoadRelatedRecords => "Failed to loaded related records. Please try again.";

        #endregion

        #region Information

        public static string Info_NoDataFound => "No data found.";

        #endregion

        #region ValidationMessages

        #endregion

        #region JSON

        public static JsonSerializerSettings JsonSettings_IgnoreNull => new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

        #endregion

        #region Concurrency Handling

        public static string Label_ConcurencyValue_Updated => "Latest value: ";
        public static string Title_Error_ConcurencyUpdated => "Record has been updated by another user.";
        public static string Message_Error_ConcurencyUpdated => "The record you attempted to edit was modified by another user after you got the original value. The edit operation was canceled and the current values in the database have been displayed. If you still want to edit this record, click the Save button again. Otherwise click the Cancel button.";
        public static string Message_Error_ConcurencyDeleted => "Unable to save changes. The record has been deleted by another user.";
        public static string Message_Error_ConcurencyDelete => "The record you attempted to delete was modified by another user after you got the original values. The delete operation was canceled and the current values in the database have been displayed. If you still want to delete this record, click the Delete button again. Otherwise click the Cancel button.";

        public static string Message_Error_ConcurencyPropDeleted => "Record was deleted by another user.";

        public static string Message_Error_ConcurencyPropAdded => "Record was added by another user.";

        public static string Message_Error_ConcurencyPropUpdated => "Record was updated by another user.";

        #endregion

        #region Standard Partial Views

        public static string System_PartialViewPath_AdminFeedback => "_FeedbackPartial";
        public static string System_PartialViewPath_AdminFeedbackPV => "_FeedbackPVPartial";
        public static string System_PartialViewPath_JQDatatable => "_FeedbackPVPartial";

        #endregion
    }
}
