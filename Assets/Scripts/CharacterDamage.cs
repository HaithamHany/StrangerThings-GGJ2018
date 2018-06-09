using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamage : MonoBehaviour {

    private Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag=="goblinTag")
        anim.SetTrigger("Damage");

    }
    
}
