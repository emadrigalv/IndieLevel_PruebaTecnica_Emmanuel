using UnityEngine;
using UnityEngine.UI;

public class UpdateSlider : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Slider slider;

    public void UpdateSliderUI(float value) => slider.value = value;
}
