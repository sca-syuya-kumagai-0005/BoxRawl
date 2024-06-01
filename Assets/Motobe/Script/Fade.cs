using System.Collections;
using UnityEngine;

public class Fade : MonoBehaviour
{
    [SerializeField] public GameObject fadePanel;
    public static GameObject fader;
    // Start is called before the first frame update
    void Start()
    {
        alpha = 1;
        fader = fadePanel;
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        FadeOut(ButtonManager.sceneChange);
    }

    public static float alpha;
    private IEnumerator FadeIn()
    {
        while (alpha > 0)
        {
            alpha -= Time.deltaTime / 2;
            fader.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, alpha);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
    public static void FadeOut(bool flag)
    {
        Debug.Log("フェードアウトします");
        if (flag)
        {
            alpha += Time.deltaTime / 2;
            fader.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, alpha);
        }
    }
}
