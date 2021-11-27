using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField] private string absorbSound = "Obstacles/Fall";
    [SerializeField] private bool loseLife = true;
    [SerializeField] private LivesManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bille"))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/" + absorbSound);
            Destroy(other.gameObject);
            if (loseLife)
                manager.LoseOneLife();
        }
    }
}
