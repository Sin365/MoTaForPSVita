using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EventLevel33Floor2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 创建魔法门
            GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 22).transform.position = (Vector2)transform.position + Vector2.up;
            GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 22).transform.position = (Vector2)transform.position - Vector2.up * 3;
            // 启用人物控制器
            GameManager.Instance.PlayerManager.Enable = true;
            // 回收物体
            GameManager.Instance.PoolManager.RecycleResource(gameObject);
        }
    }
}