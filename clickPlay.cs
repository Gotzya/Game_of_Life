using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clickPlay : MonoBehaviour
{
    public void playGame()
    {

        SceneManager.LoadScene(1);

    }

    public void quitGame()
    {
        Debug.Log("Exited program");
        Application.Quit();
    }
}
