using UnityEngine;
using UnityEngine.InputSystem;

public class PullLever : MonoBehaviour, IInteractable
{
    [SerializeField] private GameOptionsSO gameOptionsSO;
    [SerializeField] private MonitorInformation monitorInformation;
    [SerializeField] private InputActionReference interactButton;
    
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
    
    public void Interact()
    {
        if (gameOptionsSO.TotalAmount >= gameOptionsSO.Bet && gameOptionsSO.Bet != 0)
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
