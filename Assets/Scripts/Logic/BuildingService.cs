using Assets.Scripts.Data;
using Assets.Scripts.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    internal class BuildingService : IBuildingService
    {
        private readonly IBuildingsFactory _buildingsFactory = new BuildingsFactory();
        private readonly IUnitOfWork<Building> _unitOfWork = new UnitOfWork<Building>();

        private readonly IRepository<Building> _buildingsRepository;



        public BuildingService()
        {
            _buildingsRepository = _unitOfWork.Repository;
        }
        public void Add(Building entity)
        {
            _buildingsRepository.Add(entity);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<Building> GetAll()
        {
            return _buildingsRepository.GetAll();
        }

        public void Remove(int id)
        {
            _buildingsRepository.Remove(id);
        }
        public void GenerateDefaultBuildings()
        {
            string[] upgradesNames = { "Cursour", "Palm", "Sloth", "Papuan", "Monkey", "Raccoon", "Andrealla" };
            float[] baseScorePerSecond = { 0.1f, 0.5f, 4.0f, 10.0f, 40.0f, 100.0f, 666.0f };
            float[] baseCosts = { 15, 100, 500, 3000, 10000, 40000, 150000 };


            List<Building> _defaultBuildings = new List<Building>();
            for (int i = 0; i < upgradesNames.Length; i++)
            {
                Building building = _buildingsFactory
                    .CreateBuilding(upgradesNames[i], baseCosts[i], baseScorePerSecond[i]);
                building.Id = i;
                building.buffs = new List<Buff>();
                _defaultBuildings.Add(building);
            }

            var json = JsonConvert.SerializeObject(_defaultBuildings, Formatting.Indented,
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });

            string path = _defaultBuildings.GetType().GenericTypeArguments[0].Name;
            using (var f = File.CreateText(Application.persistentDataPath + $"/{path}.json"))
            {
                f.Write(json);
            }

        }
        public bool BuffBuilding(int id, float buffPrice)
        {
            var building = _buildingsRepository.GetById(id);
            if (PlayerStats.TotalScore >= buffPrice)
            {
                if(building.Id == 0)
                {
                    PlayerStats.ClickCost *= 2;
                }

                PlayerStats.TotalScore -= buffPrice;

                building.BaseScorePerSecond *= 2;
                building.CalculateScorePerSecond();
                _unitOfWork.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public void RemoveBuffFromBuildingList(int buildingId, int buffId)
        {
            var building = _buildingsRepository.GetById(buildingId);
            if (building != null)
            {
                building.buffs[buffId] = null;
            }
            _unitOfWork.SaveChanges();
        }
        public void UpgradeBuilding(int id)
        {
            
            var building = _buildingsRepository.GetById(id);
            if (PlayerStats.TotalScore >= building.Price)
            {
                PlayerStats.TotalScore -= building.Price;
                building.Buy();
                var buff = GenerateBuff(building);
                if (buff != null)
                {
                    building.buffs.Add(buff);
                }
                _unitOfWork.SaveChanges();
            }
            else
            {
                Debug.Log("No enougt money!");
            }
        }
        private Buff GenerateBuff(Building buffTarget)
        {
            Buff buff = new Buff();
            
            string[] adjactives = { "Domineering",
  "Giant",
  "Awful",
  "Incandescent",
  "Frequent",
  "Handsomely",
  "White",
  "Makeshift",
  "Nauseating",
  "Gifted",
  "Defeated",
  "Boiling",
  "Terrible",
  "Necessary",
  "Flawless",
  "Thankful",
  "Oafish",
  "Subdued",
  "Asleep",
  "Thick",
  "Torpid",
  "Lackadaisical",
  "Valuable",
  "Statuesque",
  "Delirious",
  "One",
  "Sudden",
  "Ratty",
  "Fine",
  "Protective",
  "Dapper",
  "Absurd",
  "Equable",
  "Meek",
  "Decent",
  "Unequal",
  "Typical",
  "Scandalous",
  "Famous",
  "Shivering",
  "Ripe",
  "Tense",
  "Eminent",
  "Hot",
  "Elite",
  "Pale",
  "Royal",
  "Weary",
  "Married",
  "Aggressive",
  "Delightful",
  "Voracious",
  "Ambiguous",
  "Unbiased",
  "Stereotyped",
  "Overt",
  "Green",
  "Proud",
  "Sordid",
  "Visible",
  "Wiggly",
  "Hellish",
  "Rainy",
  "Whispering",
  "Abashed",
  "Quick",
  "Two",
  "Petite",
  "Impressive",
  "Deafening",
  "Old-Fashioned",
  "Brash",
  "Enormous",
  "Loose",
  "Fine",
  "Tight",
  "Staking",
  "Suspicious",
  "Smooth",
  "Laughable",
  "Bored",
  "Unable",
  "Lying",
  "Disillusioned",
  "Horrible",
  "Hysterical",
  "Brown",
  "Profuse",
  "Hilarious",
  "Cheerful",
  "Harsh",
  "Undesirable",
  "Brainy",
  "Round",
  "Snotty",
  "Quixotic",
  "Broken",
  "Harmonious",
  "Curvy",
  "Nifty",
  "Dynamic",
  "Deserted",
  "Grey",
  "Probable",
  "Thundering",
  "Sweltering",
  "Abnormal",
  "Wet",
  "Outstanding",
  "Symptomatic",
  "Brainy",
  "Silly",
  "Absorbed",
  "Productive",
  "Nostalgic",
  "Enchanting",
  "Industrious",
  "Square",
  "Fluffy",
  "Brief",
  "Super",
  "Parsimonious",
  "Energetic",
  "Shiny",
  "Lyrical",
  "Miscreant",
  "Misty",
  "Dependent",
  "Honorable",
  "Previous",
  "Financial",
  "Infamous",
  "Spooky",
  "Thirsty",
  "Jealous",
  "Combative",
  "Neat",
  "Steep",
  "Modern",
  "Untidy",
  "Clever",
  "Grateful",
  "Shaky",
  "Sour",
  "Frightening",
  "Yellow",
  "Administrative",
  "Unfair",
  "Marvelous",
  "Wholesale",
  "Rigid",
  "Nutritious",
  "Careless",
  "Swift",
  "Quaint",
  "Fortunate",
  "Fertile",
  "Eager",
  "Enchanting",
  "Lonely",
  "Safe",
  "Jealous",
  "Severe",
  "Seemly",
  "Green",
  "Tangible",
  "Left",
  "Rotten",
  "Lying",
  "Level",
  "Cheap",
  "Gruesome",
  "Lovely",
  "Periodic",
  "Feeble",
  "Equal",
  "Bad",
  "Bloody",
  "Unarmed",
  "Glossy",
  "Hushed",
  "Evasive",
  "Logical",
  "Open",
  "Ripe",
  "Spotless",
  "Witty",
  "Quarrelsome",
  "Melted",
  "Obscene",
  "Disagreeable",
  "Far",
  "Flashy",
  "Mere",
  "Understood",
  "Milky",
  "Cute",
  "Mental",
  "Boorish",
  "Rabid"};
            int rand = Random.Range(0, adjactives.Length - 1);

            if (buffTarget.Level >= 50 && buffTarget.Level % 50 == 0)
            {
                buff.Id = buffTarget.buffs.Count;
                buff.UpgradesBuidlingsId = buffTarget.Id;

                buff.Name = $"{adjactives[rand]} {buffTarget.Name}";

                if(buffTarget.Id == 0)
                {
                    buff.Description = $"{buffTarget.Name} scores per second and click cost x2";
                }
                else
                {
                    buff.Description = $"{buffTarget.Name} scores per second x2";
                }

                buff.Price = buffTarget.Price * 100;
                return buff;

            }
            else if (buffTarget.Level == 10 || buffTarget.Level == 25)
            {
                buff.Id = buffTarget.buffs.Count;
                buff.UpgradesBuidlingsId = buffTarget.Id;

                buff.Name = $"{adjactives[rand]} {buffTarget.Name}";
                buff.Description = $"{buffTarget.Name} scores per second x2";
                buff.Price = buffTarget.Price * 100;
                return buff;

            }
            else
            {
                return null;
            }
        }

    }
}
