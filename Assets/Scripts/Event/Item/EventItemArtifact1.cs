using UnityEngine;

public class EventItemArtifact1 : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        GameManager.Instance.UIManager.ShowInfo("˫�������˷��죡");
        return false;
    }
}
