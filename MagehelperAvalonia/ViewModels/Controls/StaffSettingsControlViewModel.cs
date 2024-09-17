using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magehelper.Avalonia.ViewModels.Controls
{
    public partial class StaffSettingsControlViewModel(ArtifactSpell[] artifactSpells) : ObservableObject
    {
        public IEnumerable<ArtifactSpell> ArtifactSpells { get; set; } = artifactSpells;
    }
}
