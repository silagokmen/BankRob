using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score;
    public Slider slider;
    public ParticleSystem partical;
    private bool particalPlay = false;
    public Camera camera1;
    public Camera camera2;
    public Light cameraLight;
    private GameObject player;
    private GameObject enemy;
    private int MoneyCount;
    public Text text;



    void Start()
    {
        MoneyCount = GameObject.FindGameObjectsWithTag("Coin").Length;
      
        slider.GetComponent<Slider>().maxValue = MoneyCount/2;


        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");

        score = 0;

        camera1.enabled = true;
        camera2.enabled = false;
        cameraLight.enabled = false;
        text.enabled = false;


    }




    public void Update()

    {
        slider.value = score;


        if (slider.value == MoneyCount/2 && particalPlay == false)
        {
            text.enabled = true;

            partical.Play();

            particalPlay = true;

            player.GetComponent<PlayerController>().enabled = false;
            enemy.GetComponent<EnemyController>().enabled = false;

            camera1.enabled = false;
            camera2.enabled = true;
            cameraLight.enabled = true;

            StartCoroutine(Pause(1.5f));
        }


        //oyun sonunda oyunu durdurma.
        IEnumerator Pause(float delayTime)
        {

            yield return new WaitForSeconds(delayTime);
            Time.timeScale = 0;
        }

    }
}