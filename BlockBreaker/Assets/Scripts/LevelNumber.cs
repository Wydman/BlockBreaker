using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelNumber : MonoBehaviour {
    Text text;
    string levelnumber;


    private void Start()
    {
        text = GetComponent<Text>();
        levelnumber = (" Level " + LevelManager.levelNumber);
        text.text = levelnumber.ToString();
    }


    public void IncrementationLevelNumber()
    {
        LevelManager.levelNumber++;
    }



}
