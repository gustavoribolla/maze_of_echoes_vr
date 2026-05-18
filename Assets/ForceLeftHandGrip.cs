using UnityEngine;

public class ForceLeftHandGrip : MonoBehaviour
{
    public Animator handAnimator;

    void Update()
    {
        if (handAnimator == null) return;

        handAnimator.SetFloat("Trigger", 1f);
        handAnimator.SetFloat("Grip", 1f);
    }
}