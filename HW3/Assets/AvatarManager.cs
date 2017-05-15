using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
using DG.Tweening;

public class AvatarManager : MonoBehaviour {

    public FlickGesture flick;

    public List<GameObject> AvatarList;

    public Transform Outerspace;

    public TransformGesture transformGesture;

    public PinnedTransformGesture ptg;


    private int nowAt;

    public float rotateSpeed = 10f;

    public float moveOffset = 2f;
    public float transSpeed = 1f;

	// Use this for initialization
	void Start () {

        transformGesture.Transformed += (object sender, System.EventArgs e) =>
        {
            AvatarList[nowAt].transform.localScale *= transformGesture.DeltaScale;
        };

        ptg.Transformed += (object sender, System.EventArgs e) =>
        {
            AvatarList[nowAt].transform.Rotate(new Vector3(0, 1, 0), ptg.DeltaRotation * rotateSpeed);
        };

        flick.Flicked += (object sensor, System.EventArgs e) =>
        {
            ChangeAvatar(flick.ScreenFlickVector.x < 0);
        };

        AvatarList.ForEach(obj =>
        {
            obj.transform.position = Outerspace.transform.position;
        });

        AvatarList[0].transform.position = this.transform.position;

        nowAt = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ChangeAvatar(bool left) {
        GameObject nowObj = AvatarList[nowAt];
        GameObject nextObj = AvatarList[left ? (nowAt + 1 >= AvatarList.Count ? nowAt = 0 : ++nowAt) : (nowAt - 1 < 0 ? nowAt = AvatarList.Count - 1 : --nowAt)];
        nextObj.transform.position = new Vector3(left ? this.transform.position.x + moveOffset * nextObj.transform.localScale.x : this.transform.position.x - moveOffset * nextObj.transform.localScale.x, this.transform.position.y, this.transform.position.z);
        nowObj.transform.DOMoveX(left ? this.transform.position.x - moveOffset * nowObj.transform.localScale.x : this.transform.position.x + moveOffset * nowObj.transform.localScale.x, transSpeed).OnComplete(() => 
        {
            nowObj.transform.position = Outerspace.position;
        });
        nextObj.transform.DOMoveX(this.transform.position.x, transSpeed);
    }
    public void OnSingle() {
        AvatarList[nowAt].SendMessage("OnSingle");
    }

    public void OnDouble() {
        AvatarList[nowAt].SendMessage("OnDouble");
    }

    public void OnShake() {
        AvatarList[nowAt].SendMessage("OnShake");
    }

}
