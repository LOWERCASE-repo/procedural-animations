#pragma warning disable 0649
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Gradient), typeof(TrailRenderer))]
class SpiderProbe : Probe2 {
	
	[SerializeField]
	Gradient gradient;
	TrailRenderer trail;
	
	LinkedList<RopeSeg> segs;
	float segTime;
	float size;
	float Scale {
		get => transform.localScale.x;
		set => transform.localScale = new Vector3(value, value, 1f);
	}
	
	internal void Init(LinkedList<RopeSeg> segs, Rigidbody2D target) {
		this.segs = segs;
		this.target = target;
		size = Scale;
		segTime = 1f / (float)segs.Count;
		body.drag = speed / body.mass;
		trail = GetComponent<TrailRenderer>();
		trail.startWidth = Scale;
		trail.time = body.mass / Scale / speed;
	}
	
	protected override void Link() {
		StartCoroutine(Flash(segs.Last, 0f));
		Scale = size * 1.2f;
	}
	
	internal override void Unlink() {
		base.Unlink();
		Scale = size;
	}
	
	IEnumerator Flash(LinkedListNode<RopeSeg> seg, float time) {
		LinkedListNode<RopeSeg> next = seg.Previous;
		seg.Value.color = gradient.Evaluate(time);
		seg.Value.order = 1;
		for (int i = 0; i < 2; i++) yield return null;
		if (next != null) StartCoroutine(Flash(next, time + segTime));
		for (int i = 0; i < 2; i++) yield return null;
		seg.Value.color = Color.black;
		seg.Value.order = 0;
	}
}
