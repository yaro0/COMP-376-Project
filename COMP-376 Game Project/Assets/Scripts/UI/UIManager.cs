using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI References")]
    public TextMeshProUGUI interactText;
    public Slider sanityBar;
    public Slider energyBar;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Hide text by default
        interactText.gameObject.SetActive(false);
    }

    public void RegisterPlayer(PlayerStats stats)
    {
        stats.OnSanityChanged -= UpdateSanityBar; // unsubscribe first for when player respawns
        stats.OnSanityChanged += UpdateSanityBar;

        stats.OnEnergyChanged -= UpdateEnergyBar;
        stats.OnEnergyChanged += UpdateEnergyBar;
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
    void UpdateSanityBar(float newVal) => sanityBar.value = newVal / 100f;
    void UpdateEnergyBar(float newVal) => energyBar.value = newVal / 100f;
}
