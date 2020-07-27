using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class movement : MonoBehaviour
{
	public CharacterController controller;
	public float speed = 0.015f;
	public float gravity = -1f;
	
	Vector3 velocity;
	
	AudioSource sound;
	Vector3 lastPosition;
	
	void Start() {
		sound = GetComponent<AudioSource>();
	}
	
	void Update()
	{
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");
		
		Vector3 move = transform.right * x + transform.forward * z;
		
		// controller.Move(move * speed);
		
		velocity.y += gravity;
		
		// controller.Move(velocity);
		
		bool moving = (transform.position != lastPosition);
		if (moving && !sound.isPlaying) {
			sound.Play();
		}
		if (!moving) {
			sound.Pause();
		}
		lastPosition = transform.position;
	}
}

private void OnCollisionEnter2D(Collider2D collider)
    {
        //If the player has the 'SingTrigger' collision active, the platform will open
        if (collider.gameObject.tag == "SingTrigger")
        {
            OpenFlower();
        }
    }
