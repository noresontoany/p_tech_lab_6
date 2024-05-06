using p_tech_lab_6.Objects;
using System.DirectoryServices.ActiveDirectory;

namespace p_tech_lab_6
{
    public partial class Form1 : Form
    {
        MyRectangle? myRect; // создадим поле под наш пр€моугольник

        List<BaseObject> objects = new();
        Player player;
        Marker? marker;


        public Form1()
        {
            InitializeComponent();
            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);

            player.OnOverlap += (player, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] »грок пересекс€ с {obj}\n" + txtLog.Text;
            };
            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };

            objects.Add(player);
            objects.Add(new MyRectangle(100, 100, 45));
            objects.Add(new MyRectangle(50, 50, 0));

            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);

            objects.Add(marker);



        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    /* ”ƒјЋяё “”“ 
                       txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] »грок пересекс€ с {obj}\n" + txtLog.Text; */

                    // а вот эти строчки добавл€ем
                    player.Overlap(obj); // то есть игрок пересекс€ с объектом
                    obj.Overlap(player); // и объект пересекс€ с игроком

                    //if (obj == marker)
                    //{
                    //    objects.Remove(marker);
                    //    marker = null;
                    //}
                }

                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;

                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                player.X += dx * 2;
                player.Y += dy * 2;
            }
            pbMain.Invalidate();
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker); // и главное не забыть пололжить в objects
            }

            // а это так и остаетс€
            marker.X = e.X;
            marker.Y = e.Y;
        }
    }
}
