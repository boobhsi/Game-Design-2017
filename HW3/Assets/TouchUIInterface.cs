using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class TouchUIInterface : MonoBehaviour {

    public AvatarManager am;

    public TapGesture singleTap;
    public TapGesture doubleTap;

    public float shakeThreshold = 3f;

	// Use this for initialization
	void Start () {

        singleTap.Tapped += (object sender, System.EventArgs e) =>
        {
            Debug.Log("single");
            am.SendMessage("OnSingle");
        };

        doubleTap.Tapped += (object sender, System.EventArgs e) =>
        {
            Debug.Log("double");
            am.SendMessage("OnDouble");
        };

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.acceleration.magnitude > shakeThreshold) am.SendMessage("OnShake");
	}
}
