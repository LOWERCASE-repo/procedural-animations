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
	
	// [SerializeField]
	// AnimationCurve curve;
	
	void Start() {
		int segCount = (int)(length / radius * 0.5f) - 1;
		Rigidbody2D link = anchor;
		for (int i = 0; i < segCount; i++) {
			RopeSeg seg = Instantiate(segFab, transform);
			seg.Radius = radius;
			seg.Link = link;
			link = seg.body;
		}
	}
}
