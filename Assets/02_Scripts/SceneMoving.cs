using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMoving : MonoBehaviour {
    [SerializeField]
    private AudioSource musicPlayer;
    [SerializeField]
    private AudioClip sfx_menu;

    public void MoveScene(string SceneName) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
        musicPlayer.PlayOneShot(sfx_menu);
    }
}
