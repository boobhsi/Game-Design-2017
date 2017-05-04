using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralCubeController : MonoBehaviour {

    static public int boxCount = 0;

    public float range = 1f;
    public bool electronic;
    public string cubeType;
    public TouchInterface TouchController;
    public int boxNum = 0;
    private Rigidbody rigidBody;
    public GameObject slotBelong;

	// Use this for initialization
	void Start () {
        if (electronic) cubeType = "ele";
        else cubeType = "normal";

        boxNum = boxCount;
        Debug.Log("Box No." + boxNum + " created!");
        boxCount++;

        rigidBody = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTouchDown() {
        Debug.Log(Vector3.Distance(this.GetComponentInParent<CubeManager>().player.transform.position, this.transform.position));
        TouchController = this.GetComponentInParent<TouchMiddleware>().TouchController;
        if (this.GetComponentInParent<CubeManager>().needTarget)
        {
            this.GetComponentInParent<CubeManager>().target = this.gameObject;
            this.transform.parent.SendMessage("OnTouchDown");
        }
        else if(Vector3.Distance(this.GetComponentInParent<CubeManager>().player.transform.position, this.transform.position) < range)
        {
            this.GetComponentInParent<CubeManager>().inControll = this.gameObject;
            TouchController.SendMessage("Occupy");
            this.transform.parent.SendMessage("OnTouchDown");
        }
    }

    void OnTouchUp() {
        this.transform.parent.SendMessage("OnTouchUp");
    }

    void Attached() {
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Exit() {
        this.GetComponent<Rigidbody>().useGravity = true;
        this.GetComponent<Rigidbody>().isKinematic = false;
        slotBelong = null;
    }

    public void OnCollisionExit(Collider other) {
        rigidBody.velocity = Vector3.zero;
    }

    public void OnCollisionEnter(Collider other) {
        rigidBody.velocity = other.GetComponent<Rigidbody>().velocity;
    }
}
