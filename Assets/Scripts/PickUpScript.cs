using UnityEngine;
using System.Collections;

public class PickUpScript : MonoBehaviour {
	public float pickup_range = 1.0f;
	public Transform holder;
	public KeyCode key_pickup = KeyCode.Space;
	Transform pickedUpItem;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (key_pickup))
		{
			PickUpItem();
		}
		if (pickedUpItem) {
			pickedUpItem.position = holder.position;
			pickedUpItem.rotation = holder.rotation;
		}
	}

	void PickUpItem(){
		if (!pickedUpItem) {
				RaycastHit hit;
				if (Physics.Raycast (holder.position, holder.forward, out hit, pickup_range)) {
						Debug.Log (hit.transform);
						if (!hit.transform.CompareTag ("Player")) {
								pickedUpItem = hit.transform;
						}
				} else {
						Debug.Log ("No item");
				}
		} else {
			Vector3 newPos = pickedUpItem.position;
			newPos.y = pickedUpItem.collider.bounds.size.y/2;
			pickedUpItem.position = newPos;
			pickedUpItem = null;
		}
	}
}
