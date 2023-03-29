using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    private InputManager inputManager;
    private Player player;


    private float minDistance = 0.4f;
    private float maxTime = 1f;
    

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    private void Awake()
    {
        inputManager = InputManager.instance;
        player = Player.Instance;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        float distance = Vector3.Distance(endPosition, startPosition);
        if (((endTime - startTime) <= maxTime) && distance > minDistance)
        {
            float swipeDeciderX = Mathf.Abs(startPosition.x - endPosition.x);
            float swipeDeciderY = Mathf.Abs(startPosition.y - endPosition.y);

            if (swipeDeciderX > swipeDeciderY)
            {
                if (startPosition.x < endPosition.x)
                {
                    Debug.Log("Swiped right");
                    player.PlayerMove(false);
                }
                else if (startPosition.x > endPosition.x)
                {
                    Debug.Log("Swiped left");
                    player.PlayerMove(true);
                }
            }
            else
            {
                if (startPosition.y < endPosition.y)
                {
                    Debug.Log("Jumping up?");
                    player.Jump();
                }
            }
        }
        else
        {
            if (player != null)
            {
                Debug.Log("Player Shooting man!");
                player.Shoot();
            }
        }
    }

}
