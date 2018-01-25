using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Simulation
{

    public class Tile
    {
        public static Tile NoTile = new Tile();
    }

    public class SquareMap
    {
        Tile[,] tiles;

        public SquareMap(int x, int y)
        {
            tiles = new Tile[x, y];
            for (var i = 0; i < x; ++i)
            {
                for (var j = 0; j < y; ++j)
                {
                    tiles[i, j] = new Tile();
                }
            }
        }

        public Tile GetTile(int x, int y)
        {
            return x > 0 && x < tiles.GetLength(0) && y > 0 && y < tiles.GetLength(1) ? tiles[x, y] : Tile.NoTile;
        }

        public Mesh GetBasicMesh()
        {
            //NOTE: unoptimized
            var ret = new Mesh();
            var vertices = new Vector3[4 * tiles.Length];
            var triangles = new int[6 * tiles.Length];
            var offset = new Vector3();
            var count = 0;
            for (var i = 0; i < tiles.GetLength(1); ++i)
            {
                for (var j = 0; j < tiles.GetLength(0); ++j)
                {
                    var k = count * 4;
                    vertices[k] = (Quaternion.Euler(0, 0, 45) * Vector3.up) / (float)Math.Sqrt(2) + offset;
                    vertices[k + 1] = (Quaternion.Euler(0, 0, 135) * Vector3.up) / (float)Math.Sqrt(2) + offset;
                    vertices[k + 2] = (Quaternion.Euler(0, 0, 225) * Vector3.up)/(float)Math.Sqrt(2) + offset;
                    vertices[k + 3] = (Quaternion.Euler(0, 0, 315) * Vector3.up) / (float)Math.Sqrt(2) + offset;
                    offset += new Vector3(1f, 0);

                    triangles[count * 6] = k;
                    triangles[count * 6 + 1] = k + 1;
                    triangles[count * 6 + 2] = k + 2;
                    triangles[count * 6 + 3] = k + 2;
                    triangles[count * 6 + 4] = k + 3;
                    triangles[count * 6 + 5] = k;
                    ++count;
                }
                offset += new Vector3(-tiles.GetLength(0), 1f);
            }

            ret.SetVertices(vertices.ToList());
            ret.SetTriangles(triangles, 0);
            return ret;
        }
    }

    public class HexGridMap
    {
        Tile[,] tiles;

        public HexGridMap(int x, int y)
        {
            tiles = new Tile[x, y];
            for (var i = 0; i < x; ++i)
            {
                for (var j = 0; j < y; ++j)
                {
                    tiles[i, j] = new Tile();
                }
            }
        }

        public Tile GetTile(int x, int y)
        {
            return x > 0 && x < tiles.GetLength(0) && y > 0 && y < tiles.GetLength(1) ? tiles[x, y] : Tile.NoTile;
        }

        public Mesh GetBasicMesh()
        {
            //NOTE: unoptimized
            var ret = new Mesh();
            var vertices = new Vector3[7 * tiles.Length];
            var triangles = new int[18 * tiles.Length];
            var offset = new Vector3();
            var sign = 1;
            var count = 0;
            for (var i = 0; i < tiles.GetLength(1); ++i)
            {
                for (var j = 0; j < tiles.GetLength(0); ++j)
                {
                    var k = count * 7;
                    vertices[k] = offset;
                    vertices[k + 1] = Vector3.up + offset;
                    vertices[k + 2] = (Quaternion.Euler(0, 0, 60) * Vector3.up) + offset;
                    vertices[k + 3] = (Quaternion.Euler(0, 0, 120) * Vector3.up) + offset;
                    vertices[k + 4] = (Quaternion.Euler(0, 0, 180) * Vector3.up) + offset;
                    vertices[k + 5] = (Quaternion.Euler(0, 0, 240) * Vector3.up) + offset;
                    vertices[k + 6] = (Quaternion.Euler(0, 0, 300) * Vector3.up) + offset;
                    offset += 2 * new Vector3((float)(Math.Cos(Math.PI / 6)), 0);

                    for (var l = 0; l < 6; ++l)
                    {
                        var u = (count) * 18 + l * 3;
                        triangles[u] = k;
                        triangles[u + 1] = k + l + 1;
                        triangles[u + 2] = k + ((l + 1) % 6) + 1;
                    }
                    ++count;
                }
                offset += new Vector3((float) ((-2 * tiles.GetLength(0) * Math.Cos(Math.PI/6)) + sign * Math.Cos(Math.PI/6)), 1.5f);
                sign = -sign;
            }

            ret.SetVertices(vertices.ToList());
            ret.SetTriangles(triangles, 0);
            return ret;
        }
    }

    public class HexMap
    {
        Tile[][] tiles;

        public HexMap(int r)
        {
            tiles = new Tile[r][];

            for (var i = 0; i < r; ++i)
            {
                tiles[i] = new Tile[i == 0 ? 1 : (i * 6)];
                for (var j = 0; j < i * 6; ++j)
                {
                    tiles[i][j] = new Tile();
                }
            }
        }

        public Tile GetTile(int r, int dir)
        {
            return r >= 0 && r < tiles.Length && dir >= 0 && dir < tiles[r].Length ? tiles[r][dir] : Tile.NoTile;
        }

        public Mesh GetBasicMesh()
        {
            //NOTE: unoptimized
            var ret = new Mesh();
            var vertices = new Vector3[7 * tiles.Length];
            var triangles = new int[18 * tiles.Length];
            var offset = new Vector3();
            var count = 0;
            for (var i = 0; i < tiles.Length; ++i)
            {
                for (var j = 0; j < tiles[i].Length; ++j)
                {
                    var k = count * 7;
                    vertices[k] = offset;
                    vertices[k + 1] = Vector3.up + offset;
                    vertices[k + 2] = (Quaternion.Euler(0, 0, 60) * Vector3.up) + offset;
                    vertices[k + 3] = (Quaternion.Euler(0, 0, 120) * Vector3.up) + offset;
                    vertices[k + 4] = (Quaternion.Euler(0, 0, 180) * Vector3.up) + offset;
                    vertices[k + 5] = (Quaternion.Euler(0, 0, 240) * Vector3.up) + offset;
                    vertices[k + 6] = (Quaternion.Euler(0, 0, 300) * Vector3.up) + offset;
                    offset = Quaternion.Euler(0, 0, 360f / tiles[i].Length) * offset;

                    for (var l = 0; l < 6; ++l)
                    {
                        var u = (count) * 18 + l * 3;
                        triangles[u] = k;
                        triangles[u + 1] = k + l + 1;
                        triangles[u + 2] = k + ((l + 1) % 6) + 1;
                    }
                    ++count;
                }
                offset = tiles[i].Length * Vector3.up;
            }

            ret.SetVertices(vertices.ToList());
            ret.SetTriangles(triangles, 0);
            return ret;
        }
    }
}
