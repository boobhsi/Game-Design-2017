using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour {

    public GameObject inControll;
    public GameObject player;
    public GameObject target;
    private TouchMiddleware tm;
    public bool needTarget = false;

    enum StateNum {
        Chaining, Changing, Static, ChainComplete, ReadyToChange, ChangeFail
    };

    StateNum CurrentState;
    StateNum forDebug;

	// Use this for initialization
	void Start () {
        tm = GetComponent<TouchMiddleware>();
        CurrentState = StateNum.Static;
	}
	
	// Update is called once per frame
	void Update () {
        if (CurrentState == StateNum.ChainComplete) {
            Vector3 currentVelocity = inControll.GetComponent<Rigidbody>().velocity;
            target.GetComponent<Rigidbody>().velocity = new Vector3(currentVelocity.x, target.GetComponent<Rigidbody>().velocity.y, 0);
        }
        if (forDebug != CurrentState)
        {
            Debug.Log(CurrentState);
            forDebug = CurrentState;
        }
	}

    public void OnTouchDown() {
        switch (CurrentState) {
            case StateNum.Static:
                Debug.Log(inControll.GetComponent<GeneralCubeController>().boxNum);
                CurrentState = StateNum.Chaining;
                needTarget = true;
                break;
            case StateNum.ChainComplete:
                //do nothing
                break;
            case StateNum.Changing:
                if (tm.TouchController.target == null) {
                    //do nothing
                }
                else if (tm.TouchController.target.GetComponent<GeneralCubeController>().boxNum == inControll.GetComponent<GeneralCubeController>().boxNum)
                {
                    //Debug.Log(tm.TouchController.target.GetComponent<GeneralCubeController>().boxNum + " vs " + inControll.GetComponent<GeneralCubeController>().boxNum);
                    CurrentState = StateNum.ChangeFail;
                }
                else
                {
                    target = tm.TouchController.target;
                    CurrentState = StateNum.ReadyToChange;
                }
                break;
            default:
                break;
        }
    }

    public void OnTouchUp() {
        switch (CurrentState) {
            case StateNum.ChainComplete:
                CurrentState = StateNum.Static;
                tm.TouchController.SendMessage("Free");
                needTarget = false;
                target = null;
                inControll = null;
                break;
            case StateNum.Chaining:
                if (tm.TouchController.target == null) {
                    CurrentState = StateNum.Changing;
                }
                else if (tm.TouchController.target.GetComponent<GeneralCubeController>().boxNum == inControll.GetComponent<GeneralCubeController>().boxNum)
                {
                    CurrentState = StateNum.Changing;
                }
                else {
                    CurrentState = StateNum.ChainComplete;
                    needTarget = false;
                    tm.TouchController.SendMessage("Free");
                    target = tm.TouchController.target;
                    target.GetComponent<Rigidbody>().useGravity = true;
                }
                break;
            case StateNum.ReadyToChange:
                ChangeTwoCube();
                CurrentState = StateNum.Static;
                tm.TouchController.SendMessage("Free");
                target = null;
                inControll = null;
                needTarget = false;
                break;
            case StateNum.ChangeFail:
                inControll = null;
                target = null;
                needTarget = false;
                CurrentState = StateNum.Static;
                tm.TouchController.SendMessage("Free");
                break;
            default:
                break;
        }
    }

    void ChangeTwoCube() {
        Debug.Log("change");
        Vector3 temp = inControll.transform.position;
        if(target.GetComponent<GeneralCubeController>().slotBelong != null)
        {
            GameObject slot = target.GetComponent<GeneralCubeController>().slotBelong;
            inControll.SendMessage("Attached");
            target.SendMessage("Exit");
            slot.GetComponent<SlotController>().candiCube = inControll;
            slot.GetComponent<SlotController>().Settle();
            Debug.Log("change over");
        }
        else inControll.transform.position = target.transform.position;
        target.transform.position = temp;
    }
}
