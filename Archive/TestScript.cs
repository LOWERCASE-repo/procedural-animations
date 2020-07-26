#pragma warning disable 0649
using UnityEngine;

class TimeTracker : MonoBehaviour {
	
	float startTime;
	
	void Start() {
		startTime = Time.time;
	}
	
	void Update() {
		float elapsedTime = Time.time - startTime;
		Debug.Log("time passed: " + elapsedTime);
	}
}
