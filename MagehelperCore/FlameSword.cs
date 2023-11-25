using DSAUtils;

namespace Magehelper.Core
{
    public class FlameSword
    {
        private readonly Core core;
        /// <summary>
        /// THe points that be used in total.
        /// </summary>
        public int PointsTotal { get; set; }
        /// <summary>
        /// the points are left to use.
        /// </summary>
        public int PointsRemain => PointsTotal - Attack[1] - Parry[1] - GS[1] - TP[1];
        public int[] TP { get; private set; } = new int[] { 4, 0 };
        public int[] Attack { get; private set; } = new int[] { 12, 0 };
        public int[] Parry { get; private set; } = new int[] { 3, 0 };
        public int[] GS { get; private set; } = new int[] { 3, 0 };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="core">An instance of <see cref="Core"/>.</param>
        public FlameSword(Core core)
        {
            core.FlameSword = this;
            this.core = core;
        }

        /// <summary>
        /// Rolls the activation of this Ritual.
        /// </summary>
        /// <returns>An result of<see cref="DSAUtils.DSA.TaP"/></returns>
        public object RollActivation()
        {
            try
            {
#pragma warning disable CS8602
#pragma warning disable CS8604
                int MU = core.Character.MU;
                int IN = core.Character.IN;
                int GE = core.Character.GE;
                int TaW = core.Character.Skills.Single(a => a.Name == "Ritualkenntnis: Gildenmagie").Wert;
                return DSA.TaP(MU, IN, GE, TaW);
#pragma warning restore CS8602
#pragma warning restore CS8604
            }
            catch
            {
                throw new Exception("No Character Loaded");
            }
        }

        /// <summary>
        /// Modify's <see cref="TP"/> an recalculate <see cref="PointsRemain"/>.
        /// </summary>
        /// <param name="points">points to use.</param>
        public void ModifyTp(int points)
        {
            TP[0] = 4 + points / 2;
            TP[1] = points;
        }

        /// <summary>
        /// Modify's <see cref="Attack"/> an recalculate <see cref="PointsRemain"/>.
        /// </summary>
        /// <param name="points">points to use.</param>
        public void ModifyAttack(int points)
        {
            Attack[0] = 12 + points;
            Attack[1] = points;
        }

        /// <summary>
        /// Modify's <see cref="Parry"/> an recalculate <see cref="PointsRemain"/>.
        /// </summary>
        /// <param name="points">points to use.</param>
        public void ModifyParry(int points)
        {
            Parry[0] = 3 + points / 2;
            Parry[1] = points;
        }

        /// <summary>
        /// Modify's <see cref="GS"/> an recalculate <see cref="PointsRemain"/>.
        /// </summary>
        /// <param name="points">points to use.</param>
        public void ModifyGs(int points)
        {
            GS[0] = 3 + points / 2;
            GS[1] = points;
        }

        /// <summary>
        /// Resets the instance of this class.
        /// </summary>
        public void ResetTool()
        {
            PointsTotal = 0;
            TP = new int[] { 4, 0 };
            Attack = new int[] { 12, 0 };
            Parry = new int[] { 3, 0 };
            GS = new int[] { 3, 0 };
        }
    }
}