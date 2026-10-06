using UnityEngine;

public class TV : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        ToggleTV();
    }

    private void ToggleTV()
    {
        Debug.Log("tv is toggled");
    }
}
