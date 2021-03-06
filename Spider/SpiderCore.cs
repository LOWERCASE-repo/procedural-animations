#pragma warning disable 649
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

class SpiderCore : Core {
	
	Camera cam;
	[SerializeField]
	Transform face;
	[SerializeField]
	SpriteRenderer leftEye;
	[SerializeField]
	SpriteRenderer rightEye;
	Vector2 facePos { set => face.transform.localPosition = value; }
	float faceAlpha {
		set {
			Color color = leftEye.color;
			color.a = value;
			leftEye.color = color;
			rightEye.color = color;
		}
	}
	
	protected override void Awake() {
		base.Awake();
		cam = Camera.main;
	}
	
	protected override void FixedUpdate() {
		base.FixedUpdate();
		Vector2 dir = Vector2.zero;
		if (Input.GetKey(KeyCode.Mouse0)) {
			dir = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition) - body.position;
		}
		Move(dir);
		Vector2 intent = Vector2.ClampMagnitude(body.velocity / this.bonusSpeed / this.bonusSpeed, 1f);
		vision.offset = intent * vision.radius;
		facePos = new Vector2(intent.x * 0.7f, intent.y * 0.3f);
		faceAlpha = body.velocity.sqrMagnitude;
	}
}
