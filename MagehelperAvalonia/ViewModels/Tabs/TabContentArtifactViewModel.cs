#pragma warning disable CS8604
namespace Magehelper.Avalonia.ViewModels.Tabs
{
    public partial class TabContentArtifactViewModel : ObservableObject
    {
        private readonly Core.Core core;
        private static TabContentArtifactViewModel _instance = new();
        public static TabContentArtifactViewModel Instance => _instance;
        public StaffControl? StaffControl { get; private set; }
        public CrystalBallControl? CrystalBallControl { get; private set; }
        public BowlControl? BowlControl { get; private set; }
        public BoneCubControl? BoneCubControl { get; private set; }
        public RingOfLifeControl? RingOfLifeControl { get; private set; }
        public ObsidianDaggerControl? ObsidianDaggerControl { get; private set; }

        public TabContentArtifactViewModel()
        {
            MainWindowViewModel.Instance.Core.AddArtifactGUIAction = AddArtifact;
            core = MainWindowViewModel.Instance.Core;
        }

        public void ResetTab()
        {
           StaffControl = null;
           CrystalBallControl = null;
           BowlControl = null;
           BoneCubControl = null;
           RingOfLifeControl = null;
           ObsidianDaggerControl  = null;
        }

        public void AddArtifact(string artifact)
        {
            switch (artifact)
            {
                case "Alchemistenschale":
                    if (core.Bowl is null)
                    {
                        new Bowl(core);
                    }
                    BowlControl = new BowlControl(MainWindowViewModel.Instance.Settings, core.Bowl);
                    break;
                case "Knochenkeule":
                    if (core.BoneCub is null)
                    {
                        new BoneCub(core);
                    }
                    BoneCubControl = new BoneCubControl(MainWindowViewModel.Instance.Settings, core.BoneCub);
                    break;
                // only used by core.ReadFile()
                case "Kristallkugel":
                    CrystalBallControl = new CrystalBallControl(MainWindowViewModel.Instance.Settings, core.CrystalBall);
                    break;
                // only used by core.ReadFile() and core.ReadFileLegacy()
                case "Magierstab":
                    StaffControl = new StaffControl(MainWindowViewModel.Instance.Settings, core.Staff);
                    if (core.HasFlameSword)
                    {
                        TabContentFlameSwordViewModel.Instance.FlameSwordExist = true;
                    }
                    break;
                case "Ring des Lebens":
                    if (core.RingOfLife is null)
                    {
                        new RingOfLife(core);
                    }
                    RingOfLifeControl = new RingOfLifeControl(MainWindowViewModel.Instance.Settings, core.RingOfLife);
                    break;
                case "Vulkanglasdolch":
                    if (core.ObsidianDagger is null)
                    {
                        new ObsidianDagger(core);
                    }
                    ObsidianDaggerControl = new ObsidianDaggerControl(MainWindowViewModel.Instance.Settings, core.ObsidianDagger);
                    break;
            }
        }

        public void AddStaff(string length, string material, int pasp)
        {
            Staff staff = new(core)
            {
                Length = Array.IndexOf(Staff.LengthStrings, length),
                Material = Array.IndexOf(Staff.MaterialStrings, material),
                Pasp = pasp
            };
            staff.AfvTotal();
            StaffControl = new StaffControl(MainWindowViewModel.Instance.Settings, staff);
        }

        public void AddCrystalBall(CrystalBallMaterial crystalBallKind)
        {
            CrystalBall crystalBall = new(core) { Material = crystalBallKind };
            CrystalBallControl = new CrystalBallControl(MainWindowViewModel.Instance.Settings, crystalBall);
        }
    }
}