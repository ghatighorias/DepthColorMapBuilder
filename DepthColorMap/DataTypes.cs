using System;
using System.Drawing;

namespace DepthColorMap
{
    public class Constants
    {
        public const int MinDepthValue = 0;
        public const int MaxDepthValue = 8191;
        public const int BitmapWidth   = 128;
        public const int BitmapHeight  = 64;
    }

    public struct ColoredDepthData
    {
        public int StartOfRange;
        public int EndOfRange;
        public Color RangeColor;
        public ColoredDepthData(int sRange, int eRange, Color rColor)
        {
            StartOfRange = sRange;
            EndOfRange = eRange;
            RangeColor = rColor;
        }
    }
}
