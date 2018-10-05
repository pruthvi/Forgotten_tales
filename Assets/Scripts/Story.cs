/*  Copyright (c) Pruthvi  |  http://pruthv.com  */

using UnityEngine;

public class Story : MonoBehaviour {

    #region Variables

    [SerializeField]
    private int selectedValue;
    private GameObject[] stories;

    public int OptionsNo;


    [Space]
    [Header("Menu Audio Clips")]
    public AudioClip StoryClip;
    public AudioClip Option1CLip;
    public AudioClip Option2CLip;
    public AudioClip Option3CLip;

    [Space]
    [Header("Next Option")]
    public GameObject Option1Story;
    public GameObject Option2Story;
    public GameObject Option3Story;

    #endregion

    void Start()
    {
        selectedValue = 0;
        Debug.Log("Selected Option: " + selectedValue);
        playSound(StoryClip);
        stories = GameObject.FindGameObjectsWithTag("Story");

//        gameObject.GetComponent<Story>().
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedValue++;
            speakOption(selectedValue);
            Debug.Log("Selected Option: " + selectedValue);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedValue--;
            speakOption(selectedValue);
            Debug.Log("Selected Option: " + selectedValue);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            selectedOption(selectedValue);
        }

    }


    #region Speaking which option is selected
    private void speakOption(int value)
    {
        switch (value)
        {
            case 1:
                playSound(Option1CLip);
                break;

            case 2:
                playSound(Option2CLip);
                break;

            case 3:
                playSound(Option3CLip);
                break;

            default:
                playSound(StoryClip);
                selectedValue = 0;
                break;
        }
    }
    #endregion

    #region Playing Sound
    private void playSound(AudioClip sound)
    {
        AudioSource aud = gameObject.GetComponent<AudioSource>();
        aud.clip = sound;
        aud.Play(0);
    }
    #endregion

    #region Entering selected option
    private void selectedOption(int value)
    {
        switch (value)
        {
            case 1:
                Option(Option1Story);
                break;

            case 2:
                Option(Option2Story);
                break;

            case 3:
                Option(Option3Story);
                break;

            default:
                playSound(StoryClip);
                break;
        }
    }
    #endregion


 private void Option(GameObject NextStory)
    {
        foreach(GameObject story in stories)
        {
            story.SetActive(false);
        }
        NextStory.SetActive(true);
    }
}
