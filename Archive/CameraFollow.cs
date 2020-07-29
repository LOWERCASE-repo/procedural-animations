using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	public float speed;
	public float minX;
	public float maxX;
	public float minY;
	public float maxY;
	
	// Start is called before the first frame update
	void Start()
	{
		transform.position = target.position;
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		transform.position = Vector2.Lerp(transform.position, target.position, 0.5f);
	}
	
	// 		transform.position += new Vector3(0f, 0f, -10f);
}
