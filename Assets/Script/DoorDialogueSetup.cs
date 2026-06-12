using System.Collections;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class DoorDialogueSetup : MonoBehaviour
{
    private const string ConversationTitle = "Door Bedroom";

    private IEnumerator Start()
    {
        GameObject door = gameObject;
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Selector selector = player != null ? player.GetComponent<Selector>() : null;
        if (selector != null)
        {
            selector.useKey = KeyCode.E;
            selector.useButton = string.Empty;
            selector.defaultUseMessage = "(E to interact)";
        }

        Usable usable = door.GetComponent<Usable>();
        if (usable == null)
        {
            usable = door.AddComponent<Usable>();
        }
        usable.maxUseDistance = 5f;
        usable.overrideUseMessage = "(E to interact)";

        DialogueSystemTrigger trigger = door.GetComponent<DialogueSystemTrigger>();
        if (trigger == null)
        {
            trigger = door.AddComponent<DialogueSystemTrigger>();
        }

        trigger.trigger = DialogueSystemTriggerEvent.OnUse;
        trigger.conversation = ConversationTitle;
        trigger.conversationActor = player?.transform;

        while (PixelCrushers.DialogueSystem.DialogueManager.masterDatabase == null)
        {
            yield return null;
        }

        trigger.selectedDatabase = PixelCrushers.DialogueSystem.DialogueManager.masterDatabase;
    }
}
