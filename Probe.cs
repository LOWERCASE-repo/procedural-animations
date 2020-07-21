using UnityEngine;
using System.Collections;

class Probe : MonoBehaviour {
	
	[SerializeField]
	Motor motor;
	[SerializeField]
	Camera cam;
	
	void FixedUpdate() {
		Vector2 dir = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		motor.MoveDir(dir);
	}
}
