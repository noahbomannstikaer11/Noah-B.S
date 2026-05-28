using UnityEngine;
using System.Collections.Generic;
using System;

public class BusinessManager : MonoBehaviour
{
    [SerializeField] private List<BusinessData> availableBusinesses = new List<BusinessData>();
    [SerializeField] private List<Business> ownedBusinesses = new List<Business>();

    private EconomyManager economyManager;
    private int businessIdCounter = 0;

    public event Action<Business> OnBusinessPurchased;
    public event Action<Business> OnBusinessUpgraded;
    public event Action OnBusinessUpdated;

    public List<Business> OwnedBusinesses => ownedBusinesses;

    public void Initialize()
    {
        economyManager = GetComponent<EconomyManager>();
        InitializeBusinessData();
    }

    private void InitializeBusinessData()
    {
        // Small businesses - 10x priser
        availableBusinesses.Add(new BusinessData
        {
            name = "Lemonade Stand",
            description = "En lille limonadekiosk",
            baseCost = 1000f,
            baseIncome = 5f,
            incomeInterval = 3f,
            type = BusinessType.Kiosk
        });

        availableBusinesses.Add(new BusinessData
        {
            name = "Hot Dog Stand",
            description = "En hotdog vogn",
            baseCost = 2500f,
            baseIncome = 15f,
            incomeInterval = 5f,
            type = BusinessType.Kiosk
        });

        availableBusinesses.Add(new BusinessData
        {
            name = "Small Shop",
            description = "En lille butik",
            baseCost = 5000f,
            baseIncome = 40f,
            incomeInterval = 8f,
            type = BusinessType.Shop
        });

        availableBusinesses.Add(new BusinessData
        {
            name = "Supermarket",
            description = "En supermarked",
            baseCost = 15000f,
            baseIncome = 150f,
            incomeInterval = 10f,
            type = BusinessType.Store
        });

        availableBusinesses.Add(new BusinessData
        {
            name = "Restaurant",
            description = "En restaurant",
            baseCost = 20000f,
            baseIncome = 200f,
            incomeInterval = 12f,
            type = BusinessType.Restaurant
        });
    }

    public bool PurchaseBusiness(int businessIndex)
    {
        if (businessIndex < 0 || businessIndex >= availableBusinesses.Count)
        {
            Debug.LogError("Invalid business index");
            return false;
        }

        BusinessData data = availableBusinesses[businessIndex];

        if (!economyManager.SpendMoney(data.baseCost))
        {
            Debug.LogWarning($"Cannot afford {data.name}");
            return false;
        }

        Business newBusiness = new Business
        {
            id = businessIdCounter++,
            data = data,
            level = 1,
            currentIncome = data.baseIncome,
            purchaseTime = Time.time
        };

        ownedBusinesses.Add(newBusiness);
        OnBusinessPurchased?.Invoke(newBusiness);
        return true;
    }

    public bool UpgradeBusiness(int businessId)
    {
        Business business = ownedBusinesses.Find(b => b.id == businessId);
        if (business == null)
        {
            Debug.LogError($"Business with ID {businessId} not found");
            return false;
        }

        float upgradeCost = business.data.baseCost * 0.5f * business.level;
        if (!economyManager.SpendMoney(upgradeCost))
        {
            Debug.LogWarning($"Cannot afford upgrade for {business.data.name}");
            return false;
        }

        business.level++;
        business.currentIncome = business.data.baseIncome * (1f + (business.level - 1) * 0.25f);
        OnBusinessUpgraded?.Invoke(business);
        return true;
    }

    public void UpdateBusinesses(float deltaTime)
    {
        foreach (Business business in ownedBusinesses)
        {
            business.timeSinceLastIncome += deltaTime;
            if (business.timeSinceLastIncome >= business.data.incomeInterval)
            {
                float income = business.currentIncome;
                economyManager.EarnMoney(income);
                business.totalIncome += income;
                business.timeSinceLastIncome = 0f;
                OnBusinessUpdated?.Invoke();
            }
        }
    }

    public List<BusinessData> GetAvailableBusinesses()
    {
        return availableBusinesses;
    }
}

public class Business
{
    public int id;
    public BusinessData data;
    public int level = 1;
    public float currentIncome;
    public float totalIncome = 0f;
    public float timeSinceLastIncome = 0f;
    public float purchaseTime;
}

public class BusinessData
{
    public string name;
    public string description;
    public float baseCost;
    public float baseIncome;
    public float incomeInterval;
    public BusinessType type;
}

public enum BusinessType
{
    Kiosk,
    Shop,
    Store,
    Restaurant,
    Office
}
