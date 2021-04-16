using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannoneer
{
    public Cannoneer(Ship ship)
    {
        this.ship = ship;
        InitArtillery();
        spawner = Pool.Instance;
        prefabHolder = PrefabHolder.Instance;
        RegistrPoolOfProjectiles();
    }

    private Ship ship;
    private Pool spawner;
    private PrefabHolder prefabHolder;
    private Dictionary<BoardSide, Artillery[]> artillery;

    private const string ART = "Artillery";
    private const string BALL = "Cannonball";


    public void Shoot(BoardSide boardSide, float power)
    {
        var possibleArtillery = artillery[boardSide];
        for (int i = 0; i < possibleArtillery.Length; i++)
        {
            if (possibleArtillery[i].IsReadyToShoot)
            {
                possibleArtillery[i].Shoot(power);
                break;
            }
        }
    }

    public void VolleyShot(BoardSide boardSide, float power)
    {
        var possibleArtillery = artillery[boardSide];
        for (int i = 0; i < possibleArtillery.Length; i++)
        {
            possibleArtillery[i].Shoot(power);
        }
    }

    private void RegistrPoolOfProjectiles()
    {
        spawner.AddToPool(BALL, artillery.Count, ship.ShipID);
    }

    private void InitArtillery()
    {
        var artHolder = ship.Transform.Find(ART);
        Artillery[] allArt = artHolder.GetComponentsInChildren<Artillery>();

        artillery = new Dictionary<BoardSide, Artillery[]>();
        for (int i = 0; i < 4; i++)
        {
            List<Artillery> sameSideArtillery = new List<Artillery>();
            BoardSide board = (BoardSide)i;
            for (int j = 0; j < allArt.Length; j++)
            {
                var side = allArt[j].GetBoardSide();
                if (board == side)
                {
                    sameSideArtillery.Add(allArt[j]);
                }
            }
            Artillery[] convertedArray = sameSideArtillery.ToArray();
            artillery.Add(board, convertedArray);
        }
    }
}
