using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Joystick joystick;
    public GameObject spawnPoint;
    Vector2 sensitivity;
    public float speed;
    public float jumpForce;
    bool grounded;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sensitivity = new Vector2(0.1f, 0.1f);
    }

    private void Move()
    {
        if (grounded)
        {
            if (joystick.Horizontal > sensitivity.x)
            {
                //move right
                rb.AddForce(Vector2.right * speed * Time.deltaTime);
                GetComponent<SpriteRenderer>().flipX = false;
                GetComponent<Animator>().SetInteger("state", 1);
            }
            else if (joystick.Horizontal < -sensitivity.x)
            {
                //move left
                rb.AddForce(Vector2.left * speed * Time.deltaTime);
                GetComponent<SpriteRenderer>().flipX = true;
                GetComponent<Animator>().SetInteger("state", 1);
            }
            else if (joystick.Vertical > sensitivity.y)
            {
                // jump
                rb.AddForce(Vector2.up * jumpForce * Time.deltaTime);
                GetComponent<Animator>().SetInteger("state", 2);
            }
            else
            {
                // idle
                GetComponent<Animator>().SetInteger("state", 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        grounded = true;
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        grounded = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //respawn
        if(other.tag == "death")
        {
            transform.position = spawnPoint.transform.position;
        }
    }
}
