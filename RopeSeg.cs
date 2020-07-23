#pragma warning disable 0649
using UnityEngine;
using System.Collections.Generic;

class RopeSeg : MonoBehaviour {
	
	[SerializeField]
	internal Rigidbody2D body;
	[SerializeField]
	DistanceJoint2D joint;
	[SerializeField]
	SpriteRenderer sprite;
	float size;
	internal Vector2 Pos {
		get => body.position;
		set => body.position = value;
	}
	internal float Size {
		get => size;
		set {
			transform.localScale = new Vector3(value, value, 1f);
			joint.distance = value * 0.5f;
			size = value;
		}
	}
	internal Rigidbody2D Link { set => joint.connectedBody = value; }
	internal Color color { set => sprite.color = value; }
	internal int order { set => sprite.sortingOrder = value; }
}
