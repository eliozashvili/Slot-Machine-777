using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PullLever : MonoBehaviour
{
    [SerializeField] private Sprite[] fruitSprites;
    [SerializeField] private Image[] reelSlotFruits;

    [SerializeField] private float spinCooldown;

    private Animator _animation;

    private const string PullLeverAnimatorString = "Pull Lever";

    private float _timeSinceLastPull;

    private void Awake()
    {
        _animation = GetComponent<Animator>();
    }

    private void Start()
    {
        _timeSinceLastPull = spinCooldown;
    }

    private void Update()
    {
        _timeSinceLastPull += Time.deltaTime;
    }

    private void OnMouseDown()
    {
        if (_timeSinceLastPull <= spinCooldown) return;

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

        yield return new WaitForSeconds(0.5f);

        Spin();
    }
}
