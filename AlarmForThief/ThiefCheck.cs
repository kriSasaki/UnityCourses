using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThiefChecker : MonoBehaviour
{
    [SerializeField] private UnityEvent _Entered;
    [SerializeField] private UnityEvent _CameOut;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _Entered?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _CameOut?.Invoke();
        }
    }
}
