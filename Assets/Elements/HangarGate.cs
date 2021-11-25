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
        gate.transform.localPosition = Vector3.up * -1.1f;
    }

    public void CloseGate()
    {
        gate.transform.localPosition = Vector3.zero;
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
