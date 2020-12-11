using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour {

	//	---------- PUBLIC VARIABLES ----------
	//	        (Change as you like)
	public Vector3 currentSpeed;					// The current speed (should not be changed manually)
	public float defaultSpeed = 1.6f;				// Default speed for the player
	public float returnToDefaultSpeed = 0.02f;		// This number is how fast the player object returns to default speed
	public float acceleration = 0.2f;				// Acceleration factor
	public float maxSpeed = 3f;						// Maximum speed the player can move at
	public float minSpeed = 0.5f;					// Minimum speed for the player
	public float turningSpeed = 2f;					// How fast the player moves to the sides
	public float turnSmoothing = 50f;				// The smoothing factor of the player when returning to 0 rotation


	//	---------- PRIVATE VARIABLES ----------
	//	       (Do not change manually)
	private bool returningToDefaultSpeed = false;
	private float ts = 0;
	
	
	// Use this for initialization
	void Start () {
		// Set the current speed to the default value when the game starts
		currentSpeed = new Vector3(0,0,defaultSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Q) && !returningToDefaultSpeed){
			// If Q key is pressed
			IncreaseSpeed(acceleration);
		}else if(Input.GetKey(KeyCode.A) && !returningToDefaultSpeed){
			// If A key is pressed
			DecreaseSpeed(acceleration);
		}else if(Input.GetKey(KeyCode.Return) && !returningToDefaultSpeed){
			// If ENTER/RETURN key is pressed, make the player go back to default speed
			if(currentSpeed.z != defaultSpeed){
				returningToDefaultSpeed = true;
			}
		}
		
		if(Input.GetKey(KeyCode.RightArrow)){
			// If RIGHT ARROW key is pressed, then turn and move right
			transform.Rotate(0, 0, 20 * Time.deltaTime, Space.Self);
			transform.Translate(new Vector3(turningSpeed, 0, 0) * Time.deltaTime, Space.World);
			Debug.Log("Turning right");
		}else if(Input.GetKey(KeyCode.LeftArrow)){
			// If LEFT ARROW key is pressed, then turn and move left
			transform.Rotate(0, 0, (20 * Time.deltaTime) * -1f, Space.Self);
			transform.Translate(new Vector3(turningSpeed * -1f, 0, 0) * Time.deltaTime, Space.World);
			Debug.Log("Turning left");
		}else{
			// If none of the left or right arrow keys is pressed, rotate back to default/null rotation
			if(transform.rotation.z < 180 && transform.rotation.z > 0){
				if(transform.rotation.z != 0){
					transform.Rotate(0, 0, (turnSmoothing * Time.deltaTime) * -1f, Space.Self);
				}
			}else if(transform.rotation.z < 360 && transform.rotation.z != 0){
				if(transform.rotation.z != 0){
					transform.Rotate(0, 0, (turnSmoothing * Time.deltaTime), Space.Self);
				}
			}
		}
		
		
		if(Input.GetKey(KeyCode.UpArrow)){
			// If UP ARROW key is pressed, move player up
			transform.Translate(new Vector3(0, turningSpeed, 0) * Time.deltaTime, Space.World);
		}else if(Input.GetKey(KeyCode.DownArrow)){
			// If DOWN ARROW pressed, move player down
			transform.Translate(new Vector3(0, turningSpeed * -1f, 0) * Time.deltaTime, Space.World);
		}else{
			
		}
		
		
		// If the player are returning to default speed, do it smoothly
		if(returningToDefaultSpeed == true){
			ts = Mathf.Round(currentSpeed.z*100)/100;

			if(ts == defaultSpeed){
				returningToDefaultSpeed = false;
			}else if(currentSpeed.z > defaultSpeed){
				currentSpeed.z -= returnToDefaultSpeed;
			}else if(currentSpeed.z < defaultSpeed){
				currentSpeed.z += returnToDefaultSpeed;
			}else{

			}
		}

		// Move object forward at the speed of currentSpeed
		currentSpeed.z = Mathf.Clamp(currentSpeed.z, minSpeed, maxSpeed);
		transform.Translate(currentSpeed * Time.deltaTime, Space.World);
	}

	// Increase movement speed
	void IncreaseSpeed(float inc){
		if(currentSpeed.z >= minSpeed && currentSpeed.z <= maxSpeed){
			currentSpeed += new Vector3(0,0,inc);
		}
	}

	// Decrease movement speed
	void DecreaseSpeed(float dec){
		if(currentSpeed.z >= minSpeed && currentSpeed.z <= maxSpeed){
			currentSpeed -= new Vector3(0,0,dec);
		}
	}
}
