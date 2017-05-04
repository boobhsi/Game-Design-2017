using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject Player;

    private float offsetY;

    void Start()
    {
        offsetY = this.transform.position.y - Player.transform.position.y;
    }
    
	void Update () {
        Vector3 nextPos = Player.transform.position;
        nextPos.z = this.transform.position.z;
        nextPos.y += offsetY;
        this.transform.position = nextPos;
	}
}
