using Avalonia.Controls;
using Avalonia.Media.Imaging;
using MsBox.Avalonia;
using MsBox.Avalonia.Base;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia.Models;

namespace Magehelper.Avalonia.Models
{
    public static class MessageBoxGenerator
    {
        public enum Buttons
        {
            OK,
            YesNo,
            YesNoCancel
        }

        public static IMsBox<string> GetMessageBox(string msg, Buttons buttons)
        {
            return CreateMessageBox(msg, buttons);
        }

        public static IMsBox<string> GetMessageBox(string msg, Buttons buttons, Bitmap? imageIcon = null)
        {
            return CreateMessageBox(msg, buttons, imageIcon);
        }

        public static IMsBox<string> GetMessageBox(string msg, Buttons buttons, Icon? icon = null)
        {
            return CreateMessageBox(msg, buttons, null, icon);
        }

        private static IMsBox<string> CreateMessageBox(string msg, Buttons buttons, Bitmap? imageIcon = null, Icon? icon = null)
        {
            List<ButtonDefinition> buttonDefinitions = [];
            if (buttons == Buttons.YesNo || buttons == Buttons.YesNoCancel)
            {
                buttonDefinitions.AddRange([new ButtonDefinition { Name = "Ja" }, new ButtonDefinition { Name = "Nein" }]);
                if (buttons == Buttons.YesNoCancel)
                {
                    buttonDefinitions.Add(new ButtonDefinition { Name = "Abbrechen", IsCancel = true });
                }
            }
            else
            {
                buttonDefinitions.Add(new ButtonDefinition { Name = "OK" });
            }

            MessageBoxCustomParams MessageBoxParams = new()
            {
                ContentTitle = "Magehelper",
                ContentMessage = msg,
                Topmost = true,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                ShowInCenter = true,
                ButtonDefinitions = buttonDefinitions,
            };
            if (imageIcon != null)
            {
                MessageBoxParams.ImageIcon = imageIcon;
            }
            if (icon != null)
            {
                MessageBoxParams.Icon = (Icon)icon;
            }

            return MessageBoxManager.GetMessageBoxCustom(MessageBoxParams);
        }
    }
}
