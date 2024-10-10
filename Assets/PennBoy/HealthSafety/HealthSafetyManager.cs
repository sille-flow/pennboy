using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PennBoy
{
public class HealthSafetyManager : MonoBehaviour
{
    [Header("Background Canvas")] public GameObject repeatingBg;

    [Header("Intro Canvas")] public GameObject intro;
    public List<GameObject> texts;
    public PressAnyKeyBehavior pressAny;

    [Header("Logo Sequence Canvas")] public GameObject logoSequence;
    public GameObject upgradeLogo;
    public List<GameObject> letters;

    private bool polling;

    private void Awake() {
#if UNITY_EDITOR
        intro.SetActive(true);
        logoSequence.SetActive(false);
#endif
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
            StartCoroutine(AnimateIntroSequence());
        }
    }

    private IEnumerator AnimateIntroSequence() {
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
            var newT = Easing.EaseInOutQuint(t);
            rectTransform.localScale = Vector3.Lerp(initScale, finalScale, newT);
        }));

        yield return new WaitForSeconds(0.35f);

        // Fade background to white
        var bgImage = repeatingBg.GetComponent<Image>();
        var initBgCol = bgImage.color;
        yield return Anim.Animate(0.3f, t => {
            bgImage.color = Color.Lerp(initBgCol, Color.white, t);
        });

        StartCoroutine(AnimateLogoSequence());
    }

    private IEnumerator AnimateLogoSequence() {
        // Setup animation, transition letters downwards and set opacities to zero
        var letterRectTrans = letters.Select(letter => letter.GetComponent<RectTransform>());
        var letterCanvasGroups = letters.Select(letter => letter.GetComponent<CanvasGroup>());
        var letterAttrs = letterRectTrans.Zip(letterCanvasGroups, (a, b) => (a, b)).ToList();

        upgradeLogo.GetComponent<CanvasGroup>().alpha = 0f;
        foreach (var (rectTrans, canvasGroup) in letterAttrs) {
            canvasGroup.alpha = 0f;
            rectTrans.anchoredPosition = new Vector2(rectTrans.anchoredPosition.x, -170);
        }

        intro.SetActive(false);
        logoSequence.SetActive(true);

        // Animate PennBoy logo upwards, letter by letter, fading in
        foreach (var (rectTrans, canvasGroup) in letterAttrs) {
            var initPos = rectTrans.anchoredPosition;
            var finalPos = new Vector2(rectTrans.anchoredPosition.x, 78);

            StartCoroutine(Anim.Animate(0.05f, t => {
                canvasGroup.alpha = t;
            }));

            StartCoroutine(Anim.Animate(0.5f, t => {
                rectTrans.anchoredPosition = Vector2.Lerp(initPos, finalPos, Easing.EaseInOutExpo(t));
            }));

            yield return new WaitForSeconds(0.1f);
        }

        // Fade in UPGRADE logo
        var upgradeCanvasGroup = upgradeLogo.GetComponent<CanvasGroup>();
        yield return Anim.Animate(0.3f, t => {
            upgradeCanvasGroup.alpha = t;
        });
    }
}
}