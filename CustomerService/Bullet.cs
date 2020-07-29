using UnityEngine;

class Bullet : MonoBehaviour {
	void Start() {
		GetComponent<Rigidbody2D>().velocity = transform.up * 10f;
	}
}
