using Probeaufgabe.WinForms.Models;
using Probeaufgabe.WinForms.Models.Enum;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace Probeaufgabe.WinForms
{
    public partial class Form1 : Form
    {
        private List<IVectorObject> VectorObjects { get; set; }

        public float Zoom { get; set; } = 1f;

        public Form1()
        {
            InitializeComponent();

            panel1.MouseWheel += panel1_MouseWheel;

            // panel1 DoubleBuffered
            typeof(Control).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, panel1, new object[] { true });

            var appSettings = System.Configuration.ConfigurationManager.AppSettings;
            VectorObjects = VectorObjectDAO.GetVectorObjects(appSettings["InputFilePath"], Enum.Parse<InputFileFormat>(appSettings["InputFileFormat"], true));

            float biggestX = 0f;
            float biggestY = 0f;

            GetBiggestPoints(ref biggestX, ref biggestY);

            CalculateAndSetOffset(biggestX, biggestY);
        }

        private void CalculateAndSetOffset(float biggestX, float biggestY)
        {
            Debug.WriteLine(panel1.Size.Width);
            Debug.WriteLine(panel1.Size.Height);
            Debug.WriteLine(biggestX);
            Debug.WriteLine(biggestY);

            var width = panel1.Size.Width;
            float xOffset = width < biggestX ? width / biggestX : 1f;
            var height = panel1.Size.Height;
            float yOffset = height < biggestY ? height / biggestY : 1f;

            float offset = xOffset > yOffset ? yOffset : xOffset;

            Zoom = offset;
            Debug.WriteLine(Zoom);
        }

        private void GetBiggestPoints(ref float biggestX, ref float biggestY)
        {
            var listOfListsOfPoints = VectorObjects.Select(q => q.AllPoints);
            foreach (var listOfPoints in listOfListsOfPoints)
            {
                foreach (var point in listOfPoints)
                {
                    if (point.X > biggestX)
                    {
                        biggestX = point.X;
                    }

                    if (point.Y > biggestY)
                    {
                        biggestY = point.Y;
                    }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.ScaleTransform(Zoom, Zoom);

            foreach (var item in VectorObjects)
            {
                item.DrawVectorObject(e.Graphics);
            }
        }

        private void panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Form1.ModifierKeys == Keys.Control)
            {
                if (e.Delta > 0)
                {
                    Zoom += 0.1f;
                }
                else if (e.Delta < 0)
                {
                    Zoom -= 0.1f;
                }

                panel1.Refresh();
            }
        }
    }
}