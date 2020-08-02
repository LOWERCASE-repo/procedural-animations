#pragma warning disable 0649
using UnityEngine;

class What : MonoBehaviour {
	
	public RectTransform circle;
	
	public float speed = 50f;
	
	
	void Update() {
		circle.Rotate(new Vector3(0, 0, -1) * speed * Time.deltaTime);
	}
}
