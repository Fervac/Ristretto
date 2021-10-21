using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private int life;
    public enum Type {red, yellow, green, blue};
    public Type type;
    private Material mat;

    private void Awake()
    {
        // Different brick types need a different number of hits to get destroyed
        switch(type)
        {
            case Type.red:
            {
                life = 4;
                break;
            }
            case Type.yellow:
            {
                life = 3;
                break;
            }
            case Type.green:
            {
                life = 2;
                break;
            }
            case Type.blue:
            {
                life = 1;
                break;
            }
            default:
                break;
        }

        mat = GetComponent<Renderer>().material;
        SetColor();
    }

    // Called every time the brick gets hit
    public void Break()
    {
        life -= 1;
        SetColor();

        if (life == 0)
        {
            Manager.Instance.bricks.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    // Color changes according to 'life' points
    private void SetColor()
    {
        if (life == 4)
            mat.color = Manager.Instance.red.color;
        if (life == 3)
            mat.color = Manager.Instance.yellow.color;
        if (life == 2)
            mat.color = Manager.Instance.green.color;
        if (life == 1)
            mat.color = Manager.Instance.blue.color;
    }
}
