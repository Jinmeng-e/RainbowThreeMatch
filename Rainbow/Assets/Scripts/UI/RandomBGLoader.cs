using UnityEngine;
using UnityEngine.UI;

public class RandomBGLoader : MonoBehaviour
{
    [SerializeField] Sprite[] images;

    Image bg;

    private void OnEnable()
    {
        Set();
    }

    void Set()
    {
        this.bg = GetComponent<Image>();
        var i = Random.Range(0, images.Length);
        bg.sprite = images[i];
    }
}
