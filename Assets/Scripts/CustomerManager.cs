using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance;

    public List<ItemData> currentWantedItems;
    public List<ItemData> availableItems;

    public GameObject girlCustomerObject;
    public GameObject boyCustomerObject;
    private GameObject currentCustomerInstance;

    public Transform receiptItemContainer;
    public GameObject customerReceiptItemPrefab;

    public TMP_Text timerText;
    public int totalTime = 120;
    private float timer;
    private bool gameRunning = true;

    public TMP_Text scoreText;
    public int score = 0;

    [Header("Store Info")]
    public TMP_Text moneyEarnedText;
    public float moneyEarned = 0f;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public TMP_Text finalScoreText;
    public TMP_Text finalMoneyText;

    [Header("UI References")]
    public CanvasGroup gameplayUI; 
    public AudioSource backgroundMusic; 

    [Header("Audio")]
    public AudioSource moneyTickSound;

    [Header("Game Over UI Buttons")]
    public UnityEngine.UI.Button mainMenuButton;
    public UnityEngine.UI.Button restartButton;

    public void OnMainMenuButton()
{
    SceneManager.LoadScene("Lobby"); 
}

public void OnRestartButton()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
}

    void Awake()
    {
        Instance = this;
        currentWantedItems = new List<ItemData>();
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    void Start()
    {
        timer = totalTime;
        if (girlCustomerObject != null) girlCustomerObject.SetActive(false);
        if (boyCustomerObject != null) boyCustomerObject.SetActive(false);

        UpdateScoreUI();
        UpdateTimerUI();
        UpdateMoneyUI();
        NextCustomer();
    }

    void Update()
    {
        if (!gameRunning) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 0;
            gameRunning = false;
            EndGame();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int seconds = Mathf.CeilToInt(timer);
        if(timerText != null)
            timerText.text = "Time: " + seconds.ToString();
    }

    public void AddPoints(int amount)
    {
        if (!gameRunning) return;
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void AddMoney(float amount)
    {
        if (!gameRunning) return;
        moneyEarned += amount;
        UpdateMoneyUI();
    }

    public void UpdateMoneyUI()
    {
        if (moneyEarnedText != null)
            moneyEarnedText.text = "Money Earned: ₱" + moneyEarned.ToString("F2");
    }

void EndGame()
{
    if(gameplayUI != null)
    {
        gameplayUI.interactable = false;
        gameplayUI.blocksRaycasts = false;
    }

    if(gameOverPanel != null)
        gameOverPanel.SetActive(true);

    if(mainMenuButton != null) mainMenuButton.interactable = false;
    if(restartButton != null) restartButton.interactable = false;

    if(finalScoreText != null && finalMoneyText != null)
        StartCoroutine(AnimateScoreThenMoney(finalScoreText, score, finalMoneyText, moneyEarned, 3f));

    if (girlCustomerObject != null) girlCustomerObject.SetActive(false);
    if (boyCustomerObject != null) boyCustomerObject.SetActive(false);

    if(backgroundMusic != null)
        backgroundMusic.Pause();
}

System.Collections.IEnumerator AnimateScoreThenMoney(TMP_Text scoreText, int finalScore, TMP_Text moneyText, float finalMoney, float duration)
{
    float elapsed = 0f;
    int currentScore = 0;

    while (elapsed < duration)
    {
        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / duration);
        int newScore = Mathf.FloorToInt(Mathf.Lerp(0, finalScore, t));
        if (newScore != currentScore)
        {
            currentScore = newScore;
            scoreText.text = currentScore.ToString();
            if (moneyTickSound != null) moneyTickSound.Play(); // Play tick sound
        }
        yield return null;
    }
    scoreText.text = finalScore.ToString();

    yield return new WaitForSeconds(0.5f);

    elapsed = 0f;
    float currentAmount = 0f;
    float moneyDuration = 2f;

    while (elapsed < moneyDuration)
    {
        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / moneyDuration);
        float newAmount = Mathf.Lerp(0, finalMoney, t);
        if (Mathf.FloorToInt(newAmount) != Mathf.FloorToInt(currentAmount))
        {
            currentAmount = newAmount;
            moneyText.text = Mathf.FloorToInt(currentAmount).ToString("N0");
            if (moneyTickSound != null) moneyTickSound.Play();
        }
        yield return null;
    }
    moneyText.text = Mathf.FloorToInt(finalMoney).ToString("N0");
    
    if(mainMenuButton != null) mainMenuButton.interactable = true;
    if(restartButton != null) restartButton.interactable = true;
}

    public bool IsGameRunning()
    {
        return gameRunning;
    }

    public void NextCustomer()
    {
        if (!gameRunning) return;

        if (currentCustomerInstance != null)
        {
            currentCustomerInstance.SetActive(false);
            currentCustomerInstance = null;
        }

        currentWantedItems.Clear();

        if (receiptItemContainer != null)
        {
            foreach (Transform child in receiptItemContainer)
                Destroy(child.gameObject);
        }

        currentCustomerInstance = (Random.Range(0, 2) == 0) ? girlCustomerObject : boyCustomerObject;

        if (currentCustomerInstance != null)
            currentCustomerInstance.SetActive(true);

        int itemCount = Random.Range(1, 10);

        for (int i = 0; i < itemCount; i++)
        {
            if (availableItems.Count > 0)
            {
                int randomIndex = Random.Range(0, availableItems.Count);
                ItemData newItem = availableItems[randomIndex];

                currentWantedItems.Add(newItem);

                if (customerReceiptItemPrefab != null && receiptItemContainer != null)
                {
                    GameObject receiptGO = Instantiate(customerReceiptItemPrefab, receiptItemContainer);
                    CustomerReceiptItem receiptItem = receiptGO.GetComponent<CustomerReceiptItem>();
                    if (receiptItem != null)
                        receiptItem.Setup(newItem);
                }
            }
        }
    }
}