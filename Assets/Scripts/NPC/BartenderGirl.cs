using UnityEngine;

public class BartenderGirl : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Update()
    {
        transform.LookAt(player);
    }
}
