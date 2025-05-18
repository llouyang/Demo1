using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public float typingSpeed = 0.03f;

    private int index = -1;
    private bool isTyping;

    public void StartDialogue(Dialogue dialogueLine)
    {
        dialoguePanel.SetActive(true);
        ShowLine(dialogueLine);
    }

    public void ShowLine(Dialogue dialogueLine)
    {
        Debug.Log($"indexn is {index}. count is {dialogueLine.lines.Count}");
        
        
        if (isTyping)
        {
            dialogueText.text = dialogueLine.lines[index].line;
            StopAllCoroutines();
            isTyping = false;
            return;
        }
        if (index >= dialogueLine.lines.Count - 1)
        {
            Debug.Log("inside");
            Debug.Log("inside");
            index = -1;
            dialoguePanel.SetActive(false);
            return;
        }
        else
        {
            dialoguePanel.SetActive(true);
            index++;
            nameText.text = dialogueLine.lines[index].characterName;
            StartCoroutine(TypeLine(dialogueLine.lines[index].line));
            

        }
        
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }
}
