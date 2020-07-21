using UnityEngine;
using System.Collections.Generic;

class RopeSeg : MonoBehaviour {
	
	[SerializeField]
	Rigidbody2D body;
	[SerializeField]
	CircleCollider2D collider;
	internal Vector2 Pos { set => body.position = value; }
	internal float Size { set => collider.radius = value; }
}
