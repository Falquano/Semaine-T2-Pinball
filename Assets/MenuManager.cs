using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Text UIText;

    public void AnyInput(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Debug.Log("Any");
        if (context.performed)
        {
            Debug.Log("Performed");
            SceneManager.LoadScene("SampleScene");
        }
    }

    private void Start()
    {
        UIText.text = PlayerPrefs.GetInt("highscore").ToString();
    }
}
