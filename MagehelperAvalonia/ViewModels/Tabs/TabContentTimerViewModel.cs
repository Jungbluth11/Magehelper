using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magehelper.Avalonia.ViewModels.Tabs
{
    public class TabContentTimerViewModel
    {
        private static TabContentTimerViewModel _instance = new();
        public static TabContentTimerViewModel Instance => _instance;

        public TabContentTimerViewModel()
        {
        }
    }
}
