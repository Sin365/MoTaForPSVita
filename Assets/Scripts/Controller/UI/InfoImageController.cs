using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoImageController : MonoBehaviour
{
    private Animator _animator;
    private Text _infoText;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _infoText = GetComponentInChildren<Text>();
    }

    public void SetText(string text)
	{
		_animator = GetComponent<Animator>();
		_infoText = GetComponentInChildren<Text>();
		_infoText.text = text;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
