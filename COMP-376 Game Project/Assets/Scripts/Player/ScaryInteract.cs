using UnityEngine;

public class ScaryInteract : Interact
{
    [TextArea(2, 5)]
    public string playerThought = "I slept here last night, so uncomfortable.";
    public string afterInteractThought = "Time to move on from this.";

    [Header("Prompt Settings")]
    public GameObject promptPrefab;
    private GameObject promptInstance;
    private bool hasInteracted = false;
    public Transform promptAnchor;
    public float FadeOutLength = 1f;

    [Header("Sanity Effect")]
    public float sanityDamage = -10f;

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
        GetInteractEvent.HasInteracted += OnPlayerInteracted; //Subscribes the method to the HasInteracted event
    }

    void OnDisable()
    {
        GetInteractEvent.HasInteracted -= OnPlayerInteracted; //Unsubscribes the method to the HasInteracted event
    }

    void OnPlayerInteracted()
    {
        if (!hasInteracted)
        {
            hasInteracted = true;
            UIManager.Instance.ShowMessage(playerThought);
            PlayerStats.Instance.ModifySanity(sanityDamage);
        }
        else
        {
            UIManager.Instance.ShowMessage(afterInteractThought);
        }

        if (promptInstance)
        {
            var billboardUI = promptInstance.GetComponent<BillboardUI>();
            if (billboardUI != null) { billboardUI.FadeOut(FadeOutLength); }
            else
            {
                promptInstance.SetActive(false);
            }
        }

    }
}
