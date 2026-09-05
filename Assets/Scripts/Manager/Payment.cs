using UnityEngine;
using UnityEngine.Events;
using System;

public class Payment : MonoBehaviour
{
    [SerializeField] private UnityEvent<int> onTotalAmountChange;
    [SerializeField] private UnityEvent<int> onBetChange;
    
    [SerializeField] private GameOptionsSO gameOptionsSO;

    private void Start()
    {
        Bet = gameOptionsSO.MinimumBet;
    }

    private int _totalAmount;
    private int TotalAmount
    {
        get { return _totalAmount; }
        set
        {
            _totalAmount = value;
            gameOptionsSO.TotalAmount = _totalAmount;
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
            gameOptionsSO.Bet = _bet;
            onBetChange.Invoke(_bet);
        }
    }

    public void PlayerDeposit(int depositToTotal)
    {
        TotalAmount += depositToTotal;
    }
    
    public void PlayerBet(string betAmountString)
    {
        int bet = gameOptionsSO.StringToIntStepByFive(betAmountString);
        
        Bet = Math.Clamp(bet, gameOptionsSO.MinimumBet, gameOptionsSO.MaximumBet);
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
            Debug.Log(gameOptionsSO.PlayerCashAmount);

            
            // if (TotalAmount <= 0 && gameOptionsSO.PlayerCashAmount <= 0)
            // {
            //     TotalAmount = 0;
            //     Debug.Log("GAME OVER");
            // };
        }
    }

    private void CalculatePayout(GameOptionsSO.Fruits fruitName)
    {
        int multiplier = gameOptionsSO.GetMultiplier(fruitName);

        TotalAmount += Bet * multiplier;
    }
}
