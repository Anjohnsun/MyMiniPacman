using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIDrawer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hpField;
    [SerializeField] private TextMeshProUGUI _pointsField;

    public PlayerData PlayerData;

    public void Refresh()
    {
        _hpField.text = PlayerData.Hp.ToString();
        _pointsField.text = PlayerData.Points.ToString();
    }
}
