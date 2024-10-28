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
    public void PlaySfx(AudioClip clip)//��Ч���ŵ��÷���
    {
        SfxAudio.PlayOneShot(clip);
    }

    public void PlayFootStep(AudioClip clip)
    {
        footStepSource.PlayOneShot(clip);
    }

    public void RandomPlaySfx(AudioClip clip)//��Ч���ʲ��ŵ��÷���
    {
        int R = Random.Range(1, 11);
        if (R > 5)
        {
            SfxAudio.PlayOneShot(clip);
        }
    }

    public void DoubleRandomPlaySfx(AudioClip clip, AudioClip clip02)//��������Ч�������������һ��
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