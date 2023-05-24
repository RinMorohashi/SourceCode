using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DamageTextScript : MonoBehaviour
{
    /// <summary>
    /// �퓬�V�[���Ń_���[�W�e�L�X�g�𓮂����X�N���v�g
    /// </summary>
    public bool isHiding;//�G�����X�^�[���B��Ă���Ƃ��́uMISS!�v�ƕ\������

    // Start is called before the first frame update
    void Start()
    {
        if (!isHiding)
        {
            StartCoroutine("a");
        }
        else
        {
            StartCoroutine("b");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator a()
    {
        transform.DOLocalMove(new Vector3(15,30,0), 0.5f).SetEase(Ease.OutSine).SetRelative().OnComplete(() =>
        {
            transform.DOLocalMove(new Vector3(15, -80, 0), 0.5f).SetEase(Ease.InOutSine).SetRelative();
        });
        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
    }
    IEnumerator b()
    {
        transform.DOLocalMove(new Vector3(15, 30, 0), 0.5f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            transform.DOLocalMove(new Vector3(15, -80, 0), 0.5f).SetEase(Ease.InOutSine);
        });
        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
    }
}
