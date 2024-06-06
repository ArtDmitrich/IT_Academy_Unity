using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _value;
    [SerializeField] private string _tagToDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_tagToDetected))
        {
            GameController.Instance.ChangeCoinsCount(_value);
            gameObject.SetActive(false);
        }
    }
}
