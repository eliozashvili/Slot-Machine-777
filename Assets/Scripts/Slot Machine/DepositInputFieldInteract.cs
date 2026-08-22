using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;

public class DepositInputFieldInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private PlayerInput playerInput;

    private Payout _payout;

    private void Start()
    {
        if (!inputField) return;
        
        inputField.onSubmit.AddListener(OnSubmitEnterPress);

        _payout = FindAnyObjectByType<Payout>();
    }

    public void Interact()
    {
        InputFieldInteractable();
    }

    private void OnSubmitEnterPress(string depositAmountString)
    {
        _payout.PlayerDepositStringToInt(depositAmountString);
        
        InputFieldNotInteractable();
    }

    private void InputFieldInteractable()
    {
        EventSystem.current.SetSelectedGameObject(inputField.gameObject);

        playerInput.DeactivateInput();
    }
    
    private void InputFieldNotInteractable()
    {
        EventSystem.current.SetSelectedGameObject(null);

        playerInput.ActivateInput();
    }
}