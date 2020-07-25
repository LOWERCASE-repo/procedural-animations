#pragma warning disable 0649
using UnityEngine;

class TestScript : MonoBehaviour {
	public GameObject laser;
	
	void Start() {
		Vector2 direction = new Vector2(1f, 1f);
		// Vector2 direction = target.position - transform.position;
		// float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
		// Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		// Instantiate(laser, transform.position, rotation);
		
	}
}
