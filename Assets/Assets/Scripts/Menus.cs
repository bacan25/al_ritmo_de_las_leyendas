using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    int currentSceneIndex;

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void ButtonStart()
    {
        Debug.Log("hola?");
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    public void ButtonExit()
    {
        Debug.Log("chao");

        Application.Quit();
    }

    public void ButtonRestart()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void ButtonMenu()
    {
        SceneManager.LoadScene(0);
    }
    

}
