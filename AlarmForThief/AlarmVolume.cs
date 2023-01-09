using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmVolume : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;

    private Coroutine _volumeChanger;
    private float _volume;
    private float _volumeScale;

    private void Start()
    {
        _volume = 0f;
        _audio.volume = _volume;
    }

    public void ChangeUp()
    {
        _audio.Play();
        StartChangeVolume(1);
    }

    public void ChangeDown()
    {
        StartChangeVolume(0);
    }

    private void StartChangeVolume(float target)
    {
        if (_volumeChanger != null)
        {
            StopCoroutine(_volumeChanger);
            _volumeChanger = StartCoroutine(ChangeVolume(target));
        }
        else
        {
            _volumeChanger = StartCoroutine(ChangeVolume(target));
        }
    }

    private IEnumerator ChangeVolume(float target)
    {
        _volumeScale = 0.0005f;

        while (_audio.volume != target)
        {

            _audio.volume = Mathf.MoveTowards(_audio.volume, target, _volumeScale);
            yield return null;

            if (_audio.volume == _volume)
            {
                _audio.Stop();
            }
        }
    }
}
