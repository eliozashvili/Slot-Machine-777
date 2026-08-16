using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PullLever : MonoBehaviour
{
    public enum Fruits
    {
        Seven,
        Banana,
        Orange,
        Strawberry
    }
    [System.Serializable]
    public struct FruitsMultiplier
    {
        public Fruits fruit;
        public int multiplier;
    }
    [Header("Gameplay options\n"), Space(1f)]
    [SerializeField] private FruitsMultiplier[] fruitsMultiplier;
    [SerializeField] private int depositAmount;
    [SerializeField] private int betAmount;
    [Header("Spin options\n"), Space(1f)]
    [SerializeField] private float spinDuration;
    [SerializeField] private float startSlotSpinSpeed;
    [SerializeField] private float endSlotSpinSpeed;
    [Header("Fruit models and slots\n"), Space(1f)]
    [SerializeField] private Sprite[] fruitSprites;
    [SerializeField] private Image[] reelSlotFruits;
    [Header("Fruit visuals while spin\n"), Space(1f)]
    [SerializeField] private Vector2 defaultFruitScale;
    [SerializeField] private Vector2 fruitScaleWhileSpin;
    [SerializeField] private float defaultAlpha;
    [SerializeField] private float fruitAlphaWhileSpin;

    private Animator _animation;

    private float _timeSinceLastPull;
    private int _totalAmount;

    private const string PullLeverAnimatorString = "Pull Lever";

    private void Awake()
    {
        _animation = GetComponent<Animator>();
    }

    private void Start()
    {
        _timeSinceLastPull = spinDuration;
        _totalAmount = depositAmount;
    }

    private void Update()
    {
        _timeSinceLastPull += Time.deltaTime;
    }

    private void OnMouseDown()
    {
        if (_timeSinceLastPull <= spinDuration) return;

        StartCoroutine(StartSpin());
    }

    private void Spin()
    {
        var fruitNames = new Fruits[reelSlotFruits.Length];

        for (var i = 0; i < reelSlotFruits.Length; i++)
        {
            Sprite randomFruit = fruitSprites[Random.Range(0, fruitSprites.Length)];

            reelSlotFruits[i].sprite = randomFruit;

            fruitNames[i] = System.Enum.Parse<Fruits>(reelSlotFruits[i].sprite.name);
        }

        Payout(fruitNames);
    }

    private void Payout(Fruits[] fruitNames)
    {
        var isWin = true;

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (Fruits fruitName in fruitNames)
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
            _totalAmount -= betAmount;
            // TODO on lose action
            if (_totalAmount == 0)
            {
                Debug.Log("You Lost");
            }
        }

        Debug.Log(_totalAmount);
    }

    private void CalculatePayout(Fruits fruitName)
    {
        int multiplier = GetMultiplier(fruitName);

        _totalAmount += betAmount * multiplier;
    }

    private int GetMultiplier(Fruits fruit)
    {
        foreach (FruitsMultiplier fruitMultiplier in fruitsMultiplier)
        {
            if (fruitMultiplier.fruit == fruit)
                return fruitMultiplier.multiplier;
        }

        return 1;
    }

    private IEnumerator StartSpin()
    {
        _timeSinceLastPull = 0f;

        _animation.Play(PullLeverAnimatorString);

        ChangeFruitsVisuals(fruitScaleWhileSpin, fruitAlphaWhileSpin);

        while (_timeSinceLastPull < spinDuration)
        {
            float progress = _timeSinceLastPull / spinDuration;
            float reelSpinSpeedBeforeResult = Mathf.Lerp(startSlotSpinSpeed, endSlotSpinSpeed, progress);

            foreach (Image fruit in reelSlotFruits)
            {
                Sprite randomFruit = fruitSprites[Random.Range(0, fruitSprites.Length)];

                fruit.sprite = randomFruit;
            }

            yield return new WaitForSeconds(reelSpinSpeedBeforeResult);
        }

        Spin();
        ChangeFruitsVisuals(defaultFruitScale, defaultAlpha);
    }

    private void ChangeFruitsVisuals(Vector2 scale, float alpha)
    {
        foreach (Image fruit in reelSlotFruits)
        {
            Color color = fruit.color;
            color.a = alpha;
            fruit.color = color;

            fruit.transform.localScale = new Vector3(scale.x, scale.y, 1f);
        }
    }
}
