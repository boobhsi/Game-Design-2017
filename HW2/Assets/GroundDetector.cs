using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour {

    private PlayerControll pc;

	// Use this for initialization
	void Start () {
        pc = this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<PlayerControll>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other) {
        Debug.Log("land");
        if (!pc.onGround) pc.onGround = true;
    }
}
