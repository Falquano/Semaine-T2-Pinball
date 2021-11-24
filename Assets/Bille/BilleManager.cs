using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BilleManager : MonoBehaviour
{
    [SerializeField] private Bille bille;
    public Bille CurrentBille { get => bille; set => SetBille(value); }

    [SerializeField] private UnityEvent<Bille> onBilleChange;
    public UnityEvent<Bille> OnBilleChange => onBilleChange;

    private void SetBille(Bille value)
    {
        bille = value;
        onBilleChange.Invoke(bille);
    }
}
