using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileCarControl : MonoBehaviour {

	CarController carControl;
	// Use this for initialization
	void Start () {

		carControl = GetComponentInChildren<CarController> ();
	}

	//TOUCH BUTTONS
	public void MoveForward(float horizInput)
	{
		carControl.movement = -horizInput * carControl.speed;
	}

	public void MoveBackward(float horizInput)
	{
		carControl.movement = horizInput * carControl.speed;
	}

	public void RotateForward(float verticalInput)
	{
		carControl.rotation = verticalInput;
	}

	public void RotateBackward(float verticalInput)
	{
		carControl.rotation = -verticalInput;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
