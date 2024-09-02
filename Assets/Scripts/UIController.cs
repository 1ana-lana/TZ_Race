using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public event Action<bool> OnTapGas;
    public event Action<bool> OnTapBrake;

    [SerializeField]
    private PedalButton _gasPedalButton;
    [SerializeField] 
    private PedalButton _brakePedalButton;

    private void Awake()
    {
        _gasPedalButton.OnTouch += GasPedalButton_OnTouch;
        _brakePedalButton.OnTouch += BrakePedalButton_OnTouch;
    }

    private void BrakePedalButton_OnTouch(bool state)
    {
        OnTapBrake?.Invoke(state);
    }

    private void GasPedalButton_OnTouch(bool state)
    {
        OnTapGas?.Invoke(state);
    }
}
