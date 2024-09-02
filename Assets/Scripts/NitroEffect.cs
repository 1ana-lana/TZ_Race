using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NitroEffect : BonusEffect
{
    [SerializeField]
    private float _nitroSpeed = 30f;
    public float NitroSpeed { get { return _nitroSpeed; } private set { _nitroSpeed = value; } }
}
