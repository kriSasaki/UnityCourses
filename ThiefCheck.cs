using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class ThiefCheck : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _duration;

    private float _volumeScale;
    private float _runningTime;
    private float _target = 1f;
    private bool IsPlaying;

    private void Start()
    {
        _audio.volume = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            _runningTime += Time.deltaTime;
            _volumeScale = _runningTime / _duration;
            IsPlaying= true;
            _target = 1f;
            _audio.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            IsPlaying = false;
            _target = 0f;
        }
    }

    private void Update()
    {
        if (IsPlaying)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, _target, _volumeScale);
        }
        else
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, _target, _volumeScale);
        }
    }
}
