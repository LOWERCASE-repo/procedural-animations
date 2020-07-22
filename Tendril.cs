#pragma warning disable 0649

using UnityEngine;
using System.Collections.Generic;

class Tendril : MonoBehaviour {
	
	[SerializeField]
	Camera cam;
	internal Vector2 target = Vector2.zero;
	internal bool attached = false;
	
	[SerializeField]
	float speed, thrust;
	[SerializeField]
	float length, size;
	[SerializeField]
	AnimationCurve shape;
	[SerializeField]
	Probe probeFab;
	[SerializeField]
	Rigidbody2D anchor;
	Rigidbody2D tip;
	LinkedList<Probe> probeLinks;
	float totalSize = 0f;
	
	void Start() {
		Grow();
		int probeCount = probeLinks.Count;
		Debug.Log(totalSize);
		foreach (Probe probe in probeLinks) {
			probe.body.mass = 4f * probe.Size / totalSize;
			probe.body.drag = 4f * thrust * probe.body.mass;
		}
		tip = probeLinks.Last.Value.body;
	}
	
	void FixedUpdate() {
		// target = cam.ScreenToWorldPoint(Input.mousePosition);
		Vector2 dir = target - tip.position;
		Vector2 force = dir.normalized * thrust * speed;
		tip.AddForce(force);
	}
	
	void OnCollisionEnter2D() {
		tip.bodyType = RigidbodyType2D.Static;
		attached = true;
	}
	
	internal void Release() {
		tip.bodyType = RigidbodyType2D.Dynamic;
		attached = false;
	}
	
	List<Probe> Grow() {
		probeLinks = new LinkedList<Probe>();
		LinkedListNode<Probe> first = Spawn(0f);
		probeLinks.AddFirst(first);
		probeLinks.AddLast(Spawn(1f));
		GrowRec(first, 0f, 1f);
		Rigidbody2D link = this.anchor;
		foreach (Probe probe in probeLinks) {
			probe.Link = link;
			link = probe.body;
		}
		return new List<Probe>(probeLinks);
	}
	
	void GrowRec(LinkedListNode<Probe> prev, float prevTime, float nextTime) {
		float midTime = 0.5f * (prevTime + nextTime);
		LinkedListNode<Probe> mid = Spawn(midTime);
		probeLinks.AddAfter(prev, mid);
		if (EvalGap(prevTime, midTime)) GrowRec(prev, prevTime, midTime);
		if (EvalGap(midTime, nextTime)) GrowRec(mid, midTime, nextTime);
	}
	
	LinkedListNode<Probe> Spawn(float time) {
		Probe probe = Instantiate(probeFab, transform);
		probe.Size = size * shape.Evaluate(time);
		totalSize += probe.Size;
		return new LinkedListNode<Probe>(probe);
	}
	
	bool EvalGap(float prev, float next) {
		return (length * (next - prev) * 2f - size * (shape.Evaluate(prev) + shape.Evaluate(next)) > 0f);
	}
}
