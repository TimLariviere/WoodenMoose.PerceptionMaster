using System;
using Windows.UI;

namespace WoodenMoose.PerceptionMaster.UWP.Helpers
{
    public static class ColorHelper
    {
        private static Random _random = new Random();

        public static Color GetRandomColor()
        {
            int red = _random.Next(84, 169);
            int green = _random.Next(84, 169);
            int blue = _random.Next(84, 169);
            return Color.FromArgb(255, (byte)red, (byte)green, (byte)blue);
        }

        public static Color GetLigtherColor(Color color, float percentage)
        {
            float red = (255 - color.R) * percentage + color.R;
            float green = (255 - color.G) * percentage + color.G;
            float blue = (255 - color.B) * percentage + color.B;
            return Color.FromArgb(color.A, (byte)red, (byte)green, (byte)blue);
        }
    }
}
