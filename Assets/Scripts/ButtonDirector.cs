using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDirector : MonoBehaviour
{
	public void CallLoadLevel(string name)
	{
		LevelManager.instance.LoadLevel(name);
	}

	public void StartGame()
	{
		LevelManager.instance.LoadLevel("MikeTest", true);
	}

	public void Quit()
	{
		LevelManager.instance.QuitRequest();
	}
}
