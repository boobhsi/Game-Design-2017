using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resetter : MonoBehaviour {

    private float resetSpeed = 0.15f;
    public CameraController cc;
    public SSManager ssm;
    public TargetDamage td;
    public Text success;
    public Text again;
    public Text fail;

    private float resetSpeedSqr;
    public int frameCountDownLimit = 300;
    private int fcd = 0;

	// Use this for initialization
	void Start () {
        resetSpeedSqr = resetSpeed * resetSpeed;
        success.enabled = false;
        again.enabled = false;
        fail.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R)) {
            Reset(); //reset this episode
        }
        if(cc.target != null && cc.target.GetComponent<Rigidbody2D>().velocity.sqrMagnitude < resetSpeedSqr && cc.target.GetComponent<Asteroid>().launched && !td.succeed) {
            Destroy(cc.target);
            //Debug.Log("slow");
            Fail();
        }
        if (td.succeed) {
            if (fcd < frameCountDownLimit)
            {
                if (fcd == 0) success.enabled = true;
                fcd++;
            }
            else
            {
                Success();
            }
        }
	}

    void OnTriggerExit2D(Collider2D other) {
        if (cc.target!=null && other.GetComponent<Rigidbody2D>() == cc.target.GetComponent<Rigidbody2D>() && !td.succeed) {
            Destroy(cc.target);
            Fail();
        }
    }

    void Reset() {
        Application.LoadLevel(Application.loadedLevel);
    }

    void Fail() {
        if (!ssm.genNew()) GameOver();
        cc.Reset();
        Debug.Log("fail");
    }

    void GameOver() {
        fail.enabled = true;
        Debug.Log("over");
    }


    void Success() {
        Application.LoadLevel("Episode_2");
    }
}
