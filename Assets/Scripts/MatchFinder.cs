using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchFinder : MonoBehaviour
{
    [SerializeField] private Board _board;

    private void Awake()
    {
        _board = FindObjectOfType<Board>();
    }

    public void FindAllMatches()
    {
        for (int x = 0; x < _board.width; x++)
        {
            for (int y = 0; y < _board.height; y++)
            {
                Gem currentGem = _board.allGems[x, y];
                if (currentGem != null)
                {
                    if (x > 0 && x < _board.width-1)
                    {
                        Gem leftGem = _board.allGems[x - 1, y];
                        Gem rightGem = _board.allGems[x + 1, y];
                        if (leftGem != null && rightGem != null)
                        {
                            if (leftGem.type == currentGem.type && rightGem.type == currentGem.type)
                            {
                                currentGem.isMatched = true;
                                leftGem.isMatched = true;
                                rightGem.isMatched = true;
                            }
                        }
                    }
                    if (y > 0 && y < _board.height-1)
                    {
                        Gem aboveGem = _board.allGems[x, y+1];
                        Gem belowGem = _board.allGems[x, y-1];
                        if (aboveGem != null && belowGem != null)
                        {
                            if (aboveGem.type == currentGem.type && belowGem.type == currentGem.type)
                            {
                                currentGem.isMatched = true;
                                aboveGem.isMatched = true;
                                belowGem.isMatched = true;
                            }
                        }
                    }
                }
                
            }
        }
    }
}
