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
	
	Animator animator;
	
	void Start()
	{
		canJump = true;
		animator = GetComponent<Animator>();
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
		Debug.Log(movement.x);
		animator.SetFloat("WhyDoesntThisWork", movement.x);
		if (Input.GetKey(KeyCode.Space) && canJump)
		{
			rb.AddForce(new Vector2(0f, 20f), ForceMode2D.Impulse);
			canJump = false;
		}
		Vector3 corners[] = new Vector3[4];
		transform.GetWorldCorners(corners);
		Vector3 up = corners[1] - corners[0];
		Vector3 right = corners[3] - corners[0];
		Vector3 random = corners[0] + up * Random.value + right * Random.value;
	}
	
}
