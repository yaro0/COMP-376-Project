using UnityEngine;

public class QTE
{
    private QTEData data;
    private System.Action onSuccess, onFail;
    private float timer;
    private int mashCount;

    public bool Completed { get; private set; }

    public QTE(QTEData d, System.Action success, System.Action fail)
    {
        data = d; 
        onSuccess = success; 
        onFail = fail;
        timer = d.timeLimit;
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && !Completed) Fail();

        switch (data.type)
        {
            case QTEType.Press:
                if (Input.GetKeyDown(data.key)) Success();
                break;

            case QTEType.Mash:
                if (Input.GetKeyDown(data.key)) mashCount++;
                if (mashCount >= data.mashTarget) Success();
                break;
        }
    }

    private void Success()
    {
        Completed = true; onSuccess?.Invoke();
    }

    private void Fail()
    {
        Completed = true; onFail?.Invoke();
    }
}
