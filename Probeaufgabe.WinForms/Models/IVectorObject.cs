using Probeaufgabe.WinForms.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probeaufgabe.WinForms.Models
{
    interface IVectorObject
    {
        public Shapes Type { get; set; }

        public Color Color { get; set; }

        public List<PointF> AllPoints { get; }

        public void DrawVectorObject(Graphics graphicsTarget);
    }
}
