using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScore : MonoBehaviour {
    Text text;
    string levelScore;


    private void Start()
    {
        text = GetComponent<Text>();
        levelScore = (" Score : " + LevelManager.levelNumber);
        text.text = levelScore.ToString();
    }


}
