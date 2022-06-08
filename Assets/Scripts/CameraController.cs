using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 fark;
   
    private GameObject character;

    Vector3 _lerp;
    void Start()
    {
        character = GameObject.Find("Player");
        fark= transform.position - character.transform.position;
    }

    void LateUpdate()
    {
        //transform.position = character.transform.position + fark;

        transform.position = Vector3.Lerp(transform.position, character.transform.position + fark, 2.5f * Time.deltaTime);


    }


    
}
