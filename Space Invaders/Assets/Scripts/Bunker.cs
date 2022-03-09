using UnityEngine;

public class Bunker : MonoBehaviour
{
    public GameObject Destruction;
    private int shield = 15;
    private int maxshield = 15;
    public GOD God;
    public AudioClip audioDestroy;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
            Vector3 position = new Vector3(other.transform.position.x + Random.Range(-2f,2f),
                other.transform.position.y + Random.Range(-2f, 2f), 0.01f);
            Instantiate(Destruction, position, Quaternion.Euler(0f, 0f, Random.Range(-180f, 180f))).transform.SetParent(gameObject.transform);
           

            shield--;
        audioSource.PlayOneShot(audioDestroy);

        if (shield <= 0)
        {
            
            gameObject.SetActive(false);
        }
    }

    public void ResetBunker()
    {
        shield = maxshield;
        gameObject.SetActive(true);
        foreach (Transform child in transform) Destroy(child.gameObject);
    }
    
}
