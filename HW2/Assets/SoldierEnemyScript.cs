using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoldierEnemyScript : MonoBehaviour {

    private Animator animator;
    private float MinimumHitPeriod = 1f;
    private float HitCounter = 0;
    public GunManager GM;
    public float CurrentHP = 100;

    public float MoveSpeed;
    public GameObject FollowTarget;
    private Rigidbody rigidBody;
    public CollisionListScript PlayerSensor;
    public CollisionListScript AttackSensor;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        rigidBody = this.GetComponent<Rigidbody>();
        //GM = this.GetComponentInChildren<GunManager>();
    }
    public void Hit(float value)
    {
        if (HitCounter <= 0)
        {
            FollowTarget = GameObject.FindGameObjectWithTag("Player");
            HitCounter = MinimumHitPeriod;
            CurrentHP -= value;
            animator.SetFloat("HP", CurrentHP);
            animator.SetTrigger("Hit");
            if (CurrentHP <= 0) { BuryTheBody(); }
        }
    }

    void OnParticleCollision(GameObject particle)
    {
        if (FollowTarget == null) FollowTarget = GameObject.FindGameObjectWithTag("Player");
        CurrentHP -= 1f;
        //Debug.Log(CurrentHP);
        animator.SetFloat("HP", CurrentHP);
        animator.SetTrigger("Hit");
        if (CurrentHP <= 0) { BuryTheBody(); }
    }

    void BuryTheBody()
    {
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<Collider>().enabled = false;
        this.transform.DOMoveY(-0.8f, 1f).SetRelative(true).SetDelay(1).OnComplete(() =>
        {
            this.transform.DOMoveY(-0.8f, 1f).SetRelative(true).SetDelay(3).OnComplete(() =>
            {
                GameObject.Destroy(this.gameObject);
            });
        });
    }

    void Update()
    {
        if (PlayerSensor.CollisionObjects.Count > 0)
        {
            FollowTarget = PlayerSensor.CollisionObjects[0].gameObject;
        }
        if (CurrentHP > 0 && HitCounter > 0)
        {
            HitCounter -= Time.deltaTime;
        }
        else
        {
            if (CurrentHP > 0)
            {
                if (FollowTarget != null)
                {
                    GM.ep = FollowTarget.gameObject.transform;
                    Vector3 lookAt = FollowTarget.gameObject.transform.position;
                    lookAt.y = this.gameObject.transform.position.y;
                    this.transform.LookAt(lookAt);
                    if (AttackSensor.CollisionObjects.Count == 0)
                    {
                        animator.SetBool("Run", true);
                        rigidBody.velocity = this.transform.forward * MoveSpeed;
                    }
                    else if (AttackSensor.CollisionObjects.Count > 0)
                    {
                        animator.SetBool("Run", false);
                        rigidBody.velocity = Vector3.zero;
                    }
                    //Debug.Log(FollowTarget.gameObject.transform.position);
                    GM.TryToTriggerGun();

                }
            }
            else
            {
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }
}
