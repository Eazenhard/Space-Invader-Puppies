using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comet : MonoBehaviour
{
    float speed;
    float startposition;
    float minusx;
    float minusy;
    float health;
    float death = 5f;
    float alpha = 0f;
    float raz;
    public Sprite[] dog;
    Color color;

    void Start()
    {
        raz = Random.Range(0.2f, 1f);
        speed = Random.Range(0.5f, 30f);

        transform.localScale = new Vector3(raz, raz, 1f);
        if (Random.Range(0f, 10f) <= 2.5f)
        {
            transform.GetComponent<SpriteRenderer>().sprite = dog[Random.Range(0,dog.Length)];
        }
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(2f, 360f));

        Vector3 position = transform.position;
        position.y -= Random.Range(-25f, 25f);
        position.x -= Random.Range(-25f, 25f);
        position.z = 91;
        transform.position = position;
        startposition = transform.position.y;
        minusx = -1 * Random.Range(-1, 2);
        minusy = -1 * Random.Range(-1, 2);

        health = Random.Range(10f, 25f);
        color = transform.GetComponent<SpriteRenderer>().color;
        color.a = 0f;
        transform.GetComponent<SpriteRenderer>().color = color;

    }

    
    void Update()
    {
        transform.position = new Vector3(transform.position.x - Time.deltaTime * minusx,
                transform.position.y - Time.deltaTime * speed * minusy,
                transform.position.z);

        if (health <= 0) {
            death -= Time.deltaTime;
            appearance(-1);
            if (death <= 0)
            {
                Destroy(this.gameObject);
            }
        } 
        else if (death <= 0)
            {
                health -= Time.deltaTime;
                
                if (health <= 0)
                {
                    death = 5;
                }
            }
            else
            {
                death -= Time.deltaTime;
            appearance(1);
        }
        
    }
    private void appearance(int minus)
    {
        color.a += minus * (((125 / 5) * Time.deltaTime) / 180);
        transform.GetComponent<SpriteRenderer>().color = color;
    }
}
