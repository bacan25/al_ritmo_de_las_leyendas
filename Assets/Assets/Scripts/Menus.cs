using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    int currentSceneIndex;
    public GameObject inicio;
    public GameObject tuto1;
    public GameObject tuto2;
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void ButtonStart()
    {
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

    public void NextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }


    public void Tutorial1()
    {
        tuto1.SetActive(true);
        inicio.SetActive(false);
    }

    public void Tutorial2()
    {
        tuto2.SetActive(true);
        tuto1.SetActive(false);
        
    }
    

}
