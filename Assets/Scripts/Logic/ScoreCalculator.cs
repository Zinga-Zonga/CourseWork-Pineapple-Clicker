using System;

namespace Assets.Scripts.Logic
{
    internal class ScoreCalculator : IScoreCalculator
    {
        BuildingService _buildingService = new BuildingService();

        public void CalculateBuildingsScoresSum()
        {
            float sum = 0;
            foreach (var building in _buildingService.GetAll())
            {
                sum += (float)building.TotalScorePerSecond;
            }
            PlayerStats.ScoresPerSecond = (float)Math.Round(sum,2);
            
        }
    }
}
