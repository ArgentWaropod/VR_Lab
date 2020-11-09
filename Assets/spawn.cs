using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject prefab;
    public void OnButtonPress()
    {
        Instantiate(prefab, gameObject.transform);
    }
}
