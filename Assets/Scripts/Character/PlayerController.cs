using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public ListOfMoveSlots inventorySlots;
    private Item moveOption;
    public Button StartMovement;
    protected CharacterAnimator _animator;

    private List<ItemEntry> MoveList;
    private bool isMoving = false;
    [HideInInspector] public bool alreadyMovedOnce = false;

    private int MoveLooper = -1;

    [SerializeField] private LevelController _levelController;
    [SerializeField] private Canvas WinCanvas;
    [SerializeField] private Canvas TrueLostCanvas;
    [SerializeField] private Canvas LoseCanvas;

    public delegate void PlayerTurnStartAction();

    public event PlayerTurnStartAction OnPlayerTurnStart;

    private void Start()
    {
        alreadyMovedOnce = false;
        _animator = GetComponent<CharacterAnimator>();
        _levelController = FindObjectOfType<LevelController>();
    }

    public void MovePlayer(List<ItemEntry> MoveOptions, List<EnemyController> Enemies)
    {
        if (!isMoving && !alreadyMovedOnce)
        {
            alreadyMovedOnce = true;
            isMoving = true;
            StartCoroutine(MovePlayerCoroutine(MoveOptions, Enemies));

            if (OnPlayerTurnStart != null)
            {
                OnPlayerTurnStart();
            }
        }
        else if(!isMoving && alreadyMovedOnce)
        {
            Debug.Log("Done with the level!");

        }
    }


    private IEnumerator MovePlayerCoroutine(List<ItemEntry> MoveOptions, List<EnemyController> Enemies)
    {
        MoveList = MoveOptions;
        if (MoveList.Count != 0)
        {
            //Debug.Log($"MoveList.Count: {MoveList.Count}");
            for (int MoveOptionIndex = 0; MoveOptionIndex < MoveList.Count; MoveOptionIndex++)
            {
                MoveLooper = (MoveLooper + 1) % 4;
                foreach (EnemyController enemy in Enemies)
                {
                    enemy.HandleEnemyTurn(MoveLooper);
                }

                Item moveOption = MoveList[MoveOptionIndex].move;
                int Direction = MoveList[MoveOptionIndex].direction;
                List<Vector2> originalMoves = new List<Vector2>(moveOption.Moves);
                for (int i = 0; i < originalMoves.Count; i++)
                {
                    if (!_levelController.StopMovement)
                    {
                        Vector2 moveVector = originalMoves[i];
                        int[] Directions = { 0, 90, 180, 270 };
                        Vector2 temp;

                        float radians = Directions[Direction] * Mathf.Deg2Rad;

                        temp.x = Mathf.RoundToInt(moveVector.x * Mathf.Cos(radians) - moveVector.y * Mathf.Sin(radians));
                        temp.y = Mathf.RoundToInt(moveVector.x * Mathf.Sin(radians) + moveVector.y * Mathf.Cos(radians));

                        _animator.MoveX = Mathf.Clamp(temp.x, -1f, 1f);
                        _animator.MoveY = Mathf.Clamp(temp.y, -1f, 1f);
                        _animator.IsMoving = true;

                        Vector3 targetPosition = transform.position + new Vector3(temp.x, temp.y, 0f);

                        if (IsPathClear(targetPosition))
                        {

                            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
                            {
                                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                                yield return null;
                            }

                            yield return new WaitForSeconds(0.05f);
                            _animator.IsMoving = false;
                        }
                        else
                        {
                            yield return new WaitForSeconds(0.6f);
                            _animator.IsMoving = false;
                        }
                    }

                }
                yield return new WaitForSeconds(0.4f);
            }
        }
        else
        {
            Debug.Log("MoveList is empty");
        }
        MoveList.Clear();

        isMoving = false;

        if(!WinCanvas.isActiveAndEnabled && !TrueLostCanvas.isActiveAndEnabled)
        {
            LoseCanvas.gameObject.SetActive(true);

            FindObjectOfType<SoundManager>().StopMusic("Knarksen");
            FindObjectOfType<SoundManager>().Play("Langsam");


        }

        yield return null;
    }

    public void NotifyMoveOptionsChanged(List<ItemEntry> MoveOptionList)
    {
        if (!isMoving)
        {
            MoveList = MoveOptionList;
        }
    }

    protected bool IsPathClear(Vector3 targetPos)
    {
        var diff = targetPos - transform.position;
        var dir = diff.normalized;

        LayerMask layerMask = LayerMask.GetMask("NonWalkable", "Player", "Enemy");

        return !Physics2D.BoxCast(transform.position + dir, new Vector2(0.2f, 0.2f), 0f,
            dir, diff.magnitude - 1,
            layerMask);
    }

    public void PlayerDetected()
    {
        _levelController.LevelLost();
    }
}
