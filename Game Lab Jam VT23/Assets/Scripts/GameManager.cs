using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Wall wall;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text timerText;
    [SerializeField] float levelTimer;
    [SerializeField] int startCountdown;
    [SerializeField] GameObject countdownTextObject;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject winCanvas;
    [SerializeField] AudioClip levelMusic;
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] AudioClip winSound;

    FollowCursor followCursor;

    public static bool GameStarted;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<MouseMover>().SetActive(false);

        if (timerText != null)
        {
            timerText.text = "TIME: " + (int)levelTimer;
        }
        FaultyObject.onObjectFixed += UpdateObjectCount;

        try
        {
            FindObjectOfType<MusicManager>().GetMusic(levelMusic);
        }
        catch(Exception e)
        {

        }

        StartGame();
    }

    private void OnDestroy()
    {
        FaultyObject.onObjectFixed -= UpdateObjectCount;
    }

    private void Update()
    {
        if (!GameStarted)
            return;

        if (timerText != null)
        {
            levelTimer -= Time.deltaTime;
            timerText.text = "TIME: " + (int)levelTimer;
        }

        if (levelTimer <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        FindObjectOfType<MouseMover>().SetActive(true);
        followCursor.gameObject.SetActive(true);
        GameStarted = false;

        FindObjectOfType<AudioSource>().PlayOneShot(gameOverSound);
    }

    public void UpdateObjectCount()
    {
        scoreText.text = "Unfixed Objects: " + wall.UnfixedObjectCount();

        if (wall.UnfixedObjectCount() <= 0)
        {
            winCanvas.SetActive(true);
            FindObjectOfType<MouseMover>().SetActive(true);
            followCursor.gameObject.SetActive(true);
            FindObjectOfType<AudioSource>().PlayOneShot(winSound);
            GameStarted = false;
        }
    }

    public void StartGame()
    {
        StartCoroutine(_StartGame());
    }

    IEnumerator _StartGame()
    {
        for (int i = startCountdown; i > 0; i--)
        {
            GameObject spawnedObject = Instantiate(countdownTextObject);
            spawnedObject.GetComponentInChildren<TMP_Text>().text = i.ToString();
            yield return new WaitForSeconds(1.2f);
        }

        GameStarted = true;
        yield return 0;
    }

    public void SetFollowCursor(FollowCursor newCursor)
    {
        followCursor = newCursor;
        newCursor.gameObject.SetActive(false);
    }
}
