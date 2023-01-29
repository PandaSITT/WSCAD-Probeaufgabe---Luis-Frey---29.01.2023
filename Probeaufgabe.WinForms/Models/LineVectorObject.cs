using Probeaufgabe.WinForms.Helper;
using Probeaufgabe.WinForms.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probeaufgabe.WinForms.Models
{
    class LineVectorObject : IVectorObject
    {
        public LineVectorObject(dynamic inputObject)
        {
            Type = inputObject.type;

            Color = VectorObjectHelper.ARGBStringToColor
                ((string)inputObject.color);

            A = VectorObjectHelper.StringToPoint(
                (string)inputObject.a);

            B = VectorObjectHelper.StringToPoint(
                (string)inputObject.b);
        }

        public Shapes Type { get; set; }

        public Color Color { get; set; }

        public PointF A { get; set; }

        public PointF B { get; set; }

        public List<PointF> AllPoints => new List<PointF> { A, B };

        public void DrawVectorObject(Graphics graphicsTarget)
        {
            using (Pen pen = new Pen(Color))
            {
                pen.Width = 1;
                graphicsTarget.DrawLine(pen, A, B);
            }
        }
    }
}
