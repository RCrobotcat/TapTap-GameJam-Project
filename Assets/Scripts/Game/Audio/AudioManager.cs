using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    public AudioSource SfxAudio;
    public AudioSource footStepSource;
    public AudioClip PlayerWalk;
    public AudioClip PlayerJump;
    public AudioClip PlayerDash;
    public AudioClip PlayerDeath;
    public AudioClip PlayerHurted;
    public AudioClip PlayerClimb;
    public AudioClip MonsterWalk;
    public AudioClip MonsterJump;
    public AudioClip MonsterDeath;
    public AudioClip MonsterHurted;
    public AudioClip MonsterExplode;
    public AudioClip TurretPut;
    public AudioClip TurretATK;
    public AudioClip atkTowerPut;
    public AudioClip playerHeal;

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

    // footstep sound
    public void PlayWalkSfx()
    {
        footStepSource.PlayOneShot(PlayerWalk);
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