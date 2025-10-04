namespace Magehelper.Core
{
    /// <summary>
    /// Represents the base data of a pet.
    /// </summary>
    public struct PetData
    {
        public string Species { get; set; }
        public (int General, int Ability, int LeAe) GeneratingCost { get; set; }
        public int MUStartMin { get; set; }
        public int MUStartMax { get; set; }
        public int KLStartMin { get; set; }
        public int KLStartMax { get; set; }
        public int INStartMin { get; set; }
        public int INStartMax { get; set; }
        public int CHStartMin { get; set; }
        public int CHStartMax { get; set; }
        public int FFStartMin { get; set; }
        public int FFStartMax { get; set; }
        public int GEStartMin { get; set; }
        public int GEStartMax { get; set; }
        public int KOStartMin { get; set; }
        public int KOStartMax { get; set; }
        public int KKStartMin { get; set; }
        public int KKStartMax { get; set; }
        public int LEStartMin { get; set; }
        public int LEStartMax => LEStartMin + 3;
        public int AEStartMin { get; set; }
        public int AEStartMax => AEStartMin + 3;
        public int AUStartMin { get; set; }
        public int AUStartMax => AUStartMin + 3;
        public int MRStart { get; set; }
        public int AttackStart { get; set; }
        public int AttackFlyingStart { get; set; }
        public int ParryStart { get; set; }
        public int ParryFlyingStart { get; set; }
        public double GSStart { get; set; }
        public double GSFlyingStart { get; set; }
        public bool IsFlying { get; set; }
    }
}