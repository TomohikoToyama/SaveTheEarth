using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


    public class SoundManager : MonoBehaviour
    {
    
    protected static SoundManager instance;

        public static SoundManager Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = (SoundManager)FindObjectOfType(typeof(SoundManager));

                    if (instance == null)
                    {
                        Debug.LogError("SoundManager Instance Error");

                    }
                }

                return instance;
            }

        }


        public SoundVolume volume = new SoundVolume();

        //
        private AudioSource BGMsource;

        private AudioSource[] SEsources = new AudioSource[16];

        public AudioClip[] BGM;
        public AudioClip[] SE;

        private void Awake()
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag("SoundManager");
            if (obj.Length > 1)
            {
                //既に存在しているなら削除
                Destroy(gameObject);
            }
            else
            {
                //音管理はシーン遷移で破棄しない
                DontDestroyOnLoad(gameObject);

            }

            //全てのAudioSourceコンポーネントを追加する

            // BGM AudioSource
            BGMsource = gameObject.AddComponent<AudioSource>();
            // BGMはループを有効にする
            BGMsource.loop = true;

            // SE AudioSource
            for (int i = 0; i < SEsources.Length; i++)
            {
                SEsources[i] = gameObject.AddComponent<AudioSource>();
            }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            // ミュート設定
            BGMsource.mute = volume.Mute;
            foreach (AudioSource source in SEsources)
            {
                source.mute = volume.Mute;
            }


            // ボリューム設定
            BGMsource.volume = volume.BGM;
            foreach (AudioSource source in SEsources)
            {
                source.volume = volume.SE;
            }
        }
        // BGM再生
        public void PlayBGM(int index)
        {
            if (0 > index || BGM.Length <= index)
            {
                return;
            }
            // 同じBGMの場合は何もしない
            if (BGMsource.clip == BGM[index])
            {
                return;
            }
            BGMsource.Stop();
            BGMsource.clip = BGM[index];
            BGMsource.Play();
        }

        // BGM停止
        public void StopBGM()
        {
            BGMsource.Stop();
            BGMsource.clip = null;
        }


        // SE再生
        public void PlaySE(int index)
        {
            if (0 > index || SE.Length <= index)
            {
                return;
            }

            // 再生中で無いAudioSouceで鳴らす
            foreach (AudioSource source in SEsources)
            {
                if (false == source.isPlaying)
                {
                    source.clip = SE[index];
                    source.Play();
                    return;
                }
            }
        }

        // SE停止
        public void StopSE()
        {
            // 全てのSE用のAudioSouceを停止する
            foreach (AudioSource source in SEsources)
            {
                source.Stop();
                source.clip = null;
            }
        }

    }

    //音量クラス
    [Serializable]
    public class SoundVolume
    {
        public float BGM = 1.0f;
        public float SE = 0.5f;
        public bool Mute = false;

        public void Init()
        {
            BGM = 1.0f;
            SE = 1.0f;
            Mute = false;
        }
    }
