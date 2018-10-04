/*  Copyright (c) Pruthvi  |  http://pruthv.com  */

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BasicInput : MonoBehaviour {

    #region Variables

    [SerializeField]
    private int selectedValue;

    [Space]
    [Header("Menu Audio Clips")]
    public AudioClip InstructionClip;
    public AudioClip startClip;
    public AudioClip settingClip;
    public AudioClip exitClip;

	#endregion

	void Start ()
	{
        selectedValue = 0;
        Debug.Log("SelectedValue:" + selectedValue);
        playSound(InstructionClip);
    }

    void Update ()
	{
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedValue++;
            speakOption(selectedValue);
            Debug.Log("SelectedValue:" + selectedValue);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedValue--;
            speakOption(selectedValue);
            Debug.Log("SelectedValue:" + selectedValue);
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
                playSound(startClip);
                break;

            case 2:
                playSound(settingClip);
                break;

            case 3:
                playSound(exitClip);
                break;

            default:
                playSound(InstructionClip);
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
                Debug.Log("Entering in Start");
                break;

            case 2:
                Debug.Log("Entering in Setting");
                break;

            case 3:
                Debug.Log("Exiting Game!");
                break;

            default:
                playSound(InstructionClip);
                break;
        }
    }
    #endregion

}
