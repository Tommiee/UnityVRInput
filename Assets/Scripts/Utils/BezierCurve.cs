using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BezierCurve {
	public static Vector3 BezierPathCalculation(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
		float tt = t * t;
		float ttt = t * tt;
		float u = 1.0f - t;
		float uu = u * u;
		float uuu = u * uu;

		Vector3 B = new Vector3();
		B = uuu * p0;
		B += 3.0f * uu * t * p1;
		B += 3.0f * u * tt * p2;
		B += ttt * p3;

		return B;
	}
}