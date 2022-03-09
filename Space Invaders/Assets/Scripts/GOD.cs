using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GOD : MonoBehaviour
{
    public Text scoreText;
    public Text scoreMaxText;
    public Text scoreText_Win;
    public Text scoreText_Lose;
    public Text livesText;
    public GameObject gameOverUI;
    public GameObject gameWinUI;
    public GameObject gameStartUI;
    public GameObject CometParents;

    public Comet Comet;
    public Player player;
    public Invaders invaders;
    public MysteryShip mysteryShip;
    public Bunker[] bunkers;

    bool Continue;
    bool lose = false;
    bool button = true;

    public int score { get; private set; }
    public int scoreMax { get; private set; }
    public int lives { get; private set; }
    private void Start()
    {
        SetScore(0);
        lives = 0;
        SetLives(1);
        livesText.gameObject.SetActive(false);
        gameStartUI.SetActive(true);
        
    }

    public void NewGame()
    {
        
        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);
        NewRound();
    }

    public void SetScore(int score)
    {
        this.score += score;
        if (this.score < 0) { this.score = 0; }
        if (scoreMax < this.score) 
        { 
            scoreMax = this.score; 
            scoreMaxText.text = this.score.ToString().PadLeft(4, '0'); 
        }
        scoreText.text = this.score.ToString().PadLeft(4, '0');
    }
    public void SetLives(int lives)
    {
        this.lives += lives;
        if (this.lives > 4) { this.lives = 4; }
        livesText.text = this.lives.ToString();
    }
    public int col;
    public int colBunker;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && lose && button)
        {
            lose = false;
            button = false;
            score = 0;
            SetScore(0);
            
            NewGame();
            Continue = true;
            invaders.transform.position = new Vector3(0, 7, 0);
            gameWinUI.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.C) && !lose && button)
        {
            button = false;
            Continue = true;
            NewGame();
            SetLives(1);
            livesText.gameObject.SetActive(true);
            gameWinUI.SetActive(false);
            gameStartUI.SetActive(false);
            invaders.transform.position = new Vector3(0,7,0);
        }

        if (lives <= 0)
        {
            GameOver();
        }
        if (Random.Range(0,100) <= 2) 
            Instantiate(Comet, CometParents.transform.position, 
                Quaternion.identity).transform.SetParent(CometParents.transform);
        
        if (Continue)
        {
            col = 0;
            foreach (Transform invader in invaders.gameObject.transform)
            {

                if (!invader.gameObject.activeInHierarchy)
                {
                    col++;
                }
            }
            if (col == 55)
            {
                GameWin();
            }

            colBunker = 0;
            for (int i = 0; i < bunkers.Length; i++)
                    if (!bunkers[i].gameObject.activeInHierarchy)
                    {
                        colBunker++;
                    }

            if (colBunker == 4)
            {
                GameOver();
            }
        }
        
    }

    private void NewRound()
    {
        invaders.gameObject.SetActive(true);
        invaders.ResetInvaders();
        
        mysteryShip.gameObject.SetActive(true);
        mysteryShip.ResetMysterShip();

        if (!Continue)
        {
            lives = 0;
            SetLives(2);
            for (int i = 0; i < bunkers.Length; i++)
            {
                bunkers[i].ResetBunker();
            }
        }
        Respawn();
    }

    private void Respawn()
    {
        Vector3 position = player.transform.position;
        position.x = 0f;
        position.y = -13f;
        player.transform.position = position;
        player.gameObject.SetActive(true);
        
    }

    private void GameOver()
    {
        button = true;
        Continue = false;
        lose = true;
        scoreText.text = this.score.ToString().PadLeft(4, '0');
        scoreText_Lose.text = this.score.ToString().PadLeft(4, '0');
        gameOverUI.SetActive(true);
        invaders.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
        mysteryShip.gameObject.SetActive(false);
    }

    public void GameWin()
    {
        button = true;
        Continue = false;
        scoreText.text = this.score.ToString().PadLeft(4, '0');
        scoreText_Win.text = this.score.ToString().PadLeft(4, '0');
        gameWinUI.SetActive(true);
        invaders.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
        mysteryShip.gameObject.SetActive(false);
    }

   

}
