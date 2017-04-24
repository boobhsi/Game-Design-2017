using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlayerControll : MonoBehaviour {

    private Animator animatorController;
    public Transform rotateYTransform;
    public Transform rotateXTransform;
    public float rotateSpeed;
    public float currentRotateX = 0;
    public float MoveSpeed;
    float currentSpeed = 0;
    public AudioSource walk;

    public Rigidbody rigidBody;

    public JumpSensor JumpSensor;
    public float JumpSpeed;
    public GameObject gun;
    public GameObject firegun;
    private GunManager gunManager;
    private FireManager fireManager;

    private bool isGun = true;

    public GameUIManager uiManager;
    public int hp = 100;
    private float gunChangeCounter = 0;
    public float maxGCC = 0.7f;
    private Vector3 gunOrientation;
    private Vector3 gunPosition;
    private bool changingGun = false;
    public bool onGround = false;

    // Use this for initialization
    void Start () {
        animatorController = this.GetComponent<Animator>();
        firegun.SetActive(false);
        gunManager = gun.GetComponent<GunManager>();
        fireManager = firegun.GetComponent<FireManager>();
    }

    public void Hit(int value)
    {
        Debug.Log("Hit!");
        if (hp <= 0)
        {
            return;
        }
        hp -= value;
        uiManager.SetHP(hp);
        if (hp > 0)
        {
            uiManager.PlayHitAnimation();
        }
        else
        {
            uiManager.PlayerDiedAnimation();

            rigidBody.gameObject.GetComponent<Collider>().enabled = false;
            rigidBody.useGravity = false;
            rigidBody.velocity = Vector3.zero;
            this.enabled = false;
            rotateXTransform.transform.DOLocalRotate(new Vector3(-60, 0, 0), 0.5f);
            rotateYTransform.transform.DOLocalMoveY(-1.5f, 0.5f).SetRelative(true);
        }
    }


    // Update is called once per frame
    void Update () {
        Cursor.visible = false;
        if (gunChangeCounter > 0)
        {
            gunChangeCounter -= Time.deltaTime;
            if (gunChangeCounter <= 0)
            {
                this.transform.SetPositionAndRotation(rotateXTransform.TransformPoint(this.gunPosition), Quaternion.Euler(0, 0, 0));
                this.transform.LookAt(this.transform.position + rotateXTransform.TransformDirection(gunOrientation));

            }
            else this.transform.RotateAround(rotateXTransform.transform.position, rotateXTransform.right, -60 / maxGCC * Time.deltaTime);
        }
        if (isGun && Input.GetMouseButton(0) && gunChangeCounter <= 0)
        {

            gunManager.TryToTriggerGun();
        }
        if (!isGun && Input.GetMouseButton(0) && gunChangeCounter <= 0)
        {
            fireManager.Fire();
        }

        if (!isGun && Input.GetMouseButtonUp(0) && gunChangeCounter <= 0)
        {
            fireManager.ceaseFire();
        }

        if (Input.GetMouseButton(1) && gunChangeCounter <= 0)
        {
            gunChangeCounter = maxGCC;
            changingGun = true;
            
            gunOrientation = rotateXTransform.InverseTransformDirection(this.transform.forward);
            gunPosition = rotateXTransform.InverseTransformPoint(this.transform.position);
            this.transform.RotateAround(rotateXTransform.transform.position, rotateXTransform.right, 60);
            if (isGun)
            {
                gun.SetActive(false);
                firegun.SetActive(true);
            }
            else
            {
                gun.SetActive(true);
                firegun.SetActive(false);
            }
            isGun = !isGun;
        }

        //決定鍵盤input的結果
        Vector3 movDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) { movDirection.z += 1; }
        if (Input.GetKey(KeyCode.S)) { movDirection.z -= 1; }
        if (Input.GetKey(KeyCode.D)) { movDirection.x += 1; }
        if (Input.GetKey(KeyCode.A)) { movDirection.x -= 1; }
        movDirection = movDirection.normalized;
        //決定要給Animator的動畫參數
        if (movDirection.magnitude == 0 || !JumpSensor.IsCanJump()) { currentSpeed = 0; }
        else
        {
            if (movDirection.z < 0) { currentSpeed = -MoveSpeed; }
            else { currentSpeed = MoveSpeed; }
        }
        animatorController.SetFloat("Speed", currentSpeed);
        //轉換成世界座標的方向
        Vector3 worldSpaceDirection = movDirection.z * rotateYTransform.transform.forward +
                                      movDirection.x * rotateYTransform.transform.right;
        Vector3 velocity = rigidBody.velocity;
        velocity.x = worldSpaceDirection.x * MoveSpeed;
        velocity.z = worldSpaceDirection.z * MoveSpeed;

        if (Input.GetKey(KeyCode.Space) && JumpSensor.IsCanJump())
        {
            velocity.y = JumpSpeed;
            onGround = false;
        }

        rigidBody.velocity = velocity;

        if (rigidBody.velocity.magnitude == 0 || !onGround)
        {

            if (walk.isPlaying) walk.Stop();
        }
        else
        {
            if (!walk.isPlaying) walk.Play();
        }

        //計算滑鼠
        rotateYTransform.transform.localEulerAngles += new Vector3(0, Input.GetAxis("Horizontal"), 0) * rotateSpeed;
        currentRotateX += Input.GetAxis("Vertical") * rotateSpeed;
        if (currentRotateX > 90)
        {
            currentRotateX = 90;
        }
        else if (currentRotateX < -90)
        {
            currentRotateX = -90;
        }
        rotateXTransform.transform.localEulerAngles = new Vector3(-currentRotateX, 0, 0);
    }
}
