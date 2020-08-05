using UnityEngine.Events;
using UnityEngine;

public class WhatAreBirdsWeJustDontKnow : MonoBehaviour {
	public float speed;
	public float jumpForce;
	private float moveInput;
	
	private Rigidbody2D rb;
	// public Animator animator;
	//private CircleCollider2D cc;
	
	private bool facingRight = true;
	public bool isGrounded;
	
	private int extraJumps;
	public int extraJumpsValue;
	
	private void Start(){
		extraJumps = extraJumpsValue;
		rb = GetComponent<Rigidbody2D>();
		isGrounded = true;
		/*cc = GetComponent<CircleCollider2D>();
		cc.enabled = false;*/
		
	}
	
	private void FixedUpdate(){
		
		moveInput = Input.GetAxisRaw("Horizontal");
		rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
		if (facingRight == false && moveInput > 0) {
			Flip();
		}
		else if (facingRight == true && moveInput < 0) {
			Flip();
		}
	}
	
	private void Update(){
		// animator.SetFloat("Speed", Mathf.Abs(moveInput));
		//When the player lands, their amount of jumps will be reset
		if (isGrounded == true) {
			extraJumps = extraJumpsValue;
		}
		
		if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0) //if the player jumps and they have jumps remaining:
	 {
			//force the player up, subtract a jump, and play the jump animation
			rb.velocity = Vector2.up * jumpForce;
			extraJumps--;
			// animator.SetBool("isJumping", true);
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded == true) //if the player jumps when grounded:
	 {
			//move the player up without subtracting a jump, then play the jump animation
			rb.velocity = Vector2.up * jumpForce;
			// animator.SetBool("isJumping", true);
		}
		
		if (Input.GetKeyDown(KeyCode.DownArrow)) //when the player presses the down arrow:
	 {
			//play the crouch animation
			// animator.SetBool("isCrouching", true);
			} else if (Input.GetKeyUp(KeyCode.DownArrow)) //when the player releases the down arrow key:
		 {
				//stop playing the crouch animation
				// animator.SetBool("isCrouching", false);
			}
		}
		
		void Flip() {
			facingRight = !facingRight;
			Vector3 Scaler = transform.localScale;
			Scaler.x *= -1;
			transform.localScale = Scaler;
		}
	}
