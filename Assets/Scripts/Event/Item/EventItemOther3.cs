using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventItemOther3 : MonoBehaviour, IInteraction
{
    // 对称飞行
    public bool Interaction()
    {
        //暂时禁用
        switch (GameManager.Instance.LevelManager.Level)
        {
            case 40:
				GameManager.Instance.UIManager.ShowInfo("BOSS层无法使用");
                return false;
				break;
		}

        // 获取对称坐标
        Vector2 point = GameManager.Instance.PlayerManager.PlayerController.transform.position * -1;
        // 判断坐标是否可以传送
        foreach (var obj in GameManager.Instance.PoolManager.UseList)
        {
            if ((Vector2)obj.transform.position == point)
            {
                GameManager.Instance.UIManager.ShowInfo("只有空地才能传送哦~");
                return false;
            }
        }
        // 传送
        GameManager.Instance.PlayerManager.PlayerController.transform.position = point;
        // 提示信息
        GameManager.Instance.UIManager.ShowInfo($"使用 {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, 23).Name} ，移动到目标点。");
        GameManager.Instance.BackpackManager.ConsumeItem(23);
        return false;
    }
}
