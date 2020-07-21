using UnityEngine;
using System.Collections.Generic;

class RopeSeg : MonoBehaviour {
	
	[SerializeField]
	internal Rigidbody2D body;
	[SerializeField]
	CircleCollider2D collider;
	[SerializeField]
	DistanceJoint2D joint;
	
	internal Vector2 Pos {
		get => body.position;
		set => body.position = value;
	}
	internal float Radius {
		set {
			collider.radius = value;
			joint.distance = value * 2f; // set sep for curve
		}
	}
	internal Rigidbody2D Link { set => joint.connectedBody = value; }
}
