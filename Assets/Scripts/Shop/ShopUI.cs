using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private Transform businessItemsContainer;
    [SerializeField] private GameObject businessItemPrefab;
    [SerializeField] private TextMeshProUGUI selectedBusinessNameText;
    [SerializeField] private TextMeshProUGUI selectedBusinessDescText;
    [SerializeField] private TextMeshProUGUI selectedBusinessCostText;
    [SerializeField] private Button buyButton;

    private BusinessManager businessManager;
    private EconomyManager economyManager;
    private int selectedBusinessIndex = -1;
    private List<Button> businessButtons = new List<Button>();

    private void Start()
    {
        businessManager = GameManager.Instance.GetComponent<BusinessManager>();
        economyManager = GameManager.Instance.GetComponent<EconomyManager>();

        PopulateShop();
    }

    private void PopulateShop()
    {
        List<BusinessData> availableBusinesses = businessManager.GetAvailableBusinesses();

        for (int i = 0; i < availableBusinesses.Count; i++)
        {
            GameObject item = Instantiate(businessItemPrefab, businessItemsContainer);
            Button btn = item.GetComponent<Button>();
            int index = i; // Local copy for closure

            businessButtons.Add(btn);
            btn.onClick.AddListener(() => SelectBusiness(index));

            TextMeshProUGUI nameText = item.GetComponentInChildren<TextMeshProUGUI>();
            if (nameText != null)
                nameText.text = $"{availableBusinesses[i].name}\n${availableBusinesses[i].baseCost:F0}";
        }

        if (buyButton != null)
            buyButton.onClick.AddListener(BuySelectedBusiness);
    }

    private void SelectBusiness(int index)
    {
        selectedBusinessIndex = index;
        BusinessData business = businessManager.GetAvailableBusinesses()[index];

        selectedBusinessNameText.text = business.name;
        selectedBusinessDescText.text = business.description;
        selectedBusinessCostText.text = $"Cost: ${business.baseCost:F0}";

        // Highlight selected button
        foreach (Button btn in businessButtons)
            btn.interactable = true;
        businessButtons[index].interactable = false;
    }

    private void BuySelectedBusiness()
    {
        if (selectedBusinessIndex >= 0)
        {
            businessManager.PurchaseBusiness(selectedBusinessIndex);
        }
    }
}
