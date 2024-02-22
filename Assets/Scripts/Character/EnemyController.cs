using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    private bool isMoving = false;

    public float AreaAttackRadius = 2f;
    private int MoveAlternator = 0;

    [SerializeField] private int RotationOffset;

    private PlayerController playerController;
    [SerializeField] private Item EnemyMoves;
    [HideInInspector] private LevelController _levelController;

    protected CharacterAnimator _animator;
    private CharacterAnimator _colliders;

    public List<Collider2D> hitColliders;

    private PlayerController _player;


    private void Start()
    {
        _levelController = FindObjectOfType<LevelController>();
        _animator = GetComponent<CharacterAnimator>();
        UpdateColliders(false);
        RotationOffset = (RotationOffset % 4);

        _player = FindObjectOfType<PlayerController>();
    }

    public void HandleEnemyTurn(int MoveLooper)
    {
        //MoveLooper = (MoveLooper + 1)% 4;
        StartCoroutine(EnemyTurnCoroutine(MoveLooper, EnemyMoves.Rotation));
    }

    private void UpdateColliders(bool flag)
    {

        foreach (Collider2D col in hitColliders)
        {
            col.gameObject.SetActive(flag);
        }
    }

    private void ActivateColliders(bool activator)
    {
        foreach (Collider2D col in hitColliders)
        {
            col.enabled = activator;
        }
    }

    private IEnumerator EnemyTurnCoroutine(int MoveLooper, int[] Directions)
    {
        if (moveSpeed != 0)
        {
            MoveAlternator = 0;
            if (!isMoving)
            {
                isMoving = true;

                List<Vector2> originalMoves = new List<Vector2>(EnemyMoves.Moves);

                for (int i = 0; i < originalMoves.Count; i++)
                {

                    if (!_levelController.StopMovement)
                    {
                        if ((MoveAlternator % 2 == 0))
                        {
                            MoveAlternator = MoveAlternator + 1;
                            Vector2 moveVector = originalMoves[i];
                            Vector2 temp;
                            float radians;
                            radians = Directions[((int)EnemyMoves.Dir + MoveLooper + RotationOffset) % 4] * Mathf.Deg2Rad;

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
                            }
                            else
                            {
                                yield return new WaitForSeconds(0.65f);
                            }

                            if (moveVector.y != 0)
                            {
                                _animator.IsMoving = false;
                            }
                            else
                            {
                                yield return new WaitForSeconds(0.2f);
                                _animator.IsMoving = false;
                            }


                        }
                        else
                        {
                            MoveAlternator = MoveAlternator + 1;
                            UpdateColliders(true);
                            ActivateColliders(false);
                            yield return new WaitForSeconds(0.25f);
                            ActivateColliders(true);
                            yield return new WaitForSeconds(0.2f);
                            ActivateColliders(false);
                            yield return new WaitForSeconds(0.25f);
                            UpdateColliders(false);

                        }

                        isMoving = false;
                    }

                }

            }
        }
        else
        {
            MoveAlternator = 0;
            if (!isMoving)
            {
                isMoving = true;

                List<Vector2> originalMoves = new List<Vector2>(EnemyMoves.Moves);

                for (int i = 0; i < originalMoves.Count; i++)
                {

                    if (!_levelController.StopMovement)
                    {
                        if ((MoveAlternator % 2 == 0))
                        {
                            MoveAlternator = MoveAlternator + 1;
                            int[] RotationX = { 1, 0, -1, 0 };
                            int[] RotationY = { 0, 1, 0, -1 };

                            Vector2 moveVector = originalMoves[i];
                            Vector2 temp;
                            float radians;
                            radians = Directions[((int)EnemyMoves.Dir + MoveLooper + RotationOffset) % 4] * Mathf.Deg2Rad;

                            temp.x = Mathf.RoundToInt(moveVector.x * Mathf.Cos(radians) - moveVector.y * Mathf.Sin(radians));
                            temp.y = Mathf.RoundToInt(moveVector.x * Mathf.Sin(radians) + moveVector.y * Mathf.Cos(radians));

                            _animator.MoveX = Mathf.Clamp(RotationX[((int)EnemyMoves.Dir + MoveLooper + RotationOffset) % 4], -1f, 1f);
                            _animator.MoveY = Mathf.Clamp(RotationY[((int)EnemyMoves.Dir + MoveLooper + RotationOffset) % 4], -1f, 1f);
                            _animator.IsMoving = true;

                            Vector3 targetPosition = transform.position + new Vector3(temp.x, temp.y, 0f);

                            if (IsPathClear(targetPosition))
                            {
                                while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
                                {
                                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                                    yield return null;
                                }
                            }

                            yield return new WaitForSeconds(0.65f);
                            _animator.IsMoving = false;



                        }
                        else
                        {
                            MoveAlternator = MoveAlternator + 1;
                            UpdateColliders(true);
                            ActivateColliders(false);
                            yield return new WaitForSeconds(0.25f);
                            ActivateColliders(true);
                            yield return new WaitForSeconds(0.2f);
                            ActivateColliders(false);
                            yield return new WaitForSeconds(0.25f);
                            UpdateColliders(false);

                        }

                        isMoving = false;
                    }

                }

            }
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player in the area!");
        }
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