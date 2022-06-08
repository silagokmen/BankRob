using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region ilkHareketDenemesiDegiskenleri
    private float speed = 5.0f;
    private Vector3 targetPos;
    private bool isMove;
    #endregion

    private Animator animator;
    public ParticleSystem runPartical;
    public AudioClip moneySound;
    private GameObject enemy2;
    

    public RectTransform knob,center;


    Touch touch;

    private bool _dragStarted;
    private bool movable;
    private bool _isMoving;
    private Vector3 start;
    private Vector3 stop;
    private float rotationSpeed = 300f;
    private float speedMove = 10f;
   





    void Start()
    {

        animator = GetComponent<Animator>();

        enemy2 = GameObject.Find("Enemy2");
        enemy2.GetComponent<EnemyController>().enabled = false;
        ShowJoystick(false);
    }


    void Update()
    {
        //targetPos = transform.position;
        //isMove = false;

        TouchController();
        
        characterBound();

       
    }




    void TouchController()
    {



        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);


            //sağ sol
            if (touch.phase == TouchPhase.Began)
            {
                _dragStarted = true;
                
                ShowJoystick(true);

                



                start = touch.position;
                center.position = start;
                
                stop = touch.position;
            }

            if (_dragStarted)
            {

                if (touch.phase == TouchPhase.Moved)
                {
                    movable = true;
                    stop = touch.position;
                    knob.position = stop;
                    knob.position = center.position + Vector3.ClampMagnitude(knob.position - center.position, center.sizeDelta.x * .5f);
                }

                if (movable == true && touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)

                {
                    _isMoving = true;

                    gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation,
                                                                             CalculateRotation(),
                                                                             rotationSpeed * Time.deltaTime);

                    gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);



                }


                if (touch.phase == TouchPhase.Ended)
                {
                    stop = touch.position;
                    _dragStarted = false;
                    _isMoving = false;
                    movable = false;
                    ShowJoystick(false);
                }

                if(_isMoving)
                {
                    animator.SetBool("isRun", true);
                    runPartical.Play();
                }

                if(!_isMoving)
                {
                    animator.SetBool("isRun", false);
                    runPartical.Stop();
                }

               
                

            }
        }

        

        Vector3 CalculateDirection()
        {
            
            Vector3 temp = (stop - start).normalized;
            temp.z = temp.y;
            temp.y = 0;
            return temp;

        }


        Quaternion CalculateRotation()
        {
            Quaternion temp = Quaternion.LookRotation(CalculateDirection(), Vector3.up);
            return temp;
        }


    }




    void ShowJoystick(bool state)
    {
        knob.gameObject.SetActive(state);

        center.gameObject.SetActive(state);


    }
    IEnumerator Delay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene("MainMenu");
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            Score.score += 1;
            AudioSource.PlayClipAtPoint(moneySound, other.transform.position);
           
        }

        if (other.CompareTag("Enemy"))
        {
            animator.SetBool("isCatch 0", true);
            speed = 0;
            StartCoroutine(Delay(3));
          
        }
        

        if (other.CompareTag("Plane"))
        {
            enemy2.GetComponent<EnemyController>().enabled = true;
        }

    }

    void characterBound()
    {
       if(transform.position.z < -35.0f)
        {
          transform.position = new Vector3(transform.position.x, transform.position.y, -35.0f);
        } 
    }



    #region ilkHareketDenemesi

    void MouseButton()
    {
        if (Input.GetMouseButton(0))
       {
           KonumBulveDonustur();
           animator.SetBool("isRun", true);
           runPartical.Play();

       }
       else
       {
           animator.SetBool("isRun", false);
           runPartical.Stop();

       }

       if (isMove)
       {
           HareketeGecir();

       }

    }

    void KonumBulveDonustur()
    {
        Plane yuzey = new Plane(Vector3.up, transform.position);
        Ray isinGonder = Camera.main.ScreenPointToRay(Input.mousePosition);
        float mesafe;

        if(yuzey.Raycast(isinGonder,out mesafe))
        {
            targetPos = isinGonder.GetPoint(mesafe);
        }

        isMove = true;
        
    }

    void HareketeGecir()
    {
        transform.LookAt(targetPos);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        
        

        if(transform.position== targetPos)
        {
            isMove = false;
   
        }
    }

    #endregion

}
