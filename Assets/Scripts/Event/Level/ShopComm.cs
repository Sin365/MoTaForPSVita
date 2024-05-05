using System.Collections.Generic;
using UnityEngine;


public class ShopComm
{
	/*
	 初始价格是20金币，购买后会涨价，第n次购买所需金币10n*(n-1)+20
4层商店每次加2点攻或4点防
12层商店每次加4点攻或8点防
32层商店每次加8点攻或16点防
46层商店每次加10点攻或20点防
	*/
	public enum E_ShopFloor
	{
		F4,
		F12,
		F32,
		F46
	}
	public class FloorShopInfo
	{
		public E_ShopFloor Floor;
		public int AddHP;
		public int AddAtk;
		public int AddDef;
	}

	static Dictionary<E_ShopFloor, FloorShopInfo> dictShopFloor = new Dictionary<E_ShopFloor, FloorShopInfo>()
	{ 
		{ E_ShopFloor.F4,new FloorShopInfo(){Floor = E_ShopFloor.F4,AddAtk = 2,AddDef = 4 ,AddHP = 100}},
		{ E_ShopFloor.F12,new FloorShopInfo(){Floor = E_ShopFloor.F4,AddAtk = 4,AddDef = 8,AddHP = 100 }},
		{ E_ShopFloor.F32,new FloorShopInfo(){Floor = E_ShopFloor.F4,AddAtk = 8,AddDef = 16,AddHP = 100 }},
		{ E_ShopFloor.F46,new FloorShopInfo(){Floor = E_ShopFloor.F4,AddAtk = 10,AddDef = 20,AddHP = 100 }},
	};

	public static FloorShopInfo GetFloorShopInfo(E_ShopFloor floor)
	{
		return dictShopFloor[floor];
	}
	public static int GetShopNeedMoney(E_ShopFloor floor)
	{
		int BuyNum = 0;
		switch (floor)
		{
			case E_ShopFloor.F4: BuyNum = GameManager.Instance.PlayerManager.PlayerInfo.StoreBuyNum_F4; break;
			case E_ShopFloor.F12: BuyNum = GameManager.Instance.PlayerManager.PlayerInfo.StoreBuyNum_F12; break;
			case E_ShopFloor.F32: BuyNum = GameManager.Instance.PlayerManager.PlayerInfo.StoreBuyNum_F32; break;
			case E_ShopFloor.F46: BuyNum = GameManager.Instance.PlayerManager.PlayerInfo.StoreBuyNum_F46; break;
		}
		return (10 * BuyNum * (BuyNum - 1) + 20);
	}
	public static bool CheckMoney(E_ShopFloor floor)
	{
		int needMoney = GetShopNeedMoney(floor);
		// 判断金币是否足够
		if (GameManager.Instance.PlayerManager.PlayerInfo.Gold < needMoney)
		{
			GameManager.Instance.UIManager.ShowInfo($"不会有人连 {needMoney} 金币都没有吧！");
			// 音频播放
			GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "No");
			return false;
		}
		return true;
	}
	public static void SetShopUseMoney(E_ShopFloor floor)
	{
		int needMoney = GetShopNeedMoney(floor);
		GameManager.Instance.PlayerManager.PlayerInfo.Gold -= needMoney;

		//追加购买次数
		switch (floor)
		{
			case E_ShopFloor.F4: GameManager.Instance.PlayerManager.PlayerInfo.StoreBuyNum_F4++; break;
			case E_ShopFloor.F12: GameManager.Instance.PlayerManager.PlayerInfo.StoreBuyNum_F12++; break;
			case E_ShopFloor.F32: GameManager.Instance.PlayerManager.PlayerInfo.StoreBuyNum_F32++; break;
			case E_ShopFloor.F46: GameManager.Instance.PlayerManager.PlayerInfo.StoreBuyNum_F46++; break;
		}

	}
	public static void BuyHP(E_ShopFloor floor)
	{
		if (!CheckMoney(floor))
			return;
		SetShopUseMoney(floor);
		int hp = dictShopFloor[floor].AddHP;
		GameManager.Instance.PlayerManager.PlayerInfo.Health += hp;
		GameManager.Instance.UIManager.ShowInfo($"获得 {hp} 点生命值提升！");
		// 音频播放
		GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Yes");
	}
	public static void BuyAtk(E_ShopFloor floor)
	{
		if (!CheckMoney(floor))
			return;
		SetShopUseMoney(floor);
		int atk = dictShopFloor[floor].AddAtk;
		GameManager.Instance.PlayerManager.PlayerInfo.Attack += atk;
		GameManager.Instance.UIManager.ShowInfo($"获得 {atk} 点攻击力提升！");
	}
	public static void BuyDef(E_ShopFloor floor)
	{
		if (!CheckMoney(floor))
			return;
		SetShopUseMoney(floor);
		int def = dictShopFloor[floor].AddDef;
		GameManager.Instance.PlayerManager.PlayerInfo.Defence += def;
		GameManager.Instance.UIManager.ShowInfo($"获得 {def} 点防御力提升！");
	}

}