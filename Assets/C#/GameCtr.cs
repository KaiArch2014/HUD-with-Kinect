using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCtr : MonoBehaviour
{
    public HandCrt hand_ctr;

    public UICtr ui_ctr;

    public InteractionManager interactionManager;

    public GameCtr.UIState UISTATE;

    private KinectManager manager;
    public enum UIState : int
    {
        Close = 0,
        Loading = 1,
        MainUI = 2,
        MusicUI = 3,
        AirUI = 4,
    }
    // Start is called before the first frame update
    void Start()
    {
        manager = KinectManager.Instance;
        UISTATE = GameCtr.UIState.Close;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager && manager.IsUserDetected(0))
        {
            if (hand_ctr.handHover && UISTATE == GameCtr.UIState.Close)
            {
                ui_ctr.Play();
                UISTATE = GameCtr.UIState.MainUI;
               
            }

            if (hand_ctr.close && UISTATE != GameCtr.UIState.Close)
            {
                ui_ctr.Close();
                ui_ctr.music_ctr.Stop();
                UISTATE = GameCtr.UIState.Close;
            }

            if (!hand_ctr.standby && UISTATE != GameCtr.UIState.Close)
            {
                ui_ctr.standby_ui.Disappear(0);
                

            
                #region MainUI
                if (hand_ctr.LeftHandVerticalEvent == HandCrt.HandGripVerticalType.Up && UISTATE == GameCtr.UIState.MainUI)
                {
                    ui_ctr.LightnessDown();
                    ui_ctr.value.SliderOn(1 - ui_ctr.mask.color.a);
                }

                if (hand_ctr.LeftHandVerticalEvent == HandCrt.HandGripVerticalType.Down && UISTATE == GameCtr.UIState.MainUI)
                {
                    ui_ctr.LightnessUp();
                    ui_ctr.value.SliderOn(1 - ui_ctr.mask.color.a);
                }

                if (hand_ctr.LeftHandHorizontalEvent == HandCrt.HandGripHorizontalType.Left && UISTATE == GameCtr.UIState.MainUI)
                {
                    ui_ctr.ScaleUp();
                }

                if (hand_ctr.LeftHandHorizontalEvent == HandCrt.HandGripHorizontalType.Right && UISTATE == GameCtr.UIState.MainUI)
                {
                    ui_ctr.ScaleDown();
                }

                if (hand_ctr.RightHandHorizontalEvent == HandCrt.HandGripHorizontalType.Left && UISTATE == GameCtr.UIState.MainUI)
                {
                    ui_ctr.PositionX(false);
                }

                if (hand_ctr.RightHandHorizontalEvent == HandCrt.HandGripHorizontalType.Right && UISTATE == GameCtr.UIState.MainUI)
                {
                    ui_ctr.PositionX(true);
                }

                if (hand_ctr.RightHandVerticalEvent == HandCrt.HandGripVerticalType.Up && UISTATE == GameCtr.UIState.MainUI)
                {
                    ui_ctr.PositionY(true);
                }

                if (hand_ctr.RightHandVerticalEvent == HandCrt.HandGripVerticalType.Down && UISTATE == GameCtr.UIState.MainUI)
                {
                    ui_ctr.PositionY(false);
                }
                #endregion

                #region MusicUI
                if (hand_ctr.LeftHandHorizontalEvent == HandCrt.HandGripHorizontalType.Right && UISTATE == GameCtr.UIState.MusicUI)
                {
                    ui_ctr.musicplayer_ui.GetComponent<MusicMod>().modChange();
                }

                if (hand_ctr.LeftHandHorizontalEvent == HandCrt.HandGripHorizontalType.Left && UISTATE == GameCtr.UIState.MusicUI)
                {
                    ui_ctr.musicplayer_ui.GetComponent<MusicMod>().modChangeUp();
                }


                if (hand_ctr.handHover && UISTATE == GameCtr.UIState.MusicUI)
                {
                    ui_ctr.MusicPlayPauseToggle();
                }

                if (hand_ctr.LeftHandVerticalEvent == HandCrt.HandGripVerticalType.Down && UISTATE == GameCtr.UIState.MusicUI)
                {
                    ui_ctr.NextMusic();
                }

                if (hand_ctr.LeftHandVerticalEvent == HandCrt.HandGripVerticalType.Up && UISTATE == GameCtr.UIState.MusicUI)
                {
                    ui_ctr.PrevMusic();
                }

                if (hand_ctr.RightHandVerticalEvent == HandCrt.HandGripVerticalType.Up && UISTATE == GameCtr.UIState.MusicUI)
                {
                    ui_ctr.MusicVolumeUp();
                    ui_ctr.value.SliderOn(ui_ctr.music_ctr.volume);
                }
                if (hand_ctr.RightHandVerticalEvent == HandCrt.HandGripVerticalType.Down && UISTATE == GameCtr.UIState.MusicUI)
                {
                    ui_ctr.MusicVolumeDown();
                    ui_ctr.value.SliderOn(ui_ctr.music_ctr.volume);
                }

                if (hand_ctr.RightHandHorizontalEvent == HandCrt.HandGripHorizontalType.Left && UISTATE == GameCtr.UIState.MusicUI)
                {
                    if (ui_ctr.music_ctr.GetComponent<AudioSource>().time > 1)
                    {
                        ui_ctr.MusicRateDown();
                        ui_ctr.music_ctr.GetComponent<AudioSource>().time = ui_ctr.music_ctr.GetComponent<AudioSource>().time - Time.deltaTime * 10;
                    }
                    else
                    {
                        ui_ctr.music_ctr.GetComponent<AudioSource>().time = 1;
                    }
                }
                if (hand_ctr.RightHandHorizontalEvent == HandCrt.HandGripHorizontalType.Right && UISTATE == GameCtr.UIState.MusicUI)
                {
                    if (ui_ctr.music_ctr.GetComponent<AudioSource>().clip.length - 2 > ui_ctr.music_ctr.GetComponent<AudioSource>().time)
                    {
                        ui_ctr.MusicRateUp();
                        ui_ctr.music_ctr.GetComponent<AudioSource>().time = ui_ctr.music_ctr.GetComponent<AudioSource>().time + Time.deltaTime * 10;
                    }
                    else
                    {
                        ui_ctr.music_ctr.GetComponent<AudioSource>().time = ui_ctr.music_ctr.GetComponent<AudioSource>().clip.length - 2;
                    }
                }
                if (hand_ctr.RightHandHorizontalEvent == HandCrt.HandGripHorizontalType.None && UISTATE == GameCtr.UIState.MusicUI)
                {
                    ui_ctr.MusicRate();
                }

                #endregion

                #region AirUI
                if (hand_ctr.RightHandHorizontalEvent == HandCrt.HandGripHorizontalType.Left && UISTATE == GameCtr.UIState.AirUI) {
                    ui_ctr.TemDown();
                }

                if (hand_ctr.RightHandHorizontalEvent == HandCrt.HandGripHorizontalType.Right && UISTATE == GameCtr.UIState.AirUI)
                {
                    ui_ctr.TemUp();
                   
                }

                if (hand_ctr.LeftHandHorizontalEvent == HandCrt.HandGripHorizontalType.Left && UISTATE == GameCtr.UIState.AirUI)
                {
                   
                    ui_ctr.AirWindDown();
                }

                if (hand_ctr.LeftHandHorizontalEvent == HandCrt.HandGripHorizontalType.Right && UISTATE == GameCtr.UIState.AirUI)
                {
                    ui_ctr.AirWindUp();
                }

                #endregion
            }
            if (hand_ctr.standby && UISTATE != GameCtr.UIState.Close) { 
                ui_ctr.standby_ui.Appear(0);
            }

           
        }
    }


    public void UIstateToCloes() {
        UISTATE = UIState.Close;   
    }

    public void UIstateToMainUI()
    {
        UISTATE = UIState.MainUI;
    }

    public void UIstateToLoading()
    {
        UISTATE = UIState.Loading;
    }

    public void UIstateToMusicUI()
    {
        UISTATE = UIState.MusicUI;
    }

    public void UIstateToAirUI()
    {
        UISTATE = UIState.AirUI;
    }
}
