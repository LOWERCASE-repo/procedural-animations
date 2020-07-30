using UnityEngine;
using UnityEngine.UI;
using System;

class TextToInt : MonoBehaviour {
	
	Text text;
	
	void Start() {
		text = GetComponent<Text>();
		// text.text = "1025";
	}
	
	void Update() {
		try {
			int input = Int32.Parse(text.text);
			Debug.Log(input);
		} catch (Exception) {
			Debug.Log("text needs to be an int");
		}
	}
}
