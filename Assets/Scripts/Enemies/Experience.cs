using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    public int ExperiencePointsWorth { get; set; }

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Kill Coin
            Destroy(gameObject);
            // Give Exp
            player.experiencePoints += ExperiencePointsWorth;
        }
    }
}
