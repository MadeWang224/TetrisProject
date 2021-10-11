using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 音效管理模块
/// Music目录存放音乐，/Sounds存放音效，/bk存放背景音乐
/// </summary>
public class MusicMgr : BaseManager<MusicMgr>
{
    /// <summary>
    /// 背景音乐
    /// </summary>
    private AudioSource bkMusic = null;

    /// <summary>
    /// 背景音乐权值
    /// </summary>
    private  float bkVaule = 1;

    /// <summary>
    /// 音效权值
    /// </summary>
    private float soundVaule = 1;

    /// <summary>
    /// 音效挂载的父物体
    /// </summary>
    private GameObject soundObj = null;

    /// <summary>
    /// 音效列表
    /// </summary>
    private List<AudioSource> soundList = new List<AudioSource>();

    /// <summary>
    /// 添加到update
    /// </summary>
    public MusicMgr() {
        MonoMgr.GetInstance().AddUpdateListener(update);
    }
    private void update() {
        for (int i = soundList.Count-1; i >= 0; i--) {
            if (!soundList[i].isPlaying) {
                GameObject.Destroy(soundList[i]);
                soundList.RemoveAt(i);
            }
        }
    }


    //播放背景音乐
    public void PlayBKMusic(string name) {
        if (bkMusic == null) {
            GameObject obj = new GameObject("BKMusic");
            bkMusic = obj.AddComponent<AudioSource>();
        }
        //异步加载背景音乐并且加载完成后播放
        ResMgr.GetInstance().LoadAsync<AudioClip>("Music/bk/"+name,(clip) => {
            bkMusic.clip = clip;
            bkMusic.loop = true;
            //调整大小 
            bkMusic.volume = bkVaule;
            bkMusic.Play();
        });
    }
    //改变音量大小
    public void ChangeBKValue(float v) {
        bkVaule = v;
        if (bkMusic == null) {
            return;
        }
        bkMusic.volume = bkVaule;
    }
    //暂停背景音乐
    public void PauseBKMusic() {
        if (bkMusic == null)
        {
            return;
        }
        bkMusic.Pause();  
    }

    //停止背景音乐
    public void StopBKMusic() {
        if (bkMusic == null) {
            return;
        }
        bkMusic.Stop();
    }


    //播放音效
    public void PlaySound(string name,bool isLoop,UnityAction<AudioSource> callback=null ) {
        if (soundObj == null) {
            soundObj = new GameObject();
            soundObj.name = "Sounds";
        }
        AudioSource source=soundObj.AddComponent<AudioSource>();
        ResMgr.GetInstance().LoadAsync<AudioClip>("Music/Sounds/" + name, (clip) => {
            source.clip = clip;
            source.loop = isLoop;
            //调整大小 
            source.volume = soundVaule;
            source.Play();
            //音效资源异步加载结束后，将这个音效组件加入集合中
            soundList.Add(source);
            if (callback != null) {
                callback(source);
            }
        });
    }
    //改变所有音效大小
    public void ChangeSoundValue(float value) {
        soundVaule = value;
        for (int i = 0; i < soundList.Count; ++i) {
            soundList[i].volume = value;
        }
    }
    //停止音效
    public void StopSound(AudioSource source) {
        if (soundList.Contains(source)) {
            soundList.Remove(source);
            source.Stop();
            GameObject.Destroy(source);
        }
    }
}
