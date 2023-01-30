using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

    }
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
    public void Opciones()
    {
       
    }
    public void Creditos()
    {
       
    }
   
}
