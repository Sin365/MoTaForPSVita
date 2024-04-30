using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventLevel24Floor1 : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded += ChangeWallEvent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameManager.Instance.PlotManager.PlotDictionary[18] == 3)
        {
            // 跳跃楼层
            GameManager.Instance.LevelManager.Level = 50;
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded -= ChangeWallEvent;
    }

    /// <summary>
    /// 获取守卫事件
    /// </summary>
    private void ChangeWallEvent()
    {
        if (GameManager.Instance.PlotManager.PlotDictionary[18] == 2)
        {
            // 回收墙壁
            List<EnvironmentController> objs = new List<EnvironmentController>();
            // 从已使用物体列表中按位置获取物体
            GameManager.Instance.PoolManager.UseList.ForEach(obj =>
            {
                if ((Vector2)obj.transform.position == new Vector2(0, 2)) objs.Add(obj.GetComponent<EnvironmentController>());
                else if ((Vector2)obj.transform.position == new Vector2(0, 3)) objs.Add(obj.GetComponent<EnvironmentController>());
                else if ((Vector2)obj.transform.position == new Vector2(0, 4)) objs.Add(obj.GetComponent<EnvironmentController>());
            });
            for (int i = 0; i < objs.Count; i++)
            {
                GameManager.Instance.PoolManager.RecycleResource(objs[i].gameObject);
            }
            // 创建墙壁
            GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 6).transform.position = new Vector2(-1, 5);
            GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 6).transform.position = new Vector2(1, 5);
            // 改变剧情
            GameManager.Instance.PlotManager.PlotDictionary[18] = 3;
        }
    }
}
