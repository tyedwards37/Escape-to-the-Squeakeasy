using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPage : MonoBehaviour
{
    private void Start()
    {
        transform.localScale = Vector2.zero;
    }

    public void OpenCreditsPage()
    {
        transform.LeanScale(Vector2.one, 0.8f).setIgnoreTimeScale(true);
    }

    public void CloseCreditsPage()
    {
        transform.LeanScale(Vector2.zero, 1f).setEaseInBack().setIgnoreTimeScale(true);
    }

    public void ImmediatelyCloseCreditsPage()
    {
        transform.LeanScale(Vector2.zero, 0f).setEaseInBack().setIgnoreTimeScale(true).setOnComplete(() =>
        {
            gameObject.SetActive(true);
        });
    }
}
