using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cooldown : MonoBehaviour
{
    public Image image;
    public Button button;
    public TextMeshProUGUI text;
    public float coolTime;
    public bool isClicked = false;
    float leftTime;
    float coolDownSpeed = 1;

    void Update()
    {

        if (isClicked)
            if (leftTime > 0)
            {
                leftTime -= coolDownSpeed * Time.deltaTime;
                text.text = leftTime.ToString("F1");
                if (leftTime < 0)
                {
                    leftTime = 0;
                    text.enabled = false;
                    image.enabled = false;
                    if (button)
                        button.enabled = true;
                    isClicked = true;
                }

                float ratio = 1.0f - (leftTime / coolTime);
                if (image)
                    image.fillAmount = ratio;
            }
    }

    public void StartCoolTime()
    {
        text.enabled = true;
        image.enabled = true;
        leftTime = coolTime;
        isClicked = true;
        if (button)
            button.enabled = false;
    }
}
