using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonitorInformation : MonoBehaviour
{
    [SerializeField] private LeverSO leverSO;
    [SerializeField] private MonitorSO monitorSO;

    [SerializeField] private Image[] reelSlotFruits;

    private Payout _payout;

    private void Start()
    {
        _payout = FindAnyObjectByType<Payout>();
    }

    public void SpinReels()
    {
        StartCoroutine(StartSpin());
    }

    private IEnumerator StartSpin()
    {
        var elapsedTime = 0f;

        monitorSO.ChangeFruitsVisuals(monitorSO.FruitScaleWhileSpin, monitorSO.FruitAlphaWhileSpin, reelSlotFruits);

        while (elapsedTime < leverSO.SpinDuration)
        {
            float progress = elapsedTime / leverSO.SpinDuration;
            float reelSpinSpeedBeforeResult = Mathf.Lerp(leverSO.StartSlotSpinSpeed, leverSO.EndSlotSpinSpeed, progress);

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < reelSlotFruits.Length; i++)
            {
                reelSlotFruits[i].sprite = leverSO.GetRandomFruit().Sprite;
            }

            yield return new WaitForSeconds(reelSpinSpeedBeforeResult);

            elapsedTime += reelSpinSpeedBeforeResult;
        }

        Spin();
        monitorSO.ChangeFruitsVisuals(monitorSO.DefaultFruitScale, monitorSO.DefaultAlpha, reelSlotFruits);
    }

    private void Spin()
    {
        var fruitNames = new LeverSO.Fruits [reelSlotFruits.Length];

        for (var i = 0; i < reelSlotFruits.Length; i++)
        {
            LeverSO.FruitsData randomFruitData = leverSO.GetRandomFruit();

            reelSlotFruits[i].sprite = randomFruitData.Sprite;

            fruitNames[i] = randomFruitData.Fruit;
        }

        _payout.ResultPayout(fruitNames);
    }
}
