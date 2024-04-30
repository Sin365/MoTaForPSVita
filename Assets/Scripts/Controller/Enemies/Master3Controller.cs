using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master3Controller : EnemyController
{
    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnPlayerArrive += AttackPlayerEvent;
    }

    private new void OnDisable()
    {
        base.OnDisable();
        GameManager.Instance.EventManager.OnPlayerArrive -= AttackPlayerEvent;
    }

    private void AttackPlayerEvent(Vector2 point)
    {
        // 计算是否在攻击范围
        if (point == (Vector2)transform.position + Vector2.up) AttackPlayer(point, EDirectionType.DOWN);
        else if (point == (Vector2)transform.position + Vector2.down) AttackPlayer(point, EDirectionType.UP);
        else if (point == (Vector2)transform.position + Vector2.left) AttackPlayer(point, EDirectionType.RIGHT);
        else if (point == (Vector2)transform.position + Vector2.right) AttackPlayer(point, EDirectionType.LEFT);
    }

    private void AttackPlayer(Vector2 point, EDirectionType direction)
    {
        if (GameManager.Instance.PlayerManager.PlayerInfo.ArmorID == 33) return;
        // 扣 100 血
        GameManager.Instance.PlayerManager.PlayerInfo.Health -= 100;
        GameManager.Instance.UIManager.ShowInfo($"{GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Enemy, 26).Name} 对你造成了 100 点魔法伤害。");
        // 显示特效
        GameObject obj = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 31);
        obj.transform.position = point;
        obj.GetComponent<MagicController>().ShowMagic(direction);
    }
}
