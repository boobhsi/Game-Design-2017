using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloScript : MonoBehaviour {

    private int updateCounter = 0;

	// Use this for initialization
	void Start () {
        Debug.Log("Hello Unity Start");
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Hello Unity Update Loop i=" + (updateCounter++));
	}
}
