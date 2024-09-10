using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private float _offset = 5;

    void LateUpdate()
    {
        if (_player != null)
        {
            transform.position = new Vector3(transform.position.x, _player.transform.position.y + _offset, transform.position.z);
        }
    }
}
