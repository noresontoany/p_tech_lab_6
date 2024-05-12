using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
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

        public Action<BaseObject> underBlack;

        public Action<Black> OnDarkAreaTimeOut;

        public Dictionary<int , Color> original;

     

        public Black(float x, float y, float angle, float blackHeight) : base(x, y, angle)
        {
            this.blackHeight = blackHeight;
            original = new();
        }
        

        public override void Render(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Black), 0, 0, (int)blackWidth , (int)blackHeight);
            g.DrawRectangle(new Pen(Color.Black, 2), 0, 0, (int)blackWidth, (int)blackHeight);
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddRectangle(new Rectangle(0, 0, (int)blackWidth, (int)blackHeight));
            return path;
        }
        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);
            underBlack(obj);

            if (!original.ContainsKey(obj.GetHashCode()))
            {
                original[obj.GetHashCode()] = obj.mainColor;


            }
            obj.mainColor = Color.WhiteSmoke;


        }
        public Color freeDark (BaseObject obj)
        {
            
            Color v = original[obj.GetHashCode()];
            
            original.Remove(obj.GetHashCode());
            return v;
        }


    }
    
}
