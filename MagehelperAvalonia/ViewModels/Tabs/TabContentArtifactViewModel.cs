namespace Magehelper.Avalonia.ViewModels.Tabs
{
    public class TabContentArtifactViewModel
    {
        private readonly Core.Core core;
        private static TabContentArtifactViewModel _instance = new();
        public static TabContentArtifactViewModel Instance => _instance;
        public Staff? Staff { get; private set; }
        public CrystalBall? CrystalBall { get; private set; }
        public Bowl? Bowl { get; private set; }
        public BoneCub? BoneCub { get; private set; }
        public RingOfLife? RingOfLife { get; private set; }
        public ObsidianDagger? ObsidianDagger { get; private set; }
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

        public void AddArtifact(string artifact)
        {
            switch (artifact)
            {
                case "Alchemistenschale":
                    if (core.Bowl is null)
                    {
                        Bowl = new Bowl(core);
                    }
                    BowlControl = new BowlControl(MainWindowViewModel.Instance.Settings, Bowl);
                    break;
                case "Knochenkeule":
                    if (core.Bowl is null)
                    {
                        BoneCub = new BoneCub(core);
                    }
                    BoneCubControl = new BoneCubControl(MainWindowViewModel.Instance.Settings, BoneCub);
                    break;
                // only used by core.ReadFile()
                case "Kristallkugel":
                    CrystalBallControl = new CrystalBallControl(MainWindowViewModel.Instance.Settings, CrystalBall);
                    break;
                // only used by core.ReadFile() and core.ReadFileLegacy()
                case "Magierstab":
                    StaffControl = new StaffControl(MainWindowViewModel.Instance.Settings, Staff);
                    if (core.HasFlameSword)
                    {
                        TabContentFlameSwordViewModel.Instance.EnableTab();
                    }
                    break;
                case "Ring des Lebens":
                    if (core.Bowl is null)
                    {
                        RingOfLife = new RingOfLife(core);
                    }
                    RingOfLifeControl = new RingOfLifeControl(MainWindowViewModel.Instance.Settings, RingOfLife);
                    break;
                case "Vulkanglasdolch":
                    if (core.Bowl is null)
                    {
                        ObsidianDagger = new ObsidianDagger(core);
                    }
                    ObsidianDaggerControl = new ObsidianDaggerControl(MainWindowViewModel.Instance.Settings, ObsidianDagger);
                    break;
                default:
                    break;
            }
        }

        public void AddStaff(string length, string material, int pasp)
        {
            Staff = new Staff(core);
            Staff.Length = Array.IndexOf(Staff.LengthStrings, length);
            Staff.Material = Array.IndexOf(Staff.MaterialStrings, material);
            Staff.Pasp = pasp;
            Staff.AfvTotal();
            StaffControl = new StaffControl(MainWindowViewModel.Instance.Settings, Staff);
            if (Staff.Length == 1)
            {
                AddCrystalBallWindow addCrystalBallWindow = new();
                addCrystalBallWindow.ShowDialog();
            }
        }

        public void AddCrystalBall(CrystalBallMaterial crystalBallKind)
        {
            CrystalBall = new CrystalBall(core) { Material = crystalBallKind };
            CrystalBallControl = new CrystalBallControl(MainWindowViewModel.Instance.Settings, CrystalBall);
        }
    }
}