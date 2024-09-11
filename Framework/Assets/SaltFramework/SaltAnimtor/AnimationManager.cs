using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private List<AnimationBase> animations = new List<AnimationBase>();

    public void AddAnimation(AnimationBase animation)
    {
        animations.Add(animation);
        animation.Play();
    }

    private void Update()
    {
        for (int i = animations.Count - 1; i >= 0; i--)
        {
            animations[i].Update();
            if (!animations[i].isPlaying)
            {
                animations.RemoveAt(i);
            }
        }
    }
}
