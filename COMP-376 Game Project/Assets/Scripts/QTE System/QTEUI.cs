using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class QTEUI : MonoBehaviour
{
    [Header("Shared")]
    public TextMeshProUGUI keyCodeText;

    [Header("Timing Bar Mode")]
    public Image progressBar;
    public GameObject mashGroup;
    
    [Header("Timing Bar Mode")]
    public RectTransform timingBar;
    public Image validZone;
    public Image marker;
    public GameObject timingGroup;


    private QTE qte;

    public void Bind(QTE newQTE)
    {
        qte = newQTE;
        keyCodeText.text = newQTE.Key.ToString();

        mashGroup.SetActive(qte.Type == QTEType.Mash);
        timingGroup.SetActive(qte.Type == QTEType.TimingBar);

        if (qte.Type == QTEType.TimingBar)
        {
            Vector2 zone = qte.validZone;
            validZone.rectTransform.anchorMin = new Vector2(zone.x, 0f);
            validZone.rectTransform.anchorMax = new Vector2(zone.y, 1f);
        }
    }

    void Update()
    {
        if (qte == null) return;

        progressBar.fillAmount = 1f - qte.Progress;

        marker.rectTransform.anchorMin = new Vector2(qte.markerPosition, 0f);
        marker.rectTransform.anchorMax = new Vector2(qte.markerPosition, 1f);

        if (qte.Completed) Destroy(gameObject);
    }
}
