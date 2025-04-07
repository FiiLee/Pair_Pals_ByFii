using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;
using System;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 3;
    public const int gridCols = 4;
    public const float offsetX = 2.2f;
    public const float offsetY = 3f;

    [SerializeField] private MainCard originalCard;
    [SerializeField] private Sprite[] images;

    [SerializeField] private TMP_Text scoreLabel;
    [SerializeField] private TMP_Text timerLabel;

    private MainCard _firstRevealed;
    private MainCard _secondRevealed;

    private int _score = 0;
    private int _matchesFound = 0;
    private int _totalMatches = 6;

    private float countdownTime = 60f;
    private bool _gameOver = false;

    public bool canReveal => _secondRevealed == null && !_gameOver;

    private void Start()
    {
        if (images.Length < _totalMatches)
        {
            UnityEngine.Debug.LogError("Not enough images assigned!");
            return;
        }

        Vector3 startPos = originalCard.transform.position;
        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MainCard card = (i == 0 && j == 0) ? originalCard : Instantiate(originalCard);
                int index = j * gridCols + i;
                int id = numbers[index];
                card.SetCard(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = (offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }

        UpdateScore();
        UpdateTimerUI();
    }

    private void Update()
    {
        if (!_gameOver)
        {
            countdownTime -= Time.deltaTime;
            UpdateTimerUI();

            if (countdownTime <= 0f)
            {
                countdownTime = 0f;
                EndGame(false);
            }
        }
    }

    public void CardRevealed(MainCard card)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.5f);

        if (_firstRevealed.Id == _secondRevealed.Id)
        {
            _score += 10;
            _matchesFound++;
            UpdateScore();

            CardSoundPlayer.Instance.PlayMatchSound();

            if (_matchesFound == _totalMatches)
            {
                EndGame(true);
            }
        }
        else
        {
            CardSoundPlayer.Instance.PlayMismatchSound();

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }

        _firstRevealed = null;
        _secondRevealed = null;
    }

    private void EndGame(bool win)
    {
        if (_gameOver) return;
        _gameOver = true;
        StartCoroutine(LoadEndScene(win));
    }

    private IEnumerator LoadEndScene(bool win)
    {
        yield return new WaitForSeconds(1f);
        string sceneName = win ? "WinScene" : "GameOverScene";
        SceneManager.LoadScene(sceneName);
    }

    private void UpdateScore()
    {
        if (scoreLabel != null)
            scoreLabel.text = "Score: " + _score;
    }

    private void UpdateTimerUI()
    {
        if (timerLabel != null)
            timerLabel.text = "Time : " + Mathf.CeilToInt(countdownTime) + "s";
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = (int[])numbers.Clone();
        for (int i = 0; i < newArray.Length; i++)
        {
            int r = UnityEngine.Random.Range(i, newArray.Length);
            (newArray[i], newArray[r]) = (newArray[r], newArray[i]);
        }
        return newArray;
    }
}