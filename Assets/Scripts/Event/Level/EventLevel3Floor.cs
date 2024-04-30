using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventLevel3Floor : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 禁用人物控制器
            GameManager.Instance.PlayerManager.Enable = false;
            // 魔王出现
            _animator.SetTrigger("showDevil");
            // 音频播放
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "Trap");
            // 锁定音频
            GameManager.Instance.SoundManager.LockEnable = true;
        }
    }

    /// <summary>
    /// 魔王刚出现时说话
    /// </summary>
    public void DevilShowTalk()
    {
        // 魔王说话
        GameManager.Instance.UIManager.ShowDialog("魔王", new List<string> { "欢迎来到魔塔，你是第 114514 位挑战我的勇士。", "如果能打败我的手下，我就与你一对一的决斗。" }, (() =>
        {
            // 角色说话
            GameManager.Instance.UIManager.ShowDialog("勇者", new List<string> { "什么？" }, () =>
            {
                // 魔法守备出现
                _animator.SetTrigger("showMagicGuard");
            });
        }));
    }

    public void MagicAttack()
    {
        // 音频播放
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Magic");
    }

    /// <summary>
    /// 玩家被打败
    /// </summary>
    public void PlayerFail()
    {
        // 解锁音频
        GameManager.Instance.SoundManager.LockEnable = false;
        // 回收剧情资源
        GameManager.Instance.PoolManager.RecycleResource(gameObject);
        // 修改玩家属性
        GameManager.Instance.PlayerManager.PlayerInfo.Health = 400;
        GameManager.Instance.PlayerManager.PlayerInfo.Attack = 10;
        GameManager.Instance.PlayerManager.PlayerInfo.Defence = 10;
        GameManager.Instance.PlayerManager.PlayerInfo.WeaponID = 0;
        GameManager.Instance.PlayerManager.PlayerInfo.ArmorID = 0;
        // 移动玩家资源位置 2 楼
        GameManager.Instance.ResourceManager.MovePlayerPointForLevel(2, new Vector2(-3, -2));
        // 楼层传送
        GameManager.Instance.LevelManager.Level = 2;
        // 移动玩家资源位置 3 楼
        GameManager.Instance.ResourceManager.MovePlayerPointForLevel(3, new Vector2(-4, -5));
        // 触发小偷对话
        GameManager.Instance.UIManager.ShowDialog("小偷", new List<string> { "喂！", "醒一醒！！！" }, () =>
        {
            // 启用人物控制器
            GameManager.Instance.PlayerManager.Enable = true;
            // 音频播放
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "1-9");
        });
    }
}
