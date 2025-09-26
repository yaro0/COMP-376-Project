using UnityEngine;

[CreateAssetMenu(menuName = "QTE/QTE Data")]
public class QTEData : ScriptableObject
{
    public QTEType type;
    public KeyCode key;
    public float timeLimit = 2f;
    public int mashTarget = 10; 
}

public enum QTEType
{
    Mash,
    Press
}