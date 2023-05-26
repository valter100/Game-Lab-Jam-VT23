using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] Image cutsceneImage;
    [SerializeField] TMP_Text cutsceneText;
    [SerializeField] TMP_Text tipText;
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    [SerializeField] List<KeyCode> forwardKeys = new List<KeyCode>();
    [SerializeField] List<KeyCode> backwardKeys = new List<KeyCode>();
    [SerializeField] List<string> subtitles = new List<string>();
    int index;

    private void Start()
    {
        index = 0;
        cutsceneImage.sprite = sprites[index];
        cutsceneText.text = subtitles[index];
    }

    // Update is called once per frame
    void Update()
    {
        foreach(KeyCode key in backwardKeys)
        {
            if (Input.GetKeyDown(key))
            {
                index--;
                ChangeSlide();
            }

            break;
        }

        foreach(KeyCode key in forwardKeys)
        {
            if (Input.GetKeyDown(key))
            {
                index++;

                if(index == sprites.Count)
                {
                    GoToMenu();
                    return;
                }

                ChangeSlide();

                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToMenu();
        }

    }

    public void ChangeSlide()
    {
        if (index == 1)
            tipText.gameObject.SetActive(false);
        else if (index == 0)
            tipText.gameObject.SetActive(true);

        cutsceneImage.sprite = sprites[index];
        cutsceneText.text = subtitles[index];
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
