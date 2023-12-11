using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Animator_Behaviour : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private Character_Behaviour characterBehaviour;

    private bool inBoat;

    private void Awake()
    {
        characterBehaviour = GetComponent<Character_Behaviour>();
    }

    // Update is called once per frame
    void Update()
    { 
        if(Game_Manager._GAME_MANAGER.GetCurrentGeneral() == GameGeneral.BOAT)
        {
            inBoat = true;
        }
        else
        {
            inBoat = false;
        }

        animator.SetBool("InBoat", inBoat);
        animator.SetFloat("Speed", characterBehaviour.GetSpeed());
    }

    public void PickUpAnimation()
    {
        animator.SetTrigger("PickUp");
    }

    public void BowAnimation()
    {
        animator.SetTrigger("Bow");
    }

    public void ThanksAnimation()
    {
        animator.SetTrigger("Thanks");
    }
}
