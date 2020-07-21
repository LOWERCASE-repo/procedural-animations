using UnityEngine;

internal static class Extensions {
	
	internal static Quaternion Rot(this float angle) {
		return Quaternion.AngleAxis(angle, Vector3.forward);
	}
	
	internal static Quaternion Rotate(this Quaternion rot, float rotation) {
		float angle = rot.eulerAngles.z;
		return Quaternion.Euler(0f, 0f, angle - rotation);
	}
	
	// Quaternion.LookRotation(Vector3.forward, dir);
	/*
	// Quaternion target = (-45f).Rot() * Quaternion.LookRotation(Vector3.forward, dir);
	float d = (dir.x - transform.up.x) * (-transform.up.y) - (dir.y - transform.up.y) * (-transform.up.x);
	*/
	
	internal static Vector2 Predict(this Vector2 relPos, Vector2 relVel, float speed) {
		float a = speed * speed - relVel.sqrMagnitude;
		float b = Vector2.Dot(relPos, relVel);
		float det = b * b + a * relPos.sqrMagnitude;
		if (det < 0f) return relPos;
		det = Mathf.Sqrt(det);
		float timeA = b - det;
		float timeB = b + det;
		if (timeA > 0f) return relPos + timeA * relVel / a;
		if (timeB > 0f) return relPos + timeB * relVel / a;
		return relPos;
	}
}
