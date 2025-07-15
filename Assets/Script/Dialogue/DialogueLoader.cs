using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[CreateAssetMenu(fileName = "New Dialogue Loader", menuName = "DialogueLoader")]
public class DialogueLoader : MonoBehaviour
{

    private DialogueData dialogue;
    public string fileName;

    public void Start(){
        loadDialogue(fileName);
    }

    public void loadDialogue(string fileName){
        string path = Path.Combine(Application.dataPath,"Resources", "Dialogue", fileName);
        string dialogueObject = File.ReadAllText(path);
        dialogue = JsonUtility.FromJson<DialogueData>(dialogueObject);
    }

    public DialogueData getDialogueline(){
        return dialogue;
    }
}