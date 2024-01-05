using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAgoraView : MonoBehaviour
{
    public Text LogText;

    public Button JoinButton;
    public Button LeaveButton;
    public Button StartDanceButton;
    public Button StopDanceButton;

    public Slider MicSlider;
    public Text MicValueText;
    public Slider SoundSlider;
    public Text SoundValueText;
    public Slider BGMSlider;
    public Text BGMValueText;

    private AudioSource[] BGMAudioList;
    private float curBGMValue = 100;

    public float CurBGMValue
    {
        get { return curBGMValue; }
        set
        {
            curBGMValue = value;
            for (int i = 0; i < BGMAudioList.Length; i++)
            {
                BGMAudioList[i].volume = curBGMValue / 100.0f;
            }

            BGMValueText.text = curBGMValue.ToString();
        }
    }

    public PlayerMovement Movement
    {
        get
        {
            if (!movement)
            {
                movement = FindObjectsOfType<PlayerMovement>().Where(c => c.IsOwner).FirstOrDefault();
            }
            return movement;
        }
    }

    private PlayerMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        GameObject soundParent = GameObject.Find("Sound");
        BGMAudioList = soundParent.GetComponentsInChildren<AudioSource>();

        CurBGMValue = 100;
        UpdateBGMSlider((int)CurBGMValue, 0, 100);
    }

    private void OnEnable()
    {
        BGMSlider.onValueChanged.AddListener(OnBGMValueChange);
        StartDanceButton.onClick.AddListener(OnStartDanceButtonClick);
        StopDanceButton.onClick.AddListener(OnStopDanceButtonClick);
    }

    private void OnDisable()
    {
        BGMSlider.onValueChanged.RemoveListener(OnBGMValueChange);
        StartDanceButton.onClick.RemoveListener(OnStartDanceButtonClick);
        StopDanceButton.onClick.RemoveListener(OnStopDanceButtonClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateMicSlider(int CurValue, int MinValue, int MaxValue)
    {
        MicSlider.minValue = MinValue;
        MicSlider.maxValue = MaxValue;
        MicSlider.value = CurValue;
        MicValueText.text = CurValue.ToString();
    }

    public void UpdateSoundSlider(int CurValue, int MinValue, int MaxValue)
    {
        SoundSlider.minValue = MinValue;
        SoundSlider.maxValue = MaxValue;
        SoundSlider.value = CurValue;
        SoundValueText.text = CurValue.ToString();
    }

    public void UpdateBGMSlider(int CurValue, int MinValue, int MaxValue)
    {
        BGMSlider.minValue = MinValue;
        BGMSlider.maxValue = MaxValue;
        BGMSlider.value = CurValue;
        BGMValueText.text = CurValue.ToString();
    }

    void OnBGMValueChange(float value)
    {
        CurBGMValue = (int)value;
    }

    void OnStartDanceButtonClick()
    {
        Movement.isDancing = true;
    }

    void OnStopDanceButtonClick()
    {
        Movement.isDancing = false;
    }
}
