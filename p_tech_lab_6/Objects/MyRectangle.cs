﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p_tech_lab_6.Objects
{
    internal class MyRectangle: BaseObject
    {
        public MyRectangle(float x, float y, float angle) : base(x, y, angle)
        {

        }

        // переопределяем Render
        public override void Render(Graphics g)
        {
            // и запихиваем туда код из формы
            g.FillRectangle(new SolidBrush(Color.Yellow), -25, -15, 50, 30);
            g.DrawRectangle(new Pen(Color.Red, 2), -25, -15, 50, 30);
        }
    }
}
