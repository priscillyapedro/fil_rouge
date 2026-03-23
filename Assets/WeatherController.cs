using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public GameObject rainFX;
    public GameObject snowFX;
    public GameObject sunFX;
    
    // Ajoute ceci pour lier tes plantes dans l'Inspecteur
    public PlantReaction[] toutesLesPlantes; 

    public void SetWeather(string weather)
    {
        Debug.Log("Météo activée : " + weather);

        // Activation des visuels
        rainFX.SetActive(weather == "Rain");
        snowFX.SetActive(weather == "Snow");
        sunFX.SetActive(weather == "Sun");

        // Faire réagir les plantes (Boucle sur ton tableau de plantes)
        foreach (PlantReaction plante in toutesLesPlantes)
        {
            if (weather == "Rain") plante.OnRain();
            if (weather == "Sun") plante.OnSun();
        }
    }
}