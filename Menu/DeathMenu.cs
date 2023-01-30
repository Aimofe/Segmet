using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    GameObject efectos;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        efectos = GameObject.FindGameObjectWithTag("Effects");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Sound()
    {
       audioSource= efectos.transform.Find("ButtonSound").GetComponent<AudioSource>();
       audioSource.Play();
    }

}
