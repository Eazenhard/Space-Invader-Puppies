using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        GOD God = FindObjectOfType<GOD>();
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            God.SetScore(800);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            God.SetScore(-2400);
        }
        Destroy(gameObject);
    }
}
