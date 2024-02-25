using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTile : MonoBehaviour
{

    [SerializeField] private LevelController _levelcontroller;
    [SerializeField] private RavenCounter _ravenCounter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            Debug.Log("Won the Level!");
            _levelcontroller.LevelWon();
        }
        _ravenCounter.RavenImages[2].sprite = _ravenCounter.Collected;
    }


}
