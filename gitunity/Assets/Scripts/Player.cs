using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] ParticleSystem partik;
    [SerializeField] ParticleSystem pat;
    [SerializeField] ParticleSystem ded;
    [SerializeField] ParticleSystem fini;
    [SerializeField] float forceAmount = 1000.0f;
    [SerializeField] float rotationAmount = 1000.0f;
    [SerializeField] public int stars = 0;
    [SerializeField] float LoadDelay = 2f;
    [SerializeField] float patdelay = 0.5f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip endsound;
    [SerializeField] AudioClip flysound;
    [SerializeField] AudioClip starSound;
    [SerializeField] AudioClip obsSound;
    [SerializeField] AudioClip fuelSound;
    AudioSource audioSource;
    private float initialYRotation;
    private float initialZRotation;
    public float fuel;
    public float fuelConsumptionRate = 5f;

    bool switchControl = false; 
    public bool isAlive = true;
    public bool isFinish = false;
    

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        isAlive = true;
        isFinish = false;
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
        if (Input.GetKey(KeyCode.Space) && isAlive == true && isFinish == false)
        {
            fuel -= fuelConsumptionRate * Time.deltaTime;
        }
        if(fuel < 1)
        {
            isAlive = false;
        }
        if (isFinish == true)
        {
            audioSource.PlayOneShot(endsound);
            rb.AddRelativeForce(Vector3.up * forceAmount * Time.deltaTime);
            if (!fini.isPlaying)
            {
                fini.Play();
            }
        }
    }
    private void ForceProcess()
    {
        if(Input.GetKey(KeyCode.Space) && isAlive == true && isFinish == false)
        {
            rb.AddRelativeForce(Vector3.up * forceAmount * Time.deltaTime);
            if (!partik.isPlaying)
            {
                partik.Play();
            }
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(flysound);
            }
        }
        else
        {
            partik.Stop();
            audioSource.Stop();
        }
    }
    private void RotationProcess()
    {
        if (Input.GetKey(KeyCode.A) && switchControl == false && isAlive == true && isFinish == false)
        {
            ApplyRotation(rotationAmount);
        }
        else if(Input.GetKey(KeyCode.A) && switchControl == true && isAlive == true && isFinish == false)
        {
            ApplyRotation(-rotationAmount);
        }
        else if (Input.GetKey(KeyCode.D) && switchControl == false && isAlive == true && isFinish == false)
        {
            ApplyRotation(-rotationAmount);
        }
        else if(Input.GetKey(KeyCode.D) && switchControl == true && isAlive == true && isFinish == false)
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
            audioSource.PlayOneShot(starSound);
        }
        if(collision.gameObject.tag == "Obstacle")
        {
            isAlive = false;
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(obsSound);
            }
        }
        if (collision.gameObject.tag == "Switch")
        {
            switchControl = true;
            Destroy(collision.gameObject);
            Invoke("ResetSwitchControl", 10f);
        }
        if (collision.gameObject.tag == "Fuel")
        {
            audioSource.PlayOneShot(fuelSound);
            Destroy(collision.gameObject);
            if (fuel <= 20)
            {
                fuel += 80;
            }
            if (fuel > 20)
            {
                fuel += 100 - fuel;
            }
        }
        if (collision.gameObject.tag == "Fall")
        {
            //audioSource.PlayOneShot(crashSound);
            Destroy(collision.gameObject);
            isAlive = false;
        }
        if(collision.gameObject.tag == "Finish" && stars == 3)
        {
            Destroy(collision.gameObject);
            isFinish = true;
            Invoke("NextLevel", LoadDelay);

        }
        else if(collision.gameObject.tag == "Finish" && stars < 3)
        {
            Destroy(collision.gameObject);
            isFinish = true;
            Invoke("ReloadLevel", LoadDelay);
        }
        if( collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Border")
        {
            audioSource.PlayOneShot(obsSound);
            if (!pat.isPlaying)
            {
                pat.Play();
                Invoke("patStop", patdelay);
            }
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            if (!ded.isPlaying)
            {
                ded.Play();
                Invoke("dedStop", patdelay);
            }
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
    void patStop()
    {
        pat.Stop();
    }
    void dedStop()
    {
        ded.Stop();
    }

    void ReloadLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelIndex);
    }
    void NextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int NextLevelIndex = currentLevelIndex + 1;

        if (NextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            NextLevelIndex = 0;
        }
        SceneManager.LoadScene(NextLevelIndex);
    }


}
