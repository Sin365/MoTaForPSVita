using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeCanvasController : MonoBehaviour
{
	private Button newGameButton;
	private Button loadGameButton;
	private Button exitGameButton;
	private Text txtSaveDate;

	private void Awake()
	{
		newGameButton = transform.Find("Panel").Find("NewGameButton").GetComponent<Button>();
		loadGameButton = transform.Find("Panel").Find("LoadGameButton").GetComponent<Button>();
		exitGameButton = transform.Find("Panel").Find("ExitGameButton").GetComponent<Button>();
		txtSaveDate = transform.Find("Panel").Find("txtSaveDate").GetComponent<Text>();

		newGameButton.onClick.AddListener(() => { NewGameEvent(); });
		loadGameButton.onClick.AddListener(() => { LoadGameEvent(); });
		exitGameButton.onClick.AddListener(ExitGameEvent);
	}


	void OnEnable()
	{
		// 加载游戏存档
		if (!ResourceManager.Instance.GetGameArchiveStatus())
		{
			txtSaveDate.text = "没有存档";
		}
		else
		{
			txtSaveDate.text = ResourceManager.Instance.GetSaveDateTime();
		}

	}
	/// <summary>
	/// 新游戏
	/// </summary>
	public void NewGameEvent()
	{
		// 设置标识符
		PlayerPrefs.SetInt("NewGame", 1);
		// 加载场景
		SceneManager.LoadScene("Level");
	}

	/// <summary>
	/// 加载游戏
	/// </summary>
	public void LoadGameEvent()
	{
		// 加载游戏存档
		if (ResourceManager.Instance.GetGameArchiveStatus())
		{
			SceneManager.LoadScene("Level");
		}

	}

	/// <summary>
	/// 退出游戏
	/// </summary>
	public void ExitGameEvent()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}
}
