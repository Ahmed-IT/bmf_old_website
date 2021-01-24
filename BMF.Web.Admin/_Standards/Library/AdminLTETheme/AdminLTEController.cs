using System;
using System.Web;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using _Standards.Library.Security;
using BMF.Web.Admin.Database;

namespace _Standards.Library.AdminLTETheme
{
    public abstract class AdminLTEController : Controller
    {
        public BMFDbContext _TheDatabase;
        //public DataFunctions _DataFunctions;
        //public AuthenticationManager _AuthenticationManager = new AuthenticationManager();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                #region Feedback
                if (TempData["WarnTitle"] != null)
                {
                    ViewBag.WarnTitle = TempData["WarnTitle"];
                    ViewBag.WarnMsg = TempData["WarnMsg"];
                    ViewBag.WarnDispose = TempData["WarnDispose"];
                }
                if (TempData["PVWarnTitle"] != null)
                {
                    ViewBag.PVWarnTitle = TempData["PVWarnTitle"];
                    ViewBag.PVWarnMsg = TempData["PVWarnMsg"];
                    ViewBag.PVWarnDispose = TempData["PVWarnDispose"];
                }
                if (TempData["ErrorID"] != null)
                {
                    ViewBag.ErrorID = TempData["ErrorID"];
                    ViewBag.ErrorTitle = TempData["ErrorTitle"];
                    ViewBag.ErrorMsg = TempData["ErrorMsg"];
                    ViewBag.ErrorDie = TempData["ErrorDie"];
                    ViewBag.ErrorDispose = TempData["ErrorDispose"];
                }
                else
                    ViewBag.ErrorDie = false;
                if (TempData["PVErrorID"] != null)
                {
                    ViewBag.PVErrorID = TempData["PVErrorID"];
                    ViewBag.PVErrorTitle = TempData["PVErrorTitle"];
                    ViewBag.PVErrorMsg = TempData["PVErrorMsg"];
                    ViewBag.PVErrorDie = TempData["PVErrorDie"];
                    ViewBag.PVErrorDispose = TempData["PVErrorDispose"];
                }
                else
                    ViewBag.PVErrorDie = false;
                if (TempData["SuccessTitle"] != null)
                {
                    ViewBag.SuccessTitle = TempData["SuccessTitle"];
                    ViewBag.SuccessMsg = TempData["SuccessMsg"];
                    ViewBag.SuccessDispose = TempData["SuccessDispose"];
                }
                if (TempData["PVSuccessTitle"] != null)
                {
                    ViewBag.PVSuccessTitle = TempData["PVSuccessTitle"];
                    ViewBag.PVSuccessMsg = TempData["PVSuccessMsg"];
                    ViewBag.PVSuccessDispose = TempData["PVSuccessDispose"];
                }
                if (TempData["InfoTitle"] != null)
                {
                    ViewBag.InfoTitle = TempData["InfoTitle"];
                    ViewBag.InfoMsg = TempData["InfoMsg"];
                    ViewBag.InfoDispose = TempData["InfoDispose"];
                }
                if (TempData["PVInfoTitle"] != null)
                {
                    ViewBag.PVInfoTitle = TempData["PVInfoTitle"];
                    ViewBag.PVInfoMsg = TempData["PVInfoMsg"];
                    ViewBag.PVInfoDispose = TempData["PVInfoDispose"];
                }
                #endregion
            }
            catch (Exception LogError)
            {
            }
        }

        #region Feedback

        protected void SetError(string Title, string Message = "", bool EndRequest = false, bool DirectInjection = true, bool PartialView = false, bool Disposable = false, [CallerLineNumber] int LineNumber = 0)
        {
            Message = new StringBuilder().AppendFormat("{0}", Message).ToString();
            if (DirectInjection)
            {
                if (PartialView)
                {
                    ViewBag.PVErrorID = LineNumber.ToString();
                    ViewBag.PVErrorMsg = Message;
                    ViewBag.PVErrorTitle = Title;
                    ViewBag.PVErrorDie = EndRequest;
                    ViewBag.PVErrorDispose = Disposable;
                }
                else
                {
                    ViewBag.ErrorID = LineNumber.ToString();
                    ViewBag.ErrorMsg = Message;
                    ViewBag.ErrorTitle = Title;
                    ViewBag.ErrorDie = EndRequest;
                    ViewBag.ErrorDispose = Disposable;
                }
            }
            else
            {
                if (PartialView)
                {
                    TempData["PVErrorID"] = LineNumber.ToString();
                    TempData["PVErrorMsg"] = Message;
                    TempData["PVErrorTitle"] = Title;
                    TempData["PVErrorDie"] = EndRequest;
                    TempData["PVErrorDispose"] = Disposable;
                }
                else
                {
                    TempData["ErrorID"] = LineNumber.ToString();
                    TempData["ErrorMsg"] = Message;
                    TempData["ErrorTitle"] = Title;
                    TempData["ErrorDie"] = EndRequest;
                    TempData["ErrorDispose"] = Disposable;
                }
            }
        }

        protected void SetWarning(string Title, string Message = "", bool DirectInjection = true, bool PartialView = false, bool Disposable = false, [CallerLineNumber] int LineNumber = 0)
        {
            Message = new StringBuilder().AppendFormat("{0}", Message).ToString();
            if (DirectInjection)
            {
                if (PartialView)
                {
                    ViewBag.PVWarnTitle = Title;
                    ViewBag.PVWarnMsg = Message;
                    ViewBag.PVWarnDispose = Disposable;
                }
                else
                {
                    ViewBag.WarnTitle = Title;
                    ViewBag.WarnMsg = Message;
                    ViewBag.WarnDispose = Disposable;
                }
            }
            else
            {
                if (PartialView)
                {
                    TempData["PVWarnTitle"] = Title;
                    TempData["PVWarnMsg"] = Message;
                    TempData["PVWarnDispose"] = Disposable;
                }
                else
                {
                    TempData["WarnTitle"] = Title;
                    TempData["WarnMsg"] = Message;
                    TempData["WarnDispose"] = Disposable;
                }
            }
        }

        protected void SetSuccess(string Title, string Message = "", bool DirectInjection = false, bool PartialView = false, bool Disposable = false, [CallerLineNumber] int LineNumber = 0)
        {
            if (DirectInjection)
            {
                if (PartialView)
                {
                    ViewBag.PVSuccessTitle = Title;
                    ViewBag.PVSuccessMsg = Message;
                    ViewBag.PVSuccessDispose = Disposable;
                }
                else
                {
                    ViewBag.SuccessTitle = Title;
                    ViewBag.SuccessMsg = Message;
                    ViewBag.SuccessDispose = Disposable;
                }
            }
            else
            {
                if (PartialView)
                {
                    TempData["PVSuccessTitle"] = Title;
                    TempData["PVSuccessMsg"] = Message;
                    TempData["PVSuccessDispose"] = Disposable;
                }
                else
                {
                    TempData["SuccessTitle"] = Title;
                    TempData["SuccessMsg"] = Message;
                    TempData["SuccessDispose"] = Disposable;
                }
            }
        }

        protected void SetInfo(string Title, string Message = "", bool DirectInjection = true, bool PartialView = false, bool Disposable = false, [CallerLineNumber] int LineNumber = 0)
        {
            if (DirectInjection)
            {
                if (PartialView)
                {
                    ViewBag.PVInfoTitle = Title;
                    ViewBag.PVInfoMsg = Message;
                    ViewBag.PVInfoDispose = Disposable;
                }
                else
                {
                    ViewBag.InfoTitle = Title;
                    ViewBag.InfoMsg = Message;
                    ViewBag.InfoDispose = Disposable;
                }
            }
            else
            {
                if (PartialView)
                {
                    TempData["PVInfoTitle"] = Title;
                    TempData["PVInfoMsg"] = Message;
                    TempData["PVInfoDispose"] = Disposable;
                }
                else
                {
                    TempData["InfoTitle"] = Title;
                    TempData["InfoMsg"] = Message;
                    TempData["InfoDispose"] = Disposable;
                }
            }
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_TheDatabase != null)
                    _TheDatabase.Dispose();
            }
            //GC.Collect();
            base.Dispose(disposing);
        }
    }
}