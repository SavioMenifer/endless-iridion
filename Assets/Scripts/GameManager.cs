using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text gameOverScore;
    public GameObject gameOverText;
    public GameObject TunnelParent;
    public float score;

    public int difficulty = 0;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("IncreaseDifficulty", 5f, 25f); // increase difficulty every 25s
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            TunnelParent.transform.Translate(
                -TunnelParent.transform.forward * Time.deltaTime,
                Space.World
            );

            score += Time.deltaTime * 10;
            scoreText.text = score.ToString("F0");
        }
        else
        {
            if (Input.GetKeyDown("space"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // reload scene to restart game
            }
        }
    }

    void IncreaseDifficulty()
    {
        difficulty += 1;
    }

    public void OrbCollected()
    {
        score += 100 + difficulty * 10;
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverScore.text = "Your score: " + score.ToString("F0");
        gameOverText.SetActive(true);
    }
}
