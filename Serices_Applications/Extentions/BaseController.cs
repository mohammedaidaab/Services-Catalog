﻿using Microsoft.AspNetCore.Mvc;

namespace UI.Extentions
{
    public enum NotificationType
    {
        Success,
        Error,
        Warning,
        Info,
    }

    public enum NotificationPosition
    {
        Top,
        TopStart,
        TopEnd,
        Center,
        CenterStart,
        CenterEnd,
        Bottom,
        BottomStart,
        BottomEnd
    }


    public class BaseController : Controller
    {
        string pos = "";

        public void BasicNotification(string msg , NotificationType type , string title = "")
        {
            TempData["notification"] = $"Swal.fire('{title}','{msg}', '{type.ToString().ToLower()}')";
        }


        public void CustomNotification(string msj, NotificationType type, NotificationPosition position, string title = "", string icon = "", bool showConfirmButton = false, int timer = 2000, bool toast = true)
        {
            SetPosition(position.ToString());

            TempData["notification"] = "Swal.fire({customClass:{confirmButton:'btn btn-primary',cancelButton:'btn btn-danger'},position:'" + pos + "',icon:'" + icon + "',type:'" + type.ToString().ToLower() +
                "',title:'" + title + "',text: '" + msj + "',showConfirmButton: " + showConfirmButton.ToString().ToLower() + ",confirmButtonColor: '#4F0DA2',toast: "
                + toast.ToString().ToLower() + ",timer: " + timer + "}); ";
        }

        private void SetPosition(string position)
        {
            if (position == "Top") pos = "top";
            if (position == "TopStart") pos = "top-start";
            if (position == "TopEnd") pos = "top-end";
            if (position == "Center") pos = "center";
            if (position == "CenterStart") pos = "center-start";
            if (position == "CenterEnd") pos = "center-end";
            if (position == "Bottom") pos = "bottom";
            if (position == "BottomStart") pos = "bottom-start";
            if (position == "BottomEnd") pos = "bottom-end";
        }

    }
}
