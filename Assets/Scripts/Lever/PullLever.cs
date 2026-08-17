using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PullLever : MonoBehaviour
{
    [SerializeField] private LeverSO leverSO;
    [SerializeField] private MonitorSO monitorSO;

    [SerializeField] private Image[] reelSlotFruits;

    private Payout _payout;

    private Animator _animation;

    private float _timeSinceLastPull;

    private const string PullLeverAnimatorString = "Pull Lever";

    private void Awake()
    {
        _animation = GetComponent<Animator>();
    }

    private void Start()
    {
        _timeSinceLastPull = leverSO.spinDuration;
        _payout = FindAnyObjectByType<Payout>();
    }

    private void Update()
    {
        _timeSinceLastPull += Time.deltaTime;
    }

    private void OnMouseDown()
    {
        if (_timeSinceLastPull <= leverSO.spinDuration) return;

        StartCoroutine(StartSpin());
    }

    private IEnumerator StartSpin()
    {
        _timeSinceLastPull = 0f;

        _animation.Play(PullLeverAnimatorString);

        monitorSO.ChangeFruitsVisuals(monitorSO.fruitScaleWhileSpin, monitorSO.fruitAlphaWhileSpin, reelSlotFruits);

        while (_timeSinceLastPull < leverSO.spinDuration)
        {
            float progress = _timeSinceLastPull / leverSO.spinDuration;
            float reelSpinSpeedBeforeResult = Mathf.Lerp(leverSO.startSlotSpinSpeed, leverSO.endSlotSpinSpeed, progress);

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < reelSlotFruits.Length; i++)
            {
                reelSlotFruits[i].sprite = leverSO.GetRandomFruit().sprite;;
            }

            yield return new WaitForSeconds(reelSpinSpeedBeforeResult);
        }

        Spin();
        monitorSO.ChangeFruitsVisuals(monitorSO.defaultFruitScale, monitorSO.defaultAlpha, reelSlotFruits);
    }

    private void Spin()
    {
        var fruitNames = new LeverSO.Fruits [reelSlotFruits.Length];

        for (var i = 0; i < reelSlotFruits.Length; i++)
        {
            LeverSO.FruitsData randomFruitData = leverSO.GetRandomFruit();

            reelSlotFruits[i].sprite = randomFruitData.sprite;

            fruitNames[i] = randomFruitData.fruit;
        }

        _payout.ResultPayout(fruitNames);
    }
}
