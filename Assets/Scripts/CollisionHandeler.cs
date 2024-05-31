using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandeler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;

    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip finishSound;
    [SerializeField] float soundVolume = 1f;

    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] ParticleSystem finishEffect;
    
    Movement movement;
    AudioSource audioSource;
   

    bool isBetweenStates = false;
    bool collisionDisabled = false;
    void Start()
    {
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        RespondToCheatKeys();
    }
    void OnCollisionEnter(Collision other)
    {

        if (isBetweenStates || collisionDisabled) { return; }

        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collided with Freiendly");
                break;
            case "Finish":
                Finish();
                break;
            default:
                Crash();
                break;

        }
    }

    void Crash()
    {
        isBetweenStates = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound, soundVolume);
        crashEffect.Play();
        movement.enabled = false;
        
        Invoke("ReloadLevel", loadDelay);
        
    }

    void Finish()
    {
        isBetweenStates = true;
        audioSource.Stop();
        audioSource.PlayOneShot(finishSound, soundVolume);
        finishEffect.Play();
        movement.enabled = false;
        
        Invoke("LoadNextLevel", loadDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        
        SceneManager.LoadScene(nextSceneIndex);


    }


    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void NextLevelCheat()
    {
        LoadNextLevel();
    }

    void NoCollisionCheat()
    {
        collisionDisabled = !collisionDisabled; //toggle collision
    }

    void RespondToCheatKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            NextLevelCheat();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            NoCollisionCheat();
        }
    }

}
