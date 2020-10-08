using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIHandler : MonoBehaviour
{
    internal static GameUIHandler instance = null;

    [SerializeField] private TextMeshProUGUI scoreTxt = null;
    [SerializeField] private GameObject gamePlayUI = null;
    [SerializeField] private GameObject gameMainMenuUI = null;
    [SerializeField] private GameObject gameOverUI = null;
    [SerializeField] private TextMeshProUGUI highScoreDisplay = null;
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private TextMeshProUGUI txtMusicOnOff = null;

    internal int gameScore = 0;

    private void Awake()
    {

        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("Music", 1);
            print(PlayerPrefs.GetInt("Music"));
            PlayerPrefs.SetInt("HighScore", 0);
        }
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("Music") == 1)
        {
            audioSource.enabled = true;
            txtMusicOnOff.SetText("Music-On");
        }
        else if(PlayerPrefs.GetInt("Music") == 0)
        {
            audioSource.enabled = false;
            txtMusicOnOff.SetText("Music-Off");
        }
        highScoreDisplay.SetText("HighScore: "+PlayerPrefs.GetInt("HighScore").ToString());
    }

    private void Update()
    {
        scoreTxt.SetText(gameScore.ToString());
    }

    public void PlayBtn()
    {
        gamePlayUI.SetActive(true);
        gameMainMenuUI.SetActive(false);
        PlayerController.instance.isGameStarted = true;
        PlayerController.instance.animator.SetTrigger("Run");
    }

    public void RetryBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MusicBtn(TextMeshProUGUI txt)
    {
        if (PlayerPrefs.GetInt("Music") == 1)
        {
            txt.SetText("Music Off");
            PlayerPrefs.SetInt("Music", 0);
            audioSource.enabled = false;
        }
        else
        {
            txt.SetText("Music On");
            PlayerPrefs.SetInt("Music", 1);
            audioSource.enabled = true;
        }
    }

    internal void GameOver()
    {
        gamePlayUI.SetActive(false);
        gameOverUI.SetActive(true);

        if (gameScore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", gameScore);
        }

    }
}
