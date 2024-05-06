using UnityEngine;

public class EventItemArtifact2 : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        GameManager.Instance.UIManager.ShowInfo("Ү�ձ��ӣ�����~");
        return false;
    }
}
