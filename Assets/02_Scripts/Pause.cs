using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static GameState;

public class Pause : MonoBehaviour {
    public void PauseOnClick() {
        if (GameStateManager.Instance.CurrentGameState == GameState.Gameplay) {
            GameStateManager.Instance.SetState(GameState.Paused);
        }
        else if (GameStateManager.Instance.CurrentGameState == GameState.Paused) {
            GameStateManager.Instance.SetState(GameState.Gameplay);
        }
    }
}
