using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBox : MonoBehaviour {

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetAnimation(int shakeDegree)
    {
        anim.SetInteger("ShakeDegree", shakeDegree);
    }
}
