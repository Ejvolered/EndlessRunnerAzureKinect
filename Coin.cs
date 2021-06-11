using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int fasterCounter;
    // Start is called before the first frame update
    void Start()
    {
        fasterCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(50 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerManager.numberOfCoins += 1;
            fasterCounter++;
            Debug.Log("Coins:" + PlayerManager.numberOfCoins);
            Destroy(gameObject);
        }

        if(fasterCounter == 10)
        {
            PlayerController.forwardSpeed++;
            fasterCounter = 0;
        }
    }
}
