using UnityEngine;
using System.Collections.Generic;

class Rope : MonoBehaviour {
	
	[SerializeField]
	float length, radius;
	int segCount;
	[SerializeField]
	RopeSeg segFab, firstSeg, lastSeg;
	[SerializeField]
	LineRenderer rend;
	List<RopeSeg> segs;
	// anim curve for bezier gen too
	
	void Start() {
		int segCount = (int)(length / radius * 0.5f) - 2;
		segs = new List<RopeSeg>();
		segs.Add(firstSeg);
		for (int i = 0; i < segCount; i++) {
			RopeSeg seg = Instantiate(segFab, Vector2.zero, transform.rotation);
			seg.Radius = radius;
			segs.Add(seg);
		}
		segs.Add(lastSeg);
		this.segCount = segCount + 2;
	}
	
	void FixedUpdate() {
		for (int i = 1; i < segCount - 1; i++) {
			segs[i].VerletUpdate();
		}
		float margin = Mathf.Sqrt(segCount);
		Debug.Log("eantu" + margin);
		float count = 0;
		float error;
		do {
			count++;
			error = 0;
			for (int i = 1; i < segCount; i++) {
				Vector2 dir = segs[i].Pos - segs[i - 1].Pos;
				float mag = dir.magnitude;
				if (mag > radius) {
					error += mag - radius;
					dir = dir.normalized * (mag - radius);
					float bias = (float)(i - 1) / (float)(segs.Count - 2);
					segs[i].Pos -= dir * (1f - bias);
					segs[i - 1].Pos += dir * bias;
					// segs[i].Pos -= dir * 0.5f;
					// segs[i - 1].Pos += dir * 0.5f;
				}
			}
		} while (error > margin && count < 10);
		if (count > 5) Debug.Log(count);
	}
	
}
