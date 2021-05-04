using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class SoundsManager : MGO.Singleton<SoundsManager>
{
    public SEConfig SEConfig;
    public BGMConfig BGMConfig;

    private Stack<AudioSource> SEPlayers = new Stack<AudioSource>();
    private AudioSource BGMPlayer;
    public void PlaySE(SEName name)
    {
        AudioSource source;
        if (!SEPlayers.Any())
        {
            source = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            source = SEPlayers.Pop();
        }

        var clip = SEConfig.SEList.First(pair => pair.Name == name).Clip;
        source.clip = clip;
        source.Play();
        StartCoroutine(WaitForSound(source));
    }

    private IEnumerator WaitForSound(AudioSource audiosource)
    {
        yield return new WaitUntil(() => audiosource.isPlaying == false);
        SEPlayers.Push(audiosource);
    }

    public float SwitchBGMTime = .5f;
    public float SmoothSeg = 300f;

    public async void PlayBGM(BGMName name)
    {
        int switchTime = (int)(SwitchBGMTime * 1000f);
        float oldVolume, changeVol;
        if (BGMPlayer == null)
        {
            BGMPlayer = gameObject.AddComponent<AudioSource>();
            oldVolume = BGMPlayer.volume;
        }
        else
        {
            oldVolume = BGMPlayer.volume;
            changeVol = oldVolume - .1f;
            while (BGMPlayer.volume > .1f)
            {
                BGMPlayer.volume -= changeVol / SmoothSeg;
                await Task.Delay(switchTime / (int)SmoothSeg);
            }
            BGMPlayer.Stop();
        }
        var clip = BGMConfig.BGMList.First(pair => pair.Name == name).Clip;
        BGMPlayer.clip = clip;
        BGMPlayer.loop = true;
        BGMPlayer.Play();
        changeVol = oldVolume - BGMPlayer.volume;

        while (BGMPlayer.volume < oldVolume)
        {
            BGMPlayer.volume += changeVol / SmoothSeg;
            await Task.Delay(switchTime / (int)SmoothSeg);
        }
    }
}
