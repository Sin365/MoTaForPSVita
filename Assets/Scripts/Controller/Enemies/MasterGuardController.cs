using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterGuardController : EnemyController
{
    public bool IsBigBrother;

    private MasterGuardController _brother;
    private Vector2 _attackPoint;

    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded += GetBrotherEvent;
        GameManager.Instance.EventManager.OnPlayerArrive += AttackPlayerEvent;
    }

    private new void OnDisable()
    {
        base.OnDisable();
        GameManager.Instance.EventManager.OnResourceLoaded -= GetBrotherEvent;
        GameManager.Instance.EventManager.OnPlayerArrive -= AttackPlayerEvent;
    }

    private void AttackPlayerEvent(Vector2 point)
    {
        if (GameManager.Instance.PlayerManager.PlayerInfo.ArmorID == 33) return;
        if (point == _attackPoint && null != _brother)
        {
            // 如果有兄弟
            if (IsBigBrother)
            {
                // 扣 1/2 血
                GameManager.Instance.PlayerManager.PlayerInfo.Health = (int)(GameManager.Instance.PlayerManager.PlayerInfo.Health * .5f);
                GameManager.Instance.UIManager.ShowInfo($"两个魔法守卫合力对你造成了 {GameManager.Instance.PlayerManager.PlayerInfo.Health} 点伤害。");
            }
            // 显示特效
            GameObject obj = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 31);
            // 获取方向
            Vector2 direction = (_brother.transform.position - transform.position).normalized;
            obj.transform.position = (Vector2)transform.position + direction;
            EDirectionType directionType = EDirectionType.UP;
            if (direction == Vector2.up) directionType = EDirectionType.UP;
            else if (direction == Vector2.down) directionType = EDirectionType.DOWN;
            else if (direction == Vector2.left) directionType = EDirectionType.LEFT;
            else if (direction == Vector2.right) directionType = EDirectionType.RIGHT;
            obj.GetComponent<MagicController>().ShowMagic(directionType);
        }
    }

    // 获取隔壁守卫
    private void GetBrotherEvent()
    {
        _brother = null;
        // 从已使用物体列表中按位置获取物体
        foreach (var obj in GameManager.Instance.PoolManager.UseList)
        {
            if ((Vector2)obj.transform.position == ((Vector2)transform.position + Vector2.up * 2)) _brother = obj.GetComponent<MasterGuardController>();
            else if ((Vector2)obj.transform.position == ((Vector2)transform.position + Vector2.down * 2)) _brother = obj.GetComponent<MasterGuardController>();
            else if ((Vector2)obj.transform.position == ((Vector2)transform.position + Vector2.left * 2)) _brother = obj.GetComponent<MasterGuardController>();
            else if ((Vector2)obj.transform.position == ((Vector2)transform.position + Vector2.right * 2)) _brother = obj.GetComponent<MasterGuardController>();
            if (_brother != null) break;
        }
        // 判断大哥
        if (_brother != null)
        {
            if (!IsBigBrother && !_brother.GetComponent<MasterGuardController>().IsBigBrother) IsBigBrother = true;
            // 死亡事件
            _brother.OnDeath += () => { _brother = null; };
            // 计算攻击点
            _attackPoint = transform.position + (_brother.transform.position - transform.position).normalized;
        }
    }
}
