using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;


public class AvatarController : MonoBehaviour
{

    private Animator animationCon;

    // Use this for initialization
    void Start()
    {

        animationCon = this.GetComponent<Animator>();
    }

    void Update() {
    }

    public void OnSingle() {
        animationCon.SetTrigger("single");
    }

    public void OnDouble() {
        animationCon.SetTrigger("double");
    }

    public void OnShake() {
        animationCon.SetTrigger("shake");
    }

}
