using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel28Actor1 : ActorController
{
    public override bool Interaction()
    {

		GameManager.Instance.UIManager.ShowInteractionDialog(GetComponent<ResourceController>().Name, "我按100个金币一把的价格回收黄钥匙，你出售吗？", "成交！", "算了吧", () =>
		{
            if (GameManager.Instance.BackpackManager.ConsumeItem(1))
            {
                GameManager.Instance.PlayerManager.PlayerInfo.Gold += 100;

                GameManager.Instance.UIManager.ShowInfo("失去黄色钥匙x1");
				GameManager.Instance.UIManager.ShowInfo("获得100金币");
				// 打开人物控制器
				GameManager.Instance.PlayerManager.Enable = true;
                // 音频播放
                GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Yes");
            }
            else
			{
				GameManager.Instance.UIManager.ShowInfo("你没有黄钥匙");
				// 打开人物控制器
				GameManager.Instance.PlayerManager.Enable = true;
			}

		});
		return false;

		/*
        GameManager.Instance.UIManager.ShowInteractionDialog(GetComponent<ResourceController>().Name, "我会随机赋予你属性，但同样我也会收取你一部分属性，你确定要试试吗？", "谁怕谁", "我走了", () =>
        {
            // 随机两个属性
            string getStr = "", giveStr = "";
            ERandomShopType getType = ERandomShopType.Health, giveType = ERandomShopType.Health;
            int getNumber = Random.Range(0, 100);
            int giveNumber = Random.Range(0, 100);
            int randomNumber1 = Random.Range(0, 100);
            int randomNumber2 = Random.Range(0, 100);
            if (randomNumber1 <= 33) getType = ERandomShopType.Health;
            else if (randomNumber1 > 33 && randomNumber1 <= 66) getType = ERandomShopType.Attack;
            else if (randomNumber1 > 66) getType = ERandomShopType.Defence;
            if (randomNumber2 <= 33) giveType = ERandomShopType.Health;
            else if (randomNumber2 > 33 && randomNumber2 <= 66) giveType = ERandomShopType.Attack;
            else if (randomNumber2 > 66) giveType = ERandomShopType.Defence;
            // 收取属性
            switch (getType)
            {
                case ERandomShopType.Health:
                    getStr = "生命值";
                    getNumber *= 10;
                    GameManager.Instance.PlayerManager.PlayerInfo.Health -= getNumber;
                    break;
                case ERandomShopType.Attack:
                    getStr = "攻击力";
                    GameManager.Instance.PlayerManager.PlayerInfo.Attack -= getNumber;
                    break;
                case ERandomShopType.Defence:
                    getStr = "防御力";
                    GameManager.Instance.PlayerManager.PlayerInfo.Defence -= getNumber;
                    break;
            }
            // 获取属性
            switch (giveType)
            {
                case ERandomShopType.Health:
                    giveStr = "生命值";
                    giveNumber *= 10;
                    GameManager.Instance.PlayerManager.PlayerInfo.Health += giveNumber;
                    break;
                case ERandomShopType.Attack:
                    giveStr = "攻击力";
                    GameManager.Instance.PlayerManager.PlayerInfo.Attack += giveNumber;
                    break;
                case ERandomShopType.Defence:
                    giveStr = "防御力";
                    GameManager.Instance.PlayerManager.PlayerInfo.Defence += giveNumber;
                    break;
            }
            // 显示提示
            GameManager.Instance.UIManager.ShowInfo($"损失 {getNumber} 点{getStr},获得 {giveNumber} 点{giveStr}。");
            // 打开人物控制器
            GameManager.Instance.PlayerManager.Enable = true;
            // 音频播放
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Yes");
        });
        return false;
        */
	}
}
