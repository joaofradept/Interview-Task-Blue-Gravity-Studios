using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePanel : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public virtual void Show()
    {
        anim.SetTrigger("Show");
    }

    public virtual void Hide()
    {
        anim.SetTrigger("Hide");
    }
}
