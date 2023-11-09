using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int level = 1;
    public int score = 0;
    public int lives;

    public Ball ball { set; get; }

    public Paddle paddle { set; get; }

    public Brick[] bricks { set; get; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject); // persists across all scenes

        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        this.ball = FindObjectOfType<Ball>();
        this.paddle = FindObjectOfType<Paddle>();
        this.bricks = FindObjectsOfType<Brick>();
    }

    // Start is called before the first frame update
    void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;
        LoadLevel(1);
    }
    private void LoadLevel(int level)
    {
        this.level = level;

        if (level > 1)
            SceneManager.LoadScene("Win Screen");
        else
            SceneManager.LoadScene("Level" + this.level);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(Brick brick)
    {

        if (Cleared())
        {
            LoadLevel(this.level + 1);
        }
        this.score += brick.points;
    }
    public void Miss()
    {
        this.lives--;

        if (this.lives > 0)
        {
            ResetLevel();
        }
        else
            GameOver();
    }

    private bool Cleared()
    {
        for (int i = 0; i < this.bricks.Length; i++)
        {
            if (this.bricks[i].gameObject.activeInHierarchy)
                return false;
        }
        return true;
    }

    private void ResetLevel()
    {
        this.ball.ResetBall();
        this.paddle.ResetPaddle();
    }

    private void GameOver()
    {
        NewGame();
    }
}
