using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Monitor", menuName = "Scriptable Objects/Monitor")]
public class MonitorSO : ScriptableObject
{
    [Header("Fruit visuals on default and while spin\n"), Space(1f)]
    public Vector2 DefaultFruitScale;
    public Vector2 FruitScaleWhileSpin;
    public float DefaultAlpha;
    public float FruitAlphaWhileSpin;

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

