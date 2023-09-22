using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private BallsData ballsData;
    [SerializeField, Range(0f, 2f)] private float spawnDelay;

    void Awake()
    {
        GameEvents.SpawnBalls += SpawnBalls;
    }

    private void OnDisable()
    {
        GameEvents.SpawnBalls -= SpawnBalls;
    }

    void SpawnBalls(int amount, Transform parent, Collider spawnCollider)
    {
        StartCoroutine(Spawn(amount, parent, spawnCollider));
    }

    IEnumerator Spawn(int amount, Transform spawnParent, Collider spawnCollider)
    {
        GameObject ball = Instantiate(ballPrefab, GetSpawnPoint(spawnCollider), Quaternion.identity);
        ball.transform.parent = spawnParent;

        ball.GetComponent<Ball>().SetData(GetRandomColor(), ballsData.properties);

        for (int i = 1; i < amount; i++)
        {
            //Fly weight copy the already present object in the scene 
            GameObject duplicateBall = Instantiate(ballPrefab, GetSpawnPoint(spawnCollider), Quaternion.identity);
            duplicateBall.transform.parent = spawnParent;

            duplicateBall.GetComponent<Ball>().SetData(GetRandomColor(), ballsData.properties);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    Color GetRandomColor()
    {
        return ballsData.ballRandomColors[Random.Range(0, ballsData.ballRandomColors.Count)];
    }

    private Vector3 GetSpawnPoint(Collider ballsSpawnPoint)
    {
        var bounds = ballsSpawnPoint.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(x, y, z);
    }
}