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

    public static bool GameStarted;
    // Start is called before the first frame update
    void Start()
    {
        FaultyObject.onObjectFixed += UpdateObjectCount;
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

        levelTimer -= Time.deltaTime;

        timerText.text = "TIME: " + (int)levelTimer;

        if(levelTimer <= 0)
        {
            Debug.Log("GAME OVER");
            GameStarted = false;
        }
    }

    public void UpdateObjectCount()
    {
        scoreText.text = "Unfixed Objects: " + wall.UnfixedObjectCount();
    }

    public void StartGame()
    {
        StartCoroutine(_StartGame());
    }

    IEnumerator _StartGame()
    {
        for(int i = startCountdown; i > 0; i--)
        {
            GameObject spawnedObject = Instantiate(countdownTextObject);
            spawnedObject.GetComponentInChildren<TMP_Text>().text = i.ToString();
            yield return new WaitForSeconds(1.2f);
        }

        GameStarted = true;
        yield return 0;
    }
}
