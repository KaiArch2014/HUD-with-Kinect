using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UICtr : MonoBehaviour
{

    public HajiyevMusicManager music_ctr;

    public Delay loading_ui;

    public Delay standby_ui;

    public Delay musicbutton_ui;

    public Delay musicplayer_ui;

    public Delay airbutton_ui;

    public Delay airplayer_ui;
    public ExcelsiorUtils wind_image;
    public Text tem_text;

    public SpriteRenderer mask;

    public GameObject all_ui;

    public ValueCtr value;

    float saveVolume;

    bool playTopause;
    bool nextMusic;
    bool prevMusic;

    bool musicRate;

    bool airWindup;
    bool airWinddown;
    int wind;
    float tem;


    // Start is called before the first frame update
    void Start()
    {
        playTopause = true;
        nextMusic = true;
        prevMusic = true;
        musicRate = false;
        airWinddown = true;
        airWindup = true;
        wind = 30;
        tem = 27;
    }

    // Update is called once per frame
    void Update()
    {

      

        if (Input.GetKey(KeyCode.Keypad0)) {
            Close();
        }

        if (Input.GetKey(KeyCode.Keypad1))
        {
            Play();
        }

        if (Input.GetKey(KeyCode.Keypad2))
        {
            LightnessUp();
            value.SliderOn(1-mask.color.a);
        }

        if (Input.GetKey(KeyCode.Keypad3))
        {
            LightnessDown();
            value.SliderOn(1-mask.color.a);
        }

        if (Input.GetKey(KeyCode.Keypad4))
        {
            ScaleUp();
        }

        if (Input.GetKey(KeyCode.Keypad5))
        {
            ScaleDown();
        }

        if (Input.GetKey(KeyCode.A)) {
            PositionX(false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            PositionX(true);
        }

        if (Input.GetKey(KeyCode.W))
        {
            PositionY(true);
        }

        if (Input.GetKey(KeyCode.S))
        {
            PositionY(false);
        }

        if (Input.GetKey(KeyCode.Keypad6))
        {
            EnterMusicPlayer();
        }

        if (Input.GetKey(KeyCode.Keypad7))
        {
            ReturnMainUI();
            music_ctr.Stop();
        }

        if (Input.GetKey(KeyCode.Keypad8))
        {
            musicplayer_ui.GetComponent<MusicMod>().modChange();
        }

        if (Input.GetKey(KeyCode.Keypad9))
        {
            MusicPlayPauseToggle();
        }

        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            NextMusic();
        }

        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            PrevMusic();
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            MusicVolumeUp();
            value.SliderOn(music_ctr.volume);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            MusicVolumeDown();
            value.SliderOn(music_ctr.volume);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MusicRateDown();
            music_ctr.GetComponent<AudioSource>().time = music_ctr.GetComponent<AudioSource>().time - Time.deltaTime * 20;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MusicRateUp();
            music_ctr.GetComponent<AudioSource>().time = music_ctr.GetComponent<AudioSource>().time + Time.deltaTime * 20;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            MusicRate();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            MusicRate();
        }

        if (Input.GetKey(KeyCode.I)) {
            EnterAirPlayer();

        }

        if (Input.GetKey(KeyCode.O))
        {
            ReturnMainUI();

        }

        if (Input.GetKey(KeyCode.H))
        {
            AirWindDown();

        }

        if (Input.GetKey(KeyCode.K))
        {
            AirWindUp();

        }
        if (Input.GetKey(KeyCode.U))
        {
           TemUp();

        }

        if (Input.GetKey(KeyCode.J))
        {
            TemDown();

        }



    }

    #region Public function
    public void Close() {
        loading_ui.Disappear(0);
        musicbutton_ui.Disappear(0);
        musicplayer_ui.Disappear(0);
        standby_ui.Disappear(0);
        airbutton_ui.Disappear(0);
        airplayer_ui.Disappear(0);
    }

    public void Play() {
        loading_ui.Appear(0);
        loading_ui.Disappear(3);
        musicbutton_ui.Appear(5);
        airbutton_ui.Appear(5);
    }

    public void LightnessUp() {
        if (mask.color.a <= 1)
        {
            mask.DOColor(new Color(mask.color.r, mask.color.g, mask.color.b, mask.color.a + 0.01f), 0.2f);
        }
        else
        {
            mask.color = new Color(mask.color.r, mask.color.g, mask.color.b, 1f);
        }
    }

    public void LightnessDown()
    {
        if (mask.color.a >= 0)
        {
            mask.DOColor(new Color(mask.color.r, mask.color.g, mask.color.b, mask.color.a - 0.01f), 0.2f);
        }
        else
        {
            mask.color = new Color(mask.color.r, mask.color.g, mask.color.b, 0f);
        }
    }

    public void ScaleUp() {
        if (all_ui.transform.localScale.x < 0.6)
        {
            all_ui.transform.DOScale(new Vector3(all_ui.transform.localScale.x + 0.01f, all_ui.transform.localScale.y + 0.01f, all_ui.transform.localScale.z), 0.2f);
        }
        else
        {
            all_ui.transform.localScale = new Vector3(0.6f, 0.6f, 1);
        }
    }

    public void ScaleDown()
    {
        if (all_ui.transform.localScale.x > 0.2)
        {
            all_ui.transform.DOScale(new Vector3(all_ui.transform.localScale.x - 0.01f, all_ui.transform.localScale.y - 0.01f, all_ui.transform.localScale.z), 0.2f);
        }
        else
        {
            all_ui.transform.localScale = new Vector3(0.3f, 0.3f, 1);
        }
    }

    public void Position(Vector2 pos) {
        all_ui.transform.position = pos;
    }


    public void PositionX(bool right) {
        if (right)
        {
            all_ui.transform.DOMoveX(all_ui.transform.position.x + 0.05f, 0.1f);
        }
        else {
            all_ui.transform.DOMoveX(all_ui.transform.position.x - 0.05f, 0.1f);
        }

    }


    public void PositionY(bool up)
    {
        if (up)
        {
            all_ui.transform.DOMoveY(all_ui.transform.position.y + 0.05f, 0.1f);
        }
        else
        {
            all_ui.transform.DOMoveY(all_ui.transform.position.y - 0.05f, 0.1f);
        }

    }

    public void EnterMusicPlayer() {
        musicbutton_ui.GetComponent<Animator>().Play("MeticulousLock_SpecialAnim");
        musicbutton_ui.Disappear(0);
        musicplayer_ui.Appear(2);     
        }

    public void ReturnMainUI() {
        musicplayer_ui.Disappear(0);
        airplayer_ui.Disappear(0);
        musicbutton_ui.Appear(2);
        airbutton_ui.Appear(2);

    }

    public void MusicMod() {


    }
    public void MusicPlay() {
        music_ctr.Play();
    }

    public void MusicPause() {
        music_ctr.Pause();

    }

    public void MusicPlayPauseToggle() {
        if (playTopause)
        {
            music_ctr.PlayPauseToggle();
            playTopause = false;
            Invoke("PlayTopause", 2);
        }
        
    }

    void PlayTopause() {
        playTopause = true;

    }


    public void NextMusic() {
        if (nextMusic)
        {
            music_ctr.Next();
            nextMusic = false;
            Invoke("NextMusicOn", 2);
        }
    }

    void NextMusicOn()
    {
        nextMusic = true;

    }

    public void PrevMusic()
    {
        if (prevMusic)
        {
            music_ctr.Previous();
            prevMusic = false;
            Invoke("PrevMusicOn", 2);
        }
      
    }

    void PrevMusicOn(){
        prevMusic=true;
    }

    public void MusicVolumeUp() {
        DOTween.To(() =>music_ctr.volume, x => music_ctr.volume = x, music_ctr.volume+0.01f, 0.2f);

    }

    public void MusicVolumeDown()
    {
        DOTween.To(() => music_ctr.volume, x => music_ctr.volume = x, music_ctr.volume - 0.01f, 0.2f);

    }

    public void MusicRateUp() {
        if (music_ctr.volume != 0)
        {
            saveVolume = music_ctr.volume;
        }
        music_ctr.volume = 0;
        music_ctr.GetComponent<AudioSource>().pitch = 3;
        musicRate = true;
    }

    public void MusicRateDown()
    {
        if (music_ctr.volume != 0)
        {
            saveVolume = music_ctr.volume;
        }
        music_ctr.volume = 0;
        music_ctr.GetComponent<AudioSource>().pitch = -3;
        musicRate = true;
    }
    public void MusicRate()
    {
        if (musicRate)
        {
            if (saveVolume == 0) {
                saveVolume = 0.5f;
            }
            music_ctr.volume = saveVolume;
            music_ctr.GetComponent<AudioSource>().pitch = 1;
            musicRate = false;
        }
    }

    public void AirWindUp() {
        if (airWindup) {
            if (wind != 130)
            {
                wind = wind + 50;
                wind_image.SetUniformWidthHeight(wind);
                airWindup = false;
                Invoke("AirWindUpOn", 1);
            }
            else {
                wind =  30;
                wind_image.SetUniformWidthHeight(wind);
                airWindup = false;
                Invoke("AirWindUpOn", 1);
            }
        }

    }

    void AirWindUpOn() {
        airWindup = true ;
    }

    public void AirWindDown()
    {
        if (airWinddown)
        {
            if (wind != 30)
            {
                wind = wind - 50;
                wind_image.SetUniformWidthHeight(wind);
                airWinddown = false;
                Invoke("AirWindDownOn", 1);
            }
            else
            {
                wind = 130;
                wind_image.SetUniformWidthHeight(wind);
                airWinddown = false;
                Invoke("AirWindDownOn", 1);
            }
        }

    }

    void AirWindDownOn()
    {
        airWinddown = true;
    }

    public void TemUp() {
        DOTween.To(() => tem, x => tem = x, tem + 0.03f, 0.2f);
        tem = (float)(Mathf.Round(tem * 10)) / 10;
        tem_text.text = tem + "℃";
    }

    public void TemDown()
    {
        DOTween.To(() => tem, x => tem = x, tem - 0.03f, 0.2f);
        tem = (float)(Mathf.Round(tem * 10)) / 10;
        tem_text.text = tem + "℃";
    }

    public void EnterAirPlayer() {
        musicbutton_ui.Disappear(0);
        airbutton_ui.Disappear(0);
        airplayer_ui.Appear(1.5f);

    }


    #endregion
}
