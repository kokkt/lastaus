using UnityEngine;
using System.Collections;

public class Crate : MonoBehaviour {
	Vector3 position = new Vector3();
    public bool falling = true;

	// Use this for initialization
	void Start () {
		if (!rigidbody) {
			gameObject.AddComponent<Rigidbody> ();
		}
		rigidbody.freezeRotation = true;
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void PickedUp(){
		rigidbody.isKinematic = true;
	}
	public void Released(){
		rigidbody.isKinematic = false;
	}
}
