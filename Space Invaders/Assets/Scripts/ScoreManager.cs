using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    #region Singleton
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion
    Text scoreText;
    int currentScore, finalScore;

    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    public void addPoint(int score)
    {
        currentScore+= score;
        scoreText.text = currentScore.ToString();
    }
}
