using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public enum WeatherState { Sunny, Rainy, Snowy, Stormy }

    public WeatherState currentWeather = WeatherState.Sunny;

    [Header("Lighting")]
    public Light domeLamp;
    public Light directionalLight;

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

    [Header("Scene Details")]
    public GameObject snowLayer;

    // Light colors per weather
    private Color sunnyColor = new Color(1f,   0.95f, 0.6f);
    private Color rainyColor = new Color(0.4f, 0.6f,  1f);
    private Color snowyColor = new Color(0.8f, 0.9f,  1f);
    private Color stormColor = new Color(0.5f, 0.3f,  0.8f);

    // Original plant scales
    private Vector3 cactusOriginalScale;
    private Vector3 tomatoOriginalScale;

    // Lightning
    private bool isStormy = false;
    private float lightningTimer = 0f;

    void Start()
    {
        // Save original plant sizes
        if (cactus != null) cactusOriginalScale = cactus.localScale;
        if (tomato != null) tomatoOriginalScale  = tomato.localScale;

        // Start sunny
        SetWeather(WeatherState.Sunny);
    }

    void Update()
    {
        // Keyboard shortcuts for testing
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetWeather(WeatherState.Sunny);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetWeather(WeatherState.Rainy);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetWeather(WeatherState.Snowy);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SetWeather(WeatherState.Stormy);

        // Lightning countdown during storm
        if (isStormy)
        {
            lightningTimer -= Time.deltaTime;
            if (lightningTimer <= 0f)
            {
                StartCoroutine(LightningFlash());
                // Next flash in 3 to 7 seconds randomly
                lightningTimer = Random.Range(3f, 7f);
            }
        }
    }

    public void SetWeather(WeatherState newWeather)
    {
        currentWeather = newWeather;

        // Stop all particles first
        StopAllParticles();

        switch (currentWeather)
        {
            case WeatherState.Sunny:
                isStormy = false;
                ApplyLightColor(sunnyColor, 5f);
                PlaySound(sunnySound);
                SetPlantScale(1.2f);
                SetSkybox(new Color(0.5f, 0.7f, 1f));
                if (snowLayer != null) snowLayer.SetActive(false);
                break;

            case WeatherState.Rainy:
                isStormy = false;
                ApplyLightColor(rainyColor, 3f);
                PlaySound(rainySound);
                StartParticles(rainParticles);
                SetPlantScale(1f);
                SetSkybox(new Color(0.4f, 0.4f, 0.5f));
                if (snowLayer != null) snowLayer.SetActive(false);
                break;

            case WeatherState.Snowy:
                isStormy = false;
                ApplyLightColor(snowyColor, 3.5f);
                PlaySound(snowySound);
                StartParticles(snowParticles);
                SetPlantScale(0.85f);
                SetSkybox(new Color(0.85f, 0.9f, 0.95f));
                if (snowLayer != null) snowLayer.SetActive(true);
                break;

            case WeatherState.Stormy:
                isStormy = true;
                lightningTimer = Random.Range(1f, 3f); // first flash soon
                ApplyLightColor(stormColor, 2f);
                PlaySound(stormySound);
                StartParticles(rainParticles);
                SetPlantScale(0.7f);
                SetSkybox(new Color(0.2f, 0.2f, 0.25f));
                if (snowLayer != null) snowLayer.SetActive(false);
                break;
        }

        Debug.Log("Weather: " + currentWeather);
    }

    // --- Helper functions ---

    private void ApplyLightColor(Color color, float intensity)
    {
        if (domeLamp != null)
        {
            domeLamp.color     = color;
            domeLamp.intensity = intensity;
        }
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

    private void SetSkybox(Color color)
    {
        if (RenderSettings.skybox == null) return;
        RenderSettings.skybox.SetColor("_SkyTint", color);
        RenderSettings.skybox.SetColor("_GroundColor", color * 0.5f);
        DynamicGI.UpdateEnvironment();
    }

    private System.Collections.IEnumerator LightningFlash()
    {
        if (directionalLight == null) yield break;

        // Save original values
        Color originalColor     = directionalLight.color;
        float originalIntensity = directionalLight.intensity;

        // First flash
        directionalLight.color     = Color.white;
        directionalLight.intensity = 8f;
        yield return new WaitForSeconds(0.05f);

        // Brief dark
        directionalLight.color     = originalColor;
        directionalLight.intensity = originalIntensity;
        yield return new WaitForSeconds(0.05f);

        // Second flash
        directionalLight.color     = Color.white;
        directionalLight.intensity = 6f;
        yield return new WaitForSeconds(0.03f);

        // Back to normal
        directionalLight.color     = directionalLight.color;
        directionalLight.intensity = originalIntensity;
    }
}