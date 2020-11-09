using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public class Quiz : MonoBehaviour
{
    [SerializeField]
    public string MyName;
    [SerializeField]
    GameObject[] FragenArray;
    [SerializeField]
    public bool isFinished;
    [SerializeField]
    VideoClip videoClip;


    public int quizIndex;

    public Fächer Fach;

    public void OpenVideoPanel()
    {
        UIManager.Instance.OpenVideoScreen();
        PlayVideo();
    }

    public void PlayVideo()
    {
        VideoManager.Instance.m_VideoClips.Clear();
        VideoManager.Instance.m_VideoClips.Add(videoClip);
        VideoManager.Instance.m_VideoPlayer.clip = videoClip;
        VideoManager.Instance.Play();
    }

    public void StartQuiz()
    {
        QuizManager.Instance.StartQuiz(FragenArray);
    }
}
