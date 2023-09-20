using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField playerInput;

    private string playerName;


    public void SaveName(string playerInput)
    {
        playerName = playerInput;        
    }

    public void Exit()
    {
        SaveName(playerName);
# if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
# else
        Application.Quit();
# endif
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

}
