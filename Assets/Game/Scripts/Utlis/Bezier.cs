using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bezier
{
   // private const float stepSize = 0.01f; // Adjust the step size as needed

    public static List<Vector3> GenerateBezierPath(List<Vector3> svgPoints, float stepSize)
    {
        List<Vector3> bezierPoints = new List<Vector3>();

        // Calculate the Bezier path points and add them to meshPoints
        for (float t = 0; t <= 1; t += stepSize)
        {
            Vector3 pointOnBezierPath = CalculateBezierPoint(t, svgPoints);
            bezierPoints.Add(pointOnBezierPath);
        }

        return bezierPoints;
    }


    private static Vector3 CalculateBezierPoint(float t, List<Vector3> points)
    {
        int n = points.Count - 1;
        Vector3 result = Vector3.zero;
        for (int i = 0; i <= n; i++)
        {
            float coefficient = BinomialCoefficient(n, i) * Mathf.Pow(t, i) * Mathf.Pow(1 - t, n - i);
            result += coefficient * points[i];
        }

        return result;
    }


    private static int BinomialCoefficient(int n, int k)
    {
        int result = 1;
        if (k > n - k)
        {
            k = n - k; // Take advantage of symmetry
        }

        for (int i = 0; i < k; ++i)
        {
            result *= (n - i);
            result /= (i + 1);
        }

        return result;
    }
}