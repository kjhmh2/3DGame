using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider mainSlider;
    public float HP;

    private void Start()
    {
        mainSlider = GetComponent<Slider>();
        HP = mainSlider.maxValue;
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 40), "Increase HP"))
            HP += 10;
        if (GUI.Button(new Rect(10, 70, 100, 40), "Decrease HP"))
            HP -= 10;
        if (HP > 100)
            HP = 100;
        if (HP < 0)
            HP = 0;
        mainSlider.value = HP;
    }
}