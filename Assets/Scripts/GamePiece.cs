using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public int xIndex;
    public int yIndex;

    private bool m_isMoving;

    private void Update()
    {
        
    }


    public void SetCoordinate(int xIndex, int yIndex)
    {
        this.xIndex = xIndex;
        this.yIndex = yIndex;
    }

    public void Move(int destX, int destY, float timeToMove)
    {
        if(!m_isMoving)
            StartCoroutine(MoveCoroutine(new Vector3(destX, destY, 0), timeToMove));
    }

    private IEnumerator MoveCoroutine(Vector3 destination, float timeToMove)
    {
        Vector3 startPosition = transform.position;

        bool reachedDestination = false;

        float elapsedTime = 0f;

        m_isMoving = true;

        while (!reachedDestination)
        {
            if(Vector3.Distance(transform.position,destination) < 0.01f)
            {
                reachedDestination = true;
                transform.position = destination;
                SetCoordinate((int)destination.x, (int)destination.y);
                break;
            }

            float t = Mathf.Clamp(elapsedTime / timeToMove, 0f, 1f);

            transform.position = Vector3.Lerp(startPosition, destination, t);

            yield return null;
        }

        m_isMoving = false;
    }


}
