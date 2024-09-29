using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magehelper.Avalonia.ViewModels.Tabs
{
    public partial class TabContentTimerViewModel : ObservableObject
    {
        private static TabContentTimerViewModel _instance = new();
        public static TabContentTimerViewModel Instance => _instance;

        public Timers Timers { get; }

        public TabContentTimerViewModel()
        {
            Timers = new Timers(MainWindowViewModel.Instance.Core);
        }
    }
}
