using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValueDisplay : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private TMP_Text valueText;

    private void Start()
    {
        UpdateText(scrollbar.value);
        scrollbar.onValueChanged.AddListener(UpdateText);
    }

    private void UpdateText(float value)
    {
        valueText.text = (value * 100f).ToString("0");
    }
}
