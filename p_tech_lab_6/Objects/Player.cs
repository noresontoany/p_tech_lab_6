using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p_tech_lab_6.Objects
{
    class Player : BaseObject
    {

        public Action<Marker> OnMarkerOverlap;
        public Action<Enemy> OnEnemyOverlap;

        public float vX, vY;
        public  Player(float x , float y,  float angle, int id) : base (x,y,angle)
        {
            this.mainColor = Color.DeepSkyBlue;
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(mainColor), -15, -15, 30,30);
            using (Pen pen = new Pen(Color.Black, 2))
                g.DrawEllipse(pen, -15, -15, 30, 30);
            //g.FillEllipse(new Pen (Color.Black, 2), -15, -15, 30,30);

            g.DrawLine(new Pen(Color.Black, 2), 0, 0, 25, 0);

            //g.Clear(Color .White);
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-15, -15, 30, 30);
            return path;    
        }

        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);
            if (obj is Marker)
            {
                OnMarkerOverlap(obj as Marker);
            }
            else if (obj is Enemy)
            {
                OnEnemyOverlap(obj as Enemy);
            }
 
        }
    }
}
