using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Control Configuration", menuName = "Game/Control Config")]
public class ControlConfig : ScriptableObject
{
    [Range(10f, 200f)] public float rotationSpeed;
    [Range(0.01f, 0.1f)] public float minimumThresholdInput;
}