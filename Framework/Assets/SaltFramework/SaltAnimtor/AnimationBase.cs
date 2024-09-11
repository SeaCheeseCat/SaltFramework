using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationBase
{
    public Transform Target { get; private set; }
    public float? Duration { get; private set; }  // 修改为可空类型
    public AnimationCurve Curve { get; private set; }
    public Action OnComplete { get; private set; }

    protected float elapsedTime = 0f;
    public bool isPlaying = false;

    public AnimationBase(Transform target, float? duration, AnimationCurve curve, Action onComplete = null)
    {
        Target = target;
        Duration = duration;
        Curve = curve;
        OnComplete = onComplete;
    }

    public virtual void Play()
    {
        isPlaying = true;
        elapsedTime = 0f;
    }

    public void Stop()
    {
        isPlaying = false;
    }

    public virtual void Update()
    {
        if (!isPlaying || Duration == null) return;

        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / Duration.Value);
        Animate(Curve.Evaluate(t));

        if (elapsedTime >= Duration)
        {
            isPlaying = false;
            OnComplete?.Invoke();
        }
    }

    protected abstract void Animate(float t);
}