using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : NonDestroyableSingleton<AudioManager>
{
    [SerializeField] private AudioClip obstacleReachAudio;
    [SerializeField] private AudioClip collectAudio;
    [SerializeField] private AudioClip berryFoundAudio;

    private AudioSource _audioSource;

    protected override void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        base.Awake();
    }

    private void OnEnable()
    {
        Actions.OnTongueReachedObstacle += PlayObstacleSound;
        Actions.OnTongueCollect += PlayCollectSound;
        Actions.OnBerryFound += PlayBerryFoundSound;
    }

    private void OnDisable()
    {
        Actions.OnTongueReachedObstacle -= PlayObstacleSound;
        Actions.OnTongueCollect -= PlayCollectSound;
        Actions.OnBerryFound -= PlayBerryFoundSound;
    }

    private void PlayObstacleSound()
    {
        _audioSource.PlayOneShot(obstacleReachAudio);
    }

    private void PlayCollectSound()
    {
        _audioSource.PlayOneShot(collectAudio);
    }

    private void PlayBerryFoundSound()
    {
        _audioSource.PlayOneShot(berryFoundAudio);
    }
}
