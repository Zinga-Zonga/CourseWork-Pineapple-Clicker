namespace Assets.Scripts.Entities
{
    internal interface IBuff
    {
        string Description { get; set; }
        string Name { get; set; }
        float Price { get; set; }
    }
}