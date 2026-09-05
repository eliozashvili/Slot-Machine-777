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

    private void OnEnable()
    {
        Payment.OnGameOver += GameOverPanel;
    }

    private void OnDisable()
    {
        Payment.OnGameOver -= GameOverPanel;
    }

    private void GameOverPanel()
    {
        starterAssetsInputs.SetCursorState(false);
        playerInput.gameObject.SetActive(false);
        gameOver.SetActive(true);
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
