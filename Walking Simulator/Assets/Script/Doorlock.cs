using UnityEngine;

public class Doorlock : MonoBehaviour
{
    public bool isLocked = true;

    public void ToggleLock()
    {
        isLocked = !isLocked;
    }
}
