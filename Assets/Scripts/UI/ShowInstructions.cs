using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInstructions : MonoBehaviour
{
    [SerializeField] private GameObject instructions;

    void Start()
    {
        instructions.SetActive(true);
    }

}
