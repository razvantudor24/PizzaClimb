using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class CarController : MonoBehaviour {

	public float speed = 1500;
	public float rotationSpeed = 1500;

	public WheelJoint2D backWheel;
	public WheelJoint2D frontWheel;
	public Rigidbody2D rb;

//	private float axisV = 0f;
//	private float axisH = 0f;

	public float movement = 0f;
	public float rotation = 0f;
	AudioSource carAudio;
	GameManager gameManager;

	void Start()
	{
		carAudio = this.GetComponent<AudioSource> ();
		gameManager = FindObjectOfType<GameManager> ();
	}

	void Update()
	{
			//KEYBOARD comment for mobile func
		//movement = -Input.GetAxisRaw("Vertical") * speed;
			//Move(-Input.GetAxisRaw("Vertical"));
		//rotation = Input.GetAxisRaw("Horizontal");

		AnalogSpeedConverter.ShowSpeed (rb.velocity.magnitude, 0, 12);

		if (!gameManager.isPaused) {
			carAudio.enabled = true;
			carAudio.pitch = rb.velocity.magnitude / 4;
		} else
			carAudio.enabled = false;

		//Debug.Log (movement);

	}

	void FixedUpdate()
	{
		if(movement == 0)
		{
			backWheel.useMotor = false;
			frontWheel.useMotor = false;
		}
		else {
			backWheel.useMotor = true;
			frontWheel.useMotor= true;
			JointMotor2D motor = new JointMotor2D {motorSpeed=movement, maxMotorTorque = backWheel.motor.maxMotorTorque};
			backWheel.motor = motor;
			frontWheel.motor = motor;
		}
			rb.AddTorque(-rotation*rotationSpeed*Time.fixedDeltaTime);
	}

	//keyboard
	public void Move(float horizInput)
	{
		Debug.Log ("Fw");
		movement = horizInput * speed;
	}

/*	//TOUCH BUTTONS
	public void MoveForward(float horizInput)
	{
		movement = -horizInput * speed;
	}

	public void MoveBackward(float horizInput)
	{
		movement = horizInput * speed;
	}

	public void RotateForward(float verticalInput)
	{
		rotation = verticalInput;
	}

	public void RotateBackward(float verticalInput)
	{
		rotation = -verticalInput;
	}*/
}
