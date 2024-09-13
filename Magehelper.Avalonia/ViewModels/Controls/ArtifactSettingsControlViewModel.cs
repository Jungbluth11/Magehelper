using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;

namespace Magehelper.Avalonia.ViewModels.Controls
{
    public partial class ArtifactSettingsControlViewModel :  ObservableObject
    {
        private bool _canExecute = false;
        public IEnumerable<ArtifactSpell> ArtifactSpells {  get; set; }

        public ArtifactSettingsControlViewModel(ArtifactSpell[] artifactSpells)
        {
            ArtifactSpells = artifactSpells;

        }

    }
}
