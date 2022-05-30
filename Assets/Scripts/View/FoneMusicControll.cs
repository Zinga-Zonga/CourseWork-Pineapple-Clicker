using UnityEngine;
using UnityEngine.UI;

public class FoneMusicControll : MonoBehaviour
{
    public static FoneMusicControll foneMusic;
    [SerializeField] private Button musicButton;
    private void Start()
    {
        musicButton.image.sprite = Resources.Load<Sprite>("MusicButtonSprites/MusicOn");
    }
    public void MusicOnOrOff()
    {
        if (musicButton.GetComponent<AudioSource>().mute == false)
        {
            musicButton.image.sprite = Resources.Load<Sprite>("MusicButtonSprites/MusicOff");
            musicButton.GetComponent<AudioSource>().mute = true;
        }
        else
        {
            musicButton.image.sprite = Resources.Load<Sprite>("MusicButtonSprites/MusicOn");
            musicButton.GetComponent<AudioSource>().mute = false;
        }
        
    }
}
