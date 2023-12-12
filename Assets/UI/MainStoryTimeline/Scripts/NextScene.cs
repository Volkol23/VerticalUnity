using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    private void OnEnable()
    {
        Game_Manager._GAME_MANAGER.GoToScene("IntroScene");
    }
}
