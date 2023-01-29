using Probeaufgabe.WinForms.Helper;
using Probeaufgabe.WinForms.Models.Enum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probeaufgabe.WinForms.Models
{
    class TriangleVectorObject : IVectorObject
    {
        public TriangleVectorObject(dynamic inputObject)
        {
            Type = inputObject.type;

            Color = VectorObjectHelper.ARGBStringToColor(
                (string)inputObject.color);

            A = VectorObjectHelper.StringToPoint(
                (string)inputObject.a);

            B = VectorObjectHelper.StringToPoint(
                (string)inputObject.b);

            C = VectorObjectHelper.StringToPoint(
                (string)inputObject.c);

            Filled = inputObject.filled;
        }

        public Shapes Type { get; set; }

        public Color Color { get; set; }

        public PointF A { get; set; }

        public PointF B { get; set; }

        public PointF C { get; set; }

        public bool Filled { get; set; }

        public List<PointF> AllPoints => new List<PointF> { A, B, C };

        public void DrawVectorObject(Graphics graphicsTarget)
        {
            if (Filled)
            {
                using (var brush = new SolidBrush(Color))
                {
                    graphicsTarget.FillPolygon(brush, new PointF[] { A, B, C });
                }
            }
            else
            {
                using (var pen = new Pen(Color))
                {
                    pen.Width = 1;
                    graphicsTarget.DrawLine(pen, A, B);
                    graphicsTarget.DrawLine(pen, A, C);
                    graphicsTarget.DrawLine(pen, B, C);
                }
            }
        }
    }
}
