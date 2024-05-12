using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p_tech_lab_6.Objects
{
    internal class Black:BaseObject
    {
        public float blackHeight;
        public const int blackWidth = 60;

        public Black(float x, float y, float angle, float blackHeight) : base(x, y, angle)
        {
            this.blackHeight = blackHeight;
        }

        public override void Render(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Black), 0, 0, blackWidth, blackHeight);
            g.DrawRectangle(new Pen(Color.Black, 2), 0, 0, blackWidth, blackHeight);

        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-3, -3, 6, 6);
            return path;
        }
    }
    
}
