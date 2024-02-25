using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MovePlayerPreview : MonoBehaviour
{

    public List<Item> MoveList;
    public List<EnemyController> Enemies;
    public float moveSpeed = 5;
    private int MoveLooper;
    private bool isMoving = false;

    public CharacterAnimator _animator;

    public Item FirstMove;
    public Item SecondMove;
    public Item ThirdMove;


    // Update is called once per frame
    void Update()
    {
        
    }

    private void Start()
    {
        _animator = GetComponent<CharacterAnimator>();

    }

    public void MovePlayer()
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(MovePlayerCoroutine());

        }
    }

    private IEnumerator MovePlayerCoroutine()
    {
        
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

                Item moveOption = MoveList[MoveOptionIndex];
                int Direction = 0;
                List<Vector2> originalMoves = new List<Vector2>(moveOption.Moves);
                for (int i = 0; i < originalMoves.Count; i++)
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

                        

                            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
                            {
                                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                                yield return null;
                            }

                            yield return new WaitForSeconds(0.05f);
                            _animator.IsMoving = false;
                    

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
        //MoveList.Clear();
        yield return null;
    }
}
