using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TouchInterface : MonoBehaviour {

    public Vector3 touchPosWorld;
    private bool isControlling = false;
    private bool occupiedByBox = false;
    public GameObject target;
    private Ray ray;

    private void Start()
    {
        //rigidbody2d = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // When the player touched the screen...
        //////if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        if(Input.GetMouseButtonDown(0))
        {
            //////ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (IsCastToObject())
            {
                target.GetComponentInParent<TouchMiddleware>().TouchController = this;
                target.SendMessage("OnTouchDown");
                isControlling = true;
            }
        }

        // When the player's finger moves...
        //////if (Input.touchCount > 0 && isControlling)
        if(isControlling)
        {
            //////ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //////if (Input.GetTouch(0).phase == TouchPhase.Ended)
            if (Input.GetMouseButtonUp(0))
            {
                GameObject temp = target;
                IsCastToObject();
                temp.SendMessage("OnTouchUp");
                isControlling = false;
            }
            else {
                touchPosWorld = ray.GetPoint(1f);
            }
        }
    }

    bool IsCastToObject() {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            if (occupiedByBox && !hit.collider.CompareTag("Box")) {
                return false;
            }
            Debug.Log("Mouse hit");
            target = hit.collider.gameObject;
            return true;
        }
        return false;
    }

    public void Occupy() {
        occupiedByBox = true;
    }

    public void Free() {
        occupiedByBox = false;
    }
}
