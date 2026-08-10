using UnityEngine;
using System.Collections;
using TMPro;

public class PullLever : MonoBehaviour
{
    [SerializeField] private TMP_Text outputText;

    [SerializeField] private float spinCooldown;

    private int[] _reelSlotsRandomNumbers = new int[3];

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
        Spin();
    }

    private void Spin()
    {
        if (_timeSinceLastPull <= spinCooldown) return;

        _animation.Play(PullLeverAnimatorString);

        for (var i = 0; i < _reelSlotsRandomNumbers.Length; i++)
        {
            _reelSlotsRandomNumbers[i] = Random.Range(0, 10);
        }

        outputText.text = string.Join("", _reelSlotsRandomNumbers);
        _timeSinceLastPull = 0f;
    }
}
