using UnityEngine;

public class QTETrigger : MonoBehaviour
{
    KeyCode[] keys = { KeyCode.Q, KeyCode.X, KeyCode.E, KeyCode.R };
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(!triggered)
        TriggerRandomMash();
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
            onSuccess: () => Debug.Log("Vault succeeded!"),
            onFail: () => Debug.Log("Player stumbled!")
        );
    }

}
