using UnityEngine;

class Bullet : MonoBehaviour {
	void Start() {
		Instantiate(gameObject);
	}
}

// GetComponent<Rigidbody2D>().velocity = transform.up * 10f;
