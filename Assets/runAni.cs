using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runAni : MonoBehaviour
{
    public Animator ani;

    public void OnButtonPressed()
    {
        ani.SetTrigger("Door");
    }
}
