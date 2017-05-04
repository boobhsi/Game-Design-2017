using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDetector : MonoBehaviour {

    public GameObject cubeDetected;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other) {
        cubeDetected = other.gameObject;
    }

    void OnTriggerExit(Collider other) {
        cubeDetected = null;
    }

}
