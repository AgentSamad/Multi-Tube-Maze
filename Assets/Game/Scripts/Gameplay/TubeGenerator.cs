using System.Collections.Generic;
using System.IO;
using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TubeGenerator : MonoBehaviour
{
    [Header("Tube Settings")] [SerializeField]
    private float scaler = 0.02f;

    [SerializeField] private int numControlPoints = 4;
    [SerializeField] private float tubeRadius;

    [Header("Asset References")] public Material meshMaterial;
    public PhysicMaterial physicMaterial;

    [Header("Childs References")] [SerializeField]
    private Transform tubePoint;

    [SerializeField] private Transform unparentObject;
    [SerializeField] private Collider ballsSpawnPoint;


    private List<Vector3> controlPoints = new List<Vector3>();


    public void GenerateTube(Level data)
    {
        var textReader = new StringReader(SVGConverter.GetSvgContent(data.svgFileName));
        var sceneInfo = SVGParser.ImportSVG(textReader);

        var svgPath = sceneInfo.Scene.Root.Children[0];
        var segments = svgPath.Shapes[0].Contours[0].Segments;

        controlPoints.Clear();


        foreach (var segment in segments)
        {
            controlPoints.Add(segment.P0 * scaler);
        }


        Mesh mesh = TubeCreator.GenerateMesh(controlPoints, tubePoint, tubeRadius);

        // Mesh mesh = TubeCreator.GenerateMesh(Bezier.GenerateBezierPath(controlPoints, (float)1 / numControlPoints),
        //     tubePoint, tubeRadius);

        if (mesh != null)
        {
            tubePoint.AddComponent<MeshFilter>().mesh = mesh;
            tubePoint.AddComponent<MeshRenderer>().material = meshMaterial;
            tubePoint.AddComponent<MeshCollider>().material = physicMaterial;

            tubePoint.localPosition = data.tubeOffset;
            GameEvents.InvokeSpawnBalls(data.levelBalls, ballsSpawnPoint.transform, ballsSpawnPoint);
            unparentObject.localPosition = mesh.bounds.max;
        }
    }
}