#pragma warning disable 0649
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

class WalkerCore : Core {
	
	Camera cam;
	[SerializeField]
	Probe[] probes;
	
	protected override void Awake() {
		base.Awake();
		cam = Camera.main;
		foreach (Probe probe in probes) {
			AddProbe(probe);
		}
	}
	
	protected override void FixedUpdate() {
		base.FixedUpdate();
		Vector2 dir = Vector2.zero;
		if (Input.GetKey(KeyCode.Mouse0)) {
			dir = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition) - body.position;
		}
		dir.y = 0f;
		Move(dir);
		Vector2 intent = Vector2.ClampMagnitude(body.velocity / this.bonusSpeed / this.bonusSpeed, 1f);
		vision.offset = intent * vision.radius;
	}
}
