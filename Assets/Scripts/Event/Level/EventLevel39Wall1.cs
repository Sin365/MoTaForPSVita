using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventLevel39Wall1 : MonoBehaviour
{
    private EnvironmentController _door1;
    private EnvironmentController _door2;
    private EnvironmentController _door3;
    private EnvironmentController _door4;
    private EnvironmentController _door5;
    private EnvironmentController _door6;
    private EnvironmentController _door7;
    private EnvironmentController _door8;
    private EnvironmentController _door9;

    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded += GetGameObjectEvent;
    }

    private void OnDisable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded -= GetGameObjectEvent;
    }

    /// <summary>
    /// 获取守卫事件
    /// </summary>
    private void GetGameObjectEvent()
    {
        _door1 = null;
        _door2 = null;
        _door3 = null;
        _door4 = null;
        _door5 = null;
        _door6 = null;
        _door7 = null;
        _door8 = null;
        _door9 = null;
        // 从已使用物体列表中按位置获取物体
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if ((Vector2)obj.transform.position == new Vector2(-4, 4))
            {
                _door1 = obj.GetComponent<EnvironmentController>();
                _door1.OnOpened += () => { _door1 = null; DetectionOpen(); };
            }
            else if ((Vector2)obj.transform.position == new Vector2(-2, 4))
            {
                _door2 = obj.GetComponent<EnvironmentController>();
                _door2.OnOpened += () => { _door2 = null; DetectionOpen(); };
            }
            else if ((Vector2)obj.transform.position == new Vector2(0, 4))
            {
                _door3 = obj.GetComponent<EnvironmentController>();
                _door3.OnOpened += () => { _door3 = null; DetectionOpen(); };
            }
            else if ((Vector2)obj.transform.position == new Vector2(-4, 2))
            {
                _door4 = obj.GetComponent<EnvironmentController>();
                _door4.OnOpened += () => { _door4 = null; DetectionOpen(); };
            }
            else if ((Vector2)obj.transform.position == new Vector2(-2, 2))
            {
                _door5 = obj.GetComponent<EnvironmentController>();
                _door5.OnOpened += () => { _door5 = null; DetectionOpen(); };
            }
            else if ((Vector2)obj.transform.position == new Vector2(0, 2))
            {
                _door6 = obj.GetComponent<EnvironmentController>();
                _door6.OnOpened += () => { _door6 = null; DetectionOpen(); };
            }
            else if ((Vector2)obj.transform.position == new Vector2(-4, 0))
            {
                _door7 = obj.GetComponent<EnvironmentController>();
                _door7.OnOpened += () => { _door7 = null; DetectionOpen(); };
            }
            else if ((Vector2)obj.transform.position == new Vector2(-2, 0))
            {
                _door8 = obj.GetComponent<EnvironmentController>();
                _door8.OnOpened += () => { _door8 = null; DetectionOpen(); };
            }
            else if ((Vector2)obj.transform.position == new Vector2(0, 0))
            {
                _door9 = obj.GetComponent<EnvironmentController>();
                _door9.OnOpened += () => { _door9 = null; DetectionOpen(); };
            }
        });
    }

    /// <summary>
    /// 检测门是否能打开
    /// </summary>
    private void DetectionOpen()
    {
        if (_door2 == null && _door6 == null && _door1 != null && _door3 != null && _door4 != null && _door5 != null && _door7 != null && _door8 != null && _door9 != null)
        {
            // 打开所有门
            _door1.Open(() =>
            {
                // 创建秘宝
                GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 23).transform.position = new Vector2(-2, 2);
            });
            _door3.Open(null);
            _door4.Open(null);
            _door5.Open(null);
            _door7.Open(null);
            _door8.Open(null);
            _door9.Open(null);
        }
    }
}
