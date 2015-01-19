using UnityEngine;
using System.Collections;

public class Crate : MonoBehaviour {
	Vector3 position = new Vector3();
    public bool falling = true;
	Transform crate;

	// Use this for initialization
	void Start () {
		crate = transform;
		if (!rigidbody) {
			gameObject.AddComponent<Rigidbody> ();
		}
		rigidbody.freezeRotation = true;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (VehicleControls.blocky_controls) {
			Vector3 roundedPos = crate.position;
			roundedPos.x = Mathf.Round(roundedPos.x);
			//roundedPos.y = Mathf.Round(roundedPos.y);
			roundedPos.z = Mathf.Round(roundedPos.z);
			crate.position = roundedPos;
		}
	}

	public void PickedUp(){
		rigidbody.isKinematic = true;
	}
	public void Released(){
		rigidbody.isKinematic = false;
	}
}
