using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main)
            transform.LookAt(transform.position + Camera.main.transform.forward);
    }
}
