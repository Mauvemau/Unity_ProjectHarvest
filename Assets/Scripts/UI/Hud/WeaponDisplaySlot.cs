using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponDisplaySlot : MonoBehaviour {
    [Header("References")] 
    [SerializeField] private Image iconReference;
    [SerializeField] private TMP_Text levelIndicatorReference;

    public void SetDisplay(WeaponDisplayContainer displayData) {
        ClearDisplay();

        if (iconReference) {
            if (displayData.icon) {
                iconReference.sprite = displayData.icon;
                iconReference.enabled = true;
            }
        }

        if (!levelIndicatorReference) return;
        levelIndicatorReference.text = (displayData.level + 1).ToString();
        if (displayData.level <= 0) return;
        levelIndicatorReference.enabled = true;
    }

    public void ClearDisplay() {
        iconReference.enabled = false;
        levelIndicatorReference.enabled = false;
    }
}
