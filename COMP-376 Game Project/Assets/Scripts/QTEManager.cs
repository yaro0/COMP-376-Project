using UnityEngine;

public class QTEManager : MonoBehaviour
{
    public static QTEManager Instance;
    public GameObject qteUIPrefab;
    private QTE currentQTE;

    void Awake() => Instance = this;

    public void StartQTE(QTEData data, System.Action onSuccess, System.Action onFail)
    {
        if (currentQTE != null) return; // Already running
        currentQTE = new QTE(data, onSuccess, onFail);
        GameObject ui = Instantiate(qteUIPrefab, transform);
    }

    void Update()
    {
        if (currentQTE != null) currentQTE.Update();
    }
}
