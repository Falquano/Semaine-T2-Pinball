using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void AnyInput(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Debug.Log("Any");
        if (context.performed)
        {
            Debug.Log("Performed");
            SceneManager.LoadScene("SampleScene");
        }
    }
}
