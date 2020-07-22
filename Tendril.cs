#pragma warning disable 0649

using UnityEngine;
using System.Collections.Generic;

class Tendril : MonoBehaviour {
	
	[SerializeField]
	Camera cam;
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
	Vector2 prevPos;
	LinkedList<Probe> probeLinks;
	float totalSize = 0f;
	
	void Start() {
		Grow();
		int probeCount = probeLinks.Count;
		
		float mass = 1f / (float)(probeCount + 2);
		float drag = thrust * mass;
		// Rigidbody2D link = anchor;
		// for (int i = 0; i < probeCount; i++) {
		// 	Probe probe = Instantiate(probeFab, transform);
		// 	probe.Size = size;
		// 	probe.Link = link;
		// 	link = probe.body;
		// 	probe.body.mass = mass;
		// 	probe.body.gravityScale = 1f;
		// 	probe.body.drag = drag;
		// 	probes.Add(probe);
		// }
		// probe.Size = size;
		// probe.Link = link;
		// probes.Add(probe);
	}
	
	void FixedUpdate() {
		// Vector2 dir = cam.ScreenToWorldPoint(Input.mousePosition) - probe.transform.position;
		// MoveDir(dir);
		// if (Input.GetKey(KeyCode.Mouse0)) {
		// 	probe.body.bodyType = RigidbodyType2D.Static;
		// } else {
		// 	probe.body.bodyType = RigidbodyType2D.Dynamic;
		// }
	}
	
	void MoveDir(Vector2 dir) {
		// Vector2 force = dir.normalized * thrust * speed;
		// probe.body.AddForce(force);
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
