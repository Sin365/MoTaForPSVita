using System.Collections.Generic;
using UnityEngine;

public class EventItemArtifact4 : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        List<GameObject> tempObjs = new List<GameObject>();
        // 获取四周的岩浆
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if ((Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.up))
            {
                if (obj.GetComponent<EnvironmentController>() != null && obj.GetComponent<EnvironmentController>().ID == 15) tempObjs.Add(obj);
            }
            else if ((Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.down))
            {
                if (obj.GetComponent<EnvironmentController>() != null && obj.GetComponent<EnvironmentController>().ID == 15) tempObjs.Add(obj);
            }
            else if ((Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.left))
            {
                if (obj.GetComponent<EnvironmentController>() != null && obj.GetComponent<EnvironmentController>().ID == 15) tempObjs.Add(obj);
            }
            else if ((Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.right))
            {
                if (obj.GetComponent<EnvironmentController>() != null && obj.GetComponent<EnvironmentController>().ID == 15) tempObjs.Add(obj);
            }
        });
        // 回收资源
        foreach (var obj in tempObjs)
        {
            GameManager.Instance.PoolManager.RecycleResource(obj);
        }
        // 提示信息
        int itemId = 13;
        GameManager.Instance.UIManager.ShowInfo($"使用 {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, itemId).Name} ，冰冻了周围的岩浆。");
        return false;
    }
}
