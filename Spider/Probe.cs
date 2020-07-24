#pragma warning disable 0649
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class Probe : MonoBehaviour {
	
	internal Rigidbody2D target;
	internal Rigidbody2D body { get => self.body; }
	
	[SerializeField]
	internal RopeSeg self;
	[SerializeField]
	float speed;
	[SerializeField]
	Gradient gradient;
	[SerializeField]
	CircleCollider2D circle;
	[SerializeField]
	TrailRenderer trail;
	
	AnimationCurve gripCurve;
	LinkedList<RopeSeg> segs;
	float totalSize;
	float Scale {
		get => transform.localScale.x;
		set => transform.localScale = new Vector3(value, value, 1f);
	}
	
	internal void Init(LinkedList<RopeSeg> segs, float totalSize, Rigidbody2D target) {
		this.segs = segs;
		this.totalSize = totalSize;
		this.target = target;
		body.drag = speed / body.mass;
		trail.startWidth = self.Size;
		trail.time = body.mass / self.Size / speed;
	}
	
	void FixedUpdate() {
		Vector2 dir = target.position - body.position;
		float brake = Mathf.Clamp(dir.magnitude * 2f, 0f, 1f);
		Vector2 force = dir.normalized * speed * speed * brake;
		body.AddForce(force);
		float size = self.Size;
		if (body.bodyType == RigidbodyType2D.Static) Scale = size * 1.2f;
		else Scale = size;
	}
	
	// not called on static, so performance is fine
	void OnTriggerStay2D(Collider2D other) {
		Rigidbody2D target = other.attachedRigidbody;
		if (target == this.target) {
			transform.position = other.ClosestPoint(body.position);
			StartCoroutine(Flash(segs.Last, 0f));
			body.bodyType = RigidbodyType2D.Static;
		} else body.bodyType = RigidbodyType2D.Dynamic;
	}
	
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
