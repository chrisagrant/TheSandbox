using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation
{
    interface Tile
    {
        Tile[] GetAdjacentTiles();
    }

    abstract class RealTile : Tile
    {
        Tile[] adjacent;

        public RealTile(Tile[] adj)
        {
            adjacent = adj;
        }

        public abstract Tile[] GetAdjacentTiles();
    }

    class SquareTile : RealTile
    {
        public SquareTile(Tile[] adj) : base(adj)
        {
        }

        public override Tile[] GetAdjacentTiles()
        {
            throw new NotImplementedException();
        }
    }

    class HexTile : RealTile
    {
        public HexTile(Tile[] adj) : base(adj)
        {
        }

        public override Tile[] GetAdjacentTiles()
        {
            throw new NotImplementedException();
        }
    }

    class NoTile : Tile
    {

        public static NoTile Tile = new NoTile();

        private NoTile() { if (Tile != null) throw new InvalidOperationException("Tried to instantiate a NoTile"); }

        public Tile[] GetAdjacentTiles()
        {
            return new Tile[0];
        }
    }

    struct CartesianTileContainer
    {
        Tile[,] tiles;

        public Tile this[int x, int y]
        {
            get { return x > 0 && x < tiles.GetLength(0) && y > 0 && y < tiles.GetLength(1) ? tiles[x,y] : NoTile.Tile; }
        }
    }

    struct PolarTileContainer
    {
        Tile[][] tiles;
    }

    abstract class Map
    {
        Tile[,] tiles;

        public Map(int x, int y)
        {
            tiles = new Tile[x, y];
        }
    }

    class SquareMap : Map
    {
        public SquareMap(int x, int y) : base(x, y)
        {
            for (var i = 0; i <= x; ++i)
            {
                for (var j = 0; j <= y; ++j)
                {

                }
            }
        }
    }
      
    class HexMap : Map
    {
        public HexMap(int x, int y) : base(x, y)
        {
        }
    }
}
