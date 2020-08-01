#pragma warning disable 0649
using UnityEngine;
using System.Collections;

class ResourcesLoadJankAf : MonoBehaviour {
	
	Animator animator;
	
	void Start() {
		animator = GetComponent<Animator>();
		Debug.Log(Resources.Load<RuntimeAnimatorController>("Animations/GameObject"));
	}
}
