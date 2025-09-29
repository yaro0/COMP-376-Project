using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class QTEUI : MonoBehaviour
{
    public Image progressBar;
    public TextMeshProUGUI keyCodeText;

    private QTE qte;

    public void Bind(QTE newQTE)
    {
        qte = newQTE;
        keyCodeText.text = newQTE.Key.ToString();
    }

    void Update()
    {
        if (qte == null) return;

        progressBar.fillAmount = 1f - qte.Progress;

        if (qte.Completed) Destroy(gameObject);
    }
}
