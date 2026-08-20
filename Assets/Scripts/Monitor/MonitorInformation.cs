using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class MonitorInformation : MonoBehaviour
{
    [SerializeField] private UnityEvent<GameOptionsSO.Fruits[]> onSpinCompleted;
    
    [SerializeField] private GameOptionsSO gameOptionsSO;
    [SerializeField] private MonitorSO monitorSO;
    [SerializeField] private TMP_Text totalAmountText;

    [SerializeField] private Image[] reelSlotFruits;

    public void ShowTotalAmount(int amount)
    {
        totalAmountText.text = $"Total: {amount}";
    }

    public void SpinReels()
    {
        StartCoroutine(StartSpin());
    }

    private IEnumerator StartSpin()
    {
        var elapsedTime = 0f;

        monitorSO.ChangeFruitsVisuals(monitorSO.FruitScaleWhileSpin, monitorSO.FruitAlphaWhileSpin, reelSlotFruits);

        while (elapsedTime < gameOptionsSO.SpinDuration)
        {
            float progress = elapsedTime / gameOptionsSO.SpinDuration;
            float reelSpinSpeedBeforeResult = Mathf.Lerp(gameOptionsSO.StartSlotSpinSpeed, gameOptionsSO.EndSlotSpinSpeed, progress);

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < reelSlotFruits.Length; i++)
            {
                reelSlotFruits[i].sprite = gameOptionsSO.GetRandomFruit().Sprite;
            }

            yield return new WaitForSeconds(reelSpinSpeedBeforeResult);

            elapsedTime += reelSpinSpeedBeforeResult;
        }

        Spin();
        monitorSO.ChangeFruitsVisuals(monitorSO.DefaultFruitScale, monitorSO.DefaultAlpha, reelSlotFruits);
    }

    private void Spin()
    {
        var fruitNames = new GameOptionsSO.Fruits [reelSlotFruits.Length];

        for (var i = 0; i < reelSlotFruits.Length; i++)
        {
            GameOptionsSO.FruitsData randomFruitData = gameOptionsSO.GetRandomFruit();

            reelSlotFruits[i].sprite = randomFruitData.Sprite;

            fruitNames[i] = randomFruitData.Fruit;
        }

        onSpinCompleted.Invoke(fruitNames);
    }
}
