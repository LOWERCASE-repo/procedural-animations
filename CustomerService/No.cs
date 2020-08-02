using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class No : MonoBehaviour
{

    public float speed;
    public float jumpPower;
    public bool canJump;

    private Vector2 moveDirection;

    public Rigidbody2D rb;


    void Start()
    {
        canJump = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // if (other.gameObject.tag == "ground")
        // {
            canJump = true;
        // }
    }


    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;

        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            rb.AddForce(new Vector2(0f, 20f), ForceMode2D.Impulse);
            canJump = false;
        }
    }

}
