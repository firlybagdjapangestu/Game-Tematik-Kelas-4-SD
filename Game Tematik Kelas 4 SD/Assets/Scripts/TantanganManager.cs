using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TantanganManager : MonoBehaviour
{
    [SerializeField] private TantanganSO[] tantanganLevel0;
    [SerializeField] private TantanganSO[] tantanganLevel1;
    [SerializeField] private TantanganSO[] tantanganLevel2;
    [SerializeField] private TantanganSO[] tantanganLevel3;

    [SerializeField] private Text soalText;
    [SerializeField] private Image soalImage;
    [SerializeField] private InputField jawabanInput;

    private TantanganSO[] tantanganNow;
    private int tantanganIndexNow;
    Dictionary<int, TantanganSO[]> choiceTantangan = new Dictionary<int, TantanganSO[]>();

    [SerializeField] private GameObject[] answerCorrectionImage;
    private int highScore;
    private int score;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text scoreText;


    private void Awake()
    {
        choiceTantangan.Add(0, tantanganLevel0);
        choiceTantangan.Add(1, tantanganLevel1);
        choiceTantangan.Add(2, tantanganLevel2);
        choiceTantangan.Add(3, tantanganLevel3);
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        SetLevel();
        DisplayCurrentTantangan();
        scorePanel.SetActive(false);
        highScore = GameManager.Instance.LevelScoreLoad(GameManager.Instance.level);
    }

    private void SetLevel()
    {
        tantanganNow = choiceTantangan[GameManager.Instance.level];
    }

    private void DisplayCurrentTantangan()
    {
        if(tantanganIndexNow < tantanganNow.Length)
        {
            soalText.text = tantanganNow[tantanganIndexNow].pertanyaan;
            soalImage.sprite = tantanganNow[tantanganIndexNow].gambar;
        }
        else
        {
            if(score > highScore)
            {
                GameManager.Instance.LevelScoreSave(GameManager.Instance.level, score);
            }
            highScoreText.text = "High Score : " + GameManager.Instance.LevelScoreLoad(GameManager.Instance.level).ToString();
            scoreText.text = "Score : " + score.ToString();
            scorePanel.SetActive(true);
        }
    }


    public void OnExitTantangan()
    {
        tantanganIndexNow = 0;
    }

    public void OnSubmitButtonClick()
    {
        StartCoroutine(SubmitAnswer());
    }


    public void OnDeleteAnswer()
    {
        jawabanInput.text = "";
    }
    private IEnumerator SubmitAnswer()
    {
        string jawabanInputLower = jawabanInput.text.ToLower();
        string jawabanTantanganLower = tantanganNow[tantanganIndexNow].jawaban.ToLower();
        if (jawabanInputLower == jawabanTantanganLower)
        {
            answerCorrectionImage[0].SetActive(true);
            GameManager.Instance.PlaySfx("CorrectAnswer");
            Debug.Log("Benar");
            tantanganIndexNow++;
            score += 20;
            // Tambahkan logika atau tampilan untuk jawaban benar di sini (misalnya menampilkan teks atau memainkan musik)
        }
        else
        {
            answerCorrectionImage[1].SetActive(true);
            EventManager.BroadcastOnAnswerTantangan(false);
            GameManager.Instance.PlaySfx("WrongAnswer");
            Debug.Log("salah");
            tantanganIndexNow++;
            // Tambahkan logika atau tampilan untuk jawaban salah di sini (misalnya menampilkan pesan kesalahan)
        }

        jawabanInput.text = ""; // Mengosongkan input field setelah pengecekan

        // Menambahkan jeda sebelum melanjutkan ke soal berikutnya
        Debug.Log("Jeda Setiap Pertanyaan");
        yield return new WaitForSeconds(1f); // Ganti angka 2 dengan berapa detik jeda yang diinginkan

        // Mengubah semua AnswerCorrectionImage menjadi false setelah waktu tertentu
        foreach (var image in answerCorrectionImage)
        {
            image.SetActive(false);
        }
        DisplayCurrentTantangan();
    }
}
