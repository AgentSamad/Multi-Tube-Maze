using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Balls Data", menuName = "Game/Balls Data")]
public class BallsData : ScriptableObject
{
    public RigidBodyProperties properties;
    public List<Color> ballRandomColors;
}

[System.Serializable]
public class RigidBodyProperties
{
    public RigidbodyInterpolation interpolation;
    public RigidbodyConstraints constraints;
    public CollisionDetectionMode collisionDetectionMode;
    public float drag;
    public float angularDrag;
}