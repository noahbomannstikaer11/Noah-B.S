using UnityEngine;
using System;

public class EconomyManager : MonoBehaviour
{
    [SerializeField] private float currentMoney = 1000f;
    [SerializeField] private float totalEarnings = 0f;
    
    public event Action<float> OnMoneyChanged;
    public event Action<float> OnMoneyEarned;
    public event Action<float> OnMoneySpent;

    public float CurrentMoney => currentMoney;
    public float TotalEarnings => totalEarnings;

    public void Initialize(float startingMoney)
    {
        currentMoney = startingMoney;
        totalEarnings = 0f;
    }

    public bool SpendMoney(float amount)
    {
        if (amount > currentMoney)
        {
            Debug.LogWarning($"Insufficient funds! Need: {amount}, Have: {currentMoney}");
            return false;
        }

        currentMoney -= amount;
        OnMoneySpent?.Invoke(amount);
        OnMoneyChanged?.Invoke(currentMoney);
        return true;
    }

    public void EarnMoney(float amount)
    {
        currentMoney += amount;
        totalEarnings += amount;
        OnMoneyEarned?.Invoke(amount);
        OnMoneyChanged?.Invoke(currentMoney);
    }

    public float GetMoneyFormatted()
    {
        return Mathf.Round(currentMoney * 100f) / 100f;
    }

    public string GetMoneyString()
    {
        return $"${GetMoneyFormatted():F2}";
    }
}
