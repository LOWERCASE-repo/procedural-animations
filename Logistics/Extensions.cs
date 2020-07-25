using UnityEngine;

public static class Extensions {
	
	public static Quaternion Rotation(this float angle) {
		return Quaternion.AngleAxis(angle, Vector3.forward);
	}
	
	public static Quaternion Rotate(this Quaternion rot, float rotation) {
		float angle = rot.eulerAngles.z;
		return Quaternion.Euler(0f, 0f, angle - rotation);
	}
	
	public static float Angle(this Vector2 dir) {
		return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
	}
	
	public static Vector2 Predict(this Vector2 relPos, Vector2 relVel, float speed) {
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
