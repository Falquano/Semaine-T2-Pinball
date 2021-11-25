using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleManager : MonoBehaviour
{
    public static RumbleManager Manager { get; private set; }
    public static void Rumble(float time, float frequency) => Manager.RumbleController(frequency, time);

    private Gamepad gamepad => Gamepad.current;

    private float currentIntensity = 0f;
    private float timeRemaining = 0f;

    private void Awake()
    {
        Manager = this;
    }

    public void RumbleController(float frequency, float time)
    {
        if (gamepad == null)
            return;

        if (frequency > currentIntensity)
            gamepad.SetMotorSpeeds(frequency, frequency);
        timeRemaining = time;
    }

    public void StopRumble()
    {
        timeRemaining = 0f;
        currentIntensity = 0f;
        if (gamepad == null)
            return;
        gamepad.SetMotorSpeeds(0f, 0f);
    }

    private void Update()
    {
        if (timeRemaining > 0)
            timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            currentIntensity = 0f;
            StopRumble();
        }
    }
}
