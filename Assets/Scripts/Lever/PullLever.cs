using UnityEngine;

public class PullLever : MonoBehaviour
{
    [SerializeField] private LeverSO leverSO;
    [SerializeField] private MonitorInformation monitorInformation;

    private Animator _animator;

    private float _timeSinceLastPull;

    private const string PullLeverAnimatorString = "Pull Lever";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _timeSinceLastPull = leverSO.SpinDuration;
    }

    private void Update()
    {
        _timeSinceLastPull += Time.deltaTime;
    }

    private void OnMouseDown()
    {
        HandleSpinStart();
    }

    private void HandleSpinStart()
    {
        if (_timeSinceLastPull <= leverSO.SpinDuration) return;

        _timeSinceLastPull = 0f;
        _animator.Play(PullLeverAnimatorString);

        monitorInformation.SpinReels();
    }
}
