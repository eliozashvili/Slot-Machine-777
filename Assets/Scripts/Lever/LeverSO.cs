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
        public Fruits Fruit;
        public int Multiplier;
    }

    [System.Serializable]
    public struct FruitsData
    {
        public Fruits Fruit;
        public Sprite Sprite;
    }

    [Header("Fruits and multipliers\n"), Space(1f)]
    public FruitsMultiplier[] FruitMultiplier;
    public FruitsData[] FruitData;

    public int GetMultiplier(Fruits fruit)
    {
        foreach (FruitsMultiplier fruitMultiplier in FruitMultiplier)
        {
            if (fruitMultiplier.Fruit == fruit)
                return fruitMultiplier.Multiplier;
        }

        return 1;
    }

    public FruitsData GetRandomFruit()
    {
        int randomFruitIndex = Random.Range(0, FruitData.Length);

        return FruitData[randomFruitIndex];
    }

    [Header("Spin options\n"), Space(1f)]
    public float SpinDuration;
    public float StartSlotSpinSpeed;
    public float EndSlotSpinSpeed;
}
