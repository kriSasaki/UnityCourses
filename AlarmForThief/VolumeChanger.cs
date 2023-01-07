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
        _volume = 0f;
        _audio.volume = _volume;
    }

    public void ChangeUp()
    {
        _audio.Play();
        var volumeChanger = StartCoroutine(ChangeVolume(1));
    }

    public void ChangeDown()
    {
        var volumeChanger = StartCoroutine(ChangeVolume(0));
    }

    private IEnumerator ChangeVolume(float target)
    {
            _runningTime += Time.deltaTime;
            _volumeScale = _runningTime / _duration;
        while (_audio.volume != target)
        {

            _audio.volume = Mathf.MoveTowards(_audio.volume, target, _volumeScale);
            yield return null;

            if(_audio.volume == _volume)
            {
                _audio.Stop();
            }
        }
    }
}
