using p_tech_lab_6.Objects;
using System.Collections.Specialized;
using System.ComponentModel.Design.Serialization;
using System.DirectoryServices.ActiveDirectory;

namespace p_tech_lab_6
{
    public partial class Form1 : Form
    {
        MyRectangle? myRect; // создадим поле под наш прямоугольник

        List<BaseObject> objects = new();
        Player? player;
        Black? black;
        Marker? marker;



        int countEnemy = 0;
        int score = 0;



        public Form1()
        {
            InitializeComponent();

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);

            black = new Black(0, 0, 0, pbMain.Height);
            objects.Add(black);
            bool cop = true;
            
                player.OnOverlap += (player, obj) =>
                {
                    if (obj is Black)
                    {
                        cop = false;
                    }
                    else { cop = true; }

                    if (cop)
                    {
                        txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
                    }
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
            updatePlayer();
           
            //BlackAppers();
            // пересчитываем пересечения
            foreach (var obj in objects.ToList())
            {
                
                
                    if (player != null && obj != player && obj != black && player.Overlaps(obj, g))
                    {
                        player.Overlap(obj);
                        obj.Overlap(player);
                    }



                    if (black != null && (obj is Enemy || obj is Player || obj is Marker) && black.Overlaps(obj, g))
                    {
                        black.Overlap(obj);
                        obj.Overlap(black);
                        
                        //if (!black.original.ContainsKey(obj.GetHashCode()))
                        //{
                        //    black.original[obj.GetHashCode()] = obj.mainColor;
                            

                        //}
                        //obj.mainColor = Color.Gray;


                    }
                    else if (black != null && (obj is Enemy || obj is Player || obj is Marker) && black.original.ContainsKey(obj.GetHashCode()))
                    {

                        obj.mainColor = black.freeDark(obj);
                       
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

            updatePlayer();


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
            if (countEnemy < 3)
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
                x = rnd.Next(50, pbMain.Width - 50);
                y = rnd.Next(50, pbMain.Height - 50);

                if (x != player.X && y != player.Y)
                {
                    break;
                }
            }

            var enemy = new Enemy(x, y, 0);
            objects.Add(enemy);
            countEnemy++;
        }


        //private void BlackAppers()
        //{
        //    if (black != null)
        //    {
        //        float dx = pbMain.Width - black.X;
        //        float dy = pbMain.Height;

        //        float length = MathF.Sqrt(dx * dx + dy * dy);

        //        dx /= length;

        //        black.X += dx + (float)0.00000000000001;

        //        if (Math.Round(black.X + 60) == pbMain.Width)
        //        {
        //            objects.Remove(black);
        //            foreach (var obj in objects.ToList())
        //            {
        //                if (black != null && (obj is Enemy || obj is Player) && black.original.ContainsKey(obj.GetHashCode()))
        //                {
        //                    obj.mainColor = black.freeDark(obj);
        //                }
        //            }

        //            black = null;
                    
        //        }

        //    }
        //    else
        //    {
        //        black = new Black(0, 0, 0, pbMain.Height, id++);
        //        objects.Add(black);
                
        //    }
        //    //try {
        //    if (black != null) { black.underBlack += (obj) => { }; }

        //    //} catch { }



        //    pbMain.Invalidate();
        //}

        private void BlackTimer_Tick(object sender, EventArgs e)
        {
            
            if (black != null)
            {
                float dx = pbMain.Width - black.X;
                float dy = pbMain.Height;

                float length = MathF.Sqrt(dx * dx + dy * dy);

                dx /= length;

                black.X += dx + 1;

                if (Math.Round(black.X + 60) == pbMain.Width)
                {
                    objects.Remove(black);
                    foreach (var obj in objects.ToList())
                    {
                        if (black != null && (obj is Enemy || obj is Player || obj is Marker) && black.original.ContainsKey(obj.GetHashCode()))
                        {
                            obj.mainColor = black.freeDark(obj);
                        }
                    }

                    black = null;
                    BlackTimer.Interval = 5000;
                }

            }
            else
            {
                black = new Black(0, 0, 0, pbMain.Height);
                objects.Insert(0, black);
                BlackTimer.Interval = 1;
            }
            //try {
            if (black != null) { black.underBlack += (obj) => { }; }

            //} catch { }



            pbMain.Invalidate();
        }


        public void updatePlayer()
        {

            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;
                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                // по сути мы теперь используем вектор dx, dy
                // как вектор ускорения, точнее даже вектор притяжения
                // который притягивает игрока к маркеру
                // 0.5 просто коэффициент который подобрал на глаз
                // и который дает естественное ощущение движения
                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;

                // расчитываем угол поворота игрока 
                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }

            // тормозящий момент,
            // нужен чтобы, когда игрок достигнет маркера произошло постепенное замедление
            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            // пересчет позиция игрока с помощью вектора скорости
            player.X += player.vX;
            player.Y += player.vY;
        }
        //public void Reload()
        //{
        //    foreach (var obj in objects)
        //    {
        //        g.Transform = obj.GetTransform();
        //        obj.Render(g);
        //    }
        //}
    }
}
