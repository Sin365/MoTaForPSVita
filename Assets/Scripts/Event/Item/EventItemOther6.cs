using System.Collections.Generic;
using UnityEngine;

public class EventItemOther6 : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        List<GameObject> tempObjs = new List<GameObject>();
        // 获取四周的怪物
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if ((Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.up) && obj.GetComponent<EnemyController>() != null && obj.GetComponent<IExplosionproof>() == null) tempObjs.Add(obj);
            else if ((Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.down) && obj.GetComponent<EnemyController>() != null && obj.GetComponent<IExplosionproof>() == null) tempObjs.Add(obj);
            else if ((Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.left) && obj.GetComponent<EnemyController>() != null && obj.GetComponent<IExplosionproof>() == null) tempObjs.Add(obj);
            else if ((Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.right) && obj.GetComponent<EnemyController>() != null && obj.GetComponent<IExplosionproof>() == null) tempObjs.Add(obj);
        });
        // 回收资源
        foreach (var obj in tempObjs)
        {
            obj.GetComponent<EnemyController>().Health = 0;
            GameManager.Instance.PoolManager.RecycleResource(obj);
        }
        // 提示信息并从背包清除
        int itemId = 16;
        GameManager.Instance.UIManager.ShowInfo($"使用 {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, itemId).Name} ，击杀了周围的敌人。");
        GameManager.Instance.BackpackManager.ConsumeItem(itemId);
        return false;
    }
}
