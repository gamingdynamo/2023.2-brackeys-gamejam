using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class CutsceneTrigger : MonoBehaviour
{
    VideoPlayer videoplayer;
    public string nextscene;
    public bool isplaying;
    public RawImage videoscreen;
    public void Start()
    {
        videoplayer = GetComponent<VideoPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PlayCutscene());
        }
    }

    public IEnumerator PlayCutscene()
    {
        videoplayer.Prepare();
        while (!videoplayer.isPrepared)
        {
            yield return null;
        }
        videoscreen.texture = videoplayer.texture;

        videoplayer.Play();
       
        while (videoplayer.isPlaying)
        {
            yield return null;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextscene);

        while (!asyncLoad.isDone)
        {
            Debug.Log("sceneload progess: " + asyncLoad.progress);
            yield return null;
        }

    }
}
