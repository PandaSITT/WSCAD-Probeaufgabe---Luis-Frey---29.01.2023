using Probeaufgabe.WinForms.Helper;
using Probeaufgabe.WinForms.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probeaufgabe.WinForms.Models
{
    class CircleVectorObject : IVectorObject
    {
        public CircleVectorObject(dynamic inputObject)
        {
            Type = inputObject.type;

            Color = VectorObjectHelper.ARGBStringToColor(
                (string)inputObject.color);

            Radius = inputObject.radius;

            Filled = inputObject.filled;

            Center = VectorObjectHelper.StringToPoint(
                (string)inputObject.center);
        }

        public Shapes Type { get; set; }

        public Color Color { get; set; }

        public float Radius { get; set; }

        public PointF Center { get; set; }

        public bool Filled { get; set; }

        public List<PointF> AllPoints => new List<PointF> { Center };

        public void DrawVectorObject(Graphics graphicsTarget)
        {
            var rectangle = new RectangleF(Center.X - Radius, Center.Y - Radius, Radius * 2, Radius * 2);

            if (Filled)
            {
                using (var brush = new SolidBrush(Color))
                {
                    graphicsTarget.FillEllipse(brush, rectangle);
                }
            }
            else
            {
                using (var pen = new Pen(Color))
                {
                    pen.Width = 1;

                    graphicsTarget.DrawEllipse(pen, rectangle);
                }
            }
        }
    }
}
