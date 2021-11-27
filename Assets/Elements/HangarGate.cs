using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangarGate : MonoBehaviour
{
    [SerializeField] private Hangar hangar;
    [SerializeField] private Transform gate;
    [SerializeField] private Vector3 transformationWhenOpen = new Vector3(0, 0, .26f);

    private void Start()
    {
        OpenGate();
    }

    public void OpenGate()
    {
        gate.transform.localPosition = transformationWhenOpen;
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
