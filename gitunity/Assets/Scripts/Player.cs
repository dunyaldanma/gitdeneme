using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] ParticleSystem partik;
    [SerializeField] float forceAmount = 1000.0f;
    [SerializeField] float rotationAmount = 1000.0f;
    [SerializeField] public int stars = 0;
    [SerializeField] float LoadDelay = 2f;
    private float initialYRotation;
    private float initialZRotation;
    public int fuel;

    bool switchControl = false; 
    public bool isAlive = true;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isAlive = true;
        Vector3 initialRotation = transform.rotation.eulerAngles;
        initialYRotation = initialRotation.y;
        initialZRotation = initialRotation.z;
        fuel = 100;
    }

    void Update()
    {
        ForceProcess();
        RotationProcess();

        if(isAlive == false)
        {
            Debug.Log("You are dead");
            Invoke("ReloadLevel", LoadDelay);
        }
    }
    private void ForceProcess()
    {
        if(Input.GetKey(KeyCode.Space) && isAlive == true)
        {
            rb.AddRelativeForce(Vector3.up * forceAmount * Time.deltaTime);
            if (!partik.isPlaying)
            {
                partik.Play();
            }
        }
        else
        {
            partik.Stop();
        }
    }
    private void RotationProcess()
    {
        if (Input.GetKey(KeyCode.A) && switchControl == false && isAlive == true)
        {
            ApplyRotation(rotationAmount);
        }
        else if(Input.GetKey(KeyCode.A) && switchControl == true && isAlive == true)
        {
            ApplyRotation(-rotationAmount);
        }
        else if (Input.GetKey(KeyCode.D) && switchControl == false && isAlive == true)
        {
            ApplyRotation(-rotationAmount);
        }
        else if(Input.GetKey(KeyCode.D) && switchControl == true && isAlive == true)
        {
            ApplyRotation(rotationAmount);
        }
    }
    private void ApplyRotation(float rotationDirection)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.right * rotationDirection * Time.deltaTime);
        rb.freezeRotation = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.y = initialYRotation;
        currentRotation.z = initialZRotation;
        transform.rotation = Quaternion.Euler(currentRotation);
        if (collision.gameObject.tag == "PowerUp")
        {
            forceAmount += 300;
            Destroy(collision.gameObject);
            Invoke("forceplus", 10f);
        }
        if (collision.gameObject.tag == "PowerDown")
        {
            forceAmount -= 300;
            Destroy(collision.gameObject);
            Invoke("forcemines", 10f);
        }
        if (collision.gameObject.tag == "Star")
        {
            stars += 1;
        }
        if(collision.gameObject.tag == "Obstacle")
        {
            isAlive = false;
        }
        if (collision.gameObject.tag == "Switch")
        {
            switchControl = true;
            Destroy(collision.gameObject);
            Invoke("ResetSwitchControl", 10f);
        }
        if (collision.gameObject.tag == "Fuel")
        {
            Destroy(collision.gameObject);
        }
    }

    void ResetSwitchControl()
    {
        switchControl = false;
    }
    void forcemines()
    {
        forceAmount += 300;
    }
    void forceplus()
    {
        forceAmount -= 300;
    }

    void ReloadLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelIndex);
    }
    
}
