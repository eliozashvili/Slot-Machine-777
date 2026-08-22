using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Events;

public class InteractInputFields : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent<string> onPlayerInput;
    
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private PlayerInput playerInput;

    private void Start()
    {
        if (!inputField) return;
        
        inputField.onSubmit.AddListener(OnSubmitEnterPress);
    }

    public void Interact()
    {
        InputFieldInteractable();
    }

    private void OnSubmitEnterPress(string input)
    {
        onPlayerInput.Invoke(input);
        
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