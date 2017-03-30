using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSManager : MonoBehaviour {

    public GameObject asteroid;
    private Asteroid asScript;
    private float asOffsetUp = 1.4f;
    private float asOffsetRight = -0.5f;
    public int asRemain = 3;
    public bool asNeed = true;
    private LineRenderer front;
    private LineRenderer back;
    private Ray SlingProjection;
    bool slingRender = false;
    public AudioSource launchSFX;

	// Use this for initialization
	void Start () {
   
	}

    void LineRendererSetup() {

        front = this.gameObject.transform.GetChild(0).GetComponent<LineRenderer>();
        back = this.gameObject.transform.GetChild(1).GetComponent<LineRenderer>();

        //front.SetPosition(0, front.transform.position);
        //back.SetPosition(0, back.transform.position);

        front.sortingLayerName = "SlingshotF";
        back.sortingLayerName = "SlingshotB";

        front.sortingOrder = 1;
        back.sortingOrder = 1;

    }

    void LineRendererUpdate() {
        SlingProjection.origin = (this.transform.position + this.gameObject.transform.up * asOffsetUp + this.gameObject.transform.right * asOffsetRight);
        Vector3 AsToSling = asScript.transform.position - SlingProjection.origin;
        SlingProjection.direction = AsToSling;
        Vector3 holdPoint = SlingProjection.GetPoint(AsToSling.magnitude + asScript.radius);
        front.SetPosition(1, holdPoint);
        back.SetPosition(1, holdPoint);
    }

    void DestroySling() {
        slingRender = false; 
        front.enabled = false;
        back.enabled = false;
    }

    void ResetSling()
    {
        slingRender = true;
        front.enabled = true;
        back.enabled = true;
    }

    public bool genNew() {

        if (asRemain == 0) return false;
        asNeed = true;
        return true;
    }

	// Update is called once per frame
	void Update () {
        if (asRemain > 0 && asNeed)
        {
            if (asRemain == 3) {
                LineRendererSetup();
            }
            ResetSling();
            asNeed = false;
            asRemain -= 1;
            GameObject asobj = GameObject.Instantiate(asteroid);
            asScript = asobj.GetComponent<Asteroid>();
            asScript.SlingPivot = this.transform.position + this.gameObject.transform.up * asOffsetUp + this.gameObject.transform.right * asOffsetRight;
            asScript.InitAs();
        }
        if (slingRender)
        {
            if (!asScript.launched || asScript.launched && (asScript.rigidbody2D.velocity.y > 0 ? (asScript.transform.position.y <= asScript.SlingPivot.y) : (asScript.transform.position.y >= asScript.SlingPivot.y))) LineRendererUpdate();
            else
            {
                DestroySling();
                launchSFX.Play();
            }
        }
	}
}
