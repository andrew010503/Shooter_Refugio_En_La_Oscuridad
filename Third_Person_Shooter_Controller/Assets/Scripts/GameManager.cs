
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
    public float timeRemaining = 300f;
    public Text scoreText;
    public Text timerText;
    public Slider healthBar;
    public PlayerController player;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;
        timerText.text = "Tiempo: " + Mathf.Round(timeRemaining);
        scoreText.text = "Puntuación: " + score;

        if (timeRemaining <= 0 || healthBar.value <= 0)
        {
            Debug.Log("Fin del juego");
        }
    }

    public void AddScore(int points)
    {
        score += points;
    }
}
