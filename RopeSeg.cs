#pragma warning disable 0649

using UnityEngine;
using System.Collections.Generic;

class RopeSeg : MonoBehaviour {
	
	[SerializeField]
	internal Rigidbody2D body;
	[SerializeField]
	DistanceJoint2D joint;
	
	internal Vector2 Pos {
		get => body.position;
		set => body.position = value;
	}
	internal float Size {
		get => transform.localScale.x;
		set {
			transform.localScale = new Vector3(value, value, 1f);
			joint.distance = value * 0.5f;
		}
	}
	internal Rigidbody2D Link { set => joint.connectedBody = value; }
}
