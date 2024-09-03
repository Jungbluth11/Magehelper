using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Models;

namespace Magehelper.Avalonia
{
    internal static class MessageBoxGenerator
    {
        public static dynamic GetErrorMessageBox(string msg)
        {
            Bitmap imageIcon = new Bitmap(AssetLoader.Open(new Uri("avares://magehelper/Assets/exclamation.png")));
            return MessageBoxManager.GetMessageBoxCustom(new MessageBoxCustomParams { ImageIcon = imageIcon, ContentTitle = "Magehelper", ContentMessage = msg,
                ButtonDefinitions = new List<ButtonDefinition>
                {
                    new ButtonDefinition()
                },
            });
        }
    }
}
