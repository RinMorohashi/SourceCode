using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaAnimManager : MonoBehaviour
{
    public CharacterControll characterControll;
    public AudioSource odsc;
    public AudioClip landingSE;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //[CallFromAnimationClip]
    //public void OnAnimationCompleted() => Debug.Log("アニメーションが終了しました。");
    public void OnAnimationCompleted()
    {
        Debug.Log("アニメーションが終了しました。");
        characterControll.isMovable = true;
        //odsc.PlayOneShot(landingSE);
    }
}
