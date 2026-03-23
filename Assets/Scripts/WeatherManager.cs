using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public enum WeatherState { Sunny, Rainy, Snowy, Stormy }

    public WeatherState currentWeather = WeatherState.Sunny;

    [Header("Lighting")]
    public Light domeLamp;

    [Header("Particles")]
    public ParticleSystem rainParticles;
    public ParticleSystem snowParticles;

    [Header("Sounds")]
    public AudioSource weatherAudioSource;
    public AudioClip sunnySound;
    public AudioClip rainySound;
    public AudioClip snowySound;
    public AudioClip stormySound;

    [Header("Plants")]
    public Transform cactus;
    public Transform tomato;

    // Original scales so we can animate back
    private Vector3 cactusOriginalScale;
    private Vector3 tomatoOriginalScale;

    // Light colors
    private Color sunnyColor = new Color(1f,   0.95f, 0.6f);
    private Color rainyColor = new Color(0.4f, 0.6f,  1f);
    private Color snowyColor = new Color(0.8f, 0.9f,  1f);
    private Color stormColor = new Color(0.5f, 0.3f,  0.8f);

    void Start()
    {
        // Save the original plant sizes
        if (cactus != null) cactusOriginalScale = cactus.localScale;
        if (tomato != null) tomatoOriginalScale  = tomato.localScale;

        // Start with sunny weather
        SetWeather(WeatherState.Sunny);
    }

    void Update()
    {
        // Keyboard shortcuts for testing
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetWeather(WeatherState.Sunny);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetWeather(WeatherState.Rainy);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetWeather(WeatherState.Snowy);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SetWeather(WeatherState.Stormy);
    }

    public void SetWeather(WeatherState newWeather)
    {
        currentWeather = newWeather;

        // Stop all particles first
        StopAllParticles();

        switch (currentWeather)
        {
            case WeatherState.Sunny:
                ApplyLightColor(sunnyColor, 5f);
                PlaySound(sunnySound);
                // Plants grow bigger (happy)
                SetPlantScale(1.2f);
                break;

            case WeatherState.Rainy:
                ApplyLightColor(rainyColor, 3f);
                PlaySound(rainySound);
                StartParticles(rainParticles);
                // Plants droop slightly (normal)
                SetPlantScale(1f);
                break;

            case WeatherState.Snowy:
                ApplyLightColor(snowyColor, 3.5f);
                PlaySound(snowySound);
                StartParticles(snowParticles);
                // Plants shrink (cold)
                SetPlantScale(0.85f);
                break;

            case WeatherState.Stormy:
                ApplyLightColor(stormColor, 2f);
                PlaySound(stormySound);
                StartParticles(rainParticles); // heavy rain
                // Plants shrink a lot (scared!)
                SetPlantScale(0.7f);
                break;
        }

        Debug.Log("Weather: " + currentWeather);
    }

    // --- Helper functions ---

    private void ApplyLightColor(Color color, float intensity)
    {
        if (domeLamp == null) return;
        domeLamp.color     = color;
        domeLamp.intensity = intensity;
    }

    private void StopAllParticles()
    {
        if (rainParticles != null) rainParticles.Stop();
        if (snowParticles != null) snowParticles.Stop();
    }

    private void StartParticles(ParticleSystem ps)
    {
        if (ps != null) ps.Play();
    }

    private void PlaySound(AudioClip clip)
    {
        if (weatherAudioSource == null || clip == null) return;
        weatherAudioSource.clip = clip;
        weatherAudioSource.Play();
    }

    private void SetPlantScale(float multiplier)
    {
        if (cactus != null)
            cactus.localScale = cactusOriginalScale * multiplier;
        if (tomato != null)
            tomato.localScale = tomatoOriginalScale * multiplier;
    }
}