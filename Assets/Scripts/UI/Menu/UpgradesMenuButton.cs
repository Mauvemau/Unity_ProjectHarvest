using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradesMenuButton : MonoBehaviour {
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemNameAndLevelText;
    [SerializeField] private TMP_Text itemDescriptionText;

    public void SetDisplay(WeaponDisplayContainer displayData) {
        itemIcon.sprite = displayData.icon;
        itemNameAndLevelText.text = displayData.weaponName + (displayData.level > 0 ? " (Level " + (displayData.level + 1) + ")" : "");
        itemDescriptionText.text = displayData.description;
    }
}

