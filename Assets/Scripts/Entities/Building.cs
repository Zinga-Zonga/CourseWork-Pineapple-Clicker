using System;
using System.Collections.Generic;

namespace Assets.Scripts.Entities
{
    internal class Building : IBuilding, IEntity, IBuyable, IBuffable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public float BaseCost { get; set; }
        public float BaseScorePerSecond { get; set; }
        public float TotalScorePerSecond { get; set; }
        public float Price { get; set; }
        public List<Buff> buffs { get; set; }

        private float _multiplier = 1.15f;

        
        public void CalculateScorePerSecond()
        {
            TotalScorePerSecond = Level*BaseScorePerSecond;
        }
        private void CalculatePrice()
        {
            Price = (float)Math.Round(BaseCost * Math.Pow(_multiplier, Level), 1);
        }

        public void Buy()
        {
            Level += 1;
            CalculatePrice();
            CalculateScorePerSecond();
        }
    }
}
