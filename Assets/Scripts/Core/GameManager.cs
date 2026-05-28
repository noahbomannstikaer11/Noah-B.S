using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private float initialMoney = 1000f;
    
    private EconomyManager economyManager;
    private BusinessManager businessManager;
    private UIManager uiManager;
    private PlayerController playerController;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeManagers();
    }

    private void InitializeManagers()
    {
        economyManager = GetComponent<EconomyManager>();
        businessManager = GetComponent<BusinessManager>();
        uiManager = FindObjectOfType<UIManager>();
        playerController = FindObjectOfType<PlayerController>();

        if (economyManager != null)
            economyManager.Initialize(initialMoney);

        if (businessManager != null)
            businessManager.Initialize();

        if (uiManager != null)
            uiManager.Initialize();
    }

    private void Update()
    {
        // Update game logic
        if (businessManager != null)
            businessManager.UpdateBusinesses(Time.deltaTime);
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        // Implement game over logic
    }
}
