#pragma warning disable 0649

using UnityEngine;
using System.Collections.Generic;

class Rope : MonoBehaviour {
	
	[SerializeField]
	float length, radius;
	[SerializeField]
	RopeSeg segFab, probe;
	[SerializeField]
	Rigidbody2D anchor;
	[SerializeField]
	LineRenderer rend;
	List<RopeSeg> segs = new List<RopeSeg>();
	Vector3[] segPositions;
	// [SerializeField]
	// AnimationCurve curve;
	
	void Start() {
		int segCount = (int)(length / radius * 0.5f) - 2;
		Rigidbody2D link = anchor;
		for (int i = 0; i < segCount; i++) {
			RopeSeg seg = Instantiate(segFab, transform);
			seg.Radius = radius;
			seg.Link = link;
			link = seg.body;
			segs.Add(seg);
		}
		probe.Radius = radius;
		probe.Link = link;
		segs.Add(probe);
		segPositions = new Vector3[segs.Count];
	}
	
	void Update() {
		for (int i = 0; i < segs.Count; i++) {
			segPositions[i] = segs[i].body.position;
		}
		rend.positionCount = segs.Count;
		rend.SetPositions(segPositions);
	}
}
