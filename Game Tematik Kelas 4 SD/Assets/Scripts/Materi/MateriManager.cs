using UnityEngine;
using UnityEngine.UI;

public class MateriManager : MonoBehaviour
{
    public MateriSO[] allMateri;
    private Sprite[] currentMateriImages;
    [SerializeField] private int currentMateriIndex;

    [SerializeField] private Image materiPanel;
    [SerializeField] private int level;
    [SerializeField] private Text score;

    private void OnEnable()
    {
        GameManager.Instance.LoadGame();
        level = GameManager.Instance.level;
        score.text = "Score : " + GameManager.Instance.LevelScoreLoad(level).ToString();
        ChangeMateri(level);
        currentMateriIndex = 0;
        DisplayCurrentMateriImage();
    }

    public void NextMateriImage()
    {
        currentMateriIndex = (currentMateriIndex + 1) % currentMateriImages.Length;
        DisplayCurrentMateriImage();
    }

    public void PreviousMateriImage()
    {
        currentMateriIndex = (currentMateriIndex - 1 + currentMateriImages.Length) % currentMateriImages.Length;
        DisplayCurrentMateriImage();
    }

    private void DisplayCurrentMateriImage()
    {
        if (materiPanel != null && currentMateriImages != null && currentMateriImages.Length > 0)
        {
            materiPanel.sprite = currentMateriImages[currentMateriIndex];
        }
    }

    private void ChangeMateri(int index)
    {
        if (index >= 0 && index < allMateri.Length)
        {
            currentMateriImages = allMateri[index].gambarMateri;
            Debug.Log("Materi ke: " + index + ", " + allMateri[index].namaMateri);
            DisplayCurrentMateriImage();
        }
        else
        {
            Debug.LogWarning("Index materi tidak valid: " + index);
        }
    }
}
