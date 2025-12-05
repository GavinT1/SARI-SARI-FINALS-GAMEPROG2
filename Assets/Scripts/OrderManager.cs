using UnityEngine;
using TMPro; 
using System.Collections.Generic; 
using System.Linq; 

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;

    public TextMeshProUGUI totalText;
    public TextMeshProUGUI changedText;
    public TextMeshProUGUI itemOrderListText;

    public float currentTotal = 0f; 
    public List<string> itemNames;

    void Awake()
    {
        Instance = this;
        itemNames = new List<string>();
    }

    void Start()
    {
        ClearOrder();
    }

    public void AddItemToOrder(ItemData item)
    {
        if (!CustomerManager.Instance.IsGameRunning()) return;
        if (item == null) return;

        currentTotal += item.price;
        itemNames.Add(item.itemName);
        UpdateUI();
    }

    void UpdateUI()
    {
        totalText.text = "Total: " + currentTotal.ToString("F2");

        if (itemOrderListText != null)
            itemOrderListText.text = string.Join("\n", itemNames);
    }

    public void ClearOrder()
    {
        currentTotal = 0f;
        itemNames.Clear();
        
        totalText.text = "Total: 0.00";
        changedText.text = "Change:";

        if (itemOrderListText != null)
            itemOrderListText.text = "";
    }

    public void CheckItemsAndOpenDrawer()
    {
        if (!CustomerManager.Instance.IsGameRunning()) return;

        List<ItemData> wantedList = CustomerManager.Instance.currentWantedItems;

        if (wantedList == null || wantedList.Count == 0) return;

        if (itemNames.Count != wantedList.Count)
        {
            Debug.Log("WRONG QUANTITY!");
            CustomerManager.Instance.AddPoints(-5);
            return;
        }

        List<string> playerListCopy = new List<string>(itemNames);
        bool allCorrect = true;

        foreach (ItemData neededItem in wantedList)
        {
            if (playerListCopy.Contains(neededItem.itemName))
                playerListCopy.Remove(neededItem.itemName);
            else
            {
                allCorrect = false;
                break;
            }
        }

        if (allCorrect)
        {
            Debug.Log("Order Correct!");
            CashDrawer.Instance.OpenDrawer();
        }
        else
        {
            Debug.Log("WRONG ITEMS!");
            CustomerManager.Instance.AddPoints(-5);
        }
    }

    public void SubmitOrder()
    {
        if (!CustomerManager.Instance.IsGameRunning()) return;

        ClearOrder();
        CustomerManager.Instance.NextCustomer();
        Debug.Log("Transaction Complete!");
    }

    public void CheckPayment(float moneyGiven)
    {
        if (!CustomerManager.Instance.IsGameRunning()) return;

        if (moneyGiven < currentTotal)
        {
            Debug.Log("NOT ENOUGH MONEY!");
            CustomerManager.Instance.AddPoints(-5);
            changedText.text = "Change: 0.00";
            return;
        }

        float change = moneyGiven - currentTotal;
        changedText.text = "Change: " + change.ToString("F2");

        Debug.Log("Payment Correct!");
        CustomerManager.Instance.AddPoints(10);
        CustomerManager.Instance.AddMoney(currentTotal);

        SubmitOrder();
    }
}