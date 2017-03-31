using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public Rigidbody2D rigidbody2D;
    public Vector3 SlingPivot;
    public float radius;
    private int LengthLimit = 5;
    private SpriteRenderer spriteRenderer;
    private Color Pressed = Color.red;
    private bool charging = false;
    public bool launched = false;

    // Use this for initialization
    public void InitAs() {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        rigidbody2D = this.GetComponent<Rigidbody2D>();

        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        this.transform.position = SlingPivot;
        rigidbody2D.simulated = false;
        CircleCollider2D circle = GetComponent<CircleCollider2D>();
        radius = circle.radius;
        //Debug.Log(SlingPivot);

    }

    // Update is called once per frame
    void Update() {
        if (!launched)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mp = Input.mousePosition;
                //Debug.Log(mp);
                //Debug.Log(SlingPivot);
                if (mp.x >= 0 && mp.y >= 0 && mp.x < Screen.width && mp.y < Screen.height)
                {
                    mp = Camera.main.ScreenToWorldPoint(mp) - new Vector3(0.0f, 0.0f, Camera.main.transform.position.z);
                    //Debug.Log(mp);
                    if (InAsteroid(ref mp) && !charging)
                    {
                        spriteRenderer.color = Pressed;
                        charging = true;
                    }
                    if (charging)
                    {
                        //Debug.Log(charging);
                        charge(ref mp);
                    }
                    else this.transform.position = SlingPivot;
                }
                else this.transform.position = SlingPivot;
            }
            else
            {
                if (charging)
                {
                    spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    charging = false;

                    if (this.transform.position != SlingPivot)
                    {
                        if (Vector3.Distance(this.transform.position, SlingPivot) < 1)
                        {
                            this.transform.position = SlingPivot;
                        }
                        else
                        {
                            launched = true;
                            rigidbody2D.velocity = Vector3.Normalize((SlingPivot - this.transform.position)) * Vector3.Distance(this.transform.position, SlingPivot) * 10;
                        }
                    }
                }
                else
                {
                    this.transform.position = SlingPivot;
                }
            }
        }
        else
        {
            if (!rigidbody2D.simulated) rigidbody2D.simulated = true;
        }
    }

    void charge(ref Vector3 mp) {
        if (Vector3.Distance(mp, SlingPivot) <= LengthLimit)
        {
            this.transform.position = mp;
        }
        else {
            float dis = LengthLimit / Vector3.Distance(mp, SlingPivot);
            this.transform.position = Vector3.Lerp(SlingPivot, mp, dis);
        }
    }

    bool InAsteroid(ref Vector3 mp) {
        float dis = Vector3.Distance(mp, SlingPivot);
        bool a = (dis < radius);
        //Debug.Log(dis);
        return a;
    }
}
