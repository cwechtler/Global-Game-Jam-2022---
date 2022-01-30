using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetSelectOnEnable : MonoBehaviour
{
	private Button button;

	private void update()
	{
		EventSystem.current.SetSelectedGameObject(this.gameObject);
	}


	private void OnEnable()
	{
		button = GetComponent<Button>();
		button.Select();
		button.OnSelect(null);
	}
}
