using System.Reflection;
using System.Text.Json;

namespace Magehelper.Core;

public class PetGenerator
{
    private int pointsUsed;
    private int baseCost;
    private int abilityCost;
    private int AdditionalMU { get; set; }
    private int AdditionalKL { get; set; }
    private int AdditionalIN { get; set; }
    private int AdditionalCH { get; set; }
    private int AdditionalFF { get; set; }
    private int AdditionalGE { get; set; }
    private int AdditionalKO { get; set; }
    private int AdditionalKK { get; set; }
    private int AdditionalLE { get; set; }
    private int AdditionalAE { get; set; }
    private int AdditionalAU { get; set; }
    private string _species;
    private bool _isMightyCompanion;
    private readonly Pet pet;
    private readonly PetData[] baseData;
    /// <summary>
    /// Amount of attribute points to increase at start in total.
    /// </summary>
    public int PointsTotal { get; private set; } = 20;
    /// <summary>
    /// Amount of attribute points to increase at start that be used.
    /// </summary>
    public int PointsRemain => PointsTotal - pointsUsed;
    /// <summary>
    /// The generating cost in AP.
    /// </summary>
    public int Cost => baseCost + abilityCost;
    public int MU
    {
        get => AdditionalMU + BaseData.MUStartMin;
        set => ModAttribute("MU", value);
    }
    public int KL
    {
        get => AdditionalKL + BaseData.KLStartMin;
        set => ModAttribute("KL", value);
    }
    public int IN
    {
        get => AdditionalIN + BaseData.INStartMin;
        set => ModAttribute("IN", value);
    }
    public int CH
    {
        get => AdditionalCH + BaseData.CHStartMin;
        set => ModAttribute("CH", value);
    }
    public int FF
    {
        get => AdditionalFF + BaseData.FFStartMin;
        set => ModAttribute("FF", value);
    }
    public int GE
    {
        get => AdditionalGE + BaseData.GEStartMin;
        set => ModAttribute("GE", value);
    }
    public int KO
    {
        get => AdditionalKO + BaseData.KOStartMin;
        set => ModAttribute("KO", value);
    }
    public int KK
    {
        get => AdditionalKK + BaseData.KKStartMin;
        set => ModAttribute("KK", value);
    }
    public int LE
    {
        get => AdditionalLE + BaseData.LEStartMin;
        set => ModAttribute("LE", value);
    }
    public int AE
    {
        get => AdditionalAE + BaseData.AEStartMin;
        set => ModAttribute("AE", value);
    }
    public int AU
    {
        get => AdditionalAU + BaseData.AUStartMin;
        set => ModAttribute("AU", value);
    }
    /// <summary>
    /// Get or Set the species.
    /// </summary>
    public string Species
    {
        get => _species;
        set
        {
            _species = value;
            BaseData = baseData.Single(p => p.Species == value);
            if (!_isMightyCompanion)
            {
                baseCost = BaseData.GeneratingCost.General;
            }
        }
    }
    /// <summary>
    /// if pet is a mighty companion of not.
    /// </summary>
    public bool IsMightyCompanion
    {
        get => _isMightyCompanion;
        set
        {
            _isMightyCompanion = value;
            PointsTotal = value ? 40 : 20;
            baseCost = value ? 120 : BaseData.GeneratingCost.General;
        }
    }
    /// <summary>
    /// Names of the pet species.
    /// </summary>
    public ReadOnlyCollection<string> SpeciesStrings { get; }
    /// <summary>
    /// Base data of the current species.
    /// </summary>
    public PetData BaseData { get; private set; }
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="pet"> an instance if <see cref="Pet"/></param>
#pragma warning disable CS8618 // program files are corrupted if petData is null
    public PetGenerator()
#pragma warning restore CS8618

    {
        pet = Core.GetInstance().Pet!;
        List<string> speciesStrings = [];
        List<PetData> baseData = [];
        string json = File.ReadAllText(Path.Combine(Core.GetInstance().SettingsPath, "petData.json"));
        dynamic[][] petData = JsonSerializer.Deserialize<dynamic[][]>(json);
        foreach (dynamic[] p in petData)
        {
            speciesStrings.Add(p[0].GetString());
            baseData.Add(new PetData
            {
                Species = p[0].GetString(),
                GeneratingCost = (p[1][0].GetInt32(), p[1][1].GetInt32(), p[1][2].GetInt32()),
                MUStartMin = p[2].GetInt32(),
                MUStartMax = p[3].GetInt32(),
                KLStartMin = p[4].GetInt32(),
                KLStartMax = p[5].GetInt32(),
                INStartMin = p[6].GetInt32(),
                INStartMax = p[7].GetInt32(),
                CHStartMin = p[8].GetInt32(),
                CHStartMax = p[9].GetInt32(),
                FFStartMin = p[10].GetInt32(),
                FFStartMax = p[11].GetInt32(),
                GEStartMin = p[12].GetInt32(),
                GEStartMax = p[13].GetInt32(),
                KOStartMin = p[14].GetInt32(),
                KOStartMax = p[15].GetInt32(),
                KKStartMin = p[16].GetInt32(),
                KKStartMax = p[17].GetInt32(),
                LEStartMin = p[18].GetInt32(),
                AEStartMin = p[19].GetInt32(),
                AUStartMin = p[20].GetInt32(),
                MRStart = p[21].GetInt32(),
                AttackStart = p[22].GetInt32(),
                AttackFlyingStart = p[23].GetInt32(),
                ParryStart = p[24].GetInt32(),
                ParryFlyingStart = p[25].GetInt32(),
                GSStart = p[26].GetDouble(),
                GSFlyingStart = p[27].GetDouble(),
                IsFlying = p[28].GetBoolean()
            });
        }
        SpeciesStrings = speciesStrings.AsReadOnly();
        this.baseData = baseData.ToArray();
        Species = baseData[0].Species;


    }

    /// <summary>
    /// Adds the generated pet.
    /// </summary>
    public void AddPet()
    {
        int[] attributes = new int[11];
        string[] attributeStrings = new[] { "MU", "KL", "IN", "CH", "FF", "GE", "KO", "KK", "LE", "AE", "AU" };
        if (pointsUsed <= PointsTotal)
        {
            for (int i = 0; i < 11; i++)
            {
                PropertyInfo p = GetAttribute(attributeStrings[i]);
#pragma warning disable CS8605
                attributes[i] = (int) p.GetValue(this);
#pragma warning restore CS8605
            }
            pet.AddPet(BaseData, _isMightyCompanion, attributes);
        }
    }

    private void ModAttribute(string attribute, int totalValue)
    {


#pragma warning disable CS8605
        string suffix = "StartMin";
        PropertyInfo attributeCurrent = GetAttribute("Additional" + attribute);
        PropertyInfo attributeBase = typeof(PetData).GetProperty(attribute + suffix);
        int value = totalValue - (int) attributeBase.GetValue(BaseData);
        int currentValue = (int) attributeCurrent.GetValue(this);
        int difference = value - currentValue;
        int modifier = BaseData.GeneratingCost.Ability;
        if (attribute == "LE" || attribute == "AE")
        {
            modifier = BaseData.GeneratingCost.LeAe;
        }
        attributeCurrent.SetValue(this, value);
        pointsUsed += difference;
        abilityCost += difference * modifier;


#pragma warning restore CS8605
    }

    private PropertyInfo GetAttribute(string attribute)
    {
#pragma warning disable CS8603
        return typeof(PetGenerator).GetProperty(attribute, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
#pragma warning restore CS8603
    }
}
