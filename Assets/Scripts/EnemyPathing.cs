using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    // this is just so we can access the folder of our other "waveconfig" class
    WaveConfig waveConfig;
    List<Transform> _waypoints;

    int _waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {

        _waypoints = waveConfig.GetWaypoints();
        transform.position = _waypoints[_waypointIndex].transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        if (_waypointIndex <= _waypoints.Count - 1)
        {
            Vector3 targetPosition = _waypoints[_waypointIndex].transform.position;
            float movementThisFrame = waveConfig.GetmoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            if (transform.position == targetPosition)
            {
                _waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
