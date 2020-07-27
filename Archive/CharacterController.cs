using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour {
	public float speed;
	public float jumpForce;
	private float moveInput;
	
	private Rigidbody2D rb;
	private Animator animator;
	private Collider2D capsule;
	
	private bool facingRight = true;
	public bool isGrounded;
	
	private int extraJumps;
	public int extraJumpsValue;
	
	private void Start() {
		extraJumps = extraJumpsValue;
		rb = GetComponent<Rigidbody2D>();
		// animator = GetComponent<Animator>();
		capsule = GetComponent<CapsuleCollider2D>();
	}
	
	private void FixedUpdate() {
		
		moveInput = Input.GetAxisRaw("Horizontal");
		rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
		if(facingRight == false && moveInput > 0) {
			Flip();
		} else if(facingRight == true && moveInput < 0) {
			Flip();
		}
	}
	
	private void Update() {
		// animator.SetFloat("Speed", Mathf.Abs(moveInput));
		
		if (isGrounded == true)
		{
			extraJumps = extraJumpsValue;
		}
		
		if(Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0) {
			rb.velocity = Vector2.up * jumpForce;
			extraJumps--;
			// animator.SetBool("isJumping", true);
		} else if(Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded == true) {
			rb.velocity = Vector2.up * jumpForce;
			// animator.SetBool("isJumping", true);
		}
	}
	
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 Scaler = transform.localScale;
		Scaler.x *= -1;
		transform.localScale = Scaler;
	}
}
