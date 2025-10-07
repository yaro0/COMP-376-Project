using UnityEngine;

public class QTE
{
    private QTEParams parameters;
    private System.Action onSuccess, onFail;
    private float timer;
    private int mashCount;
    private bool completed;

    public float markerPosition;
    private bool movingRight = true;
    private float markerSpeed = 1.5f;
    public Vector2 validZone;

    public KeyCode Key => parameters.key;
    public QTEType Type => parameters.type;

    public bool Completed => completed;
    public float Progress => 1f - (timer / parameters.timeLimit);

    public QTE(QTEParams p, System.Action success, System.Action fail)
    {
        parameters = p;
        onSuccess = success;
        onFail = fail;
        timer = p.timeLimit;

        if(p.type == QTEType.TimingBar)
        {;
            markerSpeed = Random.Range(1.2f, 1.3f);
            float zoneWidth = Random.Range(0.1f, 0.25f);
            float center = Random.Range(zoneWidth / 2f, 1f - zoneWidth / 2f);
            validZone = new Vector2(center - zoneWidth / 2f, center + zoneWidth / 2f);
            markerPosition = 0f;
        }
    }

    public void Update()
    {
        if (completed) return;

        timer -= Time.deltaTime;
        if (timer <= 0f) Fail();

        switch (parameters.type)
        {
            case QTEType.Press:
                if (Input.GetKeyDown(parameters.key)) Success();
                break;

            case QTEType.Mash:
                if (Input.GetKeyDown(parameters.key)) mashCount++;
                if (mashCount >= parameters.mashTarget) Success();
                break;
            case QTEType.TimingBar:
                UpdateTimingBar();
                break;
        }
    }

    private void UpdateTimingBar()
    {
        markerPosition += markerSpeed * Time.deltaTime * (movingRight ? 1 : -1);

        if (markerPosition >= 1f) { markerPosition = 1f; movingRight = false; }
        else if (markerPosition <= 0f) { markerPosition = 0f; movingRight = true; }

        if (Input.GetKeyDown(parameters.key))
        {
            if (markerPosition >= validZone.x && markerPosition <= validZone.y)
                Success();
            else
                Fail();
        }
    }

    private void Success()
    {
        completed = true;
        onSuccess?.Invoke();
    }

    private void Fail()
    {
        completed = true;
        onFail?.Invoke();
    }
}
