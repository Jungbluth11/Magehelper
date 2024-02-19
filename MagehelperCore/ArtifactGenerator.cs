using System.Text.Json;

namespace Magehelper.Core
{
    public class ArtifactGenerator
    {
        private readonly Core core;
        private ArtifactMaterial[] materials = Array.Empty<ArtifactMaterial>();
        public int SkillArcanovi { get; set; } = 0;
        public int SkillArcanoviMatrix { get; set; } = 0;
        public int SkillArcanoviSemi { get; set; } = 0;
        public int SkillMagiekunde { get; set; } = 0;
        public int ModArcanovi { get; private set; }
        public int ZfpArcanovi { get; private set; }
        public int Charges { get; set; } = 0;
        public bool SfKraftkontrolle { get; set; }
        public bool SfVielfacheLadung { get; set; }
        public bool SfMatrixkontrolle { get; set; }
        public bool SfAuxiliator { get; set; }
        public bool SfStapeleffekt { get; set; }
        public bool SfHypervehemenz { get; set; }
        public bool SfSemipermanenz1 { get; set; }
        public bool SfSemipermanenz2 { get; set; }
        public bool Rechargeable { get; set; }
        public bool Stapel { get; set; }
        public bool SpecialSigned { get; set; }
        public bool SpecialIndestructible { get; set; }
        public bool SpecialAwarenessCreator { get; set; }
        public bool SpecialApport { get; set; }
        public bool SpecialAwarenessDistance { get; set; }
        public bool SpecialResistent { get; set; }
        public bool SpecialSelfRepair { get; set; }
        public bool SpecialReverse { get; set; }
        public bool SpecialMultipleActivator { get; set; }
        public bool SpecialShroud { get; set; }
        public bool SpecialSelfDestruct { get; set; }
        public bool UseSideEffects { get; set; }
        public bool UseOccupation { get; set; }
        public bool CreatedInLimbus { get; set; }
        public bool CreatedNamelessDays { get; set; }
        public ArtifactType ArtifactType { get; set; } = ArtifactType.SingelUse;
        public SemiType SemiType { get; set; } = SemiType.Day;
        public MatrixType MatrixType { get; set; } = MatrixType.Labil;

        public ArtifactGenerator(Core core)
        {
            this.core = core;
#pragma warning disable CS8601
            materials = JsonSerializer.Deserialize<ArtifactMaterial[]>(File.ReadAllText(Path.Combine(core.SettingsPath, "ArtifactMaterials.json")));
#pragma warning restore CS8601
            if(core.Character != null && core.Character.IsLoaded)
            {
                //TODO: Character interaction
            }
        }

        public void GenerateArtifact()
        {
            if(ArtifactType == ArtifactType.Rechargeable)
            {
                ModArcanovi += 5;
            }
        }

        private void GenerateSingelUseArtefact()
        {

        }

        private void GenerateSemiArtifact()
        {

        }

        private void GenerateMatrixArtifact()
        {

        }
    }
}