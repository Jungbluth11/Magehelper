using Avalonia.Controls;

namespace Magehelper.Avalonia.Models
{
    public struct TabItemModel
    {
        public string Header { get; set; }
        public UserControl TabContent { get; set; }
    }
}
