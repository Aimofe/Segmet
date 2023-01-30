using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
   
    public void Empezar()
    {
        SceneManager.LoadScene("DeathMenu");
    }
    public void EmpezarNueva()
    {
        SceneManager.LoadScene("Sejmet");
    }
    public void Salir()
    {
        Application.Quit();
    }
 
}
