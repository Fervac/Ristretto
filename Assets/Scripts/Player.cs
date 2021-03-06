using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Touch touch;
    private Vector2 touchSlide;
    private float speed = 12f;
    private float mobileSpeed = 1f;

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        float xMove = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        transform.Translate(xMove, 0f, 0f);
        // initially, the temporary vector should equal the player's position
        Vector3 clampedPosition = transform.position;
        // Now we can manipulte it to clamp the y element
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -7.2f, 7.2f);
        // re-assigning the transform's position will clamp it
        transform.position = clampedPosition;

        if (Input.GetKeyDown("r"))
        {
            Manager.Instance.Retry();
        }
#endif

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved) //|| touch.phase == TouchPhase.Ended)
            {
                var x = touch.deltaPosition.x * mobileSpeed * Time.deltaTime;
                transform.Translate(new Vector3(x, 0, 0));
                // initially, the temporary vector should equal the player's position
                Vector3 clampPosition = transform.position;
                // Now we can manipulte it to clamp the y element
                clampPosition.x = Mathf.Clamp(clampPosition.x, -7.2f, 7.2f);
                // re-assigning the transform's position will clamp it
                transform.position = clampPosition;
            }
        }
#endif
    }
}
