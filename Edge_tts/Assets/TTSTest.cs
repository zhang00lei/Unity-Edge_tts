using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Edge_tts_sharp;
using Edge_tts_sharp.Model;
using UnityEngine;
using UnityEngine.UI;

public class TTSTest : MonoBehaviour
{
    private Button btnPlayAudio;
    private Button btnSave;
    private InputField inputText;
    private Dropdown dropdown;

    private void Awake()
    {
        btnPlayAudio = transform.Find("BtnPlayAudio").GetComponent<Button>();
        btnSave = transform.Find("BtnSave").GetComponent<Button>();
        inputText = transform.Find("InputContent").GetComponent<InputField>();
        dropdown = transform.Find("Dropdown").GetComponent<Dropdown>();

        btnPlayAudio.onClick.AddListener(OnBtnPlayAudioClick);
        btnSave.onClick.AddListener(OnBtnSaveClick);
    }

    private void OnBtnSaveClick()
    {
        PlayOption option = new PlayOption
        {
            Rate = 0,
            Text = inputText.text,
            SavePath = Path.Combine(Application.dataPath, "test.mp3")
        };
        var voice = Edge_tts.GetVoice()
            .FirstOrDefault(x => x.ShortName.Contains(dropdown.options[dropdown.value].text)); 
        Edge_tts.SaveAudio(option, voice, () => { Debug.Log("save end"); });
    }

    private void OnBtnPlayAudioClick()
    { 
        PlayOption option = new PlayOption
        {
            Rate = 0,
            Text = inputText.text,
        };
        var voice = Edge_tts.GetVoice()
            .FirstOrDefault(x => x.ShortName.Contains(dropdown.options[dropdown.value].text));
        var player = Edge_tts.GetPlayer(option, voice);
        player.PlayAsync();
    }
}