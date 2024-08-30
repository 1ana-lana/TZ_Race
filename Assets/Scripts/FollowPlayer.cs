using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private Vector3 _offset = new Vector3(0, 5, -7);

    void LateUpdate()
    {
        transform.position = _player.transform.position + _offset;
    }
}
