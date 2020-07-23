#pragma warning disable 0649

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class RopeSpawner : MonoBehaviour {
	
	[SerializeField]
	int ropeCount;
	[SerializeField]
	float length, size;
	[SerializeField]
	AnimationCurve shape;
	[SerializeField]
	RopeSeg segFab;
	[SerializeField]
	Rope probeFab;
	[SerializeField]
	Rigidbody2D anchor;
	[SerializeField]
	Core core;
	
	Rope probe;
	LinkedList<RopeSeg> segs;
	float totalSize;
	
	void Start() {
		for (int i = 0; i < ropeCount; i++) {
			Grow();
			int segCount = segs.Count;
			foreach (RopeSeg seg in segs) {
				seg.body.mass = 4f * seg.Size / totalSize;
				seg.body.drag = 40f * seg.body.mass;
			}
			probe.Init(segs, totalSize);
			core.AddRope(probe);
		}
		Destroy(this);
	}
	
	void Grow() {
		totalSize = 0f;
		segs = new LinkedList<RopeSeg>();
		LinkedListNode<RopeSeg> first = Spawn(0f);
		segs.AddFirst(first);
		probe = Instantiate(probeFab);
		probe.self.Size = size * shape.Evaluate(1f);
		totalSize += probe.self.Size;
		segs.AddLast(probe.self);
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
		RopeSeg seg = Instantiate(segFab, transform);
		seg.Size = size * shape.Evaluate(time);
		totalSize += seg.Size;
		return new LinkedListNode<RopeSeg>(seg);
	}
	
	bool EvalGap(float prev, float next) {
		return (length * (next - prev) * 2f - size * (shape.Evaluate(prev) + shape.Evaluate(next)) > 0f);
	}
}
