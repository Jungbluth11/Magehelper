using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magehelper.Core;

namespace Magehelper.Avalonia.ViewModels.Controls
{
    public partial  class CrystalBallControlViewModel: ObservableObject
    {
        private readonly CrystalBall crystalBall;
        public string Material { get; }

        public CrystalBallControlViewModel(CrystalBall crystalBall)
        {
            this.crystalBall = crystalBall;
            Material = CrystalBall.MaterialStrings[(int)crystalBall.Material];
        }

        public ArtifactSpell? AddSpell()
        {
            //AddCrystalBallSpellWindow addCrystalBallSpellWindow = new AddCrystalBallSpellWindow(crystalBall);
            //if (addCrystalBallSpellWindow.ShowDialog() == true)
            //{
            //    try
            //    {
            //        return crystalBall.AddSpell(addCrystalBallSpellWindow.SpellName);
            //    }
            //    catch (Exception e)
            //    {
            //        ErrorMessages.Error(e.Message);
            //    }
            //}
            return null;
        }

        [RelayCommand]
        private void Apport(bool? isChecked)
        {
            crystalBall.HasApport = (bool)isChecked;
        }
    }
}
