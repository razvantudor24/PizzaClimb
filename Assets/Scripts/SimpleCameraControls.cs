using UnityEngine;
using System.Collections;

public class SimpleCameraControls : MonoBehaviour {
    //public Transform target;
	CarController target;
    public Vector2 offset = Vector2.zero;
    public float speed = 10.0f;

	void Start()
	{
		target = FindObjectOfType<CarController> ();
	}

	void LateUpdate () {
		//transform.position = Vector3.Lerp (transform.position, new Vector3 (target.transform.position.x + offset.x, target.transform.position.y + offset.y, transform.position.z), Time.deltaTime);//, Time.deltaTime * speed);
		transform.position = new Vector3(target.transform.position.x + offset.x, target.transform.position.y + offset.y, transform.position.z);
	}
}
