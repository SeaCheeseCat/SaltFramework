using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTypingAnimation : AnimationBase
{
    private Text textComponent;
    private string fullText;
    private float typingSpeed;
    private Coroutine typingCoroutine;

    public TextTypingAnimation(Text textComponent, string fullText, float typingSpeed, Action onComplete = null)
        : base(null, 0, null, onComplete) 
    {
        this.textComponent = textComponent;
        this.fullText = fullText;
        this.typingSpeed = typingSpeed;
    }

    protected override void Animate(float t)
    {
        
    }

    public override void Play()
    {
        base.Play();
        if (typingCoroutine != null)
        {
            GameBase.Instance.StopCoroutine(typingCoroutine);
        }
        typingCoroutine = GameBase.Instance.StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        textComponent.text = "";

        foreach (char character in fullText)
        {
            textComponent.text += character;
            yield return new WaitForSeconds(typingSpeed);
        }

        typingCoroutine = null;
        OnComplete?.Invoke();
    }
}