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
    /// ����Ϸ
    /// </summary>
    public void NewGameEvent()
    {
        // ���ñ�ʶ��
        PlayerPrefs.SetInt("NewGame", 1);
        // ���س���
        SceneManager.LoadScene("Level");
    }

    /// <summary>
    /// ������Ϸ
    /// </summary>
    public void LoadGameEvent()
    {
        // ������Ϸ�浵
        if (ResourceManager.Instance.GetGameArchiveStatus()) SceneManager.LoadScene("Level");
    }

    /// <summary>
    /// �˳���Ϸ
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
