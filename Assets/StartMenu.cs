using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Slider size;
    [SerializeField] private Slider startAlivePercentage;
    [SerializeField] private Slider timeBetweenGenerations;
    [SerializeField] private Toggle startPaused;

    [SerializeField] private TMP_Text sizeText;
    [SerializeField] private TMP_Text startAlivePercentageText;
    [SerializeField] private TMP_Text timeBetweenGenerationsText;

    private void Start()
    {
        if (PlayerPrefs.HasKey("size"))
            size.value = (float)PlayerPrefs.GetInt("size") / 200;
        else
            OnSizeValueChange();

        if (PlayerPrefs.HasKey("startAlivePercentage"))
            startAlivePercentage.value = (float)PlayerPrefs.GetInt("startAlivePercentage") / 100;
        else
            OnStartAlivePercentageChange();

        if (PlayerPrefs.HasKey("timeBetweenGenerations"))
            timeBetweenGenerations.value = PlayerPrefs.GetFloat("timeBetweenGenerations");
        else
            OnTimeBetweenGenerationsChange();

        if (PlayerPrefs.HasKey("startPaused"))
        {
            if (PlayerPrefs.GetInt("startPaused") == 1)
                startPaused.isOn = true;
            else
                startPaused.isOn = false;
        }
        else
            OnStartPausedChanged();
    }

    public void OnSizeValueChange()
    {
        int value = Mathf.Clamp((int)Mathf.Floor(size.value * 200), 10, 200);
        
        PlayerPrefs.SetInt("size", value);

        sizeText.text = "Size: " + value + "^2";
    }

    public void OnStartAlivePercentageChange()
    {
        int value = (int)Mathf.Floor(startAlivePercentage.value * 100);

        PlayerPrefs.SetInt("startAlivePercentage", value);

        startAlivePercentageText.text = value + "% Starts Alive";
    }

    public void OnTimeBetweenGenerationsChange()
    {
        float value = (float)Math.Round(timeBetweenGenerations.value, 2);
        value = Mathf.Clamp(value, 0.01f, 1);

        PlayerPrefs.SetFloat("timeBetweenGenerations", value);

        timeBetweenGenerationsText.text = value + "s Between Generations";
    }

    public void OnStartPausedChanged()
    {
        if (startPaused.isOn)
            PlayerPrefs.SetInt("startPaused", 1);
        else
            PlayerPrefs.SetInt("startPaused", 0);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
