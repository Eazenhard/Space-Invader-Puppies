using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryShip : MonoBehaviour
{
    public Vector3 initialPosition;
    private float timeFly = 15f;
    private float speed = 5f;
    public GOD God;
    Color color;

    private bool OnFly = false;
    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Awake()
    {
        color = transform.GetComponent<SpriteRenderer>().color;
    }
    public void ResetMysterShip()
    {
        transform.position = initialPosition;
        
        OnFly = false;
        timeFly = Random.Range(10, 15);

        
        color.a = 0f;
        transform.GetComponent<SpriteRenderer>().color = color;
    }

    private void Update()
    {
        if (timeFly <= 0)
        {
            ResetMysterShip();
            gameObject.SetActive(true);
            OnFly = true;
        }

        transform.position += Vector3.right * speed * Time.deltaTime;

        if (OnFly)
        {
            appearance(+1);
            
        }
        else
        {
            timeFly -= Time.deltaTime;
            appearance(-1);
        }

        if (transform.position.x >= 13)
        {
            OnFly = false;
        }



    }

    private void appearance(int minus)
    {
        color.a += minus * (((255 / 5) * Time.deltaTime) / 180);
        transform.GetComponent<SpriteRenderer>().color = color;
    }

    private void MysteryShipKilled()
    {
        God.SetScore(5000);
        gameObject.SetActive(false);
        OnFly = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            MysteryShipKilled();
        }
    }

}
