using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using StarterAssets;
using UnityEditor;

public class GameOver : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs starterAssetsInputs;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private InputActionReference openMenuButton;

    private void OnEnable()
    {
        Payment.OnGameOver += HandleGameOver;
        openMenuButton.action.Enable();

        openMenuButton.action.performed += OnOpenMenuButtonPressed;
    }

    private void OnDisable()
    {
        Payment.OnGameOver -= HandleGameOver;
        openMenuButton.action.Disable();
        
        openMenuButton.action.performed -= OnOpenMenuButtonPressed;
    }
    
    private void HandleGameOver()
    {
        GameOverPanel(true);
    }

    private void OnOpenMenuButtonPressed(InputAction.CallbackContext context)
    {
        ToggleGameOverPanel();
    }

    private void ToggleGameOverPanel()
    {
        bool isCurrentlyActive = gameOver.activeSelf;
        GameOverPanel(!isCurrentlyActive);
    }

    private void GameOverPanel(bool show)
    {
        gameOver.SetActive(show);
        
        if (show)
        {
            starterAssetsInputs.SetCursorState(false);
            playerInput.gameObject.SetActive(false);
        }
        else
        {
            starterAssetsInputs.SetCursorState(true);
            playerInput.gameObject.SetActive(true);
        }
    }

    public void Restart()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        starterAssetsInputs.SetCursorState(true);
        SceneManager.LoadScene(sceneIndex);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
