using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AudioManager : Singleton<AudioManager>
{
    // Start is called before the first frame update
    public AudioSource[] Musica;
    public AudioSource[] Efectos;

    float VolumenMusica = 1f;
    float VolumenEfecto = 1f;

    public Slider SliderMusic;
    public Slider SliderEffectos;

    public static AudioManager audioManager;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        audioManager = this;
        Musica[0].Play();
        VolumenInicial();

        //SliderMusic = GameObject.FindGameObjectWithTag("SlidderMusica").GetComponent<Slider>();
        //SliderEffectos = GameObject.FindGameObjectWithTag("SlidderEfectos").GetComponent<Slider>();
    }

    private void VolumenInicial()
    {
        Musica[0].volume = PlayerPrefs.GetFloat("VolumenMusica", 1f);
        Efectos[0].volume = PlayerPrefs.GetFloat("VolumenEfecto", 1f);

        SliderMusic.value = Musica[0].volume;
        SliderEffectos.value = Efectos[0].volume;
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Musica.Length; i++)
        {
            Musica[0].volume = VolumenMusica;
        }

        for (int i = 0; i < Efectos.Length; i++)
        {
            Efectos[i].volume = VolumenEfecto;
        }

    }
    public void ActualizarVolumen(float volume)
    {
        VolumenMusica = volume;
        PlayerPrefs.SetFloat("VolumenMusica", Musica[0].volume);
        PlayerPrefs.Save();
    }
    public void ActualizarEfectos(float volume)
    {
        VolumenEfecto = volume;
        PlayerPrefs.SetFloat("VolumenEfectos", Efectos[0].volume);
        PlayerPrefs.Save();
    }
  }
