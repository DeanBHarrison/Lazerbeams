using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bluelaser : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D shit)
    {
        if (shit.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }else
            if (shit.gameObject.tag == "Elaser")
        {
            Destroy(shit.gameObject);
        }
    }
}
