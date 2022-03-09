
using UnityEngine;

public class Player : MonoBehaviour
{
    public Projectile laserPrefab;
    public float speed;
    public GOD God;
    public AudioClip audioShot;
    public AudioSource audioSource;

    private bool _laserActive;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            transform.position += Vector3.left * speed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
            transform.position += Vector3.up * speed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space)){
            Shoot();
        }
    }

    private void Shoot()
    {
        audioSource.PlayOneShot(audioShot);
        Projectile projectile = Instantiate(laserPrefab, transform.position, 
            Quaternion.identity);

        if (God.score >= 10000)
        {
            projectile = Instantiate(laserPrefab, transform.position + Vector3.right * 0.5f,
            Quaternion.identity);
            projectile = Instantiate(laserPrefab, transform.position + Vector3.left * 0.5f,
            Quaternion.identity);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Missible") ||
            other.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            God.SetLives(-1);
        }
    }
}
