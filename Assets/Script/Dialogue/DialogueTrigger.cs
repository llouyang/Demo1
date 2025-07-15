using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogueTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public DialogueData dialogue;
    public string fileName;

    private bool playerInRange;
    public DialogueManager dialogueManager;
    private bool showFirst = false;


    public void loadDialogue(string fileName){
        string path = Path.Combine(Application.dataPath,"Resources", "Dialogue", fileName);
        Debug.Log(path);
        string dialogueObject = File.ReadAllText(path);
        Debug.Log(dialogueObject);
        dialogue = JsonUtility.FromJson<DialogueData>(dialogueObject);
    }

    void Start(){
        loadDialogue(fileName);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            dialogueManager.StartDialogue(dialogue);
            //showFirst = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            //showFirst = false;
        }
    }
}
