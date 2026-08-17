using UnityEngine;

[CreateAssetMenu(fileName = "GameOptions", menuName = "Scriptable Objects/GameOptions")]
public class LeverSO : ScriptableObject
{
    public enum Fruits
    {
        Seven,
        Banana,
        Orange,
        Strawberry
    }

    [System.Serializable]
    public struct FruitsMultiplier
    {
        public Fruits fruit;
        public int multiplier;
    }

    [System.Serializable]
    public struct FruitsData
    {
        public Fruits fruit;
        public Sprite sprite;
    }

    [Header("Fruits and multipliers\n"), Space(1f)]
    public FruitsMultiplier[] fruitsMultiplier;
    public FruitsData[] fruitData;

    public int GetMultiplier(Fruits fruit)
    {
        foreach (FruitsMultiplier fruitMultiplier in fruitsMultiplier)
        {
            if (fruitMultiplier.fruit == fruit)
                return fruitMultiplier.multiplier;
        }

        return 1;
    }

    public FruitsData GetRandomFruit()
    {
        int randomFruitIndex = Random.Range(0, fruitData.Length);

        return fruitData[randomFruitIndex];
    }

    [Header("Spin options\n"), Space(1f)]
    public float spinDuration;
    public float startSlotSpinSpeed;
    public float endSlotSpinSpeed;
}
