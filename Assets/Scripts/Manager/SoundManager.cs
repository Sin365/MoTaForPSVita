using System;
using UnityEngine;

public enum ESoundType
{
    Music,
    Effect,
}

public class SoundManager : MonoSingleton<SoundManager>
{
    public bool LockEnable;

    // 音乐音源
    private AudioSource _audioSourceMusic;
    // 音乐名称 防止重复播放
    private string _musicName;
    // 效果音源
    private AudioSource _audioSourceEffect;

    void Awake()
    {
        // 获取音源
        AudioSource[] audioSources = Camera.main.GetComponents<AudioSource>();
        _audioSourceMusic = audioSources[0];
        _audioSourceEffect = audioSources[1];
        // 设置音量
        _audioSourceMusic.volume = .6f;
        _audioSourceEffect.volume = 1;
        // 设置播放
        _audioSourceMusic.playOnAwake = true;
        _audioSourceMusic.loop = true;
        _audioSourceEffect.playOnAwake = false;
        _audioSourceEffect.loop = false;
        // 绑定事件
        GameManager.Instance.EventManager.OnLevelChanged += LevelMusicEvent;
    }

    private void LevelMusicEvent(int oldLevel, int newLevel)
    {
        if (newLevel == 0) PlaySound(ESoundType.Music, "0-44");
        else if (newLevel > 0 && newLevel <= 9) PlaySound(ESoundType.Music, "1-9");
        else if (newLevel == 10)
        {
            if (GameManager.Instance.PlotManager.PlotDictionary[4] == 5) PlaySound(ESoundType.Music, "LevelWin");
            else PlaySound(ESoundType.Music, "10");
        }
        else if (newLevel > 10 && newLevel < 20) PlaySound(ESoundType.Music, "11-19");
        else if (newLevel == 20)
        {
            if (GameManager.Instance.PlotManager.PlotDictionary[15] == 2) PlaySound(ESoundType.Music, "LevelWin");
            else PlaySound(ESoundType.Music, "20");
        }
        else if (newLevel > 20 && newLevel < 31 && newLevel != 24 && newLevel != 25 && newLevel != 26) PlaySound(ESoundType.Music, "21-30");
        else if (newLevel == 24)
        {
            if (GameManager.Instance.PlotManager.PlotDictionary[18] >= 2) PlaySound(ESoundType.Music, "50");
            else PlaySound(ESoundType.Music, "21-30");
        }
        else if (newLevel == 25)
        {
            if (GameManager.Instance.PlotManager.PlotDictionary[17] == 2) PlaySound(ESoundType.Music, "LevelWin");
            else PlaySound(ESoundType.Music, "25");
        }
        else if (newLevel == 26) PlaySound(ESoundType.Music, "26");
        else if (newLevel > 30 && newLevel < 40) PlaySound(ESoundType.Music, "31-39");
        else if (newLevel == 40)
        {
            if (GameManager.Instance.PlotManager.PlotDictionary[16] == 2) PlaySound(ESoundType.Music, "LevelWin");
            else PlaySound(ESoundType.Music, "40");
        }
        else if (newLevel > 40 && newLevel < 49 && newLevel != 44) PlaySound(ESoundType.Music, "41-48");
        else if (newLevel == 44) PlaySound(ESoundType.Music, "0-44");
        else if (newLevel == 49)
        {
            if (GameManager.Instance.PlotManager.PlotDictionary[14] == 3) PlaySound(ESoundType.Music, "LevelWin");
            else PlaySound(ESoundType.Music, "49");
        }
        else if (newLevel == 50) PlaySound(ESoundType.Music, "50");
    }

    /// <summary>
    /// 播放音频
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="clipName">音频名称</param>
    public void PlaySound(ESoundType type, string clipName)
	{
		//Debug.Log($"PlaySound {type}:{clipName} |_musicName {_musicName} |  LockEnable {LockEnable} ");
        try
		{
			switch (type)
			{
				case ESoundType.Music:
					// 锁定时不能播放音乐
					if (LockEnable) return;
					if (clipName != null && _musicName != clipName)
					{
						_musicName = clipName;
						_audioSourceMusic.clip = Resources.Load<AudioClip>("Sounds/Music/" + clipName);
						Debug.Log($"PlaySound clip == null:{(_audioSourceMusic.clip == null)} ,name{_audioSourceMusic?.clip?.name}");
						_audioSourceMusic.Play();
					}
					break;
				case ESoundType.Effect:
					if (clipName != null)
					{
						_audioSourceEffect.clip = Resources.Load<AudioClip>("Sounds/Effects/" + clipName);
						Debug.Log($"PlaySound clip == null:{(_audioSourceMusic.clip == null)} ,name{_audioSourceMusic?.clip?.name}");
						_audioSourceEffect.Play();
					}
					break;
				default:
					break;
			}
		}
        catch(Exception ex)
        {
			Debug.LogError($"PlaySound Error {ex}");
		}
	}

    /// <summary>
    /// 停止播放音频
    /// </summary>
    /// <param name="type">类型</param>
    public void StopSound(ESoundType type)
    {
        switch (type)
        {
            case ESoundType.Music:
                _audioSourceMusic.Stop();
                break;
            case ESoundType.Effect:
                _audioSourceEffect.Stop();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 暂停播放音频
    /// </summary>
    /// <param name="type">类型</param>
    public void PauseSound(ESoundType type)
    {
        switch (type)
        {
            case ESoundType.Music:
                _audioSourceMusic.Pause();
                break;
            case ESoundType.Effect:
                _audioSourceEffect.Pause();
                break;
            default:
                break;
        }
    }
}
