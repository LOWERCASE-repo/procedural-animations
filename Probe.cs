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
	float speed, thrust;
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
	}
	
	void FixedUpdate() {
		Vector2 dir = target.position - body.position;
		float brake = Mathf.Clamp(dir.magnitude * 2f, 0f, 1f);
		Vector2 force = dir.normalized * thrust * speed * brake;
		body.AddForce(force);
		float size = self.Size;
		if (body.bodyType == RigidbodyType2D.Static) Scale = size * 1.2f;
		else Scale = size;
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		Rigidbody2D target = collider.attachedRigidbody;
		if (target == this.target) {
			transform.position = collider.ClosestPoint(body.position);
			body.bodyType = RigidbodyType2D.Static;
			StartCoroutine(Flash(segs.Last, 0f));
		}
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
