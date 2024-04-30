using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoSingleton<CombatManager>
{
    private bool _fighting = false;
    private float _combatInterval = .25f;
    private EnemyController _enemy;
    private bool _attackerIsPlayer = true;

    public bool Fighting { get => _fighting; }

    /// <summary>
    /// 开始战斗
    /// </summary>
    /// <param name="enemyController">敌人控制器</param>
    public void StartFight(EnemyController enemyController)
    {
        _enemy = enemyController;
        // 初始化 UI
        GameManager.Instance.EventManager.OnEnemyCombated?.Invoke(_enemy);
        // 开始战斗
        _fighting = true;
        // 禁用玩家控制器
        GameManager.Instance.PlayerManager.Enable = false;
        StartCoroutine(InTheFighting());
    }

    /// <summary>
    /// 战斗
    /// </summary>
    IEnumerator InTheFighting()
    {
        while (_fighting)
        {
            // 音频播放
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Attack");
            yield return new WaitForSeconds(_combatInterval);
            // 玩家回合
            if (_attackerIsPlayer)
            {
                int damage = GameManager.Instance.PlayerManager.PlayerInfo.Attack - _enemy.Defence;
                damage = damage >= 0 ? damage : 0;
                // 拥有十字架时对吸血鬼和兽人伤害翻倍
                CalculateJudge(ref damage);
                _enemy.SetHealth(_enemy.Health - damage);
                GameManager.Instance.EventManager.OnEnemyCombated?.Invoke(_enemy);
                if (_enemy.Health <= 0)
                {
                    StopFight();
                    yield break;
                }
            }
            // 敌人回合
            else
            {
                int damage = _enemy.Attack - GameManager.Instance.PlayerManager.PlayerInfo.Defence;
                damage = damage >= 0 ? damage : 0;
                GameManager.Instance.PlayerManager.PlayerInfo.Health -= damage;
                if (GameManager.Instance.PlayerManager.PlayerInfo.Health == 0)
                {
                    StopFight();
                    yield break;
                }
            }
            // 切换攻击方
            _attackerIsPlayer = !_attackerIsPlayer;
        }
    }

    /// <summary>
    /// 结束战斗
    /// </summary>
    private void StopFight()
    {
        _fighting = false;
        // 玩家胜利 失败不在此处判断 直接绑定玩家生命值
        if (_attackerIsPlayer)
        {
            // 更新 UI
            GameManager.Instance.EventManager.OnEnemyCombated?.Invoke(null);
            GameManager.Instance.UIManager.ShowInfo($"击败 {_enemy.Name} 获得 {(GameManager.Instance.BackpackManager.BackpackDictionary.ContainsKey(18) ? _enemy.Gold * 2 : _enemy.Gold)} 赏金。");
            // 人物奖励
            GameManager.Instance.PlayerManager.PlayerInfo.Gold += GameManager.Instance.BackpackManager.BackpackDictionary.ContainsKey(18) ? _enemy.Gold * 2 : _enemy.Gold;
            // 删除怪物
            GameManager.Instance.PoolManager.RecycleResource(_enemy.gameObject);
        }
        // 启用玩家控制器
        GameManager.Instance.PlayerManager.Enable = true;
    }

    /// <summary>
    /// 计算神器对特殊怪物的伤害
    /// </summary>
    /// <param name="damage">伤害 引用类型 直接返回</param>
    private void CalculateJudge(ref int damage)
    {
        // 判断对方是否是吸血鬼
        if (_enemy.ID == 12 || _enemy.ID == 13 || _enemy.ID == 16)
        {
            // 判断背包是否有十字架 伤害翻倍
            if (GameManager.Instance.BackpackManager.BackpackDictionary.ContainsKey(19)) damage *= 2;
        }
        // 判断对方是否是魔龙
        else if (_enemy.ID == 23)
        {
            // 判断背包是否有屠龙匕首 伤害翻倍
            if (GameManager.Instance.BackpackManager.BackpackDictionary.ContainsKey(20)) damage *= 2;
        }
    }
}
