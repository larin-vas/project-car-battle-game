using Code.Common;
using Code.Map.Tile;
using System;

namespace Code.Map
{
    public class MapModel
    {
        public Observable<TileModel>[,] Map { get; }

        public int Seed { get; set; }

        public int Width { get; }
        public int Height { get; }

        public float MapScale { get; }

        public float HillsRatio { get; }

        public int BorderWidth { get; }

        public MapModel(
            int seed, int width, int height, float mapScale, float hillsRatio, int borderWidth)
        {
            ValidateArguments(width, height, mapScale, hillsRatio, borderWidth);

            Map = CreateMapArray(width, height);

            Seed = seed;

            Width = width;
            Height = height;

            MapScale = mapScale;

            HillsRatio = hillsRatio;

            BorderWidth = borderWidth;
        }

        private void ValidateArguments(int width, int height, float mapScale, float hillsRatio, int borderWidth)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException(nameof(width));

            if (height <= 0)
                throw new ArgumentOutOfRangeException(nameof(height));

            if (mapScale <= 0)
                throw new ArgumentOutOfRangeException(nameof(mapScale));

            if (hillsRatio < 0 || hillsRatio > 1)
                throw new ArgumentOutOfRangeException(nameof(hillsRatio));

            if (borderWidth <= 0)
                throw new ArgumentOutOfRangeException(nameof(borderWidth));
        }

        private Observable<TileModel>[,] CreateMapArray(int width, int height)
        {
            Observable<TileModel>[,] map = new Observable<TileModel>[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    map[x, y] = new Observable<TileModel>(null);
                }
            }

            return map;
        }
    }
}
