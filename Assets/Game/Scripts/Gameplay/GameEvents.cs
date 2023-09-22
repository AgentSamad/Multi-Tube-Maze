using System;
using UnityEngine;

public static class GameEvents
{
    public static Action<int, Transform, Collider> SpawnBalls;

    public static Action<bool> SetControl;

    public static void InvokeSpawnBalls(int amount, Transform point, Collider spawnCollider)
    {
        SpawnBalls?.Invoke(amount, point, spawnCollider);
    }

    public static void InvokeSetControl(bool canControl)
    {
        SetControl?.Invoke(canControl);
    }
}