using UnityEngine;
using UnityEngine.UI;

public class WeatherSwitcherUI : MonoBehaviour
{
    public WeatherManager weatherManager;

    public Button sunnyButton;
    public Button rainyButton;
    public Button snowyButton;
    public Button stormyButton;

    void Start()
    {
        sunnyButton.onClick.AddListener(() =>
            weatherManager.SetWeather(WeatherManager.WeatherState.Sunny));

        rainyButton.onClick.AddListener(() =>
            weatherManager.SetWeather(WeatherManager.WeatherState.Rainy));

        snowyButton.onClick.AddListener(() =>
            weatherManager.SetWeather(WeatherManager.WeatherState.Snowy));

        stormyButton.onClick.AddListener(() =>
            weatherManager.SetWeather(WeatherManager.WeatherState.Stormy));
    }
}