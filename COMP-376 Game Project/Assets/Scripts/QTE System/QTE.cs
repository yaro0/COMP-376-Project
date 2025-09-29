using UnityEngine;

public class QTE
{
    private QTEParams parameters;
    private System.Action onSuccess, onFail;
    private float timer;
    private int mashCount;
    private bool completed;

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
