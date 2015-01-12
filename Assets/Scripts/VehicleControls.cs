using UnityEngine;
using System.Collections;

public class VehicleControls : MonoBehaviour {
	bool debug = true;
	Vector3 moveVector = new Vector3();
	Vector3 limitedMove = new Vector3();
	public KeyCode key_accelerate = KeyCode.W;
	public KeyCode key_reverse = KeyCode.S;
	public KeyCode key_left = KeyCode.A;
	public KeyCode key_right = KeyCode.D;
	public float speed_max = 10.0f;
	public float acceleration = 0.5f;
	public float reverseSpeed = 0.3f;
	public float turn_acc = 0.3f;
	public float turn_max = 4.0f;
	float turn_amount = 0.0f;

	float turnDecay = 2.0f;
	float turnThres = 0.1f;
	float speedDecay = 1.05f;
	float speedThres = 0.1f;

	void OnGUI(){
		if (debug) {
			GUILayout.Label("MoveVector: " + moveVector);
			GUILayout.Label("TurnAmount: " + turn_amount);
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	// Update is called once per frame
	void Update () {
		ApplyPhysics ();
		UpdateControls ();
		Move ();
	}
	void ApplyPhysics(){
		if (turn_amount < -turnThres || turn_amount > turnThres) {
			turn_amount /= turnDecay;
		} else {
			turn_amount = 0;
		}
		if (moveVector.magnitude < -speedThres || moveVector.magnitude > speedThres) {
			moveVector = moveVector.normalized * moveVector.magnitude / speedDecay;
		} else {
			moveVector *= 0;
		}
	}
	void Move(){
		limitedMove.x = moveVector.x;
		limitedMove.y = moveVector.y;
		limitedMove.z = moveVector.z;
		if (limitedMove.magnitude > speed_max) {
			limitedMove = limitedMove.normalized * speed_max;
		}
		transform.Translate (limitedMove*Time.deltaTime);

		transform.Rotate(Vector3.up * turn_amount * Mathf.Sign (moveVector.z));

	}

	void UpdateControls(){
		if(Input.GetKey(key_accelerate))
		{
			Accelerate(Vector3.forward);
		}
		if(Input.GetKey(key_reverse))
		{
			Reverse (Vector3.back);
		}
		if(Input.GetKey(key_left))
		{
			Turn (-turn_acc);
		}
		if(Input.GetKey(key_right))
		{
			Turn (turn_acc);
		}

	}

	void Accelerate(Vector3 direction){
		ChangeVelocity (direction, acceleration);

	}
	void Reverse(Vector3 direction){
		ChangeVelocity (direction, reverseSpeed);
	}
	void ChangeVelocity(Vector3 direction, float speed){
		Vector3 velVec = direction * speed;
		moveVector.x = AddClamped (velVec.x, moveVector.x, speed_max);
		moveVector.y = AddClamped (velVec.y, moveVector.y, speed_max);
		moveVector.z = AddClamped (velVec.z, moveVector.z, speed_max);

	}
	float AddClamped(float a, float b, float limit){
		return Mathf.Clamp (a + b, -limit, limit);
	}
	void Turn(float amount){
		turn_amount = Mathf.Clamp(turn_amount+amount, -turn_max, turn_max);
	}
}
