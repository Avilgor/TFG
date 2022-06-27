using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScreen : MonoBehaviour
{
    [SerializeField]
    AudioSource source;

    [SerializeField]
    Animator anim;

    [SerializeField]
    AudioClip openImageFX;

    [SerializeField]
    Image tutoImg;

    private void OnEnable()
    {
        tutoImg.gameObject.SetActive(false);
    }

    public void OpenImage(Image img)
    {
        tutoImg.sprite = img.sprite;
        if(!tutoImg.IsActive()) tutoImg.gameObject.SetActive(true);
        anim.SetTrigger("PopIn");
    }

    public void CloseImage()
    {
        anim.SetTrigger("PopOut");
    }
}