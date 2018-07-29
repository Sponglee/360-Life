
using UnityEngine;

//audio manager object class on scene load
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    private AudioSource source;
    [Range(0f,1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;


    //get all the clips to the 'pool'
    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Stop()
    {
        source.Stop();   
    }


    public void Play(bool rotClick = false, bool powUp = false)
    {
        source.volume = volume;
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2, randomPitch / 2));

        source.Play();
        //if(!source.isPlaying)
        //{
        //    source.Play();
        //}

    }
}




public class AudioManager : Singleton<AudioManager>
{

    [SerializeField]
    Sound[] sounds;



    void Start()
    {
        DontDestroyOnLoad(gameObject);


        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            //set the source
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(string _name, bool solo = false)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                if (solo)
                {
                    foreach(Sound sound in sounds)
                    {
                        sound.Stop();
                    }
                    sounds[i].Play();
                }
                else
                    sounds[i].Play();
                return;
                    
            }
        }
        //no sounds with that name
        Debug.Log("AudioManager: no sounds like that " + _name);
    }


    public void VolumeChange (float value)
    {
        PlayerPrefs.SetFloat("Volume", value);
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].volume = value;
        }
    }
}
