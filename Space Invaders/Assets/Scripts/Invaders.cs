using UnityEngine.SceneManagement;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    [Header("Grid")]
    public int rows = 5;
    public int columns = 11;

    [Header("Invaders")]
    
    public Invader[] prefabs = new Invader[5];
    public AnimationCurve speed = new AnimationCurve();
    private Vector3 _direction = Vector2.right;
    public Vector3 initialPosition { get; private set; }

    public int amountKilled;
    public int amountAlive => totalInvaders - amountKilled;
    public int totalInvaders;
    public float percentKilled => (float)amountKilled / (float)totalInvaders;
    public float randomSpawn;

    [Header("Missiles")]
    public Projectile missilePrefab;
    public float missileAttackRate = 0.5f;
    public AudioClip audioShot;
    public AudioClip[] audioBonus;
    private AudioSource audioSource;

    public GOD God;
    public GameObject bonus;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        initialPosition = transform.position;
        for (int i = 0; i < rows; i++)
        {
            float width = 2f * (columns - 1);
            float height = 2f * (rows - 1);
            Vector3 centering = new Vector3(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3 (centering.x, centering.y + (i * 2f), 0f);

            for (int j = 0; j < columns; j++)
            {
                Invader invader = Instantiate(prefabs[i], transform);
                invader.killed += InvaderKilled;
                Vector3 position = rowPosition;
                position.x += j * 2f;
                invader.transform.localPosition = position;
            }
        }
        randomSpawn = Random.Range(10, 15);
        totalInvaders = rows * columns;
    }

    private void Start()
    {
        InvokeRepeating(nameof(MissileAttake), missileAttackRate, missileAttackRate);    
    }

    private void Update()
    {
        
        transform.position += _direction * speed.Evaluate(percentKilled) * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy){
                continue;
            }

            if (_direction == Vector3.right && invader.position.x >= (rightEdge.x - 1f)){
                AdvanceRow();
            } else if (_direction == Vector3.left && invader.position.x <= (leftEdge.x + 1f)){
                AdvanceRow();
            }
        }
        randomSpawn -= Time.deltaTime;
        if (randomSpawn <= 0)
        {
            int ran = Random.Range(0, rows * columns - 1);
            if (!transform.GetChild(ran).gameObject.activeInHierarchy)
            {
                totalInvaders++;
                transform.GetChild(ran).gameObject.SetActive(true);
                transform.GetChild(ran).gameObject.GetComponent<Invader>().Respawn();

                randomSpawn = Random.Range(10, 15);
            }
        }
    }

    private void AdvanceRow()
    {
        _direction.x *= -1f;

        Vector3 position = transform.position;
        position.y -= 1f;
        transform.position = position;
    }

    private void MissileAttake()
    {
        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (Random.value < (1f / (float)amountAlive))
            {
                
                audioSource.PlayOneShot(audioShot);
                Instantiate(missilePrefab, invader.position, Quaternion.identity);
                if (God.score >= 5000)
                {
                    Instantiate(missilePrefab, invader.position + Vector3.right * Random.Range(-2f,2f),
                    Quaternion.identity);
                    Instantiate(missilePrefab, invader.position + Vector3.left * Random.Range(-2f, 2f),
                    Quaternion.identity);
                }
                break;
            }
        }

    }

    private void InvaderKilled()
    {
        amountKilled++;
        God.SetScore(100);
        if (Random.Range(0f, 10f) <= 4.5f)
        {
            if (Random.Range(0, 2) == 1)
            {
                audioSource.PlayOneShot(audioBonus[0]);
            } else audioSource.PlayOneShot(audioBonus[1]);

            GameObject projectile = Instantiate(bonus, new Vector3(Random.Range(-10f, 10f), 0f, 0f), Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().AddTorque(Random.Range(2f, 360f));
            
        }
    }

    public void ResetInvaders()
    {
        amountKilled = 0;
        _direction = Vector3.right;
        transform.position = initialPosition;
        randomSpawn = Random.Range(10, 15);

        foreach (Transform invader in transform)
        {
            
            invader.gameObject.SetActive(true);
            invader.gameObject.GetComponent<Invader>().Respawn();
        }
    }
}
