namespace Assets.Scripts.Entities
{
    internal interface IBuilding
    {
        float BaseCost { get; set; }
        float BaseScorePerSecond { get; set; }
        int Level { get; set; }
        string Name { get; set; }
        float Price { get; set; }
        float TotalScorePerSecond { get; set; }
    }
}