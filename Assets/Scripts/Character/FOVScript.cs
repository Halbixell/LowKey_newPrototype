using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVScript : MonoBehaviour
{

    private PlayerController _player;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            Debug.Log("Found The Player!");
            _player.PlayerDetected();
        }
    }
}