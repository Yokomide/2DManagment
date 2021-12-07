using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomInputField : MonoBehaviour
{
    [HideInInspector] public ProgramVisualEditor Handler;
    [HideInInspector] public Text Text;
    public Text LineNumber;
    
    private string caret = "<color=white>|</color>";

    private void Awake()
    {
        Text = GetComponent<Text>();
    }

    IEnumerator CaretAnimating()
    {
        bool active = false;
        while (true)
        {
            if (active)
            {
                RemoveCaret();
            }
            else
            {
                AddCaret();
            }
            active = !active;
            yield return new WaitForSeconds(0.5f);
        }
    }
    
    private void AddCaret()
    {
        Text.text += caret;
    }

    public void RemoveCaret()
    {
        int index = Text.text.IndexOf(caret);
        if (index < 0)
            return;
        
        Text.text = Text.text.Remove(index);
    }

    public void FocusIt()
    {
        Handler?.OnFocused(this);
    }

    public void AnimateCaret()
    {
        StartCoroutine("CaretAnimating");
    }

    public void StopAnimateCaret()
    {
        StopCoroutine("CaretAnimating");
        RemoveCaret();
    }
}
