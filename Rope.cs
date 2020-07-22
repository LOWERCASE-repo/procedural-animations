#pragma warning disable 0649

using UnityEngine;
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
	RopeSeg segFab, self;
	[SerializeField]
	Rigidbody2D anchor;
	LinkedList<RopeSeg> ropeSegs;
	float totalSize = 0f;
	
	void Start() {
		Grow();
		int probeCount = ropeSegs.Count;
		foreach (RopeSeg probe in ropeSegs) {
			probe.body.mass = 4f * probe.Size / totalSize;
			probe.body.drag = 4f * thrust * probe.body.mass;
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
		if (target == this.target) body.bodyType = RigidbodyType2D.Static;
	}
	
	internal void Release() {
		body.bodyType = RigidbodyType2D.Dynamic;
	}
	
	void Grow() {
		ropeSegs = new LinkedList<RopeSeg>();
		LinkedListNode<RopeSeg> first = Spawn(0f);
		ropeSegs.AddFirst(first);
		self.Size = size * shape.Evaluate(1f);
		totalSize += self.Size;
		ropeSegs.AddLast(self);
		GrowRec(first, 0f, 1f);
		Rigidbody2D link = this.anchor;
		foreach (RopeSeg probe in ropeSegs) {
			probe.Link = link;
			link = probe.body;
		}
	}
	
	void GrowRec(LinkedListNode<RopeSeg> prev, float prevTime, float nextTime) {
		float midTime = 0.5f * (prevTime + nextTime);
		LinkedListNode<RopeSeg> mid = Spawn(midTime);
		ropeSegs.AddAfter(prev, mid);
		if (EvalGap(prevTime, midTime)) GrowRec(prev, prevTime, midTime);
		if (EvalGap(midTime, nextTime)) GrowRec(mid, midTime, nextTime);
	}
	
	LinkedListNode<RopeSeg> Spawn(float time) {
		RopeSeg probe = Instantiate(segFab, transform.parent);
		probe.Size = size * shape.Evaluate(time);
		totalSize += probe.Size;
		return new LinkedListNode<RopeSeg>(probe);
	}
	
	bool EvalGap(float prev, float next) {
		return (length * (next - prev) * 2f - size * (shape.Evaluate(prev) + shape.Evaluate(next)) > 0f);
	}
}
