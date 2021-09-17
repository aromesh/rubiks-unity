using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBigCube : MonoBehaviour
{
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    Vector3 previousMousePosition;
    Vector3 mouseDelta;
    public GameObject target;

    const float SPEED = 200.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Swipe();
        Drag();

    }

    void Drag()
    {
        if (Input.GetMouseButton(1))
        {
            //if mouse held down, cube can be moved around its central axis
            mouseDelta = Input.mousePosition - previousMousePosition;
            mouseDelta *= 0.2f; // scaling factor for rotation speed
            transform.rotation = Quaternion.Euler(mouseDelta.y, -mouseDelta.x, 0) * transform.rotation;
        } else 
        {
            //rotate towards target if mopuse not held down
            if (transform.rotation != target.transform.rotation)
            {
                var step = SPEED * Time.deltaTime;
                //transform from cube rotation to current tarety rotation (incrementally)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, step);
            }
        }
        previousMousePosition = Input.mousePosition;
    }

    void Swipe()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //get 2D position of first mouse click
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (Input.GetMouseButtonUp(1))
        {
            //get 2D position of scond mouse click
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //create a vector from first and second click posiiton
            currentSwipe = new Vector2(secondPressPos.x-firstPressPos.x, secondPressPos.y-firstPressPos.y);
            currentSwipe.Normalize();

            //rotate accordingly
            if (LeftSwipe(currentSwipe))
            {
                target.transform.Rotate(0,90,0, Space.World);

            } else if (RightSwipe(currentSwipe))
            {
                target.transform.Rotate(0,-90,0, Space.World);
            } else if (UpLeftSwipe(currentSwipe))
            {
                target.transform.Rotate(90,0,0, Space.World);
            } else if (UpRightSwipe(currentSwipe))
            {
                target.transform.Rotate(0,0,-90, Space.World);
            } else if (DownLeftSwipe(currentSwipe))
            {
                target.transform.Rotate(0,0,90, Space.World);
            } else if (DownRightSwipe(currentSwipe))
            {
                target.transform.Rotate(-90,0,0, Space.World);
            }

        }
    }

    bool LeftSwipe(Vector2 swipe)
    {
        return currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }

    bool RightSwipe(Vector2 swipe)
    {
        return currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }

    bool UpLeftSwipe(Vector2 swipe)
    {
        return currentSwipe.y > 0 && currentSwipe.x < 0f;
    }

    bool UpRightSwipe(Vector2 swipe)
    {
        return currentSwipe.y > 0 && currentSwipe.x > 0f;
    }

    bool DownLeftSwipe(Vector2 swipe)
    {
        return currentSwipe.y < 0 && currentSwipe.x < 0f;
    }

    bool DownRightSwipe(Vector2 swipe)
    {
        return currentSwipe.y < 0 && currentSwipe.x > 0f;
    }
}
