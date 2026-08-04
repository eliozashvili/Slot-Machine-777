using UnityEngine;

public class SlotsMachine : MonoBehaviour
{
    private int[] _reelSlotsRandomNumbers = new int[3];

    private void Start()
    {
        Spin();
    }

    private void Spin()
    {
        for (var i = 0; i < _reelSlotsRandomNumbers.Length; i++)
        {
            _reelSlotsRandomNumbers[i] = Random.Range(0, 10);
        }

        Debug.Log(string.Join(", ", _reelSlotsRandomNumbers));
    }
}
