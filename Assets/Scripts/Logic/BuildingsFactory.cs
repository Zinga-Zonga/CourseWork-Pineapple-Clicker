using Assets.Scripts.Entities;

namespace Assets.Scripts.Logic
{
    internal class BuildingsFactory : IBuildingsFactory
    {
        public Building CreateBuilding(string buildingName, float baseCost, float baseScorePerSecond)
        {
            Building building = new Building();

            building.Id = 0;
            building.Name = buildingName;
            building.Level = 0;
            building.BaseCost = baseCost;
            building.BaseScorePerSecond = baseScorePerSecond;
            building.Price = baseCost;
            return building;
        }
    }
}
