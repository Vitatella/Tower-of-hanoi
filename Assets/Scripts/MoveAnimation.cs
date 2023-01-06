using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveAnimation : MonoBehaviour
{
    private float choicedDiscHeight;
    private float time;
    private float animationTime = 0.2f;
    private bool isMoving = false;

    private Queue<Vector3> positions = new Queue<Vector3>();
    private Vector3 beginPosition, endPosition;
    private bool animationEnabled;
    void Start()
    {
        choicedDiscHeight = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GetHeight();
    }

    void Update()
    {
        if (animationEnabled)
        {
            if (!isMoving)
            {
                beginPosition = positions.Dequeue();
                endPosition = positions.Dequeue();
                isMoving = true;
                time = 0f;
            }
            time += Time.deltaTime / animationTime;
            transform.position = Vector3.Lerp(beginPosition, endPosition, time);
            if (time >= 1f)
            {
                isMoving = false;
                if (positions.Count == 0) animationEnabled = false;
            }
        }
    }
    public void DoMove(Vector3 start, Vector3 end)
    {
        positions.Enqueue(start);
        if (new Vector2(start.x, start.z) != new Vector2(end.x, end.z))
        {
            positions.Enqueue(new Vector3(end.x, choicedDiscHeight, end.z));
            positions.Enqueue(new Vector3(end.x, choicedDiscHeight, end.z));
        }
        positions.Enqueue(end);
        animationEnabled = true;
    }
    public void PickUp(Vector3 start, Vector3 end)
    {
        positions.Enqueue(start);
        positions.Enqueue(end);
        animationEnabled = true;
    }   
}
