using UnityEngine;
public struct QTEParams
{
    public QTEType type;
    public KeyCode key;
    public float timeLimit;
    public int mashTarget;

    public QTEParams(QTEType type, KeyCode key, float timeLimit = 5, int mashTarget = 0)
    {
        this.type = type;
        this.key = key;
        this.timeLimit = timeLimit;
        this.mashTarget = mashTarget;
    }
}

public enum QTEType
{
    Mash,
    Press,
    TimingBar
}