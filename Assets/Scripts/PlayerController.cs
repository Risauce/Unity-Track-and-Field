using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    public float speed;
    public Text finishText;
    public bool finished;
    public static PlayerController instance;

    private Rigidbody rb;
    private Animator anim;
    private bool grounded;

    // Start is called before the first frame update

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        grounded = true;
        finishText.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W)) 
        {
            if (grounded == true && finished == false)
            {
                Vector3 movement = new Vector3(0.0f, 0.0f, 1.5f);

                rb.AddForce(movement * speed);

                //transform.position += transform.forward * speed * Time.deltaTime;
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded == true && finished == false)
            {
                Vector3 jump = new Vector3(0.0f, 7, 0.2f);

                rb.AddForce(jump * speed);
                anim.SetTrigger("Jump");

            }
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
            
        }

        if (collision.gameObject.name == "FinishLine" && finished == false)
        {
            finishText.text = "You Finished in: " + (Time.time).ToString() + " Penalty: " + (Timer.instance.timeTackOn).ToString("f3"); //
            finished = true;
            anim.SetTrigger("Won");
        }

        if (collision.gameObject.tag == "Hurdle")
        {
            Timer.instance.hitHurdle();
            Debug.Log("Hit Hurdle");
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
