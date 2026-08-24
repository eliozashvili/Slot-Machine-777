using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float interactRange;
    [SerializeField] private InputActionReference interactButton;
    
    private Camera _camera;
    
    private void Start()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        interactButton.action.Enable();
        interactButton.action.performed += PressInteractButtonHandler;
    }
    
    private void OnDisable()
    {
        interactButton.action.Disable();
        interactButton.action.performed -= PressInteractButtonHandler;
    }

    private void PressInteractButtonHandler(InputAction.CallbackContext context)
    {
        var ray = new Ray(_camera.transform.position, _camera.transform.forward);

        if (!Physics.Raycast(ray, out RaycastHit hit, interactRange)) return;
        
        if (hit.collider.gameObject.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact();
        }
    }
}
