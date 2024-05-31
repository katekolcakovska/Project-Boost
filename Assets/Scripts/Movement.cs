using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float upForce = 100f;
    [SerializeField] float rotationForce = 100f;
    [SerializeField] AudioClip flySound;

    [SerializeField] ParticleSystem leftEngineEffect;
    [SerializeField] ParticleSystem rightEngineEffect;
    [SerializeField] ParticleSystem mainEngineEffect;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Fly();
        Rotate();
    }

    void Fly()
    {
        if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
        {
            StartFlying();

        }
        else
        {
            StopFlying();
        }

    }

    private void StopFlying()
    {
        audioSource.Stop();
        mainEngineEffect.Stop();
    }

    void StartFlying()
    {
        rb.AddRelativeForce(Vector3.up * upForce * Time.deltaTime); //Vector3.up e isto so i (0,1,0)

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(flySound);
        }
        if (!mainEngineEffect.isPlaying)
        {
            mainEngineEffect.Play();
        }
    }

    void Rotate()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    private void StopRotating()
    {
        rightEngineEffect.Stop();
        leftEngineEffect.Stop();
    }

    private void RotateRight()
    {
        ApplyRotate(-rotationForce);
        if (!leftEngineEffect.isPlaying)
        {
            leftEngineEffect.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotate(rotationForce);
        if (!rightEngineEffect.isPlaying)
        {
            rightEngineEffect.Play();
        }
    }

    void ApplyRotate(float rotationSignThisFrame)
    {
        rb.freezeRotation = true; // freeze rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationSignThisFrame * Time.deltaTime); // Vector3.forward e (0,0,1)
        rb.freezeRotation = false; //unfreezing rotation so the physics system can take over
    }
}
