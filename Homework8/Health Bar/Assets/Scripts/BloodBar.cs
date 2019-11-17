using UnityEngine;

public class BloodBar : MonoBehaviour
{
    public float health = 0.0f;

    private float resultHealth;
    private Rect HealthBar;

    void Start()
    {
        HealthBar = new Rect(200, 40, 200, 20);
        resultHealth = health;
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 40), "Increase HP"))
            resultHealth = resultHealth + 0.1f;
        if (GUI.Button(new Rect(10, 70, 100, 40), "Decrease HP"))
            resultHealth = resultHealth - 0.1f;
        if (resultHealth > 1.0f)
            resultHealth = 1.0f;
        if (resultHealth < 0.0f)
            resultHealth = 0.0f;

        // make health bar change smoothly
        health = Mathf.Lerp(health, resultHealth, 0.05f);

        // show health
        GUI.HorizontalScrollbar(HealthBar, 0.0f, health, 0.0f, 1.0f);
    }
}