#pragma warning disable 649
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class SpiderSpawner : MonoBehaviour {
	
	[SerializeField]
	int probeCount;
	[SerializeField]
	float length, size;
	[SerializeField]
	AnimationCurve shape;
	[SerializeField]
	RopeSeg segFab;
	[SerializeField]
	SpiderProbe probeFab;
	Rigidbody2D anchor;
	SpiderCore core;
	
	SpiderProbe probe;
	LinkedList<RopeSeg> segs;
	
	void Awake() {
		anchor = GetComponent<Rigidbody2D>();
		core = transform.parent.GetComponent<SpiderCore>();
	}
	
	void Start() {
		for (int i = 0; i < probeCount; i++) {
			Grow();
			probe.Init(segs, anchor);
			core.AddProbe(probe);
		}
		Destroy(this);
	}
	
	void Grow() {
		segs = new LinkedList<RopeSeg>();
		LinkedListNode<RopeSeg> first = Spawn(0f);
		segs.AddFirst(first);
		probe = Instantiate(probeFab, transform);
		probe.self.Size = size * shape.Evaluate(1f);
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
		return new LinkedListNode<RopeSeg>(seg);
	}
	
	bool EvalGap(float prev, float next) {
		return (length * (next - prev) * 2f - size * (shape.Evaluate(prev) + shape.Evaluate(next)) > 0f);
	}
}
