using UnityEngine;
using TMPro;

public class CashDrawer : MonoBehaviour
{
    public static CashDrawer Instance;

    public GameObject drawerPanel;      
    public TextMeshProUGUI givenText;   
    public TextMeshProUGUI changedText; 

    private float totalOrderCost;
    private float amountGivenByCustomer;
    private float currentChangeSelected;

    void Awake()
    {
        Instance = this;
        drawerPanel.SetActive(false);
    }

    public void OpenDrawer()
    {
        if (!CustomerManager.Instance.IsGameRunning()) return;

        totalOrderCost = OrderManager.Instance.currentTotal;
        if (totalOrderCost <= 0) return;

        CalculatePayment();
        currentChangeSelected = 0;
        UpdateUI();

        drawerPanel.SetActive(true);
    }

    void CalculatePayment()
    {
        if (totalOrderCost <= 20) amountGivenByCustomer = 20;
        else if (totalOrderCost <= 50) amountGivenByCustomer = 50;
        else if (totalOrderCost <= 100) amountGivenByCustomer = 100;
        else if (totalOrderCost <= 200) amountGivenByCustomer = 200;
        else if (totalOrderCost <= 500) amountGivenByCustomer = 500;
        else amountGivenByCustomer = 1000; 

        if (amountGivenByCustomer < totalOrderCost) amountGivenByCustomer = totalOrderCost;

        if (givenText != null)
            givenText.text = "Given: " + amountGivenByCustomer.ToString("F2");
    }

    public void AddChange(float amount)
    {
        if (!CustomerManager.Instance.IsGameRunning()) return;

        currentChangeSelected += amount;
        UpdateUI();
    }
    
    public void ResetChange()
    {
        if (!CustomerManager.Instance.IsGameRunning()) return;

        currentChangeSelected = 0;
        UpdateUI();
    }

    void UpdateUI()
    {
        if(changedText != null)
            changedText.text = "Changed: " + currentChangeSelected.ToString("F2");
    }

    public void SubmitTransaction()
    {
        if (!CustomerManager.Instance.IsGameRunning()) return;

        float neededChange = amountGivenByCustomer - totalOrderCost;

        if (Mathf.Abs(currentChangeSelected - neededChange) < 0.1f)
        {
            CustomerManager.Instance.AddPoints(10);
            CustomerManager.Instance.AddMoney(totalOrderCost);

            drawerPanel.SetActive(false); 
            if(givenText != null) 
                givenText.text = "Given:";

            OrderManager.Instance.SubmitOrder(); 
        }
        else
        {
            CustomerManager.Instance.AddPoints(-5);
            ResetChange(); 
        }
    }
}