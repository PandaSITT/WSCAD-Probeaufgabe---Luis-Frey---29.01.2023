using Probeaufgabe.WinForms.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probeaufgabe.WinForms.Helper
{
    static class VectorObjectHelper
    {
        public static Color ARGBStringToColor(string inputString)
        {
            try
            {
                var inputValues = inputString.Split(';');
                int alpha = Int32.Parse(inputValues[0]);
                int red = Int32.Parse(inputValues[1]);
                int green = Int32.Parse(inputValues[2]);
                int blue = Int32.Parse(inputValues[3]);

                return Color.FromArgb(alpha, red, green, blue);
            }
            catch (Exception ex)
            {
                throw new Exception($"Farbe '{inputString}' konnte nicht Interpretiert werden", ex);
            }
        }

        public static PointF StringToPoint(string inputString)
        {
            try
            {
                var inputValues = inputString.Split(';');
                float x = float.Parse(inputValues[0]);
                float y = float.Parse(inputValues[1]);

                return new PointF(x, y);
            }
            catch (Exception ex)
            {
                throw new Exception($"Koordinate '{inputString}' konnte nicht Interpretiert werden", ex);
            }
        }

        public static Shapes StringToShapes(string inputString)
        {
            try
            {
                return Enum.Parse<Shapes>(inputString, true);
            }
            catch (Exception ex)
            {
                throw new Exception($"Der Typ '{inputString}' konnte nicht interpretiert werden", ex);
            }
        }
    }
}
