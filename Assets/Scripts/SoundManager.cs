using UnityEngine;
using UnityEngine.UI;

namespace TamQuocDefender
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;
        public AudioSource efxSource1;
        public AudioSource efxSource2;
        public AudioSource efxSource3;

        public AudioSource musicSource;
        public float lowPitchRange = 0.95f;
        public float highPitchRange = 1.05f;
        //public AudioClip audioCoin;
        public AudioClip audioButton;
        public AudioClip audioBgmLogin;
        public AudioClip audioBgmGamePlay;
        public AudioClip BuiltTowerclip;

        [SerializeField] private Image SoundONIcon;
        [SerializeField] private Image SoundOFFIcon;
        public bool muted = false;

        public AudioClip audioDefeat;
        public AudioClip audioWin;
        public AudioClip audioEnemyPass;
        public AudioClip audioEnemyDie;
        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            if (PlayerPrefs.GetInt("FirstPlay", 0) == 0)
            {
                PlayerPrefs.SetInt("FirstPlay", 1);
                PlayerPrefs.SetInt("MusicOn", 1);
                PlayerPrefs.SetInt("SoundOn", 1);
            }
        }

        public void AudioPass()
        {
            PlaySingle(this.audioEnemyPass);
        }

        public void AudioEnemyDie()
        {
            PlaySingle(this.audioEnemyDie);
        }

        //private void Start()
        //{
        //    if (!PlayerPrefs.HasKey("muted"))
        //    {
        //        PlayerPrefs.SetInt("muted", 0);
        //        Load();
        //    }
        //    else
        //    {
        //        Load();
        //    }
        //    UpdateButtonIcon();
        //    AudioListener.pause = muted;
        //}
        //Used to play single sound clips.
        public void PlaySingle(AudioClip clip)
        {
            if (efxSource1.isPlaying)
            {
                PlaySecond(clip);
            }
            else
            {
                efxSource1.PlayOneShot(clip);
            }
        }

        private void PlaySecond(AudioClip clip)
        {
            if (efxSource2.isPlaying)
            {
                PlayThird(clip);
            }
            else
            {
                efxSource2.PlayOneShot(clip);
            }
        }

        private void PlayThird(AudioClip clip)
        {
            efxSource3.PlayOneShot(clip);
        }

        public void PlayMusic(AudioClip clip)
        {
            //if (GameController.Instance.IsSoundOn)
            {
                musicSource.clip = clip;
                musicSource.Play();
            }
        }
        public void Mute()
        {
            musicSource.volume = 0;
            //efxSource1.volume = efxSource2.volume = efxSource3.volume = 0;
            PlayerPrefs.SetInt("MusicOn", 0);
        }

        public void ContinueMusic()
        {
            musicSource.volume = 0.5f;
            //efxSource1.volume = efxSource2.volume = efxSource3.volume = 1;
            PlayerPrefs.SetInt("MusicOn", 1);
        }
        public void ToggleSound(bool isOn)
        {

            if (!isOn)
            {
                efxSource1.volume = efxSource2.volume = efxSource3.volume = 0;
                PlayerPrefs.SetInt("SoundOn", 0);
                //PlayerPrefsX.SetBool("IsSoundOn", true);
            }
            else
            {
                efxSource1.volume = efxSource2.volume = efxSource3.volume = 1f;
                PlayerPrefs.SetInt("SoundOn", 1);
                //PlayerPrefsX.SetBool("IsSoundOn", false);
            }
            //if (withSound)
            //{
            //    PlaySingle(UISfxController.Instance.SfxSettingSound);
            //}
        }

        public void OnButtonPress()
        {
            //PressButtonAudio();
            //PlaySingle(audioSide);
            if (muted == false)
            {
                muted = true;
                AudioListener.pause = true;
            }
            else
            {
                muted = false;
                AudioListener.pause = false;
            }
            Save();
            UpdateButtonIcon();
        }
        public void UpdateButtonIcon()
        {
            if (muted == false)
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
        private void Load()
        {
            muted = PlayerPrefs.GetInt("muted") == 1;
        }
        private void Save()
        {
            PlayerPrefs.SetInt("muted", muted ? 1 : 0);
        }

        //public void ChangeAudioSpeed()
        //{
        //    if (MenuController.Instance.Menu == Menu.Arena)
        //    {
        //        if (efxSource1 != null && efxSource2 != null && efxSource3 != null)
        //        {
        //            efxSource1.pitch = BattleController.instance.SpeedGame;
        //            efxSource2.pitch = BattleController.instance.SpeedGame;
        //            efxSource3.pitch = BattleController.instance.SpeedGame;
        //            Debug.Log("efxSource1.volume " + efxSource1.pitch + "efxSource2.volume " + efxSource2.pitch + "efxSourc3.volume " + efxSource3.pitch);
        //            Debug.Log("SpeedGame" + BattleController.instance.SpeedGame);
        //        }
        //        else
        //        {
        //            Debug.Log("chua gan âm thanh");
        //        }
        //    }
        //    else if (MenuController.Instance.Menu == Menu.Camp)
        //    {
        //        if (efxSource1 != null && efxSource2 != null && efxSource3 != null)
        //        {
        //            efxSource1.pitch = 1;
        //            efxSource2.pitch = 1;
        //            efxSource3.pitch = 1;
        //            Debug.Log("efxSource1.volume " + efxSource1.pitch + "efxSource2.volume " + efxSource2.pitch + "efxSourc3.volume " + efxSource3.pitch);
        //        }
        //        else
        //        {
        //            Debug.Log("chua gan âm thanh");
        //        }
        //    }
        //    else if (MenuController.Instance.Menu == Menu.Home)
        //    {
        //        if (efxSourceBoss1 != null && efxSourceBoss2 != null && efxSourceBoss3 != null)
        //        {
        //            efxSourceBoss1.pitch = 1;
        //            efxSourceBoss2.pitch = 1;
        //            efxSourceBoss3.pitch = 1;
        //            Debug.Log("efxSource1.volume " + efxSource1.pitch + "efxSource2.volume " + efxSource2.pitch + "efxSourc3.volume " + efxSource3.pitch);
        //        }
        //        else
        //        {
        //            Debug.Log("chua gan âm thanh");
        //        }
        //    }
        //}
        public void StopAudio()
        {
            if (efxSource1 != null && efxSource2 != null && efxSource3 != null)
            {
                efxSource1.Stop();
                efxSource2.Stop();
                efxSource3.Stop();
            }
            else
            {
                Debug.Log("chua gan âm thanh");
            }
        }
        public void OnsoundArena()
        {
            efxSource1.volume = 1;
            efxSource2.volume = 1;
            efxSource3.volume = 1;
        }
        public void OffSonud()
        {
            efxSource1.volume = 0;
            efxSource2.volume = 0;
            efxSource3.volume = 0;
        }

        public void AudioButton()
        {
            PlaySingle(this.audioButton);
        }

        public void AudioExitButton() 
        {

        }

        public void AudioBgStartGame()
        {
            PlayMusic(this.audioBgmLogin);
        }

        public void AudioBgGamePlay()
        {
            PlayMusic(this.audioBgmGamePlay);
        }
        public void BuiltTower()
        {
            PlaySingle(this.BuiltTowerclip);
        }

        public void AudioWin()
        {
            PlaySingle(this.audioWin);
            this.musicSource.Stop();
        }
        public void AudioDefeat()
        {
            PlaySingle(this.audioDefeat);
            this.musicSource.Stop();
        }
    }


}