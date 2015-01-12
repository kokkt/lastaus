using UnityEngine;
using System.Collections;

public class PickUpScript : MonoBehaviour {
	public float pickup_range = 1.0f;
	public Transform fork;
	public KeyCode key_pickup = KeyCode.E;
    public KeyCode key_lift_up = KeyCode.R;
    public KeyCode key_lift_down = KeyCode.F;
    public float lift_speed = 0.5f;
    public float[] fork_min_max = {0.0f, 2.0f};
    Vector3 lift = new Vector3();
    Vector3 itemOffset = new Vector3();
	Transform pickedUpItem;

	// Use this for initialization
	void Start () {
        if (!fork)
        {
            fork = GameObject.Find("Fork").transform;
        }

        if (fork)
        {
            lift = fork.localPosition;
            fork_min_max[0] += lift.y;
            fork_min_max[1] += lift.y;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (key_pickup))
		{
			PickUpItem();
		}
        if (Input.GetKey(key_lift_up))
        {
            LiftFork(lift_speed);
        }
        if (Input.GetKey(key_lift_down))
        {
            LiftFork(-lift_speed);
        }
        fork.localPosition = lift;
        /* This is how you can carry the item without re-parenting it
		if (pickedUpItem) {
			pickedUpItem.position = fork.position;
			pickedUpItem.rotation = fork.rotation;
		}
         */
	}

	void PickUpItem(){
		if (!pickedUpItem) {
				RaycastHit hit;
				if (Physics.Raycast (fork.position, fork.forward, out hit, pickup_range)) {
						Debug.Log (hit.transform);
						if (!hit.transform.CompareTag ("Player")) {
                            SetAsPickedUp(hit.transform);
						}
				} else {
						Debug.Log ("No item");
				}
		} else {
            ReleaseItem();
		}
	}
    void SetAsPickedUp(Transform target)
    {
        pickedUpItem = target;
        itemOffset.y = target.collider.bounds.size.y/2 - (target.position.y - fork.position.y); //
        pickedUpItem.Translate(itemOffset);
        pickedUpItem.parent = fork;

    }
    void ReleaseItem()
    {
        Vector3 newPos = pickedUpItem.position;
        newPos.y = pickedUpItem.collider.bounds.size.y / 2;
        pickedUpItem.position = newPos;
        pickedUpItem.parent = null;
        pickedUpItem = null;
    }
    void LiftFork(float speed)
    {
        lift.y = Mathf.Clamp(lift.y + speed * Time.deltaTime, fork_min_max[0], fork_min_max[1]);
    }
}
