using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangarGate : MonoBehaviour
{
    [SerializeField] private Hangar hangar;
    [SerializeField] private Transform gate;

    private void Start()
    {
        OpenGate();
    }

    public void OpenGate()
    {
        gate.transform.position += Vector3.up * -2.1f;
    }

    public void CloseGate()
    {
        gate.transform.position -= Vector3.up * -2.1f;
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bille"))
            Invoke("CloseGate", .5f);
    }

    public void SwitchBille(Bille newBille)
    {
        if (newBille == null)
            OpenGate();
    }
}
