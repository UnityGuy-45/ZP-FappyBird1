using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartGame : MonoBehaviour
{
    public TextMeshProUGUI FlashText;

    public bool GameStart = false;

    public Button startButton;
    public Button controlButton;
    public Button ytButton;

    public GameObject uiElements;
    public GameObject gameUI;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Flash());
        startButton.onClick.AddListener(startClick);
        controlButton.onClick.AddListener(controlClick);
        ytButton.onClick.AddListener(ytClick);
        gameUI.SetActive(false);
    }


    void startClick()
    {
        FlashText.text = "";
        GameStart = true;
        uiElements.SetActive(false);
        gameUI.SetActive(true);
        startButton.onClick.RemoveListener(startClick);
        controlButton.onClick.RemoveListener(controlClick);
        ytButton.onClick.RemoveListener(ytClick);
    }
    void controlClick()
    {
        print("control");
    }
    void ytClick()
    {
        Application.OpenURL("https://ia800905.us.archive.org/25/items/AnimalFarmByGeorgeOrwell/Animal%20Farm%20by%20George%20Orwell.pdf");
    }

    // Update is called once per frame

    IEnumerator Flash()
    {
        while (GameStart == false)
        {
            FlashText.text = "Tap to Start";
            yield return new WaitForSeconds(0.5f);
            FlashText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
        
    }
    void Update()
    {
        if(GameStart == false)
        {
            print("bad");
        }
    }
}
