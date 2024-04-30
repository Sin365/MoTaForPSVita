using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventLevel25Floor1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 禁用人物控制器
            GameManager.Instance.PlayerManager.Enable = false;
            // 锁定音乐
            GameManager.Instance.SoundManager.LockEnable = true;
            // 开始对话
            GameManager.Instance.UIManager.ShowDialog("神秘人", new List<string> { "杀！死！入！侵！者！" }, (() =>
            {
                // 启用人物控制器
                GameManager.Instance.PlayerManager.Enable = true;
                // 回收剧情资源
                GameManager.Instance.PoolManager.RecycleResource(gameObject);
            }));
        }
    }
}
