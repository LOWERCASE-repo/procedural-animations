#pragma warning disable 0649
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class Probe2 : Mover {
	
	[SerializeField]
	CircleCollider2D circle;
	internal RopeSeg self;
	internal Rigidbody2D target;
	
	protected override void Awake() {
		base.Awake();
		self = GetComponent<RopeSeg>();
	}
	
	protected void FixedUpdate() {
		Move(target.position - body.position);
	}
	
	void OnTriggerStay2D(Collider2D other) {
		if (other.attachedRigidbody == target) {
			transform.position = other.ClosestPoint(body.position);
			body.bodyType = RigidbodyType2D.Static;
			Link();
		}
		else body.bodyType = RigidbodyType2D.Dynamic;
	}
	
	protected virtual void Link() {}
	internal virtual void Unlink() {
		body.bodyType = RigidbodyType2D.Dynamic;
	}
}
