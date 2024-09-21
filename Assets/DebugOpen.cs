using UnityEngine;

public class DebugOpen : MonoBehaviour {

	public Debugger debugger;

	void Awake()
	{
		debugger.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		//按住上方向键+三角建，再按下X键
		if (Input.GetKey(KeyCode.JoystickButton3) && Input.GetKey(KeyCode.JoystickButton8))
		{
			if (Input.GetKeyDown(KeyCode.JoystickButton0))
			{
				debugger.gameObject.SetActive(true);
			}
		}
	}
}
