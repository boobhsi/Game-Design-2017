using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private Vector3 OriginPosition;
    public GameObject target;
    public Transform farLeft;
    public Transform farRight;

	// Use this for initialization
	void Start () {
        OriginPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (target == null)
        {
            target = GameObject.Find("Asteroid(Clone)");
            Debug.Log("found");
        }
        else
        {
            //if (target.transform.position.x > this.transform.position.x) this.transform.position = new Vector3(target.transform.position.x, this.transform.position.y, this.transform.position.z);
            Vector3 newPosition = this.transform.position;
            newPosition.x = target.transform.position.x;
            newPosition.x = Mathf.Clamp(newPosition.x, farLeft.position.x, farRight.position.x);
            this.transform.position = newPosition;
        }
	}

    public void Reset() {
        this.transform.position = OriginPosition;
        target = null;
        Debug.Log("ccReset");
    }
}
