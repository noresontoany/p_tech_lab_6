using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p_tech_lab_6.Objects
{
    internal class Marker: BaseObject
    {
        public Marker(float x , float y , float angle) : base(x, y, angle)
        {
            this.mainColor = Color.Red;
        } 

        public override void Render(Graphics g)
        {

            g.FillEllipse(new SolidBrush(mainColor), -3, -3, 6, 6);
            g.FillEllipse(new SolidBrush(mainColor), -6, -6, 12, 12);
            g.FillEllipse(new SolidBrush(mainColor), -10, -10, 20, 20);
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-3, -3, 6, 6);
            return path;
        }
    }
}
