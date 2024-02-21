using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavenInFlight : MonoBehaviour
{
    public float moveSpeed;
    public float height;

    private float startTime;
    private Vector2 startPos;

    private Vector2 Direction;
    bool alreadyFaded = false;
    bool isAwake = false;



    void Awake()
    {
        startTime = Time.time;
        startPos = transform.position;
        int index = Random.Range(0, 2);
        int offset = Random.Range(0, 1);

        Vector2[] DirRange = { Vector2.right, Vector2.left };
        Direction = DirRange[offset];

        height += (float)offset;
        float journeyLength = Vector2.Distance(startPos, startPos + Vector2.up * height);
        isAwake = true;


    }

    // Update is called once per frame
    void Update()
    {
        if (isAwake)
        {
            Debug.Log("StartPos    " + startPos);
            Debug.Log("Direction    " + Direction);
            float journeyLength = Vector2.Distance(startPos, startPos + Vector2.up * height);
            Debug.Log("journeyLenght     " + journeyLength);
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / journeyLength;

            transform.position = Vector2.Lerp(startPos, startPos + Vector2.up * height + Direction * 10f, fractionOfJourney);

            if (fractionOfJourney >= 0.5f && !alreadyFaded)
            {
                alreadyFaded = true;
                StartCoroutine(fadeOut(gameObject.GetComponent<SpriteRenderer>(), journeyLength));

            }
            if (fractionOfJourney >= 1)
            {
                Destroy(gameObject);
                isAwake = false;
            }
        }
        

    }

    IEnumerator fadeOut(SpriteRenderer MyRenderer, float duration)
    {
        float counter = 0;
        //Get current color
        Color spriteColor = MyRenderer.material.color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, counter / duration);

            //Change alpha only
            MyRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            //Wait for a frame
            yield return null;
        }
    }
}
