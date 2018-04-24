using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour {

    public List<GameObject> objectsToSpawn;

    List<GameObject> holyFuck;
    List<GameObject> legendary;
    List<GameObject> ultraRare;
    List<GameObject> rare;
    List<GameObject> uncommon;
    List<GameObject> common;

    public float holyFuckRarityWeight = 300;
    public float legendaryRarityWeight = 800;
    public float ultraRareRarityWeight = 2000;
    public float rareRarityWeight = 4000;
    public float uncommonRarityWeight = 7500;
    public float commonRarityWeight = 10000;

    float totalWeight = 0.0f;
    float totalWeightsInUse = 0.0f;

    [ReadOnly] public float holyFuckRarityPercent;
    [ReadOnly] public float legendaryRarityPercent;
    [ReadOnly] public float ultraRareRarityPercent;
    [ReadOnly] public float rareRarityPercent;
    [ReadOnly] public float uncommonRarityPercent;
    [ReadOnly] public float commonRarityPercent;

	// Use this for initialization
	void Start () {

        holyFuck = new List<GameObject>();
        legendary = new List<GameObject>();
        ultraRare = new List<GameObject>();
        rare = new List<GameObject>();
        uncommon = new List<GameObject>();
        common = new List<GameObject>();

        for (int i =0; i < objectsToSpawn.Count; i++)
        {
            switch (objectsToSpawn[i].GetComponent<RarityScript>().myRarityType)
            {
                case RarityType.Common:
                    {
                        common.Add(objectsToSpawn[i]);
                        break;
                    }
                case RarityType.Uncommon:
                    {
                        uncommon.Add(objectsToSpawn[i]);
                        break;
                    }
                case RarityType.Rare:
                    {
                        rare.Add(objectsToSpawn[i]);
                        break;
                    }
                case RarityType.UltraRare:
                    {
                        ultraRare.Add(objectsToSpawn[i]);
                        break;
                    }
                case RarityType.Legendary:
                    {
                        legendary.Add(objectsToSpawn[i]);
                        break;
                    }
                case RarityType.HolyFuck:
                    {
                        holyFuck.Add(objectsToSpawn[i]);
                        break;
                    }
            }
        }

        CalculatePercentages();
    }
	
	// Update is called once per frame
	void Update ()
    {
        float check = (holyFuckRarityWeight + legendaryRarityWeight) + (ultraRareRarityWeight + rareRarityWeight) + (uncommonRarityWeight + commonRarityWeight);

        if(check != totalWeight)
        {
            CalculatePercentages();
        }
    }
    void CalculatePercentages()
    {
        totalWeight = 0.0f;
        totalWeightsInUse = 0.0f;

        totalWeight = (holyFuckRarityWeight + legendaryRarityWeight) + (ultraRareRarityWeight + rareRarityWeight) + (uncommonRarityWeight + commonRarityWeight);

        holyFuckRarityPercent = (holyFuckRarityWeight / totalWeight) * 100;
        legendaryRarityPercent = (legendaryRarityWeight / totalWeight) * 100;
        ultraRareRarityPercent = (ultraRareRarityWeight / totalWeight) * 100;
        rareRarityPercent = (rareRarityWeight / totalWeight) * 100;
        uncommonRarityPercent = (uncommonRarityWeight / totalWeight) * 100;
        commonRarityPercent = (commonRarityWeight / totalWeight) * 100;

        if (common.Count > 0)
        {
            totalWeightsInUse += commonRarityWeight;
        }
        if (uncommon.Count > 0)
        {
            totalWeightsInUse += uncommonRarityWeight;
        }
        if (rare.Count > 0)
        {
            totalWeightsInUse += rareRarityWeight;
        }
        if (ultraRare.Count > 0)
        {
            totalWeightsInUse += ultraRareRarityWeight;
        }
        if (legendary.Count > 0)
        {
            totalWeightsInUse += legendaryRarityWeight;
        }
        if (holyFuck.Count > 0)
        {
            totalWeightsInUse += holyFuckRarityWeight;
        }
    }

    public GameObject SpawnObject()
    {
        float ranNum = Random.Range(0, totalWeightsInUse);

        if (common.Count > 0)
        {
            if (ranNum < commonRarityWeight)
            {
                int pos = (int)(ranNum / (commonRarityWeight / common.Count));
                return common[pos];
            }
            else
            {
                ranNum -= commonRarityWeight;
            }
        }
        if (uncommon.Count > 0)
        {
            if (ranNum < uncommonRarityWeight)
            {
                int pos = (int)(ranNum / (uncommonRarityWeight / uncommon.Count));
                return uncommon[pos];
            }
            else
            {
                ranNum -= uncommonRarityWeight;
            }
        }
        if (rare.Count > 0)
        {
            if (ranNum < rareRarityWeight)
            {
                int pos = (int)(ranNum / (rareRarityWeight / rare.Count));
                return rare[pos];
            }
            else
            {
                ranNum -= rareRarityWeight;
            }
        }
        if (ultraRare.Count > 0)
        {
            if (ranNum < ultraRareRarityWeight)
            {
                int pos = (int)(ranNum / (ultraRareRarityWeight / ultraRare.Count));
                return ultraRare[pos];
            }
            else
            {
                ranNum -= ultraRareRarityWeight;
            }
        }
        if (legendary.Count > 0)
        {
            if (ranNum < legendaryRarityWeight)
            {
                int pos = (int)(ranNum / (legendaryRarityWeight / legendary.Count));
                return legendary[pos];
            }
            else
            {
                ranNum -= legendaryRarityWeight;
            }
        }
        if (holyFuck.Count > 0)
        {
            if (ranNum < holyFuckRarityWeight)
            {
                int pos = (int)(ranNum / (holyFuckRarityWeight / holyFuck.Count));
                return holyFuck[pos];
            }
            else
            {
                ranNum -= holyFuckRarityWeight;
            }
        }

        return null;
    }
}
