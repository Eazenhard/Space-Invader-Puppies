using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction = Vector3.up;

    public float speed;
    public System.Action destroyed;

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (destroyed != null) { 
            destroyed.Invoke(); 
        }
         
        if (other.gameObject.layer == LayerMask.NameToLayer("Missible"))
        {
            GOD God = FindObjectOfType<GOD>();
            God.SetScore(500);
        }
        Destroy(gameObject);
    }
}
