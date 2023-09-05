using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UpdatePlaceHolderUI : MonoBehaviour
{ 
    [SerializeField] private TMP_Text bulletsRemainingText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private PlayerShootingController playerShootingController;
    [SerializeField] private Health playerHealth;

    private const string BulletsRemainingText = "Bullets Remaining: ";

    private void Awake()
    {
        bulletsRemainingText.text = BulletsRemainingText + playerShootingController.CurrentBullets.ToString(CultureInfo.CurrentCulture);
        hpText.text = "HP: " + playerHealth.CurrentHealth.ToString(CultureInfo.CurrentCulture);
    }

    private void Update()
    {
        bulletsRemainingText.text = BulletsRemainingText + playerShootingController.CurrentBullets.ToString(CultureInfo.CurrentCulture);
        hpText.text = "HP: " + playerHealth.CurrentHealth.ToString(CultureInfo.CurrentCulture);
    }
}
