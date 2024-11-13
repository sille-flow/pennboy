using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {
    public Slider sensitivitySlider;
    public Toggle screenTintEnabled;

    private void Start() {
        // Load saved sensitivity from PlayerPrefs or default to 1.0 if not set
        float savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1.0f);
        sensitivitySlider.value = savedSensitivity;
        bool isTintEnabled = PlayerPrefs.GetInt("ScreenTintEnabled", 1) == 1;
        screenTintEnabled.isOn = isTintEnabled;
    }
}