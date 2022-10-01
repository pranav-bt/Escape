using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource AS;

    [SerializeField] private AudioClip DamagePlayerAudio;
    [SerializeField] private AudioClip CharacterSwitchAudio;
    [SerializeField] private AudioClip CollectibleAudio;
    [SerializeField] private AudioClip GameOverAudio;
    [SerializeField] public AudioClip PlayerWinAudio;
    // Start is called before the first frame update

    private void OnEnable()
    {
        EventManager.DamageEvent += DamagePlayerAudioPlay;
        EventManager.GameOver += GameOverAudioPlay;
        EventManager.CharacterSwitch += CharacterSwitchAudioPlay;
        EventManager.CoinCollected += CollectibleAudioPlay;
        EventManager.PlayerWin += PlayerWinAudioPlay;
    }

    private void OnDisable()
    {
        EventManager.DamageEvent -= DamagePlayerAudioPlay;
        EventManager.GameOver -= GameOverAudioPlay;
        EventManager.CharacterSwitch -= CharacterSwitchAudioPlay;
        EventManager.CoinCollected -= CollectibleAudioPlay;
        EventManager.PlayerWin -= PlayerWinAudioPlay;
    }
    public void PlayOneShot(AudioClip AudioToPlay)
    {
        AS.PlayOneShot(AudioToPlay);
    }

    public void DamagePlayerAudioPlay()
    {
        PlayOneShot(DamagePlayerAudio);
    }
    public void CharacterSwitchAudioPlay()
    {
        PlayOneShot(CharacterSwitchAudio);
    }
    public void CollectibleAudioPlay()
    {
        PlayOneShot(CollectibleAudio);
    }
    public void GameOverAudioPlay()
    {
        PlayOneShot(GameOverAudio);
    }
    public void PlayerWinAudioPlay()
    {
        PlayOneShot(PlayerWinAudio);
    }

}
