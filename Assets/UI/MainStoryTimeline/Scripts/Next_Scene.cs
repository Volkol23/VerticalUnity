using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next_Scene : MonoBehaviour
{
    private void OnEnable()
    {
        Game_Manager._GAME_MANAGER.GoToScene((int)SceneIndex.INTROSCENE);
    }
}
