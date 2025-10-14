using UnityEngine;
using System.Collections;

public class Interact : MonoBehaviour
{
   InteractEvent interact = new InteractEvent();
   FPSController player;

    public InteractEvent GetInteractEvent
    {
        get
        {
            if (interact == null) interact = new InteractEvent();
            return interact;
        }
    }

    public FPSController GetPlayer
    {
        get { return player; }
    }
    public void CallInteract(FPSController interactPlayer)
    {
        player=interactPlayer;
        interact.CallInteractEvent();
    }
}

public class InteractEvent
{
    public delegate void InteractHandler();

    public event InteractHandler HasInteracted;

    public void CallInteractEvent() => HasInteracted?.Invoke();
}
