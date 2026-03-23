using UnityEngine;

public class CubeWeatherSelector : MonoBehaviour
{
    public WeatherController controller;
    private string lastDetectedWeather = "";

    void Update()
    {
        // On récupère la direction locale "Haut" du cube dans le monde réel
        Vector3 cubeUp = transform.up; 
        string detected = "";

        // On teste les 6 faces possibles pour éviter les angles morts
        if (Vector3.Dot(cubeUp, Vector3.up) > 0.5f) detected = "Sun";
        else if (Vector3.Dot(cubeUp, Vector3.down) > 0.5f) detected = "Storm"; // Optionnel
        else if (Vector3.Dot(cubeUp, Vector3.forward) > 0.5f) detected = "Rain";
        else if (Vector3.Dot(cubeUp, Vector3.back) > 0.5f) detected = "Wind"; // Optionnel
        else if (Vector3.Dot(cubeUp, Vector3.right) > 0.5f) detected = "Snow";
        else if (Vector3.Dot(cubeUp, Vector3.left) > 0.5f) detected = "Cloudy";

        // IMPORTANT : On ne change la météo QUE si la face a changé
        if (detected != "" && detected != lastDetectedWeather)
        {
            lastDetectedWeather = detected;
            controller.SetWeather(detected);
        }
    }
}