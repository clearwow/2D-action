using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    [SerializeField] GameObject gameoverTextObj;
    [SerializeField] GameObject gameclearTextObj;


    public void GameOver()
    {
        gameoverTextObj.SetActive(true);    
        Invoke("ReStart", 4f);
    }

    public void GameClear()
    {
        gameclearTextObj.SetActive(true);
        Invoke("ReStart", 4f);
    }



    void ReStart()
    {   
            SceneManager.LoadScene("SampleScene");
    }
}