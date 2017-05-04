using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour {

    public static float lineOffset = 0.55f;
    //public GameObject attachCube;
    private LineRenderer line;

	// Use this for initialization
	void Start () {
        line = this.GetComponent<LineRenderer>();
        line.SetPosition(0, this.transform.position - new Vector3(0, 0, lineOffset));
        line.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
