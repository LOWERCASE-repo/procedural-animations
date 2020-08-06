using UnityEngine;

class MouseSpawn : MonoBehaviour {
	
	[SerializeField]
	GameObject spawn;
	Camera cam;
	
	void Awake() {
		cam = Camera.main;
	}
	
	void Update() {
		Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			Instantiate(spawn, pos, Quaternion.identity);
		}
	}
}
