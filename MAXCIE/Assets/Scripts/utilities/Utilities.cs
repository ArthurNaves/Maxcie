using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities {
    public static Vector2 RotatePoint(Vector2 point, float degreesToRotate)
    {
        float pointMagnitude = point.magnitude;
        float newAngle;

        newAngle = Vector2.Angle(Vector2.right, point);
        if (point.y < 0) newAngle -= 360;

        newAngle += degreesToRotate;
        if (newAngle > 360) newAngle -= 360;
        else if (newAngle < 0) newAngle += 360;

        return (new Vector2(Mathf.Cos(Mathf.Deg2Rad * newAngle), Mathf.Sin(Mathf.Deg2Rad * newAngle)) * pointMagnitude);
    }
}
