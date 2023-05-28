using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class title : MonoBehaviour
{
    void Update()
    {
        if (Gamepad.current == null) return;
        if(Gamepad.current.leftShoulder.wasPressedThisFrame){TitleChanger();}
    }
    public void TitleChanger()
    {
        SceneManager.LoadScene("TutorialScene");
    }
}
