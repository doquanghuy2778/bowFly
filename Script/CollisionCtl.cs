using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionCtl : MonoBehaviour
{
    [SerializeField] private AudioClip finishAudio;
    [SerializeField] private AudioClip deathAudio;
    [SerializeField] private ParticleSystem finishParticle;
    [SerializeField] private ParticleSystem deathParticle;
    

    private AudioSource audioSource;
    private bool isCollision = false;   

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //private void Update()
    //{
    //    ReponNextLevel();
    //    ReponNoCollision();
        
    //}

    private void ReponNextLevel()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            nextScene();
        }
    }

    private void ReponNoCollision()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("kichhoatC");
            isCollision = !isCollision;
        } 
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(isCollision)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Finish":                  
                audioSource.Stop();
                audioSource.PlayOneShot(finishAudio);
                finishParticle.Play();
                GetComponent<MoveCtl>().enabled = false;
                Invoke("nextScene", 1f);                
                break;          
            case "Start":                
                break;
            case "Chuongngai":                
                audioSource.Stop();
                audioSource.PlayOneShot(deathAudio);
                deathParticle.Play();
                GetComponent<MoveCtl>().enabled = false;
                Invoke("resetScene", 1f);               
                break;
            default:               
                audioSource.Stop();
                audioSource.PlayOneShot(deathAudio);
                deathParticle.Play();
                GetComponent<MoveCtl>().enabled = false;
                Invoke("resetScene", 1f);              
                break;
        }        
    }

    //chuyen scene khong dung so
    void nextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if(nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }   

    void resetScene()
    {
        SceneManager.LoadScene(1);
    }    
}
11