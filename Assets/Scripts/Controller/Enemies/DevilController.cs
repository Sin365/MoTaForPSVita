using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilController : ExplosionproofController
{
    private void OnEnable()
    {
        OnDeath += DeathEvent;
        StartCoroutine(Show());
    }

    private new void OnDisable()
    {
        base.OnDisable();
        OnDeath -= DeathEvent;
    }

    IEnumerator Show()
    {
        yield return new WaitForSeconds(1);
        // 勇士说话
        GameManager.Instance.UIManager.ShowDialog(GameManager.Instance.PlayerManager.PlayerController.Name, new List<string> { "啊？你就是魔王？你怎么还活着！？" }, () =>
        {
            // 魔王说话
            GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "我是不会死的，之前只是对你的能力进行测试而已。" }, () =>
            {
                // 勇士
                GameManager.Instance.UIManager.ShowDialog(GameManager.Instance.PlayerManager.PlayerController.Name, new List<string> { "什么？", "你这话什么意思？", "你为什么要这么做！？" }, () =>
                {
                    // 魔王
                    GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "你的武器神圣剑已被先知预言，使用者必须拥有足够的智慧，而且必须是真正的战士。" }, () =>
                    {
                        // 勇士
                        GameManager.Instance.UIManager.ShowDialog(GameManager.Instance.PlayerManager.PlayerController.Name, new List<string> { "我就是那个战士？" }, () =>
                        {
                            // 魔王
                            GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "是的，你就是最佳人选。", "当你来到魔塔时，你的能力还不足以支配它。", "所以我安排了各种考验来历练你。" }, () =>
                            {
                                // 勇士
                                GameManager.Instance.UIManager.ShowDialog(GameManager.Instance.PlayerManager.PlayerController.Name, new List<string> { "所以公主被困在魔塔里的传说是一个谎言了？", "目的就是把我骗到这里？" }, () =>
                                {
                                    // 魔王
                                    GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "是的，如果我们能够合作，那么伟大的时代就会降临。" }, () =>
                                    {
                                        // 勇士
                                        GameManager.Instance.UIManager.ShowDialog(GameManager.Instance.PlayerManager.PlayerController.Name, new List<string> { "我不会让你这么做的！", "受死吧！" }, () =>
                                        {
                                            // 启用人物控制器
                                            GameManager.Instance.PlayerManager.Enable = true;
                                        });
                                    });
                                });
                            });
                        });
                    });
                });
            });
        });
        yield break;
    }

    private void DeathEvent()
    {
        // 关闭人物控制器
        GameManager.Instance.PlayerManager.Enable = false;
        // 锁定人物控制器
        GameManager.Instance.PlayerManager.LockEnable = true;
        // 解锁音乐
        GameManager.Instance.SoundManager.LockEnable = false;
        // 音频播放
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "GameOver");
        // 勇士
        GameManager.Instance.UIManager.ShowDialog("MrSunyner", new List<string> { "恭喜你顺利通关，此游戏为本人的第一个完整的游戏作品，制作比较简陋请谅解，感谢你的游玩，再见。（PS：以上是保留前人留下的原话）" }, () =>
        {
            // 回到主菜单
            GameManager.Instance.BackHomeEvent();
        });
    }
}
