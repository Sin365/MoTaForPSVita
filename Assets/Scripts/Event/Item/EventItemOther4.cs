using UnityEngine;

public class EventItemOther4 : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        // 楼层跳跃
        GameManager.Instance.LevelManager.Level -= 1;
        // 提示信息并从背包清除
        int itemId = 22;
        GameManager.Instance.UIManager.ShowInfo($"使用 {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, itemId).Name} ，向下一层传送。");
        GameManager.Instance.BackpackManager.ConsumeItem(itemId);
        return false;
    }
}
