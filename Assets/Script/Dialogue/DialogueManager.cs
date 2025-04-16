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

    private List<DialogueLine> dialogueLines;
    private int index;
    private bool isTyping;

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueLines = dialogue.lines;
        index = 0;
        dialoguePanel.SetActive(true);
        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = dialogueLines[index].line;
            isTyping = false;
            return;
        }

        if (index < dialogueLines.Count)
        {
            nameText.text = dialogueLines[index].characterName;
            StartCoroutine(TypeLine(dialogueLines[index].line));
            index++;
        }
        else
        {
            dialoguePanel.SetActive(false);
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
