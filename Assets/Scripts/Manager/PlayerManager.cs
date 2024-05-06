using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 方向类型
/// </summary>
public enum EDirectionType
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
}

/// <summary>
/// 玩家信息
/// </summary>
[Serializable]
public class PlayerInfo
{
    [SerializeField]
    private int _health;
    [SerializeField]
    private int _attack;
    [SerializeField]
    private int _defence;
    [SerializeField]
    private int _gold;
    [SerializeField]
    private int _weaponID;
    [SerializeField]
    private int _armorID;
    [SerializeField]
    private string _notepadInfo;

	[SerializeField]
	public int StoreBuyNum_F4 = 1;
	[SerializeField]
	public int StoreBuyNum_F12 = 1;
	[SerializeField]
	public int StoreBuyNum_F32 = 1;
	[SerializeField]
	public int StoreBuyNum_F46 = 1;

	public int Health
    {
        get => _health;
        set
        {
            _health = value < 0 ? 0 : value;
            GameManager.Instance.EventManager.OnHealthChanged?.Invoke(_health);
        }
    }
    public int Attack
    {
        get => _attack;
        set
        {
            _attack = value < 0 ? 0 : value;
            GameManager.Instance.EventManager.OnAttackChanged?.Invoke(_attack);
        }
    }
    public int Defence
    {
        get => _defence;
        set
        {
            _defence = value < 0 ? 0 : value;
            GameManager.Instance.EventManager.OnDefenceChanged?.Invoke(_defence);
        }
    }
    public int Gold
    {
        get => _gold;
        set
        {
            _gold = value;
            GameManager.Instance.EventManager.OnGoldChanged?.Invoke(_gold);
        }
    }
    public int WeaponID
    {
        get => _weaponID;
        set
        {
            _weaponID = value;
            GameManager.Instance.EventManager.OnWeaponChanged?.Invoke(_weaponID);
        }
    }
    public int ArmorID
    {
        get => _armorID;
        set
        {
            _armorID = value;
            GameManager.Instance.EventManager.OnArmorChanged?.Invoke(_armorID);
        }
    }
    public string NotepadInfo
    {
        get => _notepadInfo;
        set
        {
            _notepadInfo = value;
            GameManager.Instance.EventManager.OnNotepadChanged?.Invoke(_notepadInfo);
        }
    }
}

public class PlayerManager : Singleton<PlayerManager>
{
    public bool LockEnable;  // 开关加锁
    private PlayerController _playerController;
    private bool _enable = true;
    private PlayerInfo _playerInfo;

    public PlayerInfo PlayerInfo
    {
        get => _playerInfo;
        set
        {
            _playerInfo = value;
            // 手动赋值刷新 UI
            _playerInfo.Health = _playerInfo.Health;
            _playerInfo.Attack = _playerInfo.Attack;
            _playerInfo.Defence = _playerInfo.Defence;
            _playerInfo.Gold = _playerInfo.Gold;
            _playerInfo.WeaponID = _playerInfo.WeaponID;
            _playerInfo.ArmorID = _playerInfo.ArmorID;
            _playerInfo.NotepadInfo = _playerInfo.NotepadInfo;
        }
    }
    public bool Enable
    {
        get => _enable;
        set { if (!LockEnable) _enable = value; }
    }
    public PlayerController PlayerController { get => _playerController; }

    /// <summary>
    /// 绑定玩家
    /// </summary>
    /// <param name="playerController"></param>
    public void BindPlayer(PlayerController playerController)
    {
        this._playerController = playerController;
    }

    /// <summary>
    /// 解绑玩家
    /// </summary>
    public void UnbindPlayer()
    {
        this._playerController = null;
    }

    const float KeepKeyTime = 0.2f;
    Dictionary<KeyCode, float> DictKeyKeep = new Dictionary<KeyCode, float>()
	{
		{KeyCode.UpArrow,-1 },
		{KeyCode.DownArrow,-1 },
		{KeyCode.LeftArrow,-1 },
		{KeyCode.RightArrow,-1 },

		{KeyCode.JoystickButton8,-1 },
		{KeyCode.JoystickButton10,-1 },
		{KeyCode.JoystickButton11,-1},
		{KeyCode.JoystickButton9,-1},
	};

    KeyCode[] KeepKeys = {
	    KeyCode.UpArrow,
	    KeyCode.DownArrow,
	    KeyCode.LeftArrow,
	    KeyCode.RightArrow,

	    KeyCode.JoystickButton8,
	    KeyCode.JoystickButton10,
	    KeyCode.JoystickButton11,
	    KeyCode.JoystickButton9,
	}; 

