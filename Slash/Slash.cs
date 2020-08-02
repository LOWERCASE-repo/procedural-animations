#pragma warning disable 0649
using UnityEngine;
using System.Collections;

class Slash : MonoBehaviour {
	
	[SerializeField]
	AnimationCurve smooth, bounce, jolt;
	[SerializeField]
	float time, startAng, endAng;
	Transform blade;
	Vector2 endPosition;
	float endPivot;
	
	
	void Start() {
		blade = transform.GetChild(0);
		StartCoroutine(Activate());
	}
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			StartCoroutine(Activate());
		}
	}
	
	IEnumerator Activate() {
		float i = 0f;
		for (float start = Time.fixedTime; i <= 1f; i = (Time.fixedTime - start) / time) {
			float smooth = this.smooth.Evaluate(i);
			float bounce = this.bounce.Evaluate(i);
			float jolt = this.jolt.Evaluate(i);
			transform.rotation = Mathf.Lerp(startAng, endAng, smooth).Rotation();
			blade.localScale = new Vector3(smooth, blade.localScale.y, 0f);
			blade.localPosition = new Vector3(0f, 3f + 3f * bounce, 0f);
			blade.localRotation = Mathf.Lerp(-60f, -120f, smooth).Rotation();
			yield return new WaitForFixedUpdate();
		}
	}
}
