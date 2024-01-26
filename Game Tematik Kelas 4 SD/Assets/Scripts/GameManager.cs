using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
        AsignAliasForSfx();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    #region SoundSfx
    private Dictionary<string, AudioClip> sfxDictionary = new Dictionary<string, AudioClip>();
    public AudioSource audioSource;
    public AudioClip[] allSfx;
    private void AsignAliasForSfx()
    {
        sfxDictionary.Add("Choice", allSfx[0]); 
        sfxDictionary.Add("Swipe", allSfx[1]);
        sfxDictionary.Add("HeyMan", allSfx[2]);
        sfxDictionary.Add("HelloMan", allSfx[3]);
        sfxDictionary.Add("HelloWomen", allSfx[4]);
        sfxDictionary.Add("CorrectAnswer", allSfx[5]);
        sfxDictionary.Add("WrongAnswer", allSfx[6]);
        sfxDictionary.Add("HelloWomen2", allSfx[7]);

    }

    public void PlaySfx(string alias)
    {
        if (sfxDictionary.ContainsKey(alias) && audioSource != null)
        {
            audioSource.PlayOneShot(sfxDictionary[alias]);
        }
        else
        {
            Debug.LogWarning("Alias not found or AudioSource not set.");
        }
    }

    public void StopSfx()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
    #endregion

    #region Save Manager

    private string scoreKey = "PlayerScore";
    private string levelKey = "PlayerLevel";
    public int score;
    public int level;

    public void LevelScoreSave(int idLevel, int score)
    {
        PlayerPrefs.SetInt(scoreKey + idLevel, score);
    }

    public int LevelScoreLoad(int idLevel)
    {
        return PlayerPrefs.GetInt(scoreKey + idLevel);
    }
    public void SaveScore(int howMuchScore)
    {
        PlayerPrefs.SetInt(scoreKey, howMuchScore);
        PlayerPrefs.Save();
    }

    public void SaveLevel(int whereLevel)
    {
        PlayerPrefs.SetInt(levelKey, whereLevel);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        score = PlayerPrefs.GetInt(scoreKey);
        level = PlayerPrefs.GetInt(levelKey);
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
    }
    #endregion
}
