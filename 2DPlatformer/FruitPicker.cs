using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FruitPicker : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsText;
    private float _coinsAmount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Fruit fruit))
        {
            _coinsAmount++;
            _coinsText.text = _coinsAmount.ToString();
            Destroy(collision.gameObject);
        }
    }
}
