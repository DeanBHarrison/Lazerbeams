using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy wave config")]
public class WaveConfig : ScriptableObject
{

    public GameObject enemyPrefab;
    public GameObject pathPrefab;
    [SerializeField] float timebeytweenSpawns = 0.5f;
    [SerializeField] float spawRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemeyPrefab()
    {
        return enemyPrefab;
    }

    public List<Transform> GetWaypoints()
    {

        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }

    public float GettimebeytweenSpawns()
    {
        return timebeytweenSpawns;
    }

    public float GetspawRandomFactor()
    {
        return spawRandomFactor;
    }

    public int GetnumberOfEnemies()
    {
        return numberOfEnemies;
    }

    public float GetmoveSpeed()
    {
        return moveSpeed;
    }
}
