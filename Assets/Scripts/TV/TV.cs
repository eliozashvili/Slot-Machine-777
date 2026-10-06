using UnityEngine;
using UnityEngine.Video;

public class TV : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject tvBlackScreenMaterial;
    [SerializeField] private VideoPlayer videoPlayer;


    private bool _isOn = true;
    
    public void Interact()
    {
        ToggleTV();
    }

    private void ToggleTV()
    {
        _isOn = !_isOn;
        
        if (_isOn)
        {
            tvBlackScreenMaterial.SetActive(false);
            videoPlayer.Play();
        }
        else
        {
            videoPlayer.Pause();
            tvBlackScreenMaterial.SetActive(true);
        }
    }
}
