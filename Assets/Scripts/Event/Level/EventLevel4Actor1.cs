using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ERandomShopType
{
    Health,
    Attack,
    Defence,
    Gold,
}

public class EventLevel4Actor1 : ActorController
{
    public override bool Interaction()
    {
        // 打开商店界面
        GameManager.Instance.EventManager.OnShopShow?.Invoke(GetComponent<ActorController>().Name, 25, ShopShowCallback);
        return false;
    }

    /// <summary>
    /// 打开商店回调
    /// </summary>
    private void ShopShowCallback()
    {
        // 判断金币是否足够
        if (GameManager.Instance.PlayerManager.PlayerInfo.Gold < 25)
        {
            GameManager.Instance.UIManager.ShowInfo($"不会有人连 25 金币都没有吧！");
            // 音频播放
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "No");
            return;
        }
        GameManager.Instance.PlayerManager.PlayerInfo.Gold -= 25;
        // 随机一个属性
        ERandomShopType type = ERandomShopType.Health;
        int randomNumber = Random.Range(0, 100);
        if (randomNumber <= 30) type = ERandomShopType.Health;
        else if (randomNumber > 30 && randomNumber <= 60) type = ERandomShopType.Attack;
        else if (randomNumber > 60 && randomNumber <= 90) type = ERandomShopType.Defence;
        else type = ERandomShopType.Gold;
        // 随机数值
        switch (type)
        {
            case ERandomShopType.Health:
                randomNumber = Random.Range(50, 200);
                GameManager.Instance.PlayerManager.PlayerInfo.Health += randomNumber;
                GameManager.Instance.UIManager.ShowInfo($"获得 {randomNumber} 点生命值提升！");
                break;
            case ERandomShopType.Attack:
                randomNumber = Random.Range(2, 10);
                GameManager.Instance.PlayerManager.PlayerInfo.Attack += randomNumber;
                GameManager.Instance.UIManager.ShowInfo($"获得 {randomNumber} 点攻击力提升！");
                break;
            case ERandomShopType.Defence:
                randomNumber = Random.Range(2, 10);
                GameManager.Instance.PlayerManager.PlayerInfo.Defence += randomNumber;
                GameManager.Instance.UIManager.ShowInfo($"获得 {randomNumber} 点防御力提升！");
                break;
            case ERandomShopType.Gold:
                randomNumber = Random.Range(20, 100);
                GameManager.Instance.PlayerManager.PlayerInfo.Gold += randomNumber;
                GameManager.Instance.UIManager.ShowInfo($"运气爆棚！获得 {randomNumber} 金币！");
                break;
            default:
                print("商店随机了个什么玩意儿？");
                break;
        }
        // 音频播放
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Yes");
    }
}
