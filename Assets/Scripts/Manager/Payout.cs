using UnityEngine;
using UnityEngine.Events;

public class Payment : MonoBehaviour
{
    [SerializeField] private UnityEvent<int> onTotalAmountChange;
    [SerializeField] private UnityEvent<int> onBetChange;
    
    [SerializeField] private GameOptionsSO gameOptionsSO;

    private int _totalAmount;
    private int TotalAmount
    {
        get { return _totalAmount; }
        
        set
        {
            _totalAmount = value;
            onTotalAmountChange.Invoke(_totalAmount);
        }
    }
    
    private int _bet;
    // ReSharper disable once ConvertToAutoProperty
    private int Bet
    {
        get { return _bet; }
        
        set
        {
            _bet = value;
            onBetChange.Invoke(_bet);
        }
    }

    public void PlayerDepositStringToInt(string depositAmountString)
    {
        TotalAmount += gameOptionsSO.StringToInt(depositAmountString);
    }
    
    public void PlayerBetAmountStringToInt(string betAmountString)
    {
        Bet = gameOptionsSO.StringToInt(betAmountString);
    }

    public void Payout(GameOptionsSO.Fruits[] fruitNames)
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
            TotalAmount -= Bet;
            // TODO on lose action
            if (TotalAmount <= 0) TotalAmount = 0;
        }
    }

    private void CalculatePayout(GameOptionsSO.Fruits fruitName)
    {
        int multiplier = gameOptionsSO.GetMultiplier(fruitName);

        TotalAmount += Bet * multiplier;
    }
}
