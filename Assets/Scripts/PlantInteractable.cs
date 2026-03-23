using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using TMPro;

public class PlantInteractable : MonoBehaviour
{
    [Header("UI")]
    public GameObject weatherInfoPanel;
    public TextMeshProUGUI weatherText;

    [Header("Weather Manager")]
    public WeatherManager weatherManager;

    private XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }

        if (weatherInfoPanel != null)
            weatherInfoPanel.SetActive(false);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (weatherInfoPanel != null)
            weatherInfoPanel.SetActive(true);

        UpdateWeatherText();
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if (weatherInfoPanel != null)
            weatherInfoPanel.SetActive(false);
    }

    private void UpdateWeatherText()
    {
        if (weatherText == null || weatherManager == null) return;

        switch (weatherManager.currentWeather)
        {
            case WeatherManager.WeatherState.Sunny:
                weatherText.text = "Sunny\nWarm & clear\n22°C";
                weatherText.color = new Color(1f, 0.85f, 0f);
                break;

            case WeatherManager.WeatherState.Rainy:
                weatherText.text = "Rainy\nPrecipitation 80%\n14°C";
                weatherText.color = new Color(0.4f, 0.7f, 1f);
                break;

            case WeatherManager.WeatherState.Snowy:
                weatherText.text = "Snowy\nBelow freezing\n-5°C";
                weatherText.color = new Color(0.8f, 0.9f, 1f);
                break;

            case WeatherManager.WeatherState.Stormy:
                weatherText.text = "Stormy!\nStrong winds\n9°C";
                weatherText.color = new Color(0.8f, 0.5f, 1f);
                break;
        }
    }
}