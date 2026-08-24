using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using TMPro;

public class InteractInputFields : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent<string> onPlayerInput;
    
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputActionReference submitButton;
    [SerializeField] private InputActionReference cancelButton;

    private void OnEnable()
    {
        submitButton.action.Enable();
        cancelButton.action.Enable();
        
        submitButton.action.performed += OnSubmitButtonPressed;
        cancelButton.action.performed += OnCancelButtonPressed;
    }
    
    private void OnDisable()
    {
        submitButton.action.Disable();
        cancelButton.action.Disable();
        
        submitButton.action.performed -= OnSubmitButtonPressed;
        cancelButton.action.performed -= OnCancelButtonPressed;
    }

    public void Interact()
    {
        InputFieldInteractable();
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

    private void OnSubmitButtonPressed(InputAction.CallbackContext context)
    {
        onPlayerInput.Invoke(inputField.text);
        
        InputFieldNotInteractable();
    }
    
    private void OnCancelButtonPressed(InputAction.CallbackContext context)
    {
        onPlayerInput.Invoke(string.Empty);
        
        InputFieldNotInteractable();
    }
}