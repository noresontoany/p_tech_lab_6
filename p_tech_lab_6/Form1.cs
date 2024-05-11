using p_tech_lab_6.Objects;
using System.Collections.Specialized;
using System.DirectoryServices.ActiveDirectory;

namespace p_tech_lab_6
{
    public partial class Form1 : Form
    {
        MyRectangle? myRect; // создадим поле под наш прямоугольник

        List<BaseObject> objects = new();
        Player player;

        Marker? marker;

        int countEnemy = 0;
        int score = 0;


        public Form1()
        {
            InitializeComponent();
            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);

            player.OnOverlap += (player, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;

            };

            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };

            player.OnEnemyOverlap += (e) =>
            {
                objects.Remove(e);
                countEnemy--;
               
                scoreTxt.Text = "Очки :" + score.ToString();
                createEnemy();

                score++;
            };

            objects.Add(player);
            objects.Add(new Enemy(100, 100, 0));

            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);

            objects.Add(marker);



        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            // пересчитываем пересечения
            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);
                }





            }

            // рендерим объекты
            foreach (var obj in objects)
            {
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

            // а это так и остается
            marker.X = e.X;
            marker.Y = e.Y;
        }

        private void newObjTimer_Tick(object sender, EventArgs e)
        {
            if (countEnemy < 5)
            {
                createEnemy();
            }
        }

        private void createEnemy()
        {
            Random rnd = new Random();
            int x, y;
            while (true)
            {
                x = rnd.Next(25, pbMain.Width);
                y = rnd.Next(25, pbMain.Height);

                if (x != player.X && y != player.Y)
                {
                    break;
                }
            }

            var enemy = new Enemy(x, y, 0);
            objects.Add(enemy);
            countEnemy++;
        }
    }
}
