using UnityEngine;

public class ThoughtInteract : Interact
{
    [TextArea(2, 5)]
    public string playerThought = "I slept here last night, so uncomfortable.";
    public string afterInteractThought = "Time to move on from this.";

    [Header("Prompt Settings")]
    public GameObject promptPrefab;
    private GameObject promptInstance;
    private bool hasInteracted = false;
    public Transform promptAnchor;

    private void Start()
    {
        if (promptPrefab)
        {
            Transform anchor = promptAnchor ? promptAnchor : transform;
            promptInstance = Instantiate(promptPrefab, anchor);
            promptInstance.transform.localPosition = Vector3.zero;
        }
    }
    void OnEnable()
    {
        // Subscribe this method to the interaction event
        GetInteractEvent.HasInteracted += OnPlayerInteracted;
    }

    void OnDisable()
    {
        GetInteractEvent.HasInteracted -= OnPlayerInteracted;
    }

    void OnPlayerInteracted()
    {
        // When the player interacts, display the message
        if (!hasInteracted) {
            hasInteracted = true;
            UIManager.Instance.ShowMessage(playerThought);
        }
        else
        {
            UIManager.Instance.ShowMessage(afterInteractThought);
        }
        if (promptInstance)
        {
            promptInstance.SetActive(false);
        }
           
    }
}
