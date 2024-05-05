using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [HideInInspector]
    public Canvas MainCanvas;

    private Text _levelValueText;

    private Text _playerHealthValueText;
    private Text _playerAttackValueText;
    private Text _playerDefenceValueText;
    private Text _playerGoldValueText;

    private Text _weaponValueText;
    private Text _armorValueText;
    private Image _weaponImage;
    private Image _armorImage;

    private Text _enemyNameValueText;
    private Text _enemyHealthValueText;
    private Text _enemyAttackValueText;
    private Text _enemyDefenceValueText;
    private Image _enemyImage;

    private GridLayoutGroup _backpackInfoPanel;
    private Dictionary<int, GameObject> _backpackDictionary = new Dictionary<int, GameObject>();

    private GameObject _gameOverPanel;
    private Button _gameOverBackHomeButton;

    private GameObject _dialogPanel;
    private Text _dialogNameValueText;
    private Text _dialogInfoValueText;

    private GameObject _bookPanel;
    private GameObject _bookContent;
	public Button _bookPanelBtnClose;

	private GameObject _shopPanel;
    private Text _shopNameValueText;
    private Text _shopInfoValueText;
    private Button _shopAddHPButton;
    private Text _shopAddHPButtonText;
	private Button _shopAddAtkButton;
    private Text _shopAddAtkButtonText;
	private Button _shopAddDefButton;
    private Text _shopAddDefButtonText;

	private Button _shopNoButton;

    private GameObject _infoPanel;

    private GameObject _notepadPanel;
    private Text _notepadValueText;
	public Button __notepadPanelBtnClose;

	private GameObject _interactionDialogPanel;
    private Text _interactionDialogNameValueText;
    private Text _interactionDialogInfoValueText;
    private Button _interactionDialogYesButton;
    private Button _interactionDialogNoButton;
    private Text _interactionDialogYesButtonValueText;
    private Text _interactionDialogNoButtonValueText;

    public Transform BookContent { get => _bookContent.transform; }

    private new void Awake()
    {
        base.Awake();

        // 获取 UI
        MainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();

        Transform leftBackGroundPanel = MainCanvas.transform.Find("LeftBackGroundPanel");

        _levelValueText = leftBackGroundPanel.Find("LevelInfoPanel").Find("LevelValueText").GetComponent<Text>();

        _playerHealthValueText = leftBackGroundPanel.Find("PlayerInfoPanel").Find("PlayerHealthValueText").GetComponent<Text>();
        _playerAttackValueText = leftBackGroundPanel.Find("PlayerInfoPanel").Find("PlayerAttackValueText").GetComponent<Text>();
        _playerDefenceValueText = leftBackGroundPanel.Find("PlayerInfoPanel").Find("PlayerDefenceValueText").GetComponent<Text>();
        _playerGoldValueText = leftBackGroundPanel.Find("PlayerInfoPanel").Find("PlayerGoldValueText").GetComponent<Text>();

        _weaponValueText = leftBackGroundPanel.Find("EquipmentInfoPanel").Find("WeaponValueText").GetComponent<Text>();
        _armorValueText = leftBackGroundPanel.Find("EquipmentInfoPanel").Find("ArmorValueText").GetComponent<Text>();
        _weaponImage = leftBackGroundPanel.Find("EquipmentInfoPanel").Find("WeaponImage").GetComponent<Image>();
        _armorImage = leftBackGroundPanel.Find("EquipmentInfoPanel").Find("ArmorImage").GetComponent<Image>();

        Transform rightBackGroundPanel = MainCanvas.transform.Find("RightBackGroundPanel");

        _enemyImage = rightBackGroundPanel.Find("EnemyInfoPanel").Find("EnemyImage").GetComponent<Image>();
        _enemyImage.enabled = false;

        _enemyNameValueText = rightBackGroundPanel.Find("EnemyInfoPanel").Find("EnemyNameValueText").GetComponent<Text>();

        _enemyHealthValueText = rightBackGroundPanel.Find("EnemyInfoPanel").Find("EnemyHealthValueText").GetComponent<Text>();
        _enemyAttackValueText = rightBackGroundPanel.Find("EnemyInfoPanel").Find("EnemyAttackValueText").GetComponent<Text>();
        _enemyDefenceValueText = rightBackGroundPanel.Find("EnemyInfoPanel").Find("EnemyDefenceValueText").GetComponent<Text>();

        _backpackInfoPanel = rightBackGroundPanel.Find("ItemInfoPanel").Find("BackpackInfoPanel").GetComponent<GridLayoutGroup>();

        _gameOverPanel = MainCanvas.transform.Find("GameOverPanel").gameObject;
        _gameOverBackHomeButton = _gameOverPanel.transform.Find("BackHomeButton").GetComponent<Button>();
        _gameOverPanel.SetActive(false);

        _dialogPanel = MainCanvas.transform.Find("DialogPanel").gameObject;
        _dialogNameValueText = _dialogPanel.transform.Find("NameVelueText").GetComponent<Text>();
        _dialogInfoValueText = _dialogPanel.transform.Find("InfoValueText").GetComponent<Text>();
        _dialogPanel.SetActive(false);

        _bookPanel = MainCanvas.transform.Find("BookPanel").gameObject;
        _bookContent = _bookPanel.transform.Find("Scroll View").Find("Viewport").Find("BookContent").gameObject;
        //关闭按钮
        _bookPanelBtnClose = _bookPanel.transform.Find("CloseButton").GetComponent<Button>();
        _bookPanelBtnClose.onClick.AddListener(CloseBook);

		_bookPanel.SetActive(false);

        _shopPanel = MainCanvas.transform.Find("ShopPanel").gameObject;
        _shopNameValueText = _shopPanel.transform.Find("ShopNameValueText").GetComponent<Text>();
        _shopInfoValueText = _shopPanel.transform.Find("ShopValueText").GetComponent<Text>();
		_shopAddHPButton = _shopPanel.transform.Find("shopAddHPButton").GetComponent<Button>();
		_shopAddHPButtonText = _shopPanel.transform.Find("shopAddHPButton/shopAddHPButtonText").GetComponent<Text>();
		_shopAddAtkButton = _shopPanel.transform.Find("shopAddAtkButton").GetComponent<Button>();
		_shopAddAtkButtonText = _shopPanel.transform.Find("shopAddAtkButton/shopAddAtkButtonText").GetComponent<Text>();
		_shopAddDefButton = _shopPanel.transform.Find("shopAddDefButton").GetComponent<Button>();
		_shopAddDefButtonText = _shopPanel.transform.Find("shopAddDefButton/shopAddDefButtonText").GetComponent<Text>();
		_shopNoButton = _shopPanel.transform.Find("NoButton").GetComponent<Button>();
        _shopPanel.SetActive(false);

        _infoPanel = MainCanvas.transform.Find("InfoPanel").gameObject;

        _notepadPanel = MainCanvas.transform.Find("NotepadPanel").gameObject;
        _notepadValueText = _notepadPanel.transform.Find("Scroll View").Find("Viewport").Find("Content").Find("InfoText").GetComponent<Text>();
        _notepadValueText.rectTransform.position = Vector3.zero;

        //关闭按钮
		__notepadPanelBtnClose = _notepadPanel.transform.Find("CloseButton").GetComponent<Button>();
		__notepadPanelBtnClose.onClick.AddListener(CloseNotepad);

		_notepadPanel.SetActive(false);

        _interactionDialogPanel = MainCanvas.transform.Find("InteractionDialogPanel").gameObject;
        _interactionDialogNameValueText = _interactionDialogPanel.transform.Find("NameValueText").GetComponent<Text>();
        _interactionDialogInfoValueText = _interactionDialogPanel.transform.Find("InfoValueText").GetComponent<Text>();
        _interactionDialogYesButton = _interactionDialogPanel.transform.Find("YesButton").GetComponent<Button>();
        _interactionDialogNoButton = _interactionDialogPanel.transform.Find("NoButton").GetComponent<Button>();
        _interactionDialogYesButtonValueText = _interactionDialogYesButton.GetComponentInChildren<Text>();
        _interactionDialogNoButtonValueText = _interactionDialogNoButton.GetComponentInChildren<Text>();
        _interactionDialogPanel.SetActive(false);
    }

    /// <summary>
    /// 数据绑定
    /// </summary>
    public void BindEvent()
    {
        GameManager.Instance.EventManager.OnHealthChanged += (value) =>
        {
            _playerHealthValueText.text = value.ToString();
            if (value <= 0)
            {
                _gameOverPanel.SetActive(true);
                // 禁用玩家控制器
                GameManager.Instance.PlayerManager.Enable = false;
                // 锁定玩家控制器
                GameManager.Instance.PlayerManager.LockEnable = true;
                // 绑定按钮事件
                _gameOverBackHomeButton.onClick.RemoveAllListeners();
                _gameOverBackHomeButton.onClick.AddListener(() => { GameManager.Instance.BackHomeEvent(); });
            }
        };
        GameManager.Instance.EventManager.OnAttackChanged += (value) => { _playerAttackValueText.text = value.ToString(); };
        GameManager.Instance.EventManager.OnDefenceChanged += (value) => { _playerDefenceValueText.text = value.ToString(); };
        GameManager.Instance.EventManager.OnGoldChanged += (value) => { _playerGoldValueText.text = value.ToString(); };

        GameManager.Instance.EventManager.OnEnemyCombated += (enemy) =>
        {
            if (enemy != null)
            {
                _enemyNameValueText.text = enemy.Name;
                _enemyHealthValueText.text = enemy.Health.ToString();
                _enemyAttackValueText.text = enemy.Attack.ToString();
                _enemyDefenceValueText.text = enemy.Defence.ToString();
                _enemyImage.enabled = true;
                _enemyImage.sprite = Resources.Load<Sprite>(enemy.IconPath);
            }
        };

        GameManager.Instance.EventManager.OnLevelChanged += (oldValue, newValue) => { _levelValueText.text = newValue.ToString(); };

        GameManager.Instance.EventManager.OnItemChanged += ItemChangeEvent;

        GameManager.Instance.EventManager.OnShopShow += ShopShowEvent;

        GameManager.Instance.EventManager.OnWeaponChanged += (value) =>
        {
            ResourceInfo resourceInfo = GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, value);
            if (null == resourceInfo)
            {
                _weaponValueText.text = "";
                _weaponImage.enabled = false;
            }
            else
            {
                _weaponValueText.text = resourceInfo.Name;
                _weaponImage.enabled = true;
                _weaponImage.sprite = Instantiate(Resources.Load(resourceInfo.IconPath)) as Sprite;
            }
        };

        GameManager.Instance.EventManager.OnArmorChanged += (value) =>
        {
            ResourceInfo resourceInfo = GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, value);
            if (null == resourceInfo)
            {
                _armorValueText.text = "";
                _armorImage.enabled = false;
            }
            else
            {
                _armorValueText.text = resourceInfo.Name;
                _armorImage.enabled = true;
                _armorImage.sprite = Instantiate(Resources.Load(resourceInfo.IconPath)) as Sprite;
            }
        };

        GameManager.Instance.EventManager.OnNotepadChanged += (value) =>
        {
            _notepadValueText.text = value;
        };
    }



	/// <summary>
	/// 商店打开事件
	/// </summary>
	/// <param name="name">商店名称</param>
	/// <param name="gold">每次所花金币</param>
	private void ShopShowEvent(string name,  ShopComm.E_ShopFloor floor, ActorController refreshActor, Action BuyHPcallback, Action BuyAtkcallback, Action BuyDefcallback)
	{
		// 音频播放
		GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Shop");
		// 禁用人物控制器
		GameManager.Instance.PlayerManager.Enable = false;
		_shopPanel.SetActive(true);
		_shopNameValueText.text = name;
        //_shopInfoValueText.text = gold.ToString();
        _shopInfoValueText.text = $"给我<color=yellow>{ShopComm.GetShopNeedMoney(floor)}</color>金币就可以提升以下，一种能力"; ;
		ShopComm.FloorShopInfo shopinfo = ShopComm.GetFloorShopInfo(floor);

		_shopAddHPButtonText.text = $"生命+{shopinfo.AddHP}";
		_shopAddAtkButtonText.text = $"攻击+{shopinfo.AddAtk}";
		_shopAddDefButtonText.text = $"防御+{shopinfo.AddDef}";

		_shopAddHPButton.onClick.RemoveAllListeners();
		_shopAddHPButton.onClick.AddListener(() =>
		{
			BuyHPcallback?.Invoke();

			//关闭
			_shopPanel.SetActive(false);
            //刷新页面
            refreshActor.Interaction();

			//// 启用人物控制器
			//GameManager.Instance.PlayerManager.Enable = true;
		});

		_shopAddAtkButton.onClick.RemoveAllListeners();
		_shopAddAtkButton.onClick.AddListener(() =>
		{
			BuyAtkcallback?.Invoke();
			//关闭
			_shopPanel.SetActive(false);
			//刷新页面
			refreshActor.Interaction();
		});

		_shopAddDefButton.onClick.RemoveAllListeners();
		_shopAddDefButton.onClick.AddListener(() =>
		{
			BuyDefcallback?.Invoke();
			//关闭
			_shopPanel.SetActive(false);
			//刷新页面
			refreshActor.Interaction();
		});


		_shopNoButton.onClick.RemoveAllListeners();
		_shopNoButton.onClick.AddListener(() =>
		{
			// 启用人物控制器
			GameManager.Instance.PlayerManager.Enable = true;
			// 音频播放
			GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "No");
			//关闭
			_shopPanel.SetActive(false);
		});
	}

	/// <summary>
	/// 初始化背包 UI
	/// </summary>
	public void InitBackpackUI()
    {
        // 清空背包 UI
        LayoutElement[] layoutElements = _backpackInfoPanel.GetComponentsInChildren<LayoutElement>();
        foreach (var layoutElement in layoutElements)
        {
            Destroy(layoutElement.gameObject);
        }
        _backpackDictionary.Clear();
        // 添加物品
        Dictionary<int, ItemInfo> backpackDic = GameManager.Instance.BackpackManager.BackpackDictionary;
        foreach (var key in backpackDic.Keys)
        {
            // 创建物品
            AddItemToBackpack(backpackDic[key]);
        }
    }

    /// <summary>
    /// 打开怪物手册
    /// </summary>
    /// <param name="callback">回调函数</param>
    public void ShowBook(Action callback)
    {
        // 打开 UI 面板
        _bookPanel.SetActive(true);
        // 清空面板
        for (int i = 0; i < _bookContent.transform.childCount; i++)
        {
            Destroy(_bookContent.transform.GetChild(i).gameObject);
        }
        callback?.Invoke();
    }

    public void CloseBook()
	{
        //关闭
		_bookPanel.SetActive(false);

	}

    /// <summary>
    /// 打开对话框
    /// </summary>
    /// <param name="name">说话的人</param>
    /// <param name="info">说话内容</param>
    /// <param name="callback">回调函数</param>
    public void ShowDialog(string name, List<string> info, Action callback)
    {
        // 音频播放
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "Dialogue");
        // 禁用人物控制器
        GameManager.Instance.PlayerManager.Enable = false;
        // 启用对话框
        _dialogInfoValueText.transform.parent.gameObject.SetActive(true);
        // 显示说话人名字和内容
        _dialogNameValueText.text = name + ":";

        foreach (var str in info)
        {
            Debug.Log(str);
        }
        StartCoroutine(LoadDialog(info, callback));
    }

    /// <summary>
    /// 打开交互对话框
    /// </summary>
    /// <param name="name">说话人</param>
    /// <param name="info">说话内容</param>
    /// <param name="yesBtnTxt">确认按钮文本</param>
    /// <param name="noBtnTxt">取消按钮文本</param>
    /// <param name="yesBtnCallback">点击确认按钮的回调函数</param>
    public void ShowInteractionDialog(string name, string info, string yesBtnTxt, string noBtnTxt, Action yesBtnCallback)
    {
        // 音频播放
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Shop");
        // 禁用人物控制器
        GameManager.Instance.PlayerManager.Enable = false;
        // 绑定参数
        _interactionDialogNameValueText.text = name;
        _interactionDialogInfoValueText.text = info;
        _interactionDialogYesButtonValueText.text = yesBtnTxt;
        _interactionDialogNoButtonValueText.text = noBtnTxt;
        _interactionDialogYesButton.onClick.RemoveAllListeners();
        _interactionDialogYesButton.onClick.AddListener(() =>
        {
            yesBtnCallback?.Invoke();
            _interactionDialogPanel.SetActive(false);
            // 启用人物控制器
            GameManager.Instance.PlayerManager.Enable = true;
        });
        _interactionDialogNoButton.onClick.RemoveAllListeners();
        _interactionDialogNoButton.onClick.AddListener(() =>
        {
            _interactionDialogPanel.SetActive(false);
            // 启用人物控制器
            GameManager.Instance.PlayerManager.Enable = true;
            // 音频播放
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "No");
        });
        _interactionDialogPanel.SetActive(true);
    }

    /// <summary>
    /// 加载对话
    /// </summary>
    /// <param name="info">说话内容</param>
    /// <param name="callback">回调函数 结束时触发</param>
    IEnumerator LoadDialog(List<string> info, Action callback)
    {
        int index = 0;
        while (index < info.Count)
        {
            _dialogInfoValueText.text = info[index];
            if (Input.GetKeyDown(KeyCode.Return)) index++;
            //O键
			if (Input.GetKeyDown(KeyCode.JoystickButton1)) index++;
			yield return null;
        }
        // 禁用对话框
        _dialogInfoValueText.transform.parent.gameObject.SetActive(false);
        // 结尾执行回调函数
        callback?.Invoke();
        yield break;
    }

    /// <summary>
    /// 显示信息
    /// </summary>
    /// <param name="info">信息内容</param>
    public void ShowInfo(string info)
    {
        Debug.Log("事件：" + info);
        GameObject obj = Instantiate(Resources.Load("UI/InfoImage") as GameObject, _infoPanel.transform);
        obj.GetComponent<InfoImageController>().SetText(info);
    }

    /// <summary>
    /// 显示笔记本
    /// </summary>
    public void ShowNotepad()
    {
        _notepadPanel.SetActive(true);
    }

    //关闭笔记
	public void CloseNotepad()
	{
		//关闭
		_notepadPanel.SetActive(false);

	}

	/// <summary>
	/// 物品变更事件
	/// </summary>
	/// <param name="id">物品 ID</param>
	/// <param name="itemInfo">物品信息</param>
	private void ItemChangeEvent(int id, ItemInfo itemInfo)
    {
        // 如果物品存在则修改数量
        if (_backpackDictionary.ContainsKey(id))
        {
            // 判断是否销毁
            if (itemInfo.UseCount == 0)
            {
                Destroy(_backpackDictionary[id].gameObject);
                _backpackDictionary.Remove(id);
            }
            // 数量变更
            else _backpackDictionary[id].GetComponentInChildren<Text>().text = itemInfo.UseCount < 0 ? "" : itemInfo.UseCount.ToString();
        }
        // 如果物品不存在则创建
        else AddItemToBackpack(itemInfo);
    }

    /// <summary>
    /// 添加物品到背包 UI
    /// </summary>
    /// <param name="itemInfo">物品信息</param>
    private void AddItemToBackpack(ItemInfo itemInfo)
    {
        GameObject itemUI = Instantiate(Resources.Load("UI/ItemImage"), _backpackInfoPanel.transform) as GameObject;
        itemUI.GetComponentInChildren<Text>().text = itemInfo.UseCount < 0 ? "" : itemInfo.UseCount.ToString();
        itemUI.GetComponent<Image>().sprite = Instantiate(Resources.Load(itemInfo.IconPath)) as Sprite;
        // 添加特殊事件
        switch (itemInfo.ID)
        {
            case 4:
                itemUI.AddComponent<EventItemKey4>();
                break;
            case 5:
                itemUI.AddComponent<EventItemBottle1>();
                break;
            case 6:
                itemUI.AddComponent<EventItemBottle2>();
                break;
            case 9:
                itemUI.AddComponent<EventItemArtifact3>();
                break;
            case 10:
                itemUI.AddComponent<EventItemBook>();
                break;
            case 11:
                itemUI.AddComponent<EventItemNotepad>();
                break;
            case 13:
                itemUI.AddComponent<EventItemOther1>();
                break;
            case 14:
                itemUI.AddComponent<EventItemOther2>();
                break;
            case 15:
                itemUI.AddComponent<EventItemArtifact4>();
                break;
            case 16:
                itemUI.AddComponent<EventItemOther6>();
                break;
            case 17:
                itemUI.AddComponent<EventItemBottle3>();
                break;
            case 18:
                itemUI.AddComponent<EventItemArtifact1>();
                break;
            case 19:
                itemUI.AddComponent<EventItemArtifact2>();
                break;
            case 21:
                itemUI.AddComponent<EventItemOther5>();
                break;
            case 22:
                itemUI.AddComponent<EventItemOther4>();
                break;
            case 23:
                itemUI.AddComponent<EventItemOther3>();
                break;
            case 24:
                itemUI.AddComponent<EventItemEquipmentWeapon1>();
                break;
            case 25:
                itemUI.AddComponent<EventItemEquipmentArmor1>();
                break;
            case 26:
                itemUI.AddComponent<EventItemEquipmentWeapon2>();
                break;
            case 27:
                itemUI.AddComponent<EventItemEquipmentArmor2>();
                break;
            case 28:
                itemUI.AddComponent<EventItemEquipmentWeapon3>();
                break;
            case 29:
                itemUI.AddComponent<EventItemEquipmentArmor3>();
                break;
            case 30:
                itemUI.AddComponent<EventItemEquipmentWeapon4>();
                break;
            case 31:
                itemUI.AddComponent<EventItemEquipmentArmor4>();
                break;
            case 32:
                itemUI.AddComponent<EventItemEquipmentWeapon5>();
                break;
            case 33:
                itemUI.AddComponent<EventItemEquipmentArmor5>();
                break;
        }
        // 加入 UI 背包
        _backpackDictionary.Add(itemInfo.ID, itemUI);
    }
}
