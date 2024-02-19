using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingColliders : MonoBehaviour
{
    [SerializeField] private EnemyController _player;
    private CharacterAnimator _AnimState;
    private CharacterAnimator _collider;
    [SerializeField] private Vector3 AdjustX;
    [SerializeField] private Vector3 AdjustY;

    void Start()
    {
        _AnimState = _player.gameObject.GetComponent<CharacterAnimator>();
        _collider = this.gameObject.GetComponent<CharacterAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_AnimState.MoveX == -1)
        {
            transform.position = _player.transform.position - AdjustX;
            _collider.MoveX = -1;
            _collider.MoveY = 0;
        }
        if (_AnimState.MoveX == 1)
        {
            transform.position = _player.transform.position + AdjustX;
            _collider.MoveX = 1;
            _collider.MoveY = 0;
        }
        if (_AnimState.MoveY == -1)
        {
            transform.position = _player.transform.position - AdjustY;
            _collider.MoveX = 0;
            _collider.MoveY = -1;
        }
        if (_AnimState.MoveY == 1)
        {
            transform.position = _player.transform.position + AdjustY;
            _collider.MoveX = 0;
            _collider.MoveY =1;
        }
    }
}
