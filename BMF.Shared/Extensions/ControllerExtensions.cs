using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Text;

namespace BMF.Shared.Extensions
{
    public static class ControllerExtensions
    {
        #region Front-end Feedback

        public static void SetError(this Controller controller, string Title, string Message = "", bool EndResponse = false, bool DirectInjection = true, bool IsPartialView = false, bool Disposable = false, [CallerLineNumber] int LineNumber = 0)
        {
            Message = new StringBuilder().AppendFormat("{0}", Message).ToString();
            if (DirectInjection)
            {
                if (IsPartialView)
                {
                    controller.ViewData["PVErrorID"] = LineNumber.ToString();
                    controller.ViewData["PVErrorMsg"] = Message;
                    controller.ViewData["PVErrorTitle"] = Title;
                    controller.ViewData["PVErrorDie"] = EndResponse;
                    controller.ViewData["PVErrorDispose"] = Disposable;
                }
                else
                {
                    controller.ViewData["ErrorID"] = LineNumber.ToString();
                    controller.ViewData["ErrorMsg"] = Message;
                    controller.ViewData["ErrorTitle"] = Title;
                    controller.ViewData["ErrorDie"] = EndResponse;
                    controller.ViewData["ErrorDispose"] = Disposable;
                }
            }
            else
            {
                if (IsPartialView)
                {
                    controller.TempData["PVErrorID"] = LineNumber.ToString();
                    controller.TempData["PVErrorMsg"] = Message;
                    controller.TempData["PVErrorTitle"] = Title;
                    controller.TempData["PVErrorDie"] = EndResponse;
                    controller.TempData["PVErrorDispose"] = Disposable;
                }
                else
                {
                    controller.TempData["ErrorID"] = LineNumber.ToString();
                    controller.TempData["ErrorMsg"] = Message;
                    controller.TempData["ErrorTitle"] = Title;
                    controller.TempData["ErrorDie"] = EndResponse;
                    controller.TempData["ErrorDispose"] = Disposable;
                }
            }
        }

        public static void SetWarning(this Controller controller, string Title, string Message = "", bool DirectInjection = true, bool IsPartialView = false, bool Disposable = false, [CallerLineNumber] int LineNumber = 0)
        {
            Message = new StringBuilder().AppendFormat("{0}", Message).ToString();
            if (DirectInjection)
            {
                if (IsPartialView)
                {
                    controller.ViewData["PVWarnTitle"] = Title;
                    controller.ViewData["PVWarnMsg"] = Message;
                    controller.ViewData["PVWarnDispose"] = Disposable;
                }
                else
                {
                    controller.ViewData["WarnTitle"] = Title;
                    controller.ViewData["WarnMsg"] = Message;
                    controller.ViewData["WarnDispose"] = Disposable;
                }
            }
            else
            {
                if (IsPartialView)
                {
                    controller.TempData["PVWarnTitle"] = Title;
                    controller.TempData["PVWarnMsg"] = Message;
                    controller.TempData["PVWarnDispose"] = Disposable;
                }
                else
                {
                    controller.TempData["WarnTitle"] = Title;
                    controller.TempData["WarnMsg"] = Message;
                    controller.TempData["WarnDispose"] = Disposable;
                }
            }
        }

        public static void SetSuccess(this Controller controller, string Title, string Message = "", bool DirectInjection = false, bool IsPartialView = false, bool Disposable = false, [CallerLineNumber] int LineNumber = 0)
        {
            if (DirectInjection)
            {
                if (IsPartialView)
                {
                    controller.ViewData["PVSuccessTitle"] = Title;
                    controller.ViewData["PVSuccessMsg"] = Message;
                    controller.ViewData["PVSuccessDispose"] = Disposable;
                }
                else
                {
                    controller.ViewData["SuccessTitle"] = Title;
                    controller.ViewData["SuccessMsg"] = Message;
                    controller.ViewData["SuccessDispose"] = Disposable;
                }
            }
            else
            {
                if (IsPartialView)
                {
                    controller.TempData["PVSuccessTitle"] = Title;
                    controller.TempData["PVSuccessMsg"] = Message;
                    controller.TempData["PVSuccessDispose"] = Disposable;
                }
                else
                {
                    controller.TempData["SuccessTitle"] = Title;
                    controller.TempData["SuccessMsg"] = Message;
                    controller.TempData["SuccessDispose"] = Disposable;
                }
            }
        }

        public static void SetInfo(this Controller controller, string Title, string Message = "", bool DirectInjection = true, bool IsPartialView = false, bool Disposable = false, [CallerLineNumber] int LineNumber = 0)
        {
            if (DirectInjection)
            {
                if (IsPartialView)
                {
                    controller.ViewData["PVInfoTitle"] = Title;
                    controller.ViewData["PVInfoMsg"] = Message;
                    controller.ViewData["PVInfoDispose"] = Disposable;
                }
                else
                {
                    controller.ViewData["InfoTitle"] = Title;
                    controller.ViewData["InfoMsg"] = Message;
                    controller.ViewData["InfoDispose"] = Disposable;
                }
            }
            else
            {
                if (IsPartialView)
                {
                    controller.TempData["PVInfoTitle"] = Title;
                    controller.TempData["PVInfoMsg"] = Message;
                    controller.TempData["PVInfoDispose"] = Disposable;
                }
                else
                {
                    controller.TempData["InfoTitle"] = Title;
                    controller.TempData["InfoMsg"] = Message;
                    controller.TempData["InfoDispose"] = Disposable;
                }
            }
        }

        #endregion
    }
}
