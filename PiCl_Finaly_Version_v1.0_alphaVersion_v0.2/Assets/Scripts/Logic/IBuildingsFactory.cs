using Assets.Scripts.Entities;

namespace Assets.Scripts.Logic
{
    internal interface IBuildingsFactory
    {
        Building CreateBuilding( string buildingName, float baseCost, float baseScorePerSecond);
    }
}