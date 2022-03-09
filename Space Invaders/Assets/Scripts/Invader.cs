using UnityEngine;

public class Invader : MonoBehaviour
{
    public Sprite[] animationSprites;

    public float animationTime = 1f;
    public System.Action killed;

    private SpriteRenderer _spriteRenderer;

    private int _animationFrame;

    public int shield = 0;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), animationTime, animationTime);
    }

    private void AnimateSprite()
    {
        _animationFrame++;

        if (_animationFrame >= animationSprites.Length)
        {
            _animationFrame = 0;
        }

        _spriteRenderer.sprite = animationSprites[_animationFrame];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            if (shield == 1) 
            { 
                shield = 0;
                _spriteRenderer.color = Color.white;
            } else
            {
                
                gameObject.SetActive(false);
                killed.Invoke();
            }
           
        }
    }

    public void Respawn()
    {
        if (Random.Range(0,10) <= 5)
        {
            _spriteRenderer.color = Color.white;
            shield = 0;
        } else
        {
            _spriteRenderer.color = Color.grey;
            shield = 1;
        }
        
    }

}
