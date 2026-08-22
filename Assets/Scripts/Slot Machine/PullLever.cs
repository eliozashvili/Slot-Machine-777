using UnityEngine;

public class PullLever : MonoBehaviour
{
    [SerializeField] private GameOptionsSO gameOptionsSO;
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
        _timeSinceLastPull = gameOptionsSO.SpinDuration;
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
        if (_timeSinceLastPull <= gameOptionsSO.SpinDuration) return;

        _timeSinceLastPull = 0f;
        _animator.Play(PullLeverAnimatorString);

        monitorInformation.SpinReels();
    }
}
