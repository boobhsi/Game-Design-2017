using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlotController : MonoBehaviour {

    public LineRenderer attach_1, attach_2;
    public bool up, down, right, left;
    private int code = 0;
    public GameObject attachCube;
    public GameObject candiCube;

	// Use this for initialization
	void Start () {
        int count = 0;
        if (up && count < 2) {
            count++;
            code |= 8;
        }
        if (down && count < 2)
        {
            count++;
            code |= 4;
        }
        if (left && count < 2)
        {
            count++;
            code |= 2;
        }
        if (right && count < 2)
        {
            code |= 1;
        }
        attach_1.SetPosition(1, this.transform.position - new Vector3(0, 0, LineController.lineOffset));
        attach_2.SetPosition(1, this.transform.position - new Vector3(0, 0, LineController.lineOffset)); // up -> down -> left -> right

        //attach_1.enabled = false;
        //attach_2.enabled = false;

        if (attachCube != null)
        {
            Judge(attachCube);
            attachCube.GetComponent<GeneralCubeController>().slotBelong = this.gameObject;
            attachCube.transform.position = this.transform.position;
            attachCube.SendMessage("Attached");
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void OnTriggerEnter(Collider other)
    {
        candiCube = other.gameObject;

        //Debug.Log("collide!");
    }

    public void OnTriggerExit(Collider other) {
        candiCube = null;
    }

    public void Settle() {
        attachCube = candiCube;
        /*
        attachCube.transform.DOMove(this.transform.position, 1f).SetEase(Ease.OutQuad).OnComplete(() => {
            Judge(attachCube);
            attachCube.GetComponent<GeneralCubeController>().slotBelong = this.gameObject;
        });
        
        */
        Judge(attachCube);
        attachCube.GetComponent<GeneralCubeController>().slotBelong = this.gameObject;
        attachCube.transform.position = this.transform.position;
        attachCube.SendMessage("Attached");
    }

    void Judge(GameObject cube) {
        //Debug.Log("Judge:" + code);
        if (cube.GetComponent<GeneralCubeController>().cubeType == "ele")
        {
            int ans = cube.GetComponent<ElectronicCubeController>().compareCode(code);
            ans &= code;
            //Debug.Log(ans);
            if (ans == 0)
            {
                attach_1.enabled = true;
                attach_2.enabled = true; //both on
            }
            else
            {
                int noCod = code - ans;
                if (noCod == 0)
                {
                    attach_1.enabled = false;
                    attach_2.enabled = false;
                }
                else if (noCod > ans)
                {
                    attach_1.enabled = true;
                    attach_2.enabled = false; // attach_2 on
                }
                else
                {
                    attach_2.enabled = true;
                    attach_1.enabled = false; // attach_1 on
                }
            }
        }
        else {
            attach_1.enabled = false;
            attach_2.enabled = false;
        }
    }
}
