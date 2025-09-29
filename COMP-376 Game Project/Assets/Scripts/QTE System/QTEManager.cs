using UnityEngine;

public class QTEManager : MonoBehaviour
{
    public static QTEManager Instance;
    public QTEUI qteUIPrefab;
    private QTE currentQTE;
    public FPSController playerMovement;
    public Canvas canvas;

    void Awake() => Instance = this;

    public void StartQTE(QTEParams parameters, System.Action onSuccess, System.Action onFail)
    {
        if (currentQTE != null) return; // already running
        playerMovement.enabled = false;

        onSuccess += () => playerMovement.enabled = true;
        onSuccess += () => currentQTE = null;
        onFail += () => playerMovement.enabled = true;
        onFail += () => currentQTE = null;

        currentQTE = new QTE(parameters, onSuccess, onFail);

        QTEUI ui = Instantiate(qteUIPrefab, canvas.transform);
        ui.Bind(currentQTE);
    }

    void Update()
    {
        if (currentQTE != null && !currentQTE.Completed)
            currentQTE.Update();
    }
}
