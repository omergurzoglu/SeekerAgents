
using UnityEngine;

public class DetectArea : MonoBehaviour,IInteract
{
    
    public void Interact() => StatusCheck.Instance.ResetGame();
    
}
