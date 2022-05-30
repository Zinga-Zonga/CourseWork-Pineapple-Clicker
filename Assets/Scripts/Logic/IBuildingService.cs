using Assets.Scripts.Entities;
using System.Collections.Generic;

namespace Assets.Scripts.Logic
{
    internal interface IBuildingService
    {
        void Add(Building building);
        IEnumerable<Building> GetAll();
        void Remove(int id);
        void UpgradeBuilding(int id);

        public void GenerateDefaultBuildings();
    }
}