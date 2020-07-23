#pragma warning disable 0649

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class Rope : MonoBehaviour {
	
	internal Rigidbody2D target;
	internal Rigidbody2D body { get => self.body; }
	
	[SerializeField]
	internal RopeSeg self;
	[SerializeField]
	float speed, thrust;
	[SerializeField]
	Gradient gradient;
	[SerializeField]
	CircleCollider2D circle;
	
	AnimationCurve gripCurve;
	LinkedList<RopeSeg> segs;
	float totalSize;
	
	internal void Init(LinkedList<RopeSeg> segs, float totalSize) {
		this.segs = segs;
		this.totalSize = totalSize;
	}
	
	void FixedUpdate() {
		Vector2 dir = target.position - body.position;
		float brake = Mathf.Clamp(dir.magnitude * 2f, 0f, 1f);
		Vector2 force = dir.normalized * thrust * speed * brake;
		body.AddForce(force);
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		Rigidbody2D target = collision.collider.attachedRigidbody;
		if (target == this.target) {
			body.bodyType = RigidbodyType2D.Static;
			StartCoroutine(Flash(segs.Last, 0f));
		}
		else circle.isTrigger = true;
	}
	void OnCollisionExit2D() { circle.isTrigger = false; }
	void OnTriggerExit2D() { circle.isTrigger = false; }
	internal void Release() { body.bodyType = RigidbodyType2D.Dynamic; }
	
	IEnumerator Flash(LinkedListNode<RopeSeg> seg, float time) {
		LinkedListNode<RopeSeg> next = seg.Previous;
		seg.Value.color = gradient.Evaluate(time);
		seg.Value.order = 1;
		for (int i = 0; i < 3; i++) yield return null;
		if (next != null) StartCoroutine(Flash(next, time + seg.Value.Size / totalSize));
		for (int i = 0; i < 2; i++) yield return null;
		seg.Value.color = Color.black;
		seg.Value.order = 0;
	}
}
