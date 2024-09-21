using System.Collections.Generic;
using System.IO;

//using System.IO;
using UnityEngine;

/// <summary>
/// 资源类型
/// </summary>
public enum EResourceType
{
	Environment,
	Item,
	Actor,
	Enemy,
}

/// <summary>
/// 地图资源信息
/// </summary>
[System.Serializable]
public class MapResourceInfo
{
	public int Level;
	public EResourceType Type;
	public int ID;
	public Vector2 Point;
}

/// <summary>
/// 资源信息
/// </summary>
[System.Serializable]
public class ResourceInfo
{
	public EResourceType Type;
	public int ID;
	public string Name;
	public string Info;
	public string Path;
	public string IconPath;
}

/// <summary>
/// 游戏信息 用于存档
/// </summary>
[System.Serializable]
public class GameInfo
{
	public string MapArchive;
	public string PlayerInfo;
	public int LevelInfo;
	public int MaxLevelInfo;
	public string BackpackInfo;
	public string PlotInfo;
	//每个层商店购买次数
	public int StoreBuyNum_F4 = 0;
	public int StoreBuyNum_F12 = 0;
	public int StoreBuyNum_F32 = 0;
	public int StoreBuyNum_F46 = 0;
}

public class ResourceManager : Singleton<ResourceManager>
{
	//private string _mapInfoPath = Application.dataPath + "/Settings/地图信息.txt";
	//private string _propertyInfoPath = Application.dataPath + "/Settings/属性列表.txt";

	private List<MapResourceInfo> _mapResourceInfoList = new List<MapResourceInfo>();
	private Dictionary<EResourceType, Dictionary<int, ResourceInfo>> _resourceInfoDic = new Dictionary<EResourceType, Dictionary<int, ResourceInfo>>();
#if UNITY_EDITOR
	string SaveDataDirPath = Application.persistentDataPath;
#elif UNITY_PSP2
	string SaveDataDirPath = "ux0:data/MoTaForPSVita";
#endif
	string SaveDataFilePath => SaveDataDirPath + "/GameSaveData.json";

	public ResourceManager()
	{
		LoadPropertyFile();
	}

	/// <summary>
	/// 绑定事件
	/// </summary>
	public void BindEvent()
	{
		GameManager.Instance.EventManager.OnSaveGameInput += SaveGameInfoEvent;
		GameManager.Instance.EventManager.OnLevelChanged += LoadLevelEvent;
	}

	/// <summary>
	/// 新建游戏信息
	/// </summary>
	public void NewGameInfo()
	{
		//// 打开 txt 设置地图信息
		//using (FileStream fs = new FileStream(_mapInfoPath, FileMode.Open))
		//{
		//    // 创建 txt 字节长度的 byte 数组
		//    byte[] bytes = new byte[fs.Length];
		//    // 读取 txt 字节到 byte 数组
		//    fs.Read(bytes, 0, bytes.Length);
		//    // 用 utf-8 解码
		//    string mapStr = System.Text.Encoding.UTF8.GetString(bytes);
		//    // json 转对象 list
		//    _mapResourceInfoList = JsonUtility.FromJson<Serialization<MapResourceInfo>>(mapStr).ToList();
		//}

		//Debug.Log("mapinfo" + Resources.Load<TextAsset>("cfg/mapinfo").text);
		_mapResourceInfoList = JsonUtility.FromJson<Serialization<MapResourceInfo>>(Resources.Load<TextAsset>("cfg/mapinfo").text).ToList();

		// 初始化剧情信息
		GameManager.Instance.PlotManager.Init();
		// 设置层数信息
		GameManager.Instance.LevelManager.Level = 1;
		// 设置玩家信息
		GameManager.Instance.PlayerManager.PlayerInfo = new PlayerInfo
		{
			Health = 1000,
			Attack = 100,
			Defence = 100,
			Gold = 0,
			WeaponID = 32,
			ArmorID = 33,
			NotepadInfo = "",
		};
		// 删除标识符
		PlayerPrefs.DeleteKey("NewGame");
	}

	/// <summary>
	/// 加载资源
	/// </summary>
	/// <param name="type">资源类型</param>
	/// <param name="id">资源 id</param>
	/// <returns>资源物体</returns>
	public GameObject LoadResource(EResourceType type, int id)
	{
		// 获取资源属性
		ResourceInfo tempInfo = _resourceInfoDic[type][id];
		if (null == tempInfo) return null;
		//Debug.Log("加载资源："+ tempInfo.Path);
		// 创建资源并返回
		return Object.Instantiate(Resources.Load<GameObject>(tempInfo.Path), Vector3.zero, Quaternion.identity);
	}

	/// <summary>
	/// 获取资源信息
	/// </summary>
	/// <param name="type">资源类型</param>
	/// <param name="id">资源 id</param>
	/// <returns>资源信息</returns>
	public ResourceInfo GetResourceInfo(EResourceType type, int id)
	{
		if (!_resourceInfoDic.ContainsKey(type)) return null;
		if (!_resourceInfoDic[type].ContainsKey(id)) return null;
		return _resourceInfoDic[type][id];
	}

