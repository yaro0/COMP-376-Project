using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BillboardUI : MonoBehaviour
{
    [Header("Bobbing")]
    public float bobAmplitude = 0.1f;
    public float bobFrequency = 2f;

    [Header("Glow")]
    public Image arrowImage;
    public float glowSpeed = 2f;
    public float glowIntensity = 0.5f; 

    private Vector3 startPos;
    private CanvasGroup canvasGroup;
    private float glowTimer;
    private bool isFading = false;

    private void Awake()
    {
        startPos = transform.localPosition;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
    }

    void LateUpdate()
    {
        // Bobbing
        float newY = startPos.y + Mathf.Sin(Time.time * bobFrequency) * bobAmplitude;
        transform.localPosition = new Vector3(startPos.x, newY, startPos.z);

        // Glow pulse
        if (arrowImage)
        {
            glowTimer += Time.deltaTime * glowSpeed;
            float pulse = 1 + Mathf.Sin(glowTimer) * glowIntensity;
            arrowImage.color = new Color(pulse, pulse, pulse, 1);
        }

        if (Camera.main)
            transform.LookAt(transform.position + Camera.main.transform.forward);
    }

    public void FadeOut(float duration) {
        if (!isFading) {
            StartCoroutine(FadeOutRoutine(duration));
        }
    }
    public IEnumerator FadeOutRoutine(float duration) {
        isFading = true;
        float startAlpha=canvasGroup.alpha;
        float t = 0f;

        while (t < duration) {
            t += Time.deltaTime;
            canvasGroup.alpha=Mathf.Lerp(startAlpha, 0, t/duration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
}
