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
            var g = e.Graphics; // �������� ������ ������� �� �������
            g.DrawRectangle(new Pen(Color.Red), 200, 100, 50, 30);  // ������ ������������� �����
        }
    }
}
