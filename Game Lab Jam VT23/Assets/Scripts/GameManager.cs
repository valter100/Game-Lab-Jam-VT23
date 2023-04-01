using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Wall wall;
    [SerializeField] TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        FaultyObject.onObjectFixed += UpdateObjectCount;
    }

    private void OnDestroy()
    {
        FaultyObject.onObjectFixed -= UpdateObjectCount;
    }
    public void UpdateObjectCount()
    {
        scoreText.text = "Unfixed Objects: " + wall.UnfixedObjectCount();
    }
}
