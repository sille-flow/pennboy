using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HealthSafetyManager : MonoBehaviour
{
    public List<GameObject> texts;
    public PressAnyKeyBehavior pressAny;
    public GameObject repeatingBg;

    private bool polling;

    private void Awake() {
        pressAny.paused = true;
        repeatingBg.GetComponent<Image>().color = Color.black;
        foreach (var obj in texts) obj.GetComponent<CanvasGroup>().alpha = 0f;
    }

    private IEnumerator Start() {
        yield return new WaitForSeconds(0.3f);

        var rectTransform = repeatingBg.GetComponent<RectTransform>();
        var initScale = rectTransform.localScale;
        var finalScale = new Vector3(2.5f, 2.5f, 2.5f);
        StartCoroutine(Anim.Animate(0.8f, t => {
            t = Easing.EaseOutExpo(t);
            rectTransform.localScale = Vector3.Lerp(initScale, finalScale, t);
        }));

        var bgImage = repeatingBg.GetComponent<Image>();
        var finalBgCol = new Color(0.106f, 0.106f, 0.106f);
        StartCoroutine(Anim.Animate(0.3f, t => {
            t = Easing.EaseOutExpo(t);
            bgImage.color = Color.Lerp(Color.black, finalBgCol, t);
        }));

        foreach (var obj in texts) {
            var rectTrans = obj.GetComponent<RectTransform>();
            var canvasGroup = obj.GetComponent<CanvasGroup>();

            var finalPos = rectTrans.anchoredPosition;
            var initYPos = finalPos.y - 100f;

            StartCoroutine(Anim.Animate(0.7f, t => {
                canvasGroup.alpha = t;
                rectTrans.anchoredPosition =
                    new Vector2(finalPos.x, Mathf.Lerp(initYPos, finalPos.y, Easing.EaseOutExpo(t)));
            }));

            yield return new WaitForSeconds(0.1f);
        }

        polling = true;
        pressAny.paused = false;
    }

    private void Update() {
        if (!polling) return;

        if (Input.anyKey) {
            polling = false;
            StartCoroutine(AnimateSequence());
        }
    }

    private IEnumerator AnimateSequence() {
        pressAny.paused = true;
        pressAny.Flash();

        // Fade out all the text
        var canvasGroups = texts.Select(obj => obj.GetComponent<CanvasGroup>());
        StartCoroutine(Anim.Animate(0.3f, t => {
            foreach (var canvasGroup in canvasGroups) canvasGroup.alpha = 1f - t;
        }));

        // Zoom out background
        var rectTransform = repeatingBg.GetComponent<RectTransform>();
        var initScale = rectTransform.localScale;
        var finalScale = new Vector3(1.5f, 1.5f, 1.5f);
        StartCoroutine(Anim.Animate(0.7f, t => {
            t = Easing.EaseInOutQuint(t);
            rectTransform.localScale = Vector3.Lerp(initScale, finalScale, t);
        }));

        yield return new WaitForSeconds(0.35f);

        // Fade background to white
        var bgImage = repeatingBg.GetComponent<Image>();
        var initBgCol = bgImage.color;
        StartCoroutine(Anim.Animate(0.3f, t => {
            bgImage.color = Color.Lerp(initBgCol, Color.white, t);
        }));
    }
}