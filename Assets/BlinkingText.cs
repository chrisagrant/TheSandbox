using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour
{
    public float BlinkTime;
    public int Times;
    public Text Text;
    public Color BlinkColor;
    Color color;
    float nextBlink;
    int timesBlinked;
    bool blinking;

    void Start()
    {
        nextBlink = 0;
        timesBlinked = 0;
        blinking = false;
        color = BlinkColor;
    }

    public void Blink()
    {
        nextBlink = Time.time + (BlinkTime / 2); // both on and off time must be considered
        timesBlinked = 0;
        blinking = true;
        color = BlinkColor;
    }

    void Update()
    {
        if (blinking && nextBlink - Time.time <= 0)
        {
            nextBlink = Time.time + (BlinkTime / 2);

            if (timesBlinked % 2 == 0)
            {
                color = Text.color; // possible race condition.
                Text.color = BlinkColor;
            }
            else
            {
                Text.color = color;
            }

            if (++timesBlinked == 2 * Times) // symmetry makes this easier to solve - less state required.
                blinking = false;
        }
    }
}
