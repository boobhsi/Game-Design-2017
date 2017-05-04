using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;

public class ElectronicCubeController : MonoBehaviour
{
    public bool up, down, right, left;
    private int code = 0;
    //private Vector2 node_u, node_d, node_r, node_l;
    //private GameObject attachSlot;
    //public float lineOffset = 0.55f;
    //private LineRenderer inner;

    // Use this for initialization
    void Start()
    {
        int count = 0;
        if (up && count < 2)
        {
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
        //node_u = Vector3(0, 0.5, 0)
        //inner = this.GetComponent<LineRenderer>();
        //inner
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int compareCode(int com) {
        //Debug.Log("Com:" + code);
        return com ^ code;
    }

    /*
    public void CheckNode(Transform pos)
    {
        if (up && Vector2.Distance(new Vector2(pos.position.x, pos.position.y), node_u) < 0.1)
        {
            codu += 1;
        }
        if (down && Vector2.Distance(new Vector2(pos.position.x, pos.position.y), node_d) < 0.1)
        {
            codu += 1;
        }
        if (right && Vector2.Distance(new Vector2(pos.position.x, pos.position.y), node_r) < 0.1)
        {
            codu += 1;
        }
        if (left && Vector2.Distance(new Vector2(pos.position.x, pos.position.y), node_l) < 0.1)
        {
            codu += 1;
        }
    }
    */
}