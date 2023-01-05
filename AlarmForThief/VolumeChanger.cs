using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeChanger : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _duration;
    private float _volume;
    private float _volumeScale;
    private float _runningTime;
    private float _target;

    private void Start()
    {
        _volume= 0f;
        _audio.volume = _volume;
    }

    public void Change()
    {
        _runningTime += Time.deltaTime;
        _volumeScale = _runningTime / _duration;

        if(_audio.volume == _volume )
        {
            _audio.Play();
            _target = 1f;
            var volumeChanger = StartCoroutine(ChangeVolume(_target));
        }
        else
        {
            _target = 0f;
            var volumeChanger = StartCoroutine(ChangeVolume(_target));
        }
    }

    private IEnumerator ChangeVolume(float target)
    {
        while (_audio.volume != target)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, _target, _volumeScale);
            yield return null;
        }
    }
}
