using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTile : MonoBehaviour
{

    [SerializeField] private LevelController _levelcontroller;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            Debug.Log("Won the Level!");
            _levelcontroller.LevelWon();
        }
    }


}
