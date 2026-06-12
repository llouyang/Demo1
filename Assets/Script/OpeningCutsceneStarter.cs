using System.Collections;
using UnityEngine;

public class OpeningCutsceneStarter : MonoBehaviour
{
    [SerializeField] private string conversationTitle = "Opening Cutscene";
    [SerializeField] private string refrigeratorConversationTitle = "Opening Refrigerator";
    [SerializeField] private string refrigeratorObjectName = "refrigerator";
    [SerializeField] private Vector2 fallbackRefrigeratorPosition = new Vector2(42.71f, -0.17f);
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float stopDistance = 0.08f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;
    private PixelCrushers.DialogueSystem.Selector selector;

    private IEnumerator Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
        selector = GetComponent<PixelCrushers.DialogueSystem.Selector>();

        while (PixelCrushers.DialogueSystem.DialogueManager.masterDatabase == null)
        {
            yield return null;
        }

        yield return null;

        if (!PixelCrushers.DialogueSystem.DialogueManager.isConversationActive)
        {
            PixelCrushers.DialogueSystem.DialogueManager.StartConversation(conversationTitle, transform, transform);
        }

        yield return new WaitUntil(() => !PixelCrushers.DialogueSystem.DialogueManager.isConversationActive);
        yield return WalkToRefrigerator();

        PixelCrushers.DialogueSystem.DialogueManager.StartConversation(refrigeratorConversationTitle, transform, transform);
    }

    private IEnumerator WalkToRefrigerator()
    {
        Vector2 target = GetRefrigeratorPosition();

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        if (selector != null)
        {
            selector.enabled = false;
        }

        while (Mathf.Abs(transform.position.x - target.x) > stopDistance)
        {
            float direction = Mathf.Sign(target.x - transform.position.x);

            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = direction < 0f;
            }

            if (rb != null)
            {
                rb.linearVelocity = new Vector2(direction * walkSpeed, rb.linearVelocity.y);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x, transform.position.y, transform.position.z), walkSpeed * Time.deltaTime);
            }

            yield return null;
        }

        if (rb != null)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }

        if (selector != null)
        {
            selector.enabled = true;
        }

        if (playerController != null)
        {
            playerController.enabled = true;
        }
    }

    private Vector2 GetRefrigeratorPosition()
    {
        GameObject refrigerator = GameObject.Find(refrigeratorObjectName);
        if (refrigerator != null)
        {
            return refrigerator.transform.position;
        }

        return fallbackRefrigeratorPosition;
    }
}
