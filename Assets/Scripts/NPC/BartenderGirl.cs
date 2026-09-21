using UnityEngine;
using System.Collections;
using TMPro;

public class BartenderGirl : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform player;
    [SerializeField] private TMP_Text npcDialogueText;
    [SerializeField] private BartenderGirlSO bartenderGirlSO;

    private Coroutine _currentCoroutine;
    
    private void Update()
    {
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    public void Interact()
    {
        if (_currentCoroutine != null) return;
        
        _currentCoroutine = StartCoroutine(ShowDialogue());
    }

    private IEnumerator ShowDialogue()
    {
        npcDialogueText.gameObject.SetActive(true);
        
        int random = Random.Range(0, bartenderGirlSO.BartenderGirlDialogue.Length);
        
        npcDialogueText.text = bartenderGirlSO.BartenderGirlDialogue[random];

        yield return new WaitForSeconds(3f);
        
        npcDialogueText.gameObject.SetActive(false);

        _currentCoroutine = null;
    }
}
