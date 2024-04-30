using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventItemOther1 : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        // 获取四周的墙
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if ((Vector2)obj.transform.position == new Vector2(GameManager.Instance.PlayerManager.PlayerController.transform.position.x + 1, GameManager.Instance.PlayerManager.PlayerController.transform.position.y))
            {
                if (obj.GetComponent<EnvironmentController>() != null && obj.GetComponent<EnvironmentController>().ID == 6) obj.GetComponent<EnvironmentController>().Open(null);
            }
            else if ((Vector2)obj.transform.position == new Vector2(GameManager.Instance.PlayerManager.PlayerController.transform.position.x - 1, GameManager.Instance.PlayerManager.PlayerController.transform.position.y))
            {
                if (obj.GetComponent<EnvironmentController>() != null && obj.GetComponent<EnvironmentController>().ID == 6) obj.GetComponent<EnvironmentController>().Open(null);
            }
            else if ((Vector2)obj.transform.position == new Vector2(GameManager.Instance.PlayerManager.PlayerController.transform.position.x, GameManager.Instance.PlayerManager.PlayerController.transform.position.y + 1))
            {
                if (obj.GetComponent<EnvironmentController>() != null && obj.GetComponent<EnvironmentController>().ID == 6) obj.GetComponent<EnvironmentController>().Open(null);
            }
            else if ((Vector2)obj.transform.position == new Vector2(GameManager.Instance.PlayerManager.PlayerController.transform.position.x, GameManager.Instance.PlayerManager.PlayerController.transform.position.y - 1))
            {
                if (obj.GetComponent<EnvironmentController>() != null && obj.GetComponent<EnvironmentController>().ID == 6) obj.GetComponent<EnvironmentController>().Open(null);
            }
        });
        // 提示信息并从背包清除
        int itemId = 13;
        GameManager.Instance.UIManager.ShowInfo($"使用 {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, itemId).Name} 1 把，打开了周围的墙壁。");
        GameManager.Instance.BackpackManager.ConsumeItem(itemId);
        return false;
    }
}
