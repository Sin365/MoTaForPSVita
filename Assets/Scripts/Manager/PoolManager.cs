using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    private Dictionary<EResourceType, Dictionary<int, List<GameObject>>> _resourcePool;
    private List<GameObject> _useList;

    public PoolManager()
    {
        _resourcePool = new Dictionary<EResourceType, Dictionary<int, List<GameObject>>>();
        _useList = new List<GameObject>();
    }

    public List<GameObject> UseList { get => _useList; }

    /// <summary>
    /// 从对象池获取资源
    /// </summary>
    /// <param name="type">资源类型</param>
    /// <param name="id">资源 id</param>
    /// <returns>资源对象</returns>
    public GameObject GetResourceInFreePool(EResourceType type, int id)
    {
        GameObject tempObj = null;
        // 判断对象池是否有该类型对象
        if (_resourcePool.ContainsKey(type))
        {
            // 判断空闲对象池是否有该 id
            if (_resourcePool[type].ContainsKey(id))
            {
                // 如果个数大于 0
                if (_resourcePool[type][id].Count > 0)
                {
                    tempObj = _resourcePool[type][id][0];
                    _resourcePool[type][id].Remove(tempObj);
                }
            }
        }
        // 没有则创建
        if (null == tempObj) tempObj = NewResource(type, id);
        // 加入使用列表
        UseList.Add(tempObj);
        tempObj.SetActive(true);
        return tempObj;
    }

    /// <summary>
    /// 回收所有资源
    /// </summary>
    public void RecycleResource()
    {
        for (int i = UseList.Count - 1; i >= 0; i--)
        {
            RecycleResource(UseList[i]);
        }
    }

    /// <summary>
    /// 回收资源
    /// </summary>
    /// <param name="gameObject">资源对象</param>
    public void RecycleResource(GameObject gameObject)
    {
        EResourceType type = EResourceType.Actor;
        if (gameObject.GetComponent<EnvironmentController>()) type = EResourceType.Environment;
        else if (gameObject.GetComponent<ItemController>()) type = EResourceType.Item;
        else if (gameObject.GetComponent<ActorController>()) type = EResourceType.Actor;
        else if (gameObject.GetComponent<EnemyController>())
        {
            // 触发怪物死亡事件
            if (gameObject.GetComponent<EnemyController>().Health == 0) gameObject.GetComponent<EnemyController>().OnDeath?.Invoke();
            // 怪物需要重置生命值
            type = EResourceType.Enemy;
            gameObject.GetComponent<EnemyController>().Health = gameObject.GetComponent<EnemyController>().MaxHealth;
        }
        else return;
        int id = gameObject.GetComponent<ResourceController>().ID;
        // 判断对象池是否有该类型对象
        if (_resourcePool.ContainsKey(type))
        {
            // 判断对象池是否有该 id
            if (_resourcePool[type].ContainsKey(id)) _resourcePool[type][id].Add(gameObject);
            // 没有则增加
            else
            {
                List<GameObject> tempList = new List<GameObject>();
                tempList.Add(gameObject);
                _resourcePool[type].Add(id, tempList);
            }
        }
        // 没有则增加
        else
        {
            Dictionary<int, List<GameObject>> tempDic = new Dictionary<int, List<GameObject>>();
            List<GameObject> tempList = new List<GameObject>();
            tempList.Add(gameObject);
            tempDic.Add(id, tempList);
            _resourcePool.Add(type, tempDic);
        }
        // 将物体从使用列表中删除
        UseList.Remove(gameObject);
        // 将物体设置为禁用
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 创建资源
    /// </summary>
    /// <param name="type">对象类型</param>
    /// <param name="id">对象 id</param>
    /// <returns>资源对象</returns>
    private GameObject NewResource(EResourceType type, int id)
    {
        return GameManager.Instance.ResourceManager.LoadResource(type, id);

    }
}

