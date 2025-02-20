using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    public Image cutsceneImage;
    public TMP_Text cutsceneText;
    public Button nextButton;
    public List<Sprite> cutsceneFrames;
    public List<string> cutsceneWriting;
    private int currentIndex;

    void Start()
    {
        SwitchSprite();
        SwitchText();
    }

    public void OnNextButtonClick()
    {
        TurnPage();
    }

    private void TurnPage()
    {
        if (currentIndex < (cutsceneFrames.Count - 1))
        {
            currentIndex++;
            SwitchSprite();
            SwitchText();
        } else {
            AudioInterface.Instance.StopThemeMusic();
            SceneManager.LoadScene(2);
        }
    }

    private void SwitchSprite()
    {
        cutsceneImage.sprite = cutsceneFrames[currentIndex];
    }

    private void SwitchText()
    {
        cutsceneText.text = cutsceneWriting[currentIndex];
    }
}
