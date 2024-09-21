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

	private void Awake()
	{
		newGameButton = transform.Find("Panel").Find("NewGameButton").GetComponent<Button>();
		loadGameButton = transform.Find("Panel").Find("LoadGameButton").GetComponent<Button>();
		exitGameButton = transform.Find("Panel").Find("ExitGameButton").GetComponent<Button>();

		newGameButton.onClick.AddListener(() => { NewGameEvent(); });
		loadGameButton.onClick.AddListener(() => { LoadGameEvent(); });
		exitGameButton.onClick.AddListener(ExitGameEvent);
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
			// UI 提示
			GameManager.Instance.UIManager.ShowInfo("已读档！");
			// 音频播放
			GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Save");
		}
		else
		{
			// UI 提示
			GameManager.Instance.UIManager.ShowInfo("未发现存档！");
			GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "No");
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
