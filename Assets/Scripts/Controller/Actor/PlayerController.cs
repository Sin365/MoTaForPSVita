using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : ActorController
{
    private Rigidbody2D rigidbody2d;

    private bool walking = false;
    private EDirectionType direction;

    protected new void Awake()
    {
        base.Awake();

        // 绑定玩家控制器
        GameManager.Instance.PlayerManager.BindPlayer(this);
        // 绑定输入事件
        GameManager.Instance.EventManager.OnMoveInput = OnMoveInputEvent;

        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        walking = false;
    }

    void Update()
    {
        CheckAnimator();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 捡东西
        if (collision.CompareTag("Item"))
        {
            // 宝石自动使用
            if (collision.GetComponent<ItemController>().ID == 7)
            {
                GameManager.Instance.PlayerManager.PlayerInfo.Attack += 3;
                GameManager.Instance.UIManager.ShowInfo($"获得 {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, 7).Name} 1 个，增加 3 点攻击力。");
                GameManager.Instance.PoolManager.RecycleResource(collision.gameObject);
                GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "PickUp");
            }
            else if (collision.GetComponent<ItemController>().ID == 8)
            {
                GameManager.Instance.PlayerManager.PlayerInfo.Defence += 3;
                GameManager.Instance.UIManager.ShowInfo($"获得 {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, 8).Name} 1 个，增加 3 点防御力。");
                GameManager.Instance.PoolManager.RecycleResource(collision.gameObject);
                GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "PickUp");
            }
            else GameManager.Instance.BackpackManager.PickUp(collision.GetComponent<ItemController>());
        }
        // 打怪
        else if (collision.CompareTag("Enemy")) GameManager.Instance.CombatManager.StartFight(collision.GetComponent<EnemyController>());
    }

    private void OnDestroy()
    {
        // 解绑输入事件
        GameManager.Instance.PlayerManager.UnbindPlayer();
    }

    /// <summary>
    /// 检测动画状态机
    /// </summary>
    private void CheckAnimator()
    {
        _animator.SetBool("attacking", GameManager.Instance.CombatManager.Fighting);
        _animator.SetBool("walking", walking);
        _animator.SetFloat("direction", (int)direction);
    }

    /// <summary>
    /// 获取角色输入
    /// </summary>
    /// <param name="inputType">输入类型</param>
    private void OnMoveInputEvent(EDirectionType inputType)
    {
        if (walking || GameManager.Instance.CombatManager.Fighting) return;
        // 状态赋值
        direction = GameManager.Instance.CombatManager.Fighting ? direction : inputType;
        // 尝试获取面前的物体
        Vector2 targetPoint = Vector2.zero;
        switch (direction)
        {
            case EDirectionType.UP:
                targetPoint.y = 1;
                break;
            case EDirectionType.DOWN:
                targetPoint.y = -1;
                break;
            case EDirectionType.LEFT:
                targetPoint.x = -1;
                break;
            case EDirectionType.RIGHT:
                targetPoint.x = 1;
                break;
            default:
                break;
        }
        targetPoint += (Vector2)transform.position;
        RaycastHit2D[] hits = Physics2D.RaycastAll(targetPoint, (targetPoint - (Vector2)transform.position), .1f);
        // 如果有可交互的物体
        if (hits.Length > 0)
        {
            // 筛选最上层可物体
            int maxOrder = -100;
            GameObject obj = null;
            foreach (var hit in hits)
            {
                if (hit.collider.GetComponent<SpriteRenderer>().sortingOrder > maxOrder)
                {
                    maxOrder = hit.collider.GetComponent<SpriteRenderer>().sortingOrder;
                    obj = hit.collider.gameObject;
                }
            }
            if (null != obj.GetComponent<IInteraction>())
            {
                // 进行交互 视情况停止移动
                if (!obj.GetComponent<IInteraction>().Interaction()) return;
            }
        }
        // 匀速移动
        StartCoroutine(Moving(transform.position, targetPoint));
    }

    /// <summary>
    /// 移动
    /// </summary>
    IEnumerator Moving(Vector2 oldPoint, Vector2 newPoint)
    {
        float speed = 8f;
        float timer = 0;
        walking = true;
        while ((Vector2)transform.position != newPoint)
        {
            timer += Time.deltaTime;
            float t = timer / (oldPoint - newPoint).sqrMagnitude * speed;
            Vector2 point = Vector2.Lerp(oldPoint, newPoint, t);
            rigidbody2d.MovePosition(point);
            yield return null;
        }
        walking = false;
        // 移动完成后触发事件
        GameManager.Instance.EventManager.OnPlayerArrive?.Invoke(newPoint);
    }
}
