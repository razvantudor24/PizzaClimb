using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalogSpeedConverter : MonoBehaviour {

	static float minAngle = 41f;
	static float maxAngle = -223f;
	static AnalogSpeedConverter thisSpeedo;
	// Use this for initialization
	void Start () {
		thisSpeedo = this;
	}

	public static void ShowSpeed(float speed, float min, float max)
	{
		float ang = Mathf.Lerp (minAngle, maxAngle, Mathf.InverseLerp (min, max, speed));
		thisSpeedo.transform.eulerAngles = new Vector3 (0, 0, ang);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
