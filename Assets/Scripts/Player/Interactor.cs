using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float interactRange;
    [SerializeField] private GameOptionsSO gameOptionsSO;
    [SerializeField] private InputActionReference interactButton;
    [SerializeField] private TMP_Text pressEText;

    private Camera _camera;

    private IInteractable _currentInteractable;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        CheckIfInteractable();
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
        if (_currentInteractable != null && !gameOptionsSO.IsSpinning)
            _currentInteractable.Interact();
    }

    private void CheckIfInteractable()
    {
        var ray = new Ray(_camera.transform.position, _camera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactRange) &&
            hit.collider.gameObject.TryGetComponent(out IInteractable interactable))
        {
            _currentInteractable = interactable;
            pressEText.gameObject.SetActive(true);
        }
        else
        {
            _currentInteractable = null;
            pressEText.gameObject.SetActive(false);
        }
    }
}
