namespace Magehelper.ViewModels.Tabs;

public partial class TabArtifactViewModel : ObservableObject,
    IRecipient<FileActionMessage>,
    IRecipient<AddTraditionArtifactMessage>,
    IRecipient<DeleteTraditionArtifactMessage>
{
    private readonly Core.Core _core = Core.Core.GetInstance();
    [ObservableProperty] private bool _canAddArtifact = true;
    [ObservableProperty] private string _tabName = "Traditionsartefakt";

    public ObservableCollection<TraditionArtifactControlViewModel> ArtifactControls { get; } = [];

    public TabArtifactViewModel()
    {
        WeakReferenceMessenger.Default.RegisterAll(this);

        if (Settings.GetInstance().ChangeTraditionArtifactTabName)
        {
            ArtifactControls.CollectionChanged += ArtifactControls_CollectionChanged;
        }
    }

    private void ArtifactControls_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        TabName = ArtifactControls.Count == 1 ? ArtifactControls[0].ArtifactName : "Traditionsartefakt";

        if (ArtifactControls.Count > 1)
        {
            TabName += "e";
        }
    }

    public void ResetTab()
    {
        CanAddArtifact = true;
        ArtifactControls.Clear();
    }

    public void AddArtifact(Artifact artifact)
    {
        ArtifactControls.Add(new(artifact));

        foreach (string artifactName in _core.ArtifactNames)
        {
            if (!TraditionalArtifactHelper.IsInitialized[artifactName])
            {
                CanAddArtifact = true;
                return;
            }

            CanAddArtifact = false;
        }
    }

    private void CreateStaff(string length, string material, string pasp)
    {
        Staff staff = new()
        {
            Length = Array.IndexOf(Staff.LengthStrings, length),
            Material = Array.IndexOf(Staff.MaterialStrings, material),
            Pasp = int.Parse(pasp)
        };

        staff.AfvTotal();
        AddArtifact(staff);
    }

    private void CreateCrystalBall(string material)
    {
        CrystalBall crystalBall = new()
        {
            Material = material switch
            {
                "Künstlicher Kristall" => CrystalBallMaterial.ArtificialCrystal,
                "Natürlicher Kristall" => CrystalBallMaterial.NaturalCrystal,
                _ => CrystalBallMaterial.Glass
            }
        };

        AddArtifact(crystalBall);
    }

    private void CreateBowl(string material)
    {
        Bowl bowl = new()
        {
            Material = material == "Silber" ? BowlMaterial.Silber : BowlMaterial.Mondsilber
        };

        AddArtifact(bowl);
    }

    private void CreateBoneCub(string type)
    {
        BoneCub boneCub = new()
        {
            Type = type
        };

        AddArtifact(boneCub);
    }

    public void Receive(FileActionMessage message)
    {
        switch (message.Value)
        {
            case FileAction.New:
                ResetTab();

                break;
            case FileAction.Loaded:
                ResetTab();

                foreach (string artifactName in _core.ArtifactNames)
                {
                    if (TraditionalArtifactHelper.IsInitialized[artifactName])
                    {
                        AddArtifact(TraditionalArtifactHelper.GetArtifact[artifactName]!);
                    }
                }

                break;
        }
    }

    public void Receive(AddTraditionArtifactMessage message)
    {
        switch (message.ArtifactName)
        {
            case "Alchemistenschale":
                CreateBowl(message.AdditionalValues["BowlMaterial"]);
                break;
            case "Knochenkeule":
                CreateBoneCub(message.AdditionalValues["BoneCubType"]);
                break;
            case "Kristallkugel":
                CreateCrystalBall(message.AdditionalValues["CrystalBallMaterial"]);
                break;
            case "Magierstab":
                CreateStaff(message.AdditionalValues["StaffLength"], message.AdditionalValues["StaffMaterial"], message.AdditionalValues["AdditionalPasp"]);
                break;
            case "Ring des Lebens":
                AddArtifact(new RingOfLife());
                break;
            case "Vulkanglasdolch":
                AddArtifact(new ObsidianDagger());
                break;
        }
    }

    public void Receive(DeleteTraditionArtifactMessage message)
    {
        ArtifactControls.Remove(ArtifactControls.First(c => c.Artifact.GetType() == message.Value));
    }
}
