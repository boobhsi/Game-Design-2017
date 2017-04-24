using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour {

    public AudioSource fireSound;
    public ParticleSystem fireHole;
    public List<ParticleCollisionEvent> collisionEvents;
    public float damage = 0.01f;

	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame


    public void Fire() {
        fireHole.Play();
        if(!fireSound.isPlaying) fireSound.Play();
    }

    public void ceaseFire() {
        if (fireSound.isPlaying) fireSound.Stop();
    }
}
