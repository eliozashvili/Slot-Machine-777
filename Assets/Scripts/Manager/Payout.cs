using UnityEngine;
using UnityEngine.Events;

public class Payout : MonoBehaviour
{
    [SerializeField] private UnityEvent<int> onTotalAmountChanged;
    
    [SerializeField] private GameOptionsSO gameOptionsSO;
    [SerializeField] private MonitorSO monitorSO;

    private int _totalAmount;
    public int TotalAmount
    {
        get { return _totalAmount; }
        
        private set
        {
            _totalAmount = value;
            onTotalAmountChanged.Invoke(_totalAmount);
        }
    }

    public void PlayerDepositStringToInt(string depositAmountString)
    {
        if (int.TryParse(depositAmountString, out int deposit))
            TotalAmount += deposit;
    }

    public void ResultPayout(GameOptionsSO.Fruits[] fruitNames)
    {
        var isWin = true;

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (GameOptionsSO.Fruits fruitName in fruitNames)
        {
            if (fruitName == fruitNames[0]) continue;

            isWin = false;
            break;
        }

        if (isWin)
        {
            CalculatePayout(fruitNames[0]);
        }
        else
        {
            TotalAmount -= monitorSO.BetAmount;
            // TODO on lose action
            if (TotalAmount <= 0) TotalAmount = 0;
        }
    }

    private void CalculatePayout(GameOptionsSO.Fruits fruitName)
    {
        int multiplier = gameOptionsSO.GetMultiplier(fruitName);

        TotalAmount += monitorSO.BetAmount * multiplier;
    }
}
