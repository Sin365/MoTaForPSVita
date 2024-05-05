public class EventLevel46Actor2 : ActorController
{
	ShopComm.E_ShopFloor floor = ShopComm.E_ShopFloor.F46;
	public override bool Interaction()
	{
		// 打开商店界面
		GameManager.Instance.EventManager.OnShopShow?.Invoke(GetComponent<ActorController>().Name, floor,
			this,
			() => ShopComm.BuyHP(floor),
			() => ShopComm.BuyAtk(floor),
			() => ShopComm.BuyDef(floor));
		return false;
	}
}
