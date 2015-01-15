using UnityEngine;
using System.Collections;

public class Crate : MonoBehaviour {
    bool falling = true;

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<Rigidbody>();
	
	}
	
	// Update is called once per frame
	void Update () {
        if (falling)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 0.5f);
        }
	}

    void OnEnterCollision(Collision c)
    {
        falling = false;
    }
    void OnExitCollision(Collision c)
    {
        falling = true;
    }
}
