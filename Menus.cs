using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{

     
    public void ExitScene()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("CampoDeTiro");
}

    public void WinCondition()
    {
          SceneManager.LoadScene("Victory");
        
    }
 }
