using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector2 direction;

    private float rotation;
    [SerializeField]
    private Vector2 velocity;
    private Vector2 previous;
    private float speed;

    private void Awake()
    {
        speed = 10f;
        direction = Random.Range(0, 2) == 0 ? new Vector2(speed, -speed) : new Vector2(-speed, -speed);
    }

    private void Update()
    {
        transform.Translate(direction * Time.deltaTime) ;

        velocity = ((transform.position - new Vector3(previous.x, previous.y, 0.0f))) / Time.deltaTime;
        previous = transform.position;
    }

    private void FixedUpdate()
    {
        FireRaycast();

        CheckDeath();
    }

    private void CheckDeath()
    {
        if (transform.position.y < -5)
            Manager.Instance.Retry();
    }

    private void FireRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit))
            {
                //print("There is a " + hit.collider.gameObject.name + " " + hit.distance +"m away from gameobject");

                Debug.DrawLine(transform.position, hit.point);

                if (hit.distance <= 0.5f)
                {
                    direction = Vector2.Reflect(velocity, hit.normal);

                    if (hit.collider.gameObject.CompareTag("Brick"))
                    {
                        Manager.Instance.bricks.Remove(hit.collider.gameObject);
                        Destroy(hit.collider.gameObject);

                        Manager.Instance.CheckWin();

                        // Increase ball speed after each brick destroyed ?
                    }
                }

                Vector2 tmp = Vector2.Reflect(velocity, hit.normal);
                Debug.DrawLine(hit.point, hit.point + new Vector3(tmp.x, tmp.y, 0.0f));
            }
    }
}
