using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInstructions : MonoBehaviour
{
    public GameObject instructions;

    // Start is called before the first frame update
    void Start()
    {
        instructions.SetActive(true);
    }

}
