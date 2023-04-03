using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class FPSCamera : MonoBehaviour
{
	public Vector3 angles;
	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;

		angles = Camera.main.transform.localEulerAngles;
	}

	[SerializeField]
	TMPro.TMP_Text dateText;

	public float speed = 1440;
	void Update()
	{
		Debug.Log("Update : " + Cursor.lockState);
		if(!CursorLock.ActualState)
			return;

		angles += speed * new Vector3(-Input.GetAxis("Mouse Y"),Input.GetAxis("Mouse X"),0) / Mathf.Max(Screen.width,Screen.height);
		angles.x = Mathf.Clamp(angles.x,-89,89);

		Camera.main.transform.localEulerAngles = new Vector3(angles.x,angles.y,0);

		if(Input.GetKeyDown(KeyCode.T))
		{
			dateText.text = new JSDate().Value.ToString();
		}
	}
}
