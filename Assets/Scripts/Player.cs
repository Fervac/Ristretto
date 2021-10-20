using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float varTime;

    private void Awake()
    {
        varTime = 3f;
    }

    private void Update()
    {
        CheckInput();
        float speed = 10f;
        float xMove = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        transform.Translate(xMove, 0f, 0f);
        // initially, the temporary vector should equal the player's position
        Vector3 clampedPosition = transform.position;
        // Now we can manipulte it to clamp the y element
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -7f, 7f);
        // re-assigning the transform's position will clamp it
        transform.position = clampedPosition;
    }

    private void Start()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }

    private void CheckInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetKey("space"))
        {
            
        }

        if (Input.GetKeyUp("space"))
        {
            
        }


        if (Input.GetKeyDown("r"))
        {
            Manager.Instance.Retry();
        }
#endif

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                
            }
        }
#endif
    }


    private void Coroutine()
    {
        //_boosting = true;
        StartCoroutine(TestTimer());
    }

    IEnumerator TestTimer()
    {
        // a = velocity;
        // velocity += boostSpeed;
        // b = velocity;

        yield return new WaitForSeconds(varTime);

        //_decelerate = true;
    }

}
