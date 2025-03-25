using TamQuocDefender;
using UnityEngine;
using UnityEngine.UI;

public class OnOffSound : MonoBehaviour
{
    [SerializeField] private Image SoundONIcon;
    [SerializeField] private Image SoundOFFIcon;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }
        UpdateButtonIcon();
        AudioListener.pause = SoundManager.instance.muted;
    }
    public void UpdateButtonIcon()
    {
        if (SoundManager.instance.muted == false)
        {
            SoundONIcon.enabled = true;
            SoundOFFIcon.enabled = false;
        }
        else
        {
            SoundONIcon.enabled = false;
            SoundOFFIcon.enabled = true;
        }
    }
    public void OnButtonPress()
    {
        //PressButtonAudio();
        //PlaySingle(audioSide);
        if (SoundManager.instance.muted == false)
        {
            SoundManager.instance.muted = true;
            AudioListener.pause = true;
        }
        else
        {
            SoundManager.instance.muted = false;
            AudioListener.pause = false;
        }
        Save();
        UpdateButtonIcon();
    }
    private void Save()
    {
        PlayerPrefs.SetInt("muted", SoundManager.instance.muted ? 1 : 0);
    }
    private void Load()
    {
        SoundManager.instance.muted = PlayerPrefs.GetInt("muted") == 1;
    }
}
