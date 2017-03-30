using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDamage : MonoBehaviour {

    public Sprite damageSprite;
    public float damageImpactSpeed;
    private float damageImpactSpeedSqr;
    private SpriteRenderer spriteRenderer;
    public AudioSource dieSFX;

    public bool succeed = false;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        damageImpactSpeedSqr = damageImpactSpeed * damageImpactSpeed;
	}
	
	// Update is called once per frame
	void OnCollisionEnter2D (Collision2D collision) {
        if (collision.collider.tag != "Damager") return;
        if (collision.relativeVelocity.sqrMagnitude < damageImpactSpeedSqr) return;
        spriteRenderer.sprite = damageSprite;
        Kill();
	}

    void Kill() {
        spriteRenderer.enabled = false;
        dieSFX.Play();
        this.GetComponent<Collider2D>().enabled = false;
        this.GetComponent<Rigidbody2D>().isKinematic = true;
        this.GetComponent<ParticleSystem>().Play();
        succeed = true;
        Debug.Log("Success");
    }
}
