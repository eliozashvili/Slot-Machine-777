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
        gameOptionsSO.ResetTotalAmount();
        Bet = gameOptionsSO.MinimumBet;
    }
    
    private int TotalAmount
    {
        get { return gameOptionsSO.TotalAmount; }
        set
        {
            gameOptionsSO.TotalAmount = value;
            onTotalAmountChange.Invoke(gameOptionsSO.TotalAmount);
        }
    }
    
    // ReSharper disable once ConvertToAutoProperty
    private int Bet
    {
        get { return gameOptionsSO.Bet; }
        set
        {
            gameOptionsSO.Bet = value;
            onBetChange.Invoke(gameOptionsSO.Bet);
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
        }
    }

    private void CalculatePayout(GameOptionsSO.Fruits fruitName)
    {
        int multiplier = gameOptionsSO.GetMultiplier(fruitName);

        TotalAmount += Bet * multiplier;
    }
}
