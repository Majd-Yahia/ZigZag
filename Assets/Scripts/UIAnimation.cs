using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public void Open(GameObject obj)
    {
        obj.transform.LeanScale(Vector2.one, 0.8f);
    }

    public void Close(GameObject obj)
    {
        obj.transform.LeanScale(Vector2.zero, 0.8f);
    }

    public void Shrink(TextMeshProUGUI text)
    {
        text.transform.LeanScale(Vector2.zero, 0.2f);
    }

    public void Grow(TextMeshProUGUI text)
    {
        text.transform.LeanScale(Vector2.one, 0.2f);
    }

    public void ChangeColor(TextMeshProUGUI text, float countDown)
    {
        if (countDown > 1)
        {
            text.color = Color.yellow;
        }
        else
        {
            text.color = Color.green;
        }
    }


}
