using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource SfxAudio;
    public AudioSource footStepSource;
    public AudioClip PlayerWalk_solid;
    public AudioClip PlayerWalk_soft;
    public AudioClip PlayerRun_solid;
    public AudioClip PlayerRun_soft;
    public AudioClip PlayerSlash;
    public AudioClip PlayerSlash_target;
    public AudioClip JealousHitPlane;
    public AudioClip collectBean;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // Update is called once per frame
    public void PlaySfx(AudioClip clip)//音效播放调用方法
    {
        SfxAudio.PlayOneShot(clip);
    }

    public void PlayFootStep(AudioClip clip)
    {
        footStepSource.PlayOneShot(clip);
    }

    public void RandomPlaySfx(AudioClip clip)//音效概率播放调用方法
    {
        int R = Random.Range(1, 11);
        if (R > 5)
        {
            SfxAudio.PlayOneShot(clip);
        }
    }

    public void DoubleRandomPlaySfx(AudioClip clip, AudioClip clip02)//在两种音效中随机调用其中一种
    {
        int R = Random.Range(1, 11);
        if (R > 5)
        {
            SfxAudio.PlayOneShot(clip);
        }
        else
        {
            SfxAudio.PlayOneShot(clip02);
        }
    }
}