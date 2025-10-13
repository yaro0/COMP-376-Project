using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI References")]
    public TextMeshProUGUI interactText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Hide text by default
        interactText.gameObject.SetActive(false);
    }

    public void ShowMessage(string message, float duration = 3f)
    {
        StopAllCoroutines();
        StartCoroutine(ShowMessageRoutine(message, duration));
    }

    private IEnumerator ShowMessageRoutine(string message, float duration)
    {
        interactText.text = message;
        interactText.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        interactText.gameObject.SetActive(false);
    }

}
