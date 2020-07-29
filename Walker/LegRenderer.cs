#pragma warning disable 0649
using UnityEngine;

class LegRenderer : MonoBehaviour {
	
	[SerializeField]
	Rigidbody2D core, seg;
	Rigidbody2D body;
	LineRenderer line;
	
	void Awake() {
		body = GetComponent<Rigidbody2D>();
		line = GetComponent<LineRenderer>();
	}
	
	void Update() {
		Vector3[] positions = new Vector3[] {
			core.position, seg.position, body.position
		};
		line.SetPositions(positions);
	}
}
