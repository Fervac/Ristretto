using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ball : MonoBehaviour
{
    private Vector2 direction;
    private Vector2 velocity;
    private Vector2 previous;
    [SerializeField]
    private float ballSpeed;
    public TextMeshProUGUI countDown;

    private void Awake()
    {
        ballSpeed = 10f;
    }

    private void Start()
    {
        StartCoroutine(LaunchGame());
    }

    IEnumerator LaunchGame()
    {
        // Countdown before the ball starts moving (paddle can already move)
        countDown.text = "3";
        yield return new WaitForSeconds(.5f);
        countDown.text = "2";
        yield return new WaitForSeconds(.5f);
        countDown.text = "1";
        yield return new WaitForSeconds(.5f);
        countDown.text = "GO !";
        yield return new WaitForSeconds(.2f);
        // Hide countdown panel
        Manager.Instance.SwitchShowWindow(Manager.Instance.countdownPanel);
        // Random ball starting direction (down left or down right)
        direction = Random.Range(0, 2) == 0 ? new Vector2(ballSpeed, -ballSpeed) : new Vector2(-ballSpeed, -ballSpeed);
    }

    private void Update()
    {
        transform.Translate(direction * Time.deltaTime) ;
        velocity = ((transform.position - new Vector3(previous.x, previous.y, 0.0f))) / Time.deltaTime;
        previous = transform.position;

        FireRaycast();
    }

    private void LateUpdate()
    {
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (transform.position.y < -5f)
            Manager.Instance.Retry();
    }

    private void FireRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit))
            {
                //Debug.DrawLine(transform.position, hit.point);

                if (hit.distance <= 0.5f)
                {
                    direction = Vector2.Reflect(velocity, hit.normal);

                    if (hit.collider.gameObject.CompareTag("Brick"))
                    {
                        hit.collider.gameObject.GetComponent<Brick>().Break();
                        Manager.Instance.CheckWin();
                    }
                }
                //Vector2 tmp = Vector2.Reflect(velocity, hit.normal);
                //Debug.DrawLine(hit.point, hit.point + new Vector3(tmp.x, tmp.y, 0.0f));
            }
    }
}
