using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GpsValueUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lat;
    [SerializeField] private TextMeshProUGUI _lng;
    [SerializeField] private TextMeshProUGUI _refreshCount;

    private void Update() 
    {
        Refresh(_refreshCount, GameManager.Instance.Gps.RefreshCount);
        Refresh(_lat, GameManager.Instance.Gps.Pos.Lat);
        Refresh(_lng, GameManager.Instance.Gps.Pos.Lng);
    }

    private void Refresh(TextMeshProUGUI target, double posValue)
    {
        target.text = posValue.ToString();
    }

    private void Refresh(TextMeshProUGUI target, int posValue)
    {
        target.text = posValue.ToString();
    }
}
