using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Bille : MonoBehaviour
{
    private Rigidbody body;
    private StudioEventEmitter sound;

    private StreamWriter writer;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        sound = GetComponent<StudioEventEmitter>();
        writer = new StreamWriter("C:/Users/Etudiant/Desktop/data.csv");
    }

    private void OnApplicationQuit()
    {
        writer.Close();
    }

    private void Update()
    {
        sound.SetParameter("Vitesse", body.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, body.velocity, Color.yellow);
        writer.Write(Time.time.ToString().Replace(',', '.') + ", " + body.velocity.magnitude.ToString("F1").Replace(',', '.') + "\n");
    }
}
