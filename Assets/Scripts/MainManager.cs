using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    private DataManager dataManager;

    public Text highScoreText;
    public Text ScoreText;

    public GameObject GameOverText; 
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    


    // Start is called before the first frame update
    void Start()
    {
        dataManager = DataManager.Instance;
        dataManager.LoadHighScore();

        SetupGame();
        
    }

    private void Update()
    {
        StartGame();
        if (m_GameOver)
        {
            Restart();
        }
        
        UpdateHighScoreText(m_Points);

        RebuildGame();
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";

        if(dataManager != null)
        {
            CheckScore(m_Points);            
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        dataManager.SaveHighScore();
    }

    public void CheckScore(int score) 
    {
            if (score > dataManager.highScore)
            {
                dataManager.SetHighScore(score);
            }
    }

    public void UpdateHighScoreText(int score)
    {
        highScoreText.text = $"Player Name: {dataManager.playerName}. High Score: {dataManager.highScore}.";        
    }

    public void StartGame()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
    }

    public void SetupGame()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }
    public void Restart()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void RebuildGame()
    {
        if (!GameObject.Find("BrickPrefab(Clone)"))
        {           
            SetupGame();         
        }
    }
}
