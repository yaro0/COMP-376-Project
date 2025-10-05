using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class QTETrigger : MonoBehaviour
{
    [SerializeField] KeyCode[] keys = { KeyCode.Q, KeyCode.X, KeyCode.E, KeyCode.R };
    private bool triggered = false;
    [SerializeField] QTEType qteType;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            switch (qteType)
            {
                case QTEType.Press:
                    TriggerRandomPress();
                    break;
                case QTEType.Mash:
                    TriggerRandomMash();
                    break;

                case QTEType.TimingBar:
                    TriggerTimingBar();
                    break;
                default:
                    break;
            }
        }
    }

    private void TriggerRandomPress()
    {
        triggered = true;
        KeyCode randomKey = keys[Random.Range(0, keys.Length)];

        QTEParams parameters = new QTEParams(QTEType.Press, randomKey);

        QTEManager.Instance.StartQTE(
            parameters,
            onSuccess: () => Debug.Log("Press succeeded!"),
            onFail: () => Debug.Log("Press failed!")
        );
    }

    void TriggerRandomMash()
    {
        triggered = true;
        KeyCode randomKey = keys[Random.Range(0, keys.Length)];

        int mashDifficulty = 5;   // scale with chase intensity
        float timeLimit = 5;

        QTEParams parameters = new QTEParams(QTEType.Mash, randomKey, timeLimit, mashDifficulty);

        QTEManager.Instance.StartQTE(
            parameters,
            onSuccess: () => Debug.Log("Mash succeeded!"),
            onFail: () => Debug.Log("Mash failed!")
        );
    }

    void TriggerTimingBar()
    {
        triggered = true;
        KeyCode randomKey = keys[Random.Range(0, keys.Length)];
        float timeLimit = 5f;

        QTEParams parameters = new QTEParams(QTEType.TimingBar, randomKey, timeLimit);

        QTEManager.Instance.StartQTE(
            parameters,
            onSuccess: () => Debug.Log("Perfect timing!"),
            onFail: () => Debug.Log("Too early or too late!")
        );
    }

}
