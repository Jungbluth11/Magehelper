namespace Magehelper.Core
{
    public struct ArtifactMaterial
    {
        public string Name { get; }
        public int ArcanoviMod { get; }
        public int SpellAmountMod { get; }
        public int AspMod { get; }
        public double PaspMod { get; }
        public int OccupationMod { get; }
        public int OccupationTypeMod { get; }
        public int SideEffectMod { get; }
        public int SideEffectTypeMod { get; }

        public ArtifactMaterial(string name, int arcanoviMod, int spellAmountMod, int aspMod, double paspMod, int occupationMod, int occupationTypeMod, int sideEffectMod, int sideEffectTypeMod)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ArcanoviMod = arcanoviMod;
            SpellAmountMod = spellAmountMod;
            AspMod = aspMod;
            PaspMod = paspMod;
            OccupationMod = occupationMod;
            OccupationTypeMod = occupationTypeMod;
            SideEffectMod = sideEffectMod;
            SideEffectTypeMod = sideEffectTypeMod;
        }
    }
}