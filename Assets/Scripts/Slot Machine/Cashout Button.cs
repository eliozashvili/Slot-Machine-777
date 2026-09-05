using UnityEngine;
using UnityEngine.Events;

public class CashoutButton : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent<int> onCashoutButtonPressed;
    [SerializeField] private UnityEvent<int> onTotalReset;
    
    [SerializeField] private GameOptionsSO gameOptionsSO;
    
    private Animator _animator;
    
    private const string PressButtonAnimationString = "Pressed";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        Cashout();
    }

    private void Cashout()
    {
        int total = gameOptionsSO.TotalAmount;

        if (total <= 0) return;
        
        _animator.Play(PressButtonAnimationString);
        
        onCashoutButtonPressed.Invoke(total);

        gameOptionsSO.ResetTotalAmount();
        
        onTotalReset.Invoke(gameOptionsSO.TotalAmount);
    }
}
