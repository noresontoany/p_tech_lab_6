namespace p_tech_lab_6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics; // вытащили объект графики из события
            g.DrawRectangle(new Pen(Color.Red), 200, 100, 50, 30);  // рисуем прямоугольную рамку
        }
    }
}
