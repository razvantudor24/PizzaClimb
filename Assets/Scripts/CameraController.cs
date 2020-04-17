using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

//	public GameObject target;
	CarController targetObj;



	void Start()
	{
		targetObj = FindObjectOfType<CarController> ();
		//index = PlayerPrefs.GetInt ("Character");

		//targetList = new GameObject[transform.childCount];
	}

	void Update () {
		Vector3 newPosition = targetObj.transform.position;
		newPosition.z = -10;
		//newPosition.y = 3.85f;

		transform.position = newPosition;
	}
}
