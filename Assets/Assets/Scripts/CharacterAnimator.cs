using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public Animator anim;
    
    void Start()
    {
        anim.GetComponent<Animator>();
    }

    public void Press1()
    {
        anim.SetTrigger("press1");
    }
    
    public void Press2()
    {
        anim.SetTrigger("press2");
    }

    public void Press3()
    {
        anim.SetTrigger("press3");
    }

    public void Press4()
    {
        anim.SetTrigger("press4");
    }

    public void MissANote()
    {
        anim.SetTrigger("miss");
    }

    public void LoseGame()
    {
        anim.SetTrigger("miss");
        anim.SetBool("lose", true);
        
    }

    public void Won()
    {
        anim.SetBool("won", true);
    }

    




   
    

}
