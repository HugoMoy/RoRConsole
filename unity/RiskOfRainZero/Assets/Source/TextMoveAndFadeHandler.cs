using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMoveAndFadeHandler : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public float TimeBeforeFading;
    public float TimeBeforeDestroy;

   public void Initialize(Vector3 direction, float speed, float timeBeforeFading, float timeBeforeDestroy)
    {
        this.direction = direction;
        this.speed = speed;
        this.TimeBeforeFading = timeBeforeFading;
        this.TimeBeforeDestroy = timeBeforeDestroy;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        TimeBeforeFading -= Time.deltaTime;
        TimeBeforeDestroy -= Time.deltaTime;

        if(TimeBeforeFading <= 0)
        {
            Text text = GetComponent<Text>();
            Color color = text.color;
            color.a -= 0.01f;
            text.color = color;
        }

        if(TimeBeforeDestroy <= 0)
        {
            Destroy(gameObject);
        }

        // Debug.Log("TimeBeforeFading: " + TimeBeforeFading);
        // Debug.Log("TimeBeforeDestroy: " + TimeBeforeDestroy);
    }
}
