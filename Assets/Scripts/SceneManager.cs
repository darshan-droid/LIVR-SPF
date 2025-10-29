using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class SceneController : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene(2 );
    }
}
