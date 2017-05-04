using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerController : MonoBehaviour {

    public float MoveSpeed = 5.0f;
    public float MoveRange = 0.3f;

    private Rigidbody rigidbody;
    private Vector3 velocity = Vector3.zero;
    private bool isControlling;
    public TouchInterface TouchController;
    public FaceDetector face;

    private SpriteRenderer sp;

    private Vector3 pre;
    private float transformCounter;

    private void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        isControlling = false;
        sp = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        if (transformCounter > 0)
        {
            transformCounter -= Time.deltaTime;
            return;
        }

        if (TouchController != null) {
            float fingerVelocity = (TouchController.touchPosWorld - pre).magnitude / Time.deltaTime;
            Debug.Log(fingerVelocity);
            if (fingerVelocity > 300f && face.cubeDetected != null) {
                this.GetComponent<Collider>().enabled = false;
                this.GetComponent<Rigidbody>().useGravity = false;
                transformCounter = 0.7f;
                this.transform.DOMoveX(2 * face.cubeDetected.transform.position.x - this.transform.position.x, 0.5f).OnComplete(() => {
                    this.GetComponent<Collider>().enabled = true;
                    this.GetComponent<Rigidbody>().useGravity = true;
                });
                //Debug.Log("slide");
            }
            pre = TouchController.touchPosWorld;
        }

        // When the player touched the screen...
        // When the player's finger moves...
        if (isControlling)
        {
            Vector3 deltaPos = TouchController.touchPosWorld - this.transform.position;
            if (deltaPos.x > MoveRange) // move +x
            {
                velocity.x = MoveSpeed;
                sp.flipX = false;
                face.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (deltaPos.x < -MoveRange) // move -x
            {
                velocity.x = -MoveSpeed;
                sp.flipX = true;
                face.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else velocity.x = 0;
        }
        else
        {
            isControlling = false;
            velocity = Vector3.zero;
        }
        rigidbody.velocity = velocity;
    }

    public void OnTouchDown() {
        TouchController = this.GetComponentInParent<TouchMiddleware>().TouchController;
        isControlling = true;
    }

    public void OnTouchUp() {
        TouchController = null;
        isControlling = false;
    }

}


