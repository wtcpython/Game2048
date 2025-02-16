using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TileBoard board;

    public CanvasGroup gameOver;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;

    public List<Button> mobileButtons;

    private int score;

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        SetScore(0);
        hiscoreText.text = Loadhiscore().ToString();

        gameOver.alpha = 0f;
        gameOver.interactable = false;

        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;

        bool isMobile = Application.isMobilePlatform;

        foreach (var button in mobileButtons)
        {
            button.gameObject.SetActive(isMobile);
        }
    }

    public void GameOver()
    {
        board.enabled = false;
        gameOver.interactable = true;

        StartCoroutine(Fade(gameOver, 1f, 1f));
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapesd = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while (elapesd < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapesd / duration);
            elapesd += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int points)
    {
        SetScore(score + points);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
        SaveHiscore();
    }

    private void SaveHiscore()
    {
        int hiscore = Loadhiscore();
        if (score > hiscore)
        {
            PlayerPrefs.SetInt("hiscore", score);
        }
    }

    private int Loadhiscore()
    {
        return PlayerPrefs.GetInt("hiscore", 0);
    }

    // Mobile Move
    public void MoveLeft()
    {
        board.MoveTiles(Direction.Left);
    }

    public void MoveUp()
    {
        board.MoveTiles(Direction.Up);
    }

    public void MoveDown()
    {
        board.MoveTiles(Direction.Down);
    }

    public void MoveRight()
    {
        board.MoveTiles(Direction.Right);
    }
}
