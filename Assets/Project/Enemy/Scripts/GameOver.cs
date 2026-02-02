using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
            UnityEngine.SceneManagement.SceneManager.
                LoadScene("ResultScene");
            }
        }
    }
