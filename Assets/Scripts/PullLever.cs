using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PullLever : MonoBehaviour
{
    [Header("Fruit models and slots\n"), Space(1f)]
    [SerializeField] private Sprite[] fruitSprites;
    [SerializeField] private Image[] reelSlotFruits;
    [Header("Fruit visuals while spin\n"), Space(1f)]
    [SerializeField] private Vector2 defaultFruitScale;
    [SerializeField] private Vector2 fruitScaleWhileSpin;
    [SerializeField] private float defaultAlpha;
    [SerializeField] private float fruitAlphaWhileSpin;
    [Header("Spin options\n"), Space(1f)]
    [SerializeField] private float spinDuration;
    [SerializeField] private float startSlotSpinSpeed;
    [SerializeField] private float endSlotSpinSpeed;

    private Animator _animation;

    private const string PullLeverAnimatorString = "Pull Lever";

    private float _timeSinceLastPull;

    private void Awake()
    {
        _animation = GetComponent<Animator>();
    }

    private void Start()
    {
        _timeSinceLastPull = spinDuration;
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
        foreach (Image fruit in reelSlotFruits)
        {
            Sprite randomFruit = fruitSprites[Random.Range(0, fruitSprites.Length)];

            fruit.sprite = randomFruit;
        }
    }

    private IEnumerator StartSpin()
    {
        _timeSinceLastPull = 0f;

        _animation.Play(PullLeverAnimatorString);

        while (_timeSinceLastPull < spinDuration)
        {
            float progress = _timeSinceLastPull / spinDuration;
            float reelSpinSpeedBeforeResult = Mathf.Lerp(startSlotSpinSpeed, endSlotSpinSpeed, progress);

            foreach (Image fruit in reelSlotFruits)
            {
                Sprite randomFruit = fruitSprites[Random.Range(0, fruitSprites.Length)];

                ChangeFruitsVisuals(fruitScaleWhileSpin, fruitAlphaWhileSpin);

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
