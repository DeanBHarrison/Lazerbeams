using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherthing)
    {
        Destroy(otherthing.gameObject);
    }
}
