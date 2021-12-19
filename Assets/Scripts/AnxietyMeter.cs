using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnxietyMeter : MonoBehaviour
{
    public static AnxietyMeter I;
    public UnityEvent DieEvent;

    private float meterAmount = 0;
    private bool dead = false;

    private void Start ()
    {
        I = this;
    }

    public void Increase (float amount)
    {
        if (dead) return;

        meterAmount += amount / 1.75f;
        if (meterAmount > 10)
        {
            dead = true;
            DieEvent?.Invoke ();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }

    private void Update ()
    {
        if (dead) return;

        meterAmount = Mathf.Max (meterAmount - Time.deltaTime / 3.5f, 0);
        Vector3 scale = transform.localScale;
        scale.x = meterAmount / 10;
        transform.localScale = scale;

        if (Input.GetKeyDown (KeyCode.Escape))
            Application.Quit ();
    }
}
