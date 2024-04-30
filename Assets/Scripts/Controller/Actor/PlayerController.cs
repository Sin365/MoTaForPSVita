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

        // ����ҿ�����
        GameManager.Instance.PlayerManager.BindPlayer(this);
        // �������¼�
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
        // ����
        if (collision.CompareTag("Item"))
        {
            // ��ʯ�Զ�ʹ��
            if (collision.GetComponent<ItemController>().ID == 7)
            {
                GameManager.Instance.PlayerManager.PlayerInfo.Attack += 3;
                GameManager.Instance.UIManager.ShowInfo($"��� {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, 7).Name} 1 �������� 3 �㹥������");
                GameManager.Instance.PoolManager.RecycleResource(collision.gameObject);
                GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "PickUp");
            }
            else if (collision.GetComponent<ItemController>().ID == 8)
            {
                GameManager.Instance.PlayerManager.PlayerInfo.Defence += 3;
                GameManager.Instance.UIManager.ShowInfo($"��� {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, 8).Name} 1 �������� 3 ���������");
                GameManager.Instance.PoolManager.RecycleResource(collision.gameObject);
                GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "PickUp");
            }
            else GameManager.Instance.BackpackManager.PickUp(collision.GetComponent<ItemController>());
        }
        // ���
        else if (collision.CompareTag("Enemy")) GameManager.Instance.CombatManager.StartFight(collision.GetComponent<EnemyController>());
    }

    private void OnDestroy()
    {
        // ��������¼�
        GameManager.Instance.PlayerManager.UnbindPlayer();
    }

    /// <summary>
    /// ��⶯��״̬��
    /// </summary>
    private void CheckAnimator()
    {
        _animator.SetBool("attacking", GameManager.Instance.CombatManager.Fighting);
        _animator.SetBool("walking", walking);
        _animator.SetFloat("direction", (int)direction);
    }

    /// <summary>
    /// ��ȡ��ɫ����
    /// </summary>
    /// <param name="inputType">��������</param>
    private void OnMoveInputEvent(EDirectionType inputType)
    {
        if (walking || GameManager.Instance.CombatManager.Fighting) return;
        // ״̬��ֵ
        direction = GameManager.Instance.CombatManager.Fighting ? direction : inputType;
        // ���Ի�ȡ��ǰ������
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
        // ����пɽ���������
        if (hits.Length > 0)
        {
            // ɸѡ���ϲ������
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
                // ���н��� �����ֹͣ�ƶ�
                if (!obj.GetComponent<IInteraction>().Interaction()) return;
            }
        }
        // �����ƶ�
        StartCoroutine(Moving(transform.position, targetPoint));
    }

    /// <summary>
    /// �ƶ�
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
        // �ƶ���ɺ󴥷��¼�
        GameManager.Instance.EventManager.OnPlayerArrive?.Invoke(newPoint);
    }
}