	/// <summary>
	/// 获取新游戏状态
	/// </summary>
	/// <returns></returns>
	public bool GetNewGameStatus()
	{
		return PlayerPrefs.HasKey("NewGame");
	}

	/// <summary>
	/// 获取游戏存档状态
	/// </summary>
	/// <returns>是否有存档</returns>
	public bool GetGameArchiveStatus()
	{
#if UNITY_EDITOR_WIN
		return PlayerPrefs.HasKey("GameInfo");
#else
        return System.IO.File.Exists(SaveDataFilePath);
#endif

	}

	/// <summary>
	/// 存档时间
	/// </summary>
	/// <returns></returns>
	public string GetSaveDateTime()
	{
#if UNITY_EDITOR_WIN
		return $"PC:{System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
#else
		FileInfo fileinfo = new FileInfo(SaveDataFilePath);
		return $"存档时间:{fileinfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss")}";
#endif

	}

	/// <summary>
	/// 加载游戏存档
	/// </summary>
	/// <returns>是否能够加载</returns>
	public void LoadGameArchive()
	{
		// 判断存档是否存在
#if UNITY_EDITOR_WIN
		if (!PlayerPrefs.HasKey("GameInfo")) return;
		// json 对象转 GameInfo
		GameInfo gameInfo = JsonUtility.FromJson<GameInfo>(PlayerPrefs.GetString("GameInfo"));
#else
        if (!System.IO.File.Exists(SaveDataFilePath)) return;
		GameInfo gameInfo = JsonUtility.FromJson<GameInfo>(System.IO.File.ReadAllText(SaveDataFilePath));
#endif
		// 加载存档
		_mapResourceInfoList = JsonUtility.FromJson<Serialization<MapResourceInfo>>(gameInfo.MapArchive).ToList();
		// 加载背包信息
		GameManager.Instance.BackpackManager.BackpackDictionary = JsonUtility.FromJson<Serialization<int, ItemInfo>>(gameInfo.BackpackInfo).ToDictionary();
		// 加载玩家信息
		GameManager.Instance.PlayerManager.PlayerInfo = JsonUtility.FromJson<PlayerInfo>(gameInfo.PlayerInfo);
		// 加载剧情信息
		GameManager.Instance.PlotManager.PlotDictionary = JsonUtility.FromJson<Serialization<int, int>>(gameInfo.PlotInfo).ToDictionary();
		// 加载层数信息
		GameManager.Instance.LevelManager.Level = gameInfo.LevelInfo;
		GameManager.Instance.LevelManager.MaxLevel = gameInfo.MaxLevelInfo;
	}

	/// <summary>
	/// 移动玩家位置
	/// </summary>
	/// <param name="level">关卡</param>
	/// <param name="point">位置</param>
	public void MovePlayerPointForLevel(int level, Vector2 point)
	{
		_mapResourceInfoList.ForEach(mri =>
		{
			if (mri.Level == level && mri.Type == EResourceType.Actor && mri.ID == 1)
			{
				mri.Point = point;
				return;
			}
		});
	}

	/// <summary>
	/// 创建资源到关卡
	/// </summary>
	/// <param name="level"></param>
	/// <param name="type"></param>
	/// <param name="id"></param>
	/// <param name="point"></param>
	public void MakeResourceForLevel(int level, EResourceType type, int id, Vector2 point)
	{
		_mapResourceInfoList.Add(new MapResourceInfo()
		{
			Level = level,
			Type = type,
			ID = id,
			Point = point,
		});
	}

	/// <summary>
	/// 加载属性文件
	/// </summary>
	private void LoadPropertyFile()
	{

		//Debug.Log("attinfo" + Resources.Load<TextAsset>("cfg/attinfo").text);
		List<ResourceInfo> tempList = JsonUtility.FromJson<Serialization<ResourceInfo>>(Resources.Load<TextAsset>("cfg/attinfo").text).ToList();
		// 将 list 加入字典
		tempList.ForEach(ri =>
		{
			if (!_resourceInfoDic.ContainsKey(ri.Type)) _resourceInfoDic.Add(ri.Type, new Dictionary<int, ResourceInfo>());
			if (!_resourceInfoDic[ri.Type].ContainsKey(ri.ID)) _resourceInfoDic[ri.Type].Add(ri.ID, ri);
		});

		/*// 判断属性文件是否存在
        if (!File.Exists(_propertyInfoPath)) return;
        // 打开 txt
        using (FileStream fs = new FileStream(_propertyInfoPath, FileMode.Open))
        {
            // 创建 txt 字节长度的 byte 数组
            byte[] bytes = new byte[fs.Length];
            // 读取 txt 字节到 byte 数组
            fs.Read(bytes, 0, bytes.Length);
            // 用 utf-8 解码
            string infoStr = System.Text.Encoding.UTF8.GetString(bytes);
            // json 转对象 list
            List<ResourceInfo> tempList = JsonUtility.FromJson<Serialization<ResourceInfo>>(infoStr).ToList();
            // 将 list 加入字典
            tempList.ForEach(ri =>
            {
                if (!_resourceInfoDic.ContainsKey(ri.Type)) _resourceInfoDic.Add(ri.Type, new Dictionary<int, ResourceInfo>());
                if (!_resourceInfoDic[ri.Type].ContainsKey(ri.ID)) _resourceInfoDic[ri.Type].Add(ri.ID, ri);
            });
        }*/
	}


	/// <summary>
	/// 加载关卡事件
	/// </summary>
	/// <param name="oldIndex">旧关卡序号</param>
	/// <param name="newIndex">新关卡序号</param>
	private void LoadLevelEvent(int oldIndex, int newIndex)
	{
		// 判断地图信息是否存在
		if (_mapResourceInfoList.Count == 0) return;
		// 获取当前关卡在用资源
		for (int i = _mapResourceInfoList.Count - 1; i >= 0; i--)
		{
			if (_mapResourceInfoList[i].Level == oldIndex) _mapResourceInfoList.RemoveAt(i);
		}
		// 更新到资源信息中
		GameManager.Instance.PoolManager.UseList.ForEach(u =>
		{
			EResourceType type = EResourceType.Actor;
			switch (u.tag)
			{
				case "Item":
					type = EResourceType.Item;
					break;
				case "Enemy":
					type = EResourceType.Enemy;
					break;
				case "Environment":
					type = EResourceType.Environment;
					break;
				default:
					break;
			}
			_mapResourceInfoList.Add(new MapResourceInfo
			{
				Level = oldIndex,
				Type = type,
				ID = u.GetComponent<ResourceController>().ID,
				Point = u.transform.position,
			});
		});
		// 回收资源
		GameManager.Instance.PoolManager.RecycleResource();
		// 加载资源
		foreach (var info in _mapResourceInfoList)
		{
			// 判断关卡一致
			if (info.Level == newIndex)
			{
				GameObject tempObj = GameManager.Instance.PoolManager.GetResourceInFreePool(info.Type, info.ID);
				tempObj.transform.position = info.Point;
			}
		}
		// 加载完毕执行事件
		GameManager.Instance.EventManager.OnResourceLoaded?.Invoke();
	}

	/// <summary>
	/// 保存游戏信息事件
	/// </summary>
	private void SaveGameInfoEvent()
	{
		// 删除当前关卡资源
		for (int i = _mapResourceInfoList.Count - 1; i >= 0; i--)
		{
			if (_mapResourceInfoList[i].Level == GameManager.Instance.LevelManager.Level) _mapResourceInfoList.RemoveAt(i);
		}
		// 将在用对象池资源加入资源信息中
		GameManager.Instance.PoolManager.UseList.ForEach(u =>
		{
			EResourceType type = EResourceType.Actor;
			switch (u.tag)
			{
				case "Item":
					type = EResourceType.Item;
					break;
				case "Enemy":
					type = EResourceType.Enemy;
					break;
				case "Environment":
					type = EResourceType.Environment;
					break;
			}
			_mapResourceInfoList.Add(new MapResourceInfo
			{
				Level = GameManager.Instance.LevelManager.Level,
				Type = type,
				ID = u.GetComponent<ResourceController>().ID,
				Point = u.transform.position,
			});
		});
		// 获取游戏信息
		GameInfo gameInfo = new GameInfo
		{
			MapArchive = JsonUtility.ToJson(new Serialization<MapResourceInfo>(_mapResourceInfoList)),
			PlayerInfo = JsonUtility.ToJson(GameManager.Instance.PlayerManager.PlayerInfo),
			LevelInfo = GameManager.Instance.LevelManager.Level,
			MaxLevelInfo = GameManager.Instance.LevelManager.MaxLevel,
			BackpackInfo = JsonUtility.ToJson(new Serialization<int, ItemInfo>(GameManager.Instance.BackpackManager.BackpackDictionary)),
			PlotInfo = JsonUtility.ToJson(new Serialization<int, int>(GameManager.Instance.PlotManager.PlotDictionary)),

		};
		// 保存资源信息

		try
		{
#if UNITY_EDITOR_WIN
			PlayerPrefs.SetString("GameInfo", JsonUtility.ToJson(gameInfo));


#else
			Debug.Log($"SaveDataDirPath->{SaveDataDirPath}");
			Debug.Log($"Directory.Exists(SaveDataDirPath)->{Directory.Exists(SaveDataDirPath)}");
			if (!Directory.Exists(SaveDataDirPath))
			{
				Debug.Log($"Directory.CreateDirectory(SaveDataDirPath)");
				Directory.CreateDirectory(SaveDataDirPath);
			}
			string savestring = JsonUtility.ToJson(gameInfo);
			Debug.Log($"savestring->{savestring}");
			File.WriteAllText(SaveDataFilePath, savestring);
#endif

			// UI 提示
			GameManager.Instance.UIManager.ShowInfo("游戏已存档！");
			// 音频播放
			GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Save");
		}
		catch
		{
			// UI 提示
			GameManager.Instance.UIManager.ShowInfo("存档失败！");
			GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "No");
		}

	}

}
