using TMPro;
using UnityEngine;
using Unity.Netcode;
using System.Collections;

public class ScoreUI : MonoBehaviour
{
    [Header("Score UI Elements")]
    [SerializeField] private TextMeshProUGUI leftText;
    [SerializeField] private TextMeshProUGUI rightText;
    [SerializeField] private TextMeshProUGUI winText;

    [Header("Game Settings")]
    [SerializeField] private int winningScore = 5; // configurable winning score

    private void OnEnable()
    {
        StartCoroutine(WaitForGameManager());
    }

    private IEnumerator WaitForGameManager()
    {
        // Wait until GameManager exists and is spawned
        while (GameManager.Instance == null || !GameManager.Instance.IsSpawned)
            yield return null;

        winText.gameObject.SetActive(false);

        // Subscribe to changes
        GameManager.Instance.LeftScore.OnValueChanged += OnScoreChanged;
        GameManager.Instance.RightScore.OnValueChanged += OnScoreChanged;
        GameManager.Instance.GameOver.OnValueChanged += OnGameOverChanged;

        UpdateScore();
    }

    private void OnScoreChanged(int previous, int current)
    {
        UpdateScore();
    }

    private void OnGameOverChanged(bool previous, bool current)
    {
        if (!current) return;

        winText.gameObject.SetActive(true);

        bool leftWon = GameManager.Instance.LeftScore.Value >= winningScore;
        bool rightWon = GameManager.Instance.RightScore.Value >= winningScore;

        PaddleController[] paddles = FindObjectsOfType<PaddleController>();

        foreach (var paddle in paddles)
        {
            if (paddle.IsOwner)
            {
                bool iAmLeft = paddle.transform.position.x < 0;

                if ((iAmLeft && leftWon) || (!iAmLeft && rightWon))
                    winText.text = "YOU WIN!";
                else
                    winText.text = "YOU LOSE!";

                break;
            }
        }
    }

    private void UpdateScore()
    {
        if (GameManager.Instance == null) return;

        leftText.text = GameManager.Instance.LeftScore.Value.ToString();
        rightText.text = GameManager.Instance.RightScore.Value.ToString();
    }

    private void OnDisable()
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.LeftScore.OnValueChanged -= OnScoreChanged;
        GameManager.Instance.RightScore.OnValueChanged -= OnScoreChanged;
        GameManager.Instance.GameOver.OnValueChanged -= OnGameOverChanged;
    }
}