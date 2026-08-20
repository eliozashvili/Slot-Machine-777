using UnityEngine;

public class Payout : MonoBehaviour
{
    [SerializeField] private GameOptionsSO gameOptionsSO;
    [SerializeField] private MonitorSO monitorSO;

    private int _totalAmount;

    private void Start()
    {
        _totalAmount = monitorSO.DepositAmount;
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
            _totalAmount -= monitorSO.BetAmount;
            // TODO on lose action
            if (_totalAmount == 0)
            {
                Debug.Log("You Lost");
            }
        }

        Debug.Log(_totalAmount);
    }

    private void CalculatePayout(GameOptionsSO.Fruits fruitName)
    {
        int multiplier = gameOptionsSO.GetMultiplier(fruitName);

        _totalAmount += monitorSO.BetAmount * multiplier;
    }
}
