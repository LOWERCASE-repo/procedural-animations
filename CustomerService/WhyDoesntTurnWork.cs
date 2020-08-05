using UnityEngine;

class WhyDoesntTurnWork : MonoBehaviour {
	
	Camera cam;
	
	void Awake() {
		cam = Camera.main;
	}
	
	void Update() {
		Debug.Log(Input.mousePosition);
		Vector3 dir = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
		transform.rotation = Quaternion.Euler(0f, 0f, ang);
	}
}