    /// <summary>
    /// 检查输入
    /// </summary>
    public void CheckInput()
    {
        if (_playerController && _enable)
        {
			if (Input.GetKeyDown(KeyCode.UpArrow)) GameManager.Instance.EventManager.OnMoveInput?.Invoke(EDirectionType.UP);
			if (Input.GetKeyDown(KeyCode.DownArrow)) GameManager.Instance.EventManager.OnMoveInput?.Invoke(EDirectionType.DOWN);
			if (Input.GetKeyDown(KeyCode.LeftArrow)) GameManager.Instance.EventManager.OnMoveInput?.Invoke(EDirectionType.LEFT);
			if (Input.GetKeyDown(KeyCode.RightArrow)) GameManager.Instance.EventManager.OnMoveInput?.Invoke(EDirectionType.RIGHT);

			if (Input.GetKeyDown(KeyCode.S)) GameManager.Instance.EventManager.OnSaveGameInput?.Invoke();
			if (Input.GetKeyDown(KeyCode.Escape)) GameManager.Instance.EventManager.OnBackHomeInput?.Invoke();

			if (Input.GetKeyDown(KeyCode.PageUp)) GameManager.Instance.EventManager.OnArtifactUp?.Invoke();
			if (Input.GetKeyDown(KeyCode.PageDown)) GameManager.Instance.EventManager.OnArtifactDown?.Invoke();

            //PSV
			if (Input.GetKeyDown(KeyCode.JoystickButton8)) GameManager.Instance.EventManager.OnMoveInput?.Invoke(EDirectionType.UP);
            if (Input.GetKeyDown(KeyCode.JoystickButton10)) GameManager.Instance.EventManager.OnMoveInput?.Invoke(EDirectionType.DOWN);
            if (Input.GetKeyDown(KeyCode.JoystickButton11)) GameManager.Instance.EventManager.OnMoveInput?.Invoke(EDirectionType.LEFT);
            if (Input.GetKeyDown(KeyCode.JoystickButton9)) GameManager.Instance.EventManager.OnMoveInput?.Invoke(EDirectionType.RIGHT);

            if (Input.GetKeyDown(KeyCode.JoystickButton7)) GameManager.Instance.EventManager.OnSaveGameInput?.Invoke();
            //三角形 替代 空格
            //SELECT
            if (Input.GetKeyDown(KeyCode.JoystickButton6)) GameManager.Instance.EventManager.OnBackHomeInput?.Invoke();

            if (Input.GetKeyDown(KeyCode.JoystickButton3)) GameManager.Instance.EventManager.OnArtifactUp?.Invoke();
            if (Input.GetKeyDown(KeyCode.JoystickButton0)) GameManager.Instance.EventManager.OnArtifactDown?.Invoke();

            #region 长按

			for(int i=0; i< KeepKeys.Length; i++)
            {
                KeyCode key = KeepKeys[i];

				if (!Input.GetKey(key))
                    DictKeyKeep[key] = -1;
                else
                {
                    if (DictKeyKeep[key] == -1)
                        DictKeyKeep[key] = Time.time;
                    else if(Time.time - DictKeyKeep[key] >= KeepKeyTime)
					{
						DictKeyKeep[key] = -1;
						switch (key)
                        {
                            case KeyCode.UpArrow:
							case KeyCode.JoystickButton8:
								GameManager.Instance.EventManager.OnMoveInput?.Invoke(EDirectionType.UP);
								break;
							case KeyCode.DownArrow:
							case KeyCode.JoystickButton10:
								GameManager.Instance.EventManager.OnMoveInput?.Invoke(EDirectionType.DOWN);
								break;
							case KeyCode.LeftArrow:
							case KeyCode.JoystickButton11:
								GameManager.Instance.EventManager.OnMoveInput?.Invoke(EDirectionType.LEFT);
								break;
							case KeyCode.RightArrow:
							case KeyCode.JoystickButton9:
								GameManager.Instance.EventManager.OnMoveInput?.Invoke(EDirectionType.RIGHT);
								break;
						}
					}
                }
			}

			#endregion

		}
	}

    /// <summary>
    /// 添加信息到记事本
    /// </summary>
    /// <param name="info">信息内容</param>
    public void AddInfoToNotepad(string info)
    {
        // 判断是否有记事本
        if (!GameManager.Instance.BackpackManager.BackpackDictionary.ContainsKey(11)) return;
        _playerInfo.NotepadInfo += $"{info}\n\r\n\r";

    }
}
