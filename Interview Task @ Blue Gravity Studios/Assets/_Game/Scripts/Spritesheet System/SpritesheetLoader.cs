using UnityEngine;

public class SpritesheetLoader
{
    Sprite[] loadedSprites;

    public SpritesheetLoader(string spriteName)
    {
        loadedSprites = Resources.LoadAll<Sprite>(spriteName);
    }

    public Sprite[] GetSpriteArrayFromIndexes(string sAnimIndexes)
    {
        string[] sAnimIndexesSplit = sAnimIndexes.Split(',');

        Sprite[] spriteGroup = new Sprite[sAnimIndexesSplit.Length];

        int index = 0;
        foreach (var item in sAnimIndexesSplit)
        {
            int animIndex = int.Parse(item);

            spriteGroup[index] = loadedSprites[animIndex];

            ++index;
        }

        return spriteGroup;
    }
}
