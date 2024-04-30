using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventItemKey4 : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        List<EnvironmentController> environmentControllers = new List<EnvironmentController>();
        // 获取本层所有黄色门
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if (obj.GetComponent<EnvironmentController>() != null && obj.GetComponent<EnvironmentController>().ID == 1) environmentControllers.Add(obj.GetComponent<EnvironmentController>());
        });
        // 打开所有门
        environmentControllers.ForEach(ec =>
        {
            ec.Open(null);
        });
        // 提示信息并从背包清除
        int itemId = 4;
        GameManager.Instance.UIManager.ShowInfo($"使用 {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, itemId).Name} 1 把，打开了所有的黄色门。");
        GameManager.Instance.BackpackManager.ConsumeItem(itemId);
        return false;
    }
}
