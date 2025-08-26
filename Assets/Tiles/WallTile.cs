using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Tile_Wall", menuName = "2D/Tiles/WallTile")]
public class WallTile : RuleTile<WallTile.Neighbor>
{
    public List<TileBase> innerTiles = new List<TileBase>();

    public class Neighbor : RuleTile.TilingRule.Neighbor
    {
        public const int InnerTile = 3;
    }

    public override bool RuleMatch(int neighbor, TileBase tile)
    {
        switch (neighbor) {
            case Neighbor.InnerTile: return innerTiles.Contains(tile);
        }
        return base.RuleMatch(neighbor, tile);
    }
}