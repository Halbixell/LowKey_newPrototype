using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingColliders : MonoBehaviour
{
    [SerializeField] private EnemyController _player;
    private CharacterAnimator _AnimState;
    [SerializeField] private Vector3 AdjustX;
    [SerializeField] private Vector3 AdjustY;

    void Start()
    {
        _AnimState = _player.gameObject.GetComponent<CharacterAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_AnimState.MoveX == -1)
        {
            transform.position = _player.transform.position - AdjustX;
        }
        if (_AnimState.MoveX == 1)
        {
            transform.position = _player.transform.position + AdjustX;
        }
        if (_AnimState.MoveY == -1)
        {
            transform.position = _player.transform.position - AdjustY;
        }
        if (_AnimState.MoveY == 1)
        {
            transform.position = _player.transform.position + AdjustY;
        }
    }
}
