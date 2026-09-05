using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PlayerCash : MonoBehaviour
{
    [SerializeField] private UnityEvent<int> onPlayerCashAmountChange;
    
    [SerializeField] private TMP_Text playerCashText;
    [SerializeField] private GameOptionsSO gameOptionsSO;
    
    [SerializeField] private int playerCash;
    private int PlayerCashAmount
    {
        get { return playerCash; }
        set
        {
            playerCash = value;
            gameOptionsSO.PlayerCashAmount = playerCash;
            UpdatePlayerCash(playerCash);
        }
    }

    private void Start()
    {
        PlayerCashAmount = playerCash;
    }

    public void HandlePlayerCash(string depositAmountString)
    {
        int depositAmount = gameOptionsSO.StringToIntStepByFive(depositAmountString);

        if (depositAmount > PlayerCashAmount) return;
        
        PlayerCashAmount -= depositAmount;
        
        onPlayerCashAmountChange.Invoke(depositAmount);
    }

    public void TotalToCash(int amount)
    {
        PlayerCashAmount += amount;
    }

    private void UpdatePlayerCash(int amount)
    {
        playerCashText.text = $"CASH: ${amount:N0}";
    }
}
