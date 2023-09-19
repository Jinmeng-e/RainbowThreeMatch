using System.Collections;
using UnityEngine;
using DG.Tweening;

public class AnimationBlock : MonoBehaviour
{
    [Space]
    [Header("Drop")]
    int maxHeight = 10;
    [SerializeField] float maxDropDuration = 1f;
    [SerializeField] float heightUnit = 160f;
    [Space]
    [Header("Dropped")]
    [SerializeField] float droppedDuration = 0.3f;
    [Space]
    [Header("Change Block")]
    [SerializeField] float upScale = 1.2f;
    [SerializeField] float upScaleDuration = .2f;
    [SerializeField] float downScaleDuration = .2f;

    public float PosY => this.transform.localPosition.y;

    void Init()
    {
        StopAllCoroutines();
        this.transform.DOScale(1,0f);
        this.transform.DOLocalMoveY(0,0f,true);
    }


    public void Pop(Color c)
    {
        Init();
        //Debug.Log("AnimationBlock : POP");
        // hide
        SetActive(false);
        //sfx
        Sfx.Instnace.Pop(transform.position,c);
    }
    public void SetDrop(int height)
    {
        var posY = height * heightUnit;
        Debug.Log($"AnimationBlock : SetDrop :: {posY}");
        transform.DOLocalMoveY(posY,0f,true);
        SetActive(true);
    }
    public void SetActive(bool isShow)
    {
        //Debug.Log("AnimationBlock : SetActive");
        //set show
        gameObject.SetActive(isShow);
    }
    public Coroutine Drop(int height)
    {
        return StartCoroutine(IEDrop(height));
    }
    public Coroutine Dropped()
    {
        return StartCoroutine(IEDropped());
    }
    public Coroutine ChangeBlock()
    {
        return StartCoroutine(IEChangeBlock());
    }
    public void Failed()
    {
        transform.DOShakePosition(.3f);
        transform.localPosition = Vector3.zero;
    }


    IEnumerator IEDrop(int height)
    {
        var duration = ((float)height / maxHeight) * maxDropDuration;
        yield return transform.DOLocalMoveY(0f, duration,true);

        transform.DOLocalMoveY(0, 0f);
    }
    IEnumerator IEDropped()
    {
        yield return transform.DOShakeScale(droppedDuration, 1, 3);
    }
    IEnumerator IEChangeBlock()
    {
        yield return transform.DOPunchScale(Vector3.one * upScale, upScaleDuration,3);
        // change block

        transform.DOScale(1, 0);
    }
}
