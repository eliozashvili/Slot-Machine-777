using UnityEngine;

public class Radio : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioSource[] audioSource;

    private bool _isPlaying = true;
    
    public void Interact()
    {
        ToggleRadio();
    }

    private void ToggleRadio()
    {
        _isPlaying = !_isPlaying;

        PlayRadio(_isPlaying);
    }

    private void PlayRadio(bool play)
    {
        if (play)
        {
            foreach (AudioSource a in audioSource)
            {
                a.UnPause();
            }
        }
        else
        {
            foreach (AudioSource a in audioSource)
            {
                a.Pause();
            }
        }
    }
}
