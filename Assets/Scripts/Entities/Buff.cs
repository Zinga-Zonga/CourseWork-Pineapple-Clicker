namespace Assets.Scripts.Entities
{
    internal class Buff : IBuff, IEntity
    {
        public int Id { get; set; }
        public int UpgradesBuidlingsId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }

    }
}
