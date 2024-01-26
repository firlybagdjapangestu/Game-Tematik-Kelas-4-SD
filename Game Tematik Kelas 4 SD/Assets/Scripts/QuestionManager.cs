using UnityEngine;
using TMPro;
using System.Collections;

public class QuestionManager : MonoBehaviour
{
    // Variabel untuk pertanyaan
    public QuestionSO[] questions;
    public int questionIndex = 0;
    public bool timeAnswering;

    // Tampilan teks
    public TextMeshProUGUI question;
    public TextMeshProUGUI[] option;
    public TextMeshProUGUI timeBetweenQuestionsText;

    // Waktu antara pertanyaan dan waktu jeda antara pertanyaan
    public float timeBetweenQuestions = 5f;
    public float interQuestionDelay = 2f;
    private float timeRemaining;
    private bool isInterQuestionDelay = false;
    private float interQuestionTimer;

    // Gameobject
    public GameObject[] planeOption;
    public GameObject groundPlane;
    public GameObject enemy;
    void Start()
    {
        timeRemaining = timeBetweenQuestions;
        DisplayQuestion();
    }

    void Update()
    {
        // Logika untuk menampilkan pertanyaan dan waktu jeda
        if (isInterQuestionDelay)
        {
            interQuestionTimer -= Time.deltaTime;
            if (interQuestionTimer <= 0f)
            {
                
                isInterQuestionDelay = false;
                NextQuestion();
            }
            else
            {
                DisplayInformation();
            }
        }
        else
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0f)
            {
                timeRemaining = timeBetweenQuestions;
                isInterQuestionDelay = true;
                interQuestionTimer = interQuestionDelay;
                UpdateTimerDisplay();
            }
        }

        UpdateTimerDisplay();
    }

    // Fungsi untuk menampilkan pertanyaan dan pilihan jawaban
    void DisplayQuestion()
    {
        timeAnswering = false;
        groundPlane.SetActive(true);
        enemy.SetActive(false);
        question.text = questions[questionIndex].questionText;
        char optionLetter = 'A'; // Karakter awalan yang akan digunakan

        for (int i = 0; i < option.Length; i++)
        {
            planeOption[i].SetActive(true);
            option[i].text = optionLetter + ". " + questions[questionIndex].options[i];
            optionLetter++; // Menaikkan karakter awalan ke huruf berikutnya (A ke B, B ke C, dan seterusnya)
        }
    }

    // Fungsi untuk menampilkan informasi selama waktu jeda
    void DisplayInformation()
    {
        timeAnswering = true;
        groundPlane.SetActive(false);
        enemy.SetActive(true);
        question.text = "Larilah Dari kejaran buaya";
        for (int i = 0; i < option.Length; i++)
        {
            if(i == questions[questionIndex].correctOptionIndex)
            {
                planeOption[i].SetActive(true);
                option[i].text = questions[questionIndex].options[i];
            }
            else
            {
                planeOption[i].SetActive(false);
                option[i].text = "";
            } 
        }
        timeBetweenQuestionsText.text = Mathf.Ceil(interQuestionTimer).ToString();
    }

    // Fungsi untuk menampilkan pertanyaan berikutnya
    void NextQuestion()
    {
        questionIndex = (questionIndex + 1) % questions.Length;
        if (questionIndex == 0)
        {
            // Jika sudah menampilkan semua pertanyaan
            EndGame(); // Panggil fungsi untuk mengakhiri game
        }
        else
        {
            DisplayQuestion();
        }
    }

    void EndGame()
    {
      
    }

    void UpdateTimerDisplay()
    {
        if (isInterQuestionDelay)
        {
            timeBetweenQuestionsText.text = Mathf.Ceil(interQuestionTimer).ToString();
        }
        else
        {
            timeBetweenQuestionsText.text = Mathf.Ceil(timeRemaining).ToString();
        }
    }


   
}
