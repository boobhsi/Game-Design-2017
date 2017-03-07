using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOController : MonoBehaviour {

    public float forceValue;

    private Rigidbody2D rigidbody2D = null;

    public ScoreManager scoremanager;

    // Use this for initialization

    void Start()
    {

        rigidbody2D = this.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame

    void Update()
    {

        Vector2 force2D = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {

            force2D.y += forceValue;

        }

        if (Input.GetKey(KeyCode.S))
        {

            force2D.y -= forceValue;

        }

        if (Input.GetKey(KeyCode.A))
        {

            force2D.x -= forceValue;

        }

        if (Input.GetKey(KeyCode.D))
        {

            force2D.x += forceValue;

        }

        rigidbody2D.AddForce(force2D);

    }

    void OnTriggerEnter2D(Collider2D other)

    {

        scoremanager.AddScore(100);

        other.gameObject.SetActive(false);

    }

}