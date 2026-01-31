using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private string winSceneName = "WinScene";

    private bool hasWon = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("asdas");
        if (hasWon) {return;} //bc of in update, this function may be called multiple times

        if (other.CompareTag(playerTag))
        {
            hasWon = true;
            WinGame();
        }
    }

void Start()
{
    Debug.Log("asd");
}

   private void WinGame()
   {
    Debug.Log ("You WIn!");

    if(!string.IsNullOrEmpty(winSceneName))
    {
        SceneManager.LoadScene(winSceneName);
    }

    Time.timeScale = 0f;
   }
}