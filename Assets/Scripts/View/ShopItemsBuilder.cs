using Assets.Scripts.Entities;
using Assets.Scripts.Logic;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemsBuilder : MonoBehaviour
{
    
    public RectTransform prefab;
    public RectTransform content;

    BuildingService _buildingsService = new BuildingService();
    IScoreCalculator _scoreCalculator = new ScoreCalculator();


    public class ShopItemModel
    {
        public int upgradesBuildingId;
        public int buffOwnId;
        public Sprite sprite;
        public string name = "";
        public string level = "";
        public string price = "";
        public string baseScorePerSecond = "";
        public float floatPrice;

        public delegate void UpgadeButtonClicked();
        public UpgadeButtonClicked upgadeButtonClickDelegate;

        public delegate void UpgradeBuildingDelegate(int id);
        public UpgradeBuildingDelegate UpgadeBuilding;

        public delegate void BuyBuff(int buildingsId, int buffsOwnId, float price);
        public BuyBuff BuyBuffDelegate;

    }
    public class ShopItemView
    {

        public Image upgradeImage;
        public Text upgradeName;
        public Text upgradeLevel;
        public Text upgradeBaseScorePerSecond;
        public Button buyButton;


        public ShopItemView(Transform rootView)
        {
         
            upgradeName = rootView.Find("UpgradeName").GetComponent<Text>();
            upgradeImage = rootView.Find("UpgradeImage").GetComponent<Image>();
            upgradeLevel = rootView.Find("UpgradeLevel").GetComponent<Text>();
            upgradeBaseScorePerSecond = rootView.Find("BaseScorePerSecText").GetComponent<Text>();
            buyButton = rootView.Find("BuyButton").GetComponent<Button>();
        }
    }
    List<ShopItemModel> GetModelsFromBuildings(IEnumerable<Building> buildingList)
    {
        List<Building> buildings = new List<Building>();
        buildings = (List<Building>)buildingList;

        Sprite[] sprites;
        sprites = Resources.LoadAll<Sprite>("NatureSprites");

        if(sprites.Length != buildings.Count)
        {
            Debug.Log("No enought sprites or buildings! I'd know how to check this problem sorry!" +
                " P.S. I hope this log help U!!");
            return null;
        }

        var models = new List<ShopItemModel>();
        for(int i = 0; i < buildings.Count; i++)
        {
            

            ShopItemModel model = new ShopItemModel();
            model.upgradesBuildingId = buildings[i].Id;
            model.sprite = sprites[i];
            model.name = buildings[i].Name;
            model.price = $"Cost: {buildings[i].Price.ToString("N2")}";
            model.floatPrice = buildings[i].Price;
            model.level = $"Level: {buildings[i].Level}";
            model.baseScorePerSecond = $"Gain: {buildings[i].BaseScorePerSecond}/sec";

            model.UpgadeBuilding = _buildingsService.UpgradeBuilding;
            model.BuyBuffDelegate = Skip;
            model.upgadeButtonClickDelegate = LoadBuildingItems;
            model.upgadeButtonClickDelegate += _scoreCalculator.CalculateBuildingsScoresSum;

            models.Add(model);
        }
        return models;
    }
    List<ShopItemModel> GetModelsFromBuffs(IEnumerable<Building> buildingList)
    {
        List<Building> buildings = new List<Building>();
        buildings = (List<Building>)buildingList;

        Sprite[] sprites;
        sprites = Resources.LoadAll<Sprite>("BuffsSprites");

        if (sprites.Length != buildings.Count)
        {
            Debug.Log("No enought sprites or buildings! I'd know how to check this problem sorry!" +
                " P.S. I hope this log help U!!");
            return null;
        }

        var models = new List<ShopItemModel>();
        foreach (var building in buildings)
        {
            for(int i = 0; i < building.buffs.Count; i++)
            {
                if(building.buffs.Count == 0)
                {
                    return models;
                }
                if(building.buffs[i] == null)
                {
                    continue;
                }
                ShopItemModel model = new ShopItemModel();


                model.upgradesBuildingId = building.buffs[i].UpgradesBuidlingsId;
                model.buffOwnId = building.buffs[i].Id;

                model.sprite = sprites[model.upgradesBuildingId];

                model.name = building.buffs[i].Name;
                model.baseScorePerSecond = building.buffs[i].Description;
                model.price = $"Cost: {building.buffs[i].Price.ToString("N2")}";
                model.floatPrice = building.buffs[i].Price;

                model.UpgadeBuilding = Skip;
                model.BuyBuffDelegate = BuffBuilding;
                model.upgadeButtonClickDelegate = LoadBuffsItems;
                model.upgadeButtonClickDelegate += _scoreCalculator.CalculateBuildingsScoresSum;

                models.Add(model);
            }
        }
        return models;
        

        
    }

    public void OnRecivedModels(List<ShopItemModel> models)
    {
        foreach (RectTransform child in content)
        {
            Destroy(child.gameObject);
        }
        foreach (ShopItemModel model in models)
        {
            var instance = Instantiate(prefab.gameObject);
            instance.transform.SetParent(content, false);
            InitializeItemView(instance, model);
        }
    }
    void InitializeItemView(GameObject viewGameObject, ShopItemModel model)
    {
        ShopItemView item = new ShopItemView(viewGameObject.transform);
        item.upgradeImage.sprite = model.sprite;
        item.upgradeName.text = model.name;
        item.upgradeLevel.text = model.level;
        item.buyButton.GetComponentInChildren<Text>().text = model.price;
        item.upgradeBaseScorePerSecond.text = model.baseScorePerSecond;
        item.buyButton.onClick.AddListener(
            (UnityEngine.Events.UnityAction)(() =>
            {
                Console.WriteLine("Button Clicked!");

                model.UpgadeBuilding(model.upgradesBuildingId);
                model.BuyBuffDelegate(model.upgradesBuildingId, model.buffOwnId, model.floatPrice);
                model.upgadeButtonClickDelegate();
            })
            );
    }

    public void BuffBuilding(int buildingsId,  int buffsOwnId, float price)
    {
        if(_buildingsService.BuffBuilding(buildingsId, price) == true)
        {
            _buildingsService.RemoveBuffFromBuildingList(buildingsId, buffsOwnId);
        }
    }
    public void LoadBuildingItems()
    {
        OnRecivedModels(GetModelsFromBuildings(_buildingsService.GetAll()));
    }
    public void LoadBuffsItems()
    {
        OnRecivedModels(GetModelsFromBuffs(_buildingsService.GetAll()));
    }
    private void Skip(int id,int buffsOwnId, float price)
    {
        Debug.Log("MockFunctionForBuffDelegate");
    }
    private void Skip(int id)
    {
        Debug.Log("MockFunctionForUpgrade");
    }
}
