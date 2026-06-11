using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("播放器频道设置")]
    public AudioSource bgmSource;       // 放背景音乐
    public AudioSource sfxSource;       // 放敲门、擦神灯、牛出现的单次音效
    public AudioSource footstepSource;  // 【新增】专门放脚步声的独立频道

    [Header("音效文件 (Audio Clips)")]
    public AudioClip backgroundMusic;
    public AudioClip buttonClickSound;
    public AudioClip doorKnockSound;
    public AudioClip popUpSound;
    public AudioClip rubLampSound;
    public AudioClip footstepSound;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // 自动播放 BGM
        if (backgroundMusic != null)
        {
            bgmSource.clip = backgroundMusic;
            bgmSource.loop = true;
            bgmSource.Play();
        }

        // 初始化脚步声
        if (footstepSound != null && footstepSource != null)
        {
            footstepSource.clip = footstepSound;
            // 【关键修改】改成不循环！让它自然播完
            footstepSource.loop = false;
        }
    }

    // ================= 播放单次音效的通用方法 =================
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    // 提供给外部一键调用的快捷方法
    public void PlayButtonSound() { PlaySFX(buttonClickSound); }
    public void PlayDoorKnock() { PlaySFX(doorKnockSound); }
    public void PlayPopUp() { PlaySFX(popUpSound); }
    public void PlayRubLamp() { PlaySFX(rubLampSound); }

    // ================= 控制脚步声的开关 =================
    public void PlayFootstep()
    {
        if (footstepSource == null || footstepSound == null) return;

        // 【关键修改】如果没在播放，才播放一次。如果正在播放，绝对不打断它！
        if (!footstepSource.isPlaying)
        {
            footstepSource.Play();
        }
    }
}