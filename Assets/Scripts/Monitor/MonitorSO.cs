using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Monitor", menuName = "Scriptable Objects/Monitor")]
public class MonitorSO : ScriptableObject
{
    [Header("Player information\n"), Space(1f)]
    public int depositAmount;
    public int betAmount;

    [Header("Fruit visuals on default and while spin\n"), Space(1f)]
    public Vector2 defaultFruitScale;
    public Vector2 fruitScaleWhileSpin;
    public float defaultAlpha;
    public float fruitAlphaWhileSpin;

    public void ChangeFruitsVisuals(Vector2 scale, float alpha, Image[] reelSlotFruits)
    {
        foreach (Image fruit in reelSlotFruits)
        {
            Color color = fruit.color;
            color.a = alpha;
            fruit.color = color;

            fruit.transform.localScale = new Vector3(scale.x, scale.y, 1f);
        }
    }
}
