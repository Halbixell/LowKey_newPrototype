using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public enum Direction
{
    Right = 0,
    Up = 1,
    Left = 2,
    Down = 3
}

[CreateAssetMenu(fileName = "MoveOption", menuName = "MoveOption/Create new movement Option")]

public class Item : ScriptableObject 
{
    [Header("UI")]
    [HideInInspector]public DraggableItem draggableItem;

    [Header("Description")]
    [SerializeField] private string _name;
    [TextArea]
    [SerializeField] private string _description;

    [Header("Art")]
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Animation _anim;

    [Header("Movement")]
    [SerializeField] Direction _dir;
    [SerializeField] List<Vector2> _moves;

    [Header("Rotation")]
    [SerializeField] int[] _rotation;

    public string Name => _name;
    public string Description => _description;
    public Sprite Sprite => _sprite;
    public Animation Anim => _anim;
    public Direction Dir => _dir;
    [HideInInspector] public List<Vector2> Moves => _moves;
    public int[] Rotation => _rotation;
}


