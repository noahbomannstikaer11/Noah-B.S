using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI totalEarningsText;
    [SerializeField] private TextMeshProUGUI businessCountText;
    [SerializeField] private Transform businessListContainer;
    [SerializeField] private GameObject businessItemPrefab;
    [SerializeField] private Button shopButton;
    [SerializeField] private GameObject shopPanel;

    private EconomyManager economyManager;
    private BusinessManager businessManager;
    private bool shopPanelActive = false;

    public void Initialize()
    {
        economyManager = GameManager.Instance.GetComponent<EconomyManager>();
        businessManager = GameManager.Instance.GetComponent<BusinessManager>();

        if (economyManager != null)
        {
            economyManager.OnMoneyChanged += UpdateMoneyDisplay;
        }

        if (businessManager != null)
        {
            businessManager.OnBusinessPurchased += OnBusinessPurchased;
            businessManager.OnBusinessUpdated += UpdateBusinessDisplay;
        }

        if (shopButton != null)
            shopButton.onClick.AddListener(ToggleShop);

        UpdateMoneyDisplay(economyManager.CurrentMoney);
        UpdateBusinessDisplay();
    }

    private void UpdateMoneyDisplay(float money)
    {
        if (moneyText != null)
            moneyText.text = $"Money: ${money:F2}";
    }

    private void UpdateBusinessDisplay()
    {
        if (businessCountText != null)
            businessCountText.text = $"Businesses: {businessManager.OwnedBusinesses.Count}";

        if (totalEarningsText != null)
            totalEarningsText.text = $"Total Earnings: ${economyManager.TotalEarnings:F2}";
    }

    private void OnBusinessPurchased(Business business)
    {
        UpdateBusinessDisplay();
        SpawnBusinessItem(business);
    }

    private void SpawnBusinessItem(Business business)
    {
        if (businessItemPrefab == null || businessListContainer == null)
            return;

        GameObject item = Instantiate(businessItemPrefab, businessListContainer);
        // Configure business item UI here
    }

    private void ToggleShop()
    {
        if (shopPanel != null)
        {
            shopPanelActive = !shopPanelActive;
            shopPanel.SetActive(shopPanelActive);
        }
    }

    private void OnDestroy()
    {
        if (economyManager != null)
            economyManager.OnMoneyChanged -= UpdateMoneyDisplay;
    }
}
