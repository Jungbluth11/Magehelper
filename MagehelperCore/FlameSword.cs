namespace Magehelper.Core;

public class FlameSword
{
    private readonly Core _core = Core.Instance;
    /// <summary>
    /// THe points that be used in total.
    /// </summary>
    public int PointsTotal { get; set; }
    /// <summary>
    /// the points are left to use.
    /// </summary>
    public int PointsRemain => PointsTotal - Attack[1] - Parry[1] - Gs[1] - Tp[1];
    public int[] Tp { get; private set; } = [4, 0];
    public int[] Attack { get; private set; } = [12, 0];
    public int[] Parry { get; private set; } = [3, 0];
    public int[] Gs { get; private set; } = [3, 0];
    /// <summary>
    /// Maximum amount of RkP* to increase Attack
    /// </summary>
    public int AttackMaxPoints => 6;
    /// <summary>
    /// Maximum amount of RkP* to increase Parry
    /// </summary>
    public int ParryMaxPoints = 10;
    /// <summary>
    /// Maximum amount of RkP* to increase GS
    /// </summary>
    public int GsMaxPoints = 10;
    /// <summary>
    /// Constructor
    /// </summary>
    public FlameSword()
    {
        _core.FlameSword = this;
    }

    /// <summary>
    /// Rolls the activation of this Ritual.
    /// </summary>
    /// <returns>An result of<see cref="DSAUtils.DSA.TaP"/></returns>
    public (int, int[], string) RollActivation()
    {
        try
        {
            int MU = _core.Character!.Mu;
            int IN = _core.Character.In;
            int GE = _core.Character.Ge;
            int TaW = _core.Character.Skills!.Single(a => a.Name == "Ritualkenntnis: Gildenmagie").Wert;
            return DSA.TaP(MU, IN, GE, TaW);
        }
        catch
        {
            throw new Exception("No Character Loaded");
        }
    }

    /// <summary>
    /// Modify's <see cref="Tp"/> and recalculate <see cref="PointsRemain"/>.
    /// </summary>
    /// <param name="points">points to use.</param>
    public void ModifyTp(int points)
    {
        Tp[0] = 4 + points / 2;
        Tp[1] = points;
    }

    /// <summary>
    /// Modify's <see cref="Attack"/> and recalculate <see cref="PointsRemain"/>.
    /// </summary>
    /// <param name="points">points to use.</param>
    public void ModifyAttack(int points)
    {
        Attack[0] = 12 + points;
        Attack[1] = points;
    }

    /// <summary>
    /// Modify's <see cref="Parry"/> and recalculate <see cref="PointsRemain"/>.
    /// </summary>
    /// <param name="points">points to use.</param>
    public void ModifyParry(int points)
    {
        Parry[0] = 3 + points / 2;
        Parry[1] = points;
    }

    /// <summary>
    /// Modify's <see cref="Gs"/> and recalculate <see cref="PointsRemain"/>.
    /// </summary>
    /// <param name="points">points to use.</param>
    public void ModifyGs(int points)
    {
        Gs[0] = 3 + points / 2;
        Gs[1] = points;
    }

    /// <summary>
    /// Resets the instance of this class.
    /// </summary>
    public void ResetTool()
    {
        PointsTotal = 0;
        Tp = [4, 0];
        Attack = [12, 0];
        Parry = [3, 0];
        Gs = [3, 0];
    }
}
