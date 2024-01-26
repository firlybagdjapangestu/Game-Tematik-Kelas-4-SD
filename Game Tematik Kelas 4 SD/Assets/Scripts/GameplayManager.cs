using System;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private Button talkButton;
    [SerializeField] private GameObject panelMateri;

    private void Start()
    {
        GameManager.Instance.LoadGame();
    }
    private void OnEnable()
    {
        EventManager.OnKidsCanTalk += ButtonTalkEnable;
        talkButton.onClick.AddListener(OnTalkButtonClick); // Menambahkan listener untuk event klik tombol
    }

    private void OnDisable()
    {
        EventManager.OnKidsCanTalk -= ButtonTalkEnable;
        talkButton.onClick.RemoveListener(OnTalkButtonClick); // Menghapus listener saat nonaktif
    }

    public void ButtonTalkEnable(bool canTalk)
    {
        talkButton.interactable = canTalk;
    }

    private void OnTalkButtonClick()
    {
        // Aktifkan panelMater saat tombol ditekan
        panelMateri.SetActive(true);
    }


}
