using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentController : MonoBehaviour
{

    public float speed;
    public Text finishText;
    public bool finished;
    public static OpponentController instance;


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

        finished = false;
        grounded = true;

        finishText.text = "";

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (grounded == true && finished == false)
        {
            Vector3 movement = new Vector3(0.0f, 0.0f, 1);

            rb.AddForce(movement * speed);

            //transform.position += transform.forward * speed * Time.deltaTime;
        }

        if (Time.time % 1 == 0)
        {
            if (grounded == true && finished == false)
            {
                Vector3 jump = new Vector3(0.0f, 20, 0.2f);

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
            finishText.text = "Opponent Finished In: " + (Time.time).ToString() + " Penalty: " + (Timer.instance.opponentPenalty).ToString("f3"); //
            finished = true;
            anim.SetTrigger("Finished");
        }

        if (collision.gameObject.tag == "Hurdle")
        {
            Timer.instance.opponentHitHurdle();
            Debug.Log("Opponent Hit Hurdle");
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
        if (other.gameObject.tag == "Finish")
        {
            finishText.text = "Opponent Finished In: " + (Time.time).ToString() + " Penalty: " + (Timer.instance.opponentPenalty).ToString("f3"); //
            finished = true;
            anim.SetTrigger("Finished");

            Destroy(other.gameObject);
        }
    }
}
