#pragma warning disable 0649

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class Rope : MonoBehaviour {
	
	internal Rigidbody2D target;
	internal Rigidbody2D body { get => self.body; }
	
	[SerializeField]
	float speed, thrust;
	[SerializeField]
	float length, size;
	[SerializeField]
	AnimationCurve shape;
	[SerializeField]
	Gradient gradient;
	[SerializeField]
	RopeSeg segFab, self;
	[SerializeField]
	Rigidbody2D anchor;
	[SerializeField]
	CircleCollider2D circle;
	
	AnimationCurve gripCurve;
	LinkedList<RopeSeg> segs;
	float totalSize = 0f;
	
	void Start() {
		Grow();
		int segCount = segs.Count;
		foreach (RopeSeg seg in segs) { // TODO physicss
			seg.body.mass = 4f * seg.Size / totalSize;
			seg.body.drag = 4f * thrust * seg.body.mass;
		}
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
	
	void Grow() {
		segs = new LinkedList<RopeSeg>();
		LinkedListNode<RopeSeg> first = Spawn(0f);
		segs.AddFirst(first);
		self.Size = size * shape.Evaluate(1f);
		totalSize += self.Size;
		segs.AddLast(self);
		GrowRec(first, 0f, 1f);
		Rigidbody2D link = this.anchor;
		foreach (RopeSeg seg in segs) {
			seg.Link = link;
			link = seg.body;
		}
	}
	
	void GrowRec(LinkedListNode<RopeSeg> prev, float prevTime, float nextTime) {
		float midTime = 0.5f * (prevTime + nextTime);
		LinkedListNode<RopeSeg> mid = Spawn(midTime);
		segs.AddAfter(prev, mid);
		if (EvalGap(prevTime, midTime)) GrowRec(prev, prevTime, midTime);
		if (EvalGap(midTime, nextTime)) GrowRec(mid, midTime, nextTime);
	}
	
	LinkedListNode<RopeSeg> Spawn(float time) {
		RopeSeg seg = Instantiate(segFab, transform.parent);
		seg.Size = size * shape.Evaluate(time);
		totalSize += seg.Size;
		return new LinkedListNode<RopeSeg>(seg);
	}
	
	bool EvalGap(float prev, float next) {
		return (length * (next - prev) * 2f - size * (shape.Evaluate(prev) + shape.Evaluate(next)) > 0f);
	}
}
