using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public List<Page> pages;
    public UnityEngine.UI.Image blackBackground;
    public float fadeInTime = 0.5f;

    private void Start()
    {
        blackBackground.gameObject.SetActive(false);

        foreach (Page page in pages)
            page.canvasGroup.gameObject.SetActive(false);
    }

    public void Play(Action whenFinished)
    {
        StopAllCoroutines();
        StartCoroutine(PlayCutscene(whenFinished));
    }

    IEnumerator PlayCutscene(Action whenFinished)
    {
        blackBackground.gameObject.SetActive(true);

        // Fade in background
        float fadeTimer;
        fadeTimer = fadeInTime;
        while (fadeTimer > 0)
        {
            fadeTimer -= Time.deltaTime;
            blackBackground.color = new Color(0, 0, 0, 1f - (fadeTimer / fadeInTime)); // Fade in slowly
            yield return null;
        }

        blackBackground.color = new Color(0, 0, 0); // Solid black

        // Go through pages one at a time
        for (int i = 0; i < pages.Count; i++)
        {
            yield return StartCoroutine(pages[i].Display(this));
        }

        whenFinished();
        gameObject.SetActive(false);
    }

    [Serializable]
    public class Page
    {
        public CanvasGroup canvasGroup;
        public List<Image> images;
        public float extraTimeAfterAllImagesVisible = 1f;
        public float fadeOutTime = 0.5f;

        public IEnumerator Display(MonoBehaviour src)
        {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.alpha = 1; // Enable

            float longestImage = 0;
            foreach (Image image in images)
            {
                image.element.color = new Color(1, 1, 1, 0f); // Transparent
                src.StartCoroutine(FadeInImage(image)); // Start all asynchronously
                // Find out when the last image is fully displayed
                longestImage = Mathf.Max(longestImage, image.fadeInTime + image.delayBeforeFadingIn);
            }

            yield return new WaitForSeconds(longestImage + extraTimeAfterAllImagesVisible);

            float timer = fadeOutTime;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                canvasGroup.alpha = timer / fadeOutTime; // Fade out slowly
                yield return null;
            }
        }

        public IEnumerator FadeInImage(Image image)
        {
            yield return new WaitForSeconds(image.delayBeforeFadingIn);

            float fadeTimer = image.fadeInTime;
            while (fadeTimer > 0)
            {
                fadeTimer -= Time.deltaTime;
                image.element.color = new Color(1, 1, 1, 1f - (fadeTimer / image.fadeInTime)); // Fade in slowly
                yield return null;
            }
        }
    }

    [Serializable]
    public class Image
    {
        public UnityEngine.UI.Image element;
        public float fadeInTime = 1f;
        public float delayBeforeFadingIn;
    }
}
