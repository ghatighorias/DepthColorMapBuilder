using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthColorMap
{
    public class ColorMapper
    {

        public void SetImageZeroDistance(ref Bitmap ColoredMap)
        {
            try
            {
                CheckBitmap(ref ColoredMap);
            }
            catch (ArgumentException e)
            {
                throw e;
            }

            for (int i = Constants.MinDepthValue; i <= Constants.MaxDepthValue; i++)
                ColoredMap.SetPixel(i % Constants.BitmapWidth, i / Constants.BitmapWidth, Color.Transparent);
        }

        public void SetColorRange(int startingDepth, int endingDepth, Color color, ref Bitmap ColoredMap)
        {
            try
            {
                CheckBitmap(ref ColoredMap);
            }
            catch (ArgumentException e)
            {
                throw e;
            }

            for (int i = startingDepth; i <= endingDepth; i++)
                ColoredMap.SetPixel(i % Constants.BitmapWidth, i / Constants.BitmapWidth, color);
        }

        private void CheckBitmap(ref Bitmap bitmap)
        {
            if (bitmap == null)
                bitmap = new Bitmap(Constants.BitmapWidth, Constants.BitmapHeight);
            else if (bitmap.Size.Height != Constants.BitmapHeight || bitmap.Size.Width != Constants.BitmapWidth)
                throw new ArgumentException("Size of the bitmap is restricted to 128 * 64");
        }

        public List<ColoredDepthData> ReadColorMap(Bitmap ColoredMap)
        {
            List<ColoredDepthData> RangeSet = new List<ColoredDepthData>();
            int startOfRange = 0;
            int endOfRange = 0;
            Color rangeColor = ColoredMap.GetPixel(0, 0);

            for (int i = 0; i < Constants.BitmapHeight; i++)
                for (int j = 0; j < Constants.BitmapWidth; j++)
                {

                    if (rangeColor != ColoredMap.GetPixel(j, i))
                    {

                        // closing the current range
                        endOfRange = i * Constants.BitmapWidth + (j - 1);
                        RangeSet.Add(new ColoredDepthData(startOfRange, endOfRange, rangeColor));

                        // start of the new range
                        startOfRange = i * Constants.BitmapWidth + j;
                        rangeColor = ColoredMap.GetPixel(j, i);
                        //     newIsRangeDetected = false;
                    }

                }
            // adding the last range
            endOfRange = Constants.MaxDepthValue;
            RangeSet.Add(new ColoredDepthData(startOfRange, endOfRange, rangeColor));

            return RangeSet;
        }
    }
}