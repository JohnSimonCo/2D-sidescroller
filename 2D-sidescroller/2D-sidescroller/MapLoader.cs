using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;

namespace _2D_sidescroller
{
	class MapLoader
	{

		public void Load(string filename)
		{
			Bitmap map = TextureLoader.LoadBitmap(filename);

			int width = map.Width;
			int height = map.Height;

			for (int i = 0; i < map.Width; i++) {
				Color[] row = GetPixelColumn(map, i);
				for (int j = 0; j < row.Length; j++)
				{

				}
			}
		}


		/*Might be a little slow, see
		 * http://stackoverflow.com/questions/7699056/what-is-the-best-and-fast-way-to-get-pixels-column-of-bitmap
		 * faster versio nrequires 'unsafe' command though
		 */
		static Color[] GetPixelColumn(Bitmap bmp, int x)
		{
			Color[] pixelColumn = new Color[bmp.Height];
			for (int i = 0; i < bmp.Height; ++i)
			{
				pixelColumn[i] = bmp.GetPixel(x, i);
			}
			return pixelColumn;
		}

	}
}
