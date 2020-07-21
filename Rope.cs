using UnityEngine;
using System.Collections.Generic;

class Rope : MonoBehaviour {
	
	[SerializeField]
	float length, radius;
	float width;
	[SerializeField]
	RopeSeg segFab;
	[SerializeField]
	RopeSeg firstSeg;
	List<RopeSeg> segs;
	
	void Start() {
		width = length / radius * 0.5f;
		segs = new List<RopeSeg>();
		segs.Add(firstSeg);
		for (int i = 1; i < width + 1; i++) {
			RopeSeg seg = Instantiate(segFab, Vector2.zero, transform.rotation);
			seg.Radius = radius;
			segs.Add(seg);
		}
	}
	
	void FixedUpdate() {
		float error = 0f;
		foreach (RopeSeg seg in segs) seg.VerletUpdate();
		for (int j = 0; j < 5; j++) {
			for (int i = 1; i < segs.Count; i++) {
				Vector2 dir = segs[i].Pos - segs[i - 1].Pos;
				float mag = dir.magnitude;
				if (mag > radius) {
					Debug.Log(mag);
					error += mag - radius;
					dir = dir.normalized * (mag - radius);
					segs[i].Pos -= dir;
				}
			}
		}
		// Debug.Log(error);
	}
	
}
