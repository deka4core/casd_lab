using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Drawing2D;
using System.Security.Policy;

namespace ConsoleApp1
{
    public partial class Graph : Form
    {
        private ZedGraphControl _zedGraphControl2;
        int indexChapter = 0;
        int _indexName = 0;
        int indexColor = 0;
        int size = 0;
        List<string> _names = new List<string>();
        List<string> chapters = new List<string>();
        Color[] color = { Color.Coral, Color.PowderBlue, Color.RoyalBlue, Color.SpringGreen, Color.YellowGreen, Color.Violet, Color.Teal };
        Button button;

        public Graph(int a, int b, int size_, double[] x, long[][] y)
        {
            switch (a)
            {
                case 1:
                    _names.Add("Bubble");
                    _names.Add("Insertion");
                    _names.Add("Selection");
                    _names.Add("Shaker");
                    _names.Add("Gnome");
                    break;
                case 2:
                    _names.Add("Bitonic");
                    _names.Add("Shell");
                    _names.Add("Tree");
                    break;
                case 3:
                    _names.Add("Comb");
                    _names.Add("Heap");
                    _names.Add("Quick");
                    _names.Add("Merge");
                    _names.Add("Counting");
                    _names.Add("Bucker");
                    _names.Add("Radiax");
                    break;
            }
            switch (b)
            {
                case 1:
                    chapters.Add("Массивы случайных чисел по модулю 1000");
                    break;
                case 2:
                    chapters.Add("Массивы, разбитые на несколько отсортированных подмассивов разного размера");
                    break;
                case 3:
                    chapters.Add("Изначально отсортированные массивы случайных чисел с некоторым числом перестановок двух случайных элементов");
                    break;
                case 4:
                    chapters.Add("Полностью отсортированные массивы в прямом порядке");
                    chapters.Add("Полностью отсортированные массивы в обратном порядке");
                    chapters.Add("Полностью отсортированные массивы с несколькими заменёнными элементами");
                    chapters.Add("Полностью отсортированные массивы с большим количеством повторений одного элемента");
                    break;
                default:
                    chapters.Add("Массивы случайных чисел по модулю 1000");
                    break;
            }

            _zedGraphControl2 = new ZedGraphControl { Dock = DockStyle.Fill };
            this.Controls.Add(_zedGraphControl2);
            var graphPane = _zedGraphControl2.GraphPane;
            if (chapters != null)
            {
                graphPane.Title.Text = chapters[0].ToString();
                indexChapter++;
            }
            graphPane.XAxis.Title.Text = "Ось X";
            graphPane.YAxis.Title.Text = "Ось Y";

            for (var i = 0; i < y.Length; i++)
            {
                var points = new PointPairList();
                for(var j = 0; j != y[i].Length; j++)
                {
                    points.Add(x[j], y[i][j]);
                }
                var lineItem = graphPane.AddCurve(_names[i], points, color[i], SymbolType.Circle);
                
                lineItem.Line.Width = 2.0f;
                lineItem.Line.SmoothTension = 0.5f;
                lineItem.Symbol.Size = 6;
            }
            InitializeComponent();
            _zedGraphControl2.AxisChange();
            _zedGraphControl2.Invalidate();
            button = new Button();
            button.Text = @"Save to file";
            this.Controls.Add(button);
            button.BringToFront();
            button.Click += button1_MouseClick;


        }
        [STAThread]
        private void button1_MouseClick(object sender, EventArgs e)
        {
            if (_zedGraphControl2.GraphPane == null) return;
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = @"PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                saveFileDialog.Title = @"Сохранить график как изображение";

                if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
                var bmp = (Bitmap)_zedGraphControl2.GetImage();
                bmp.Save(saveFileDialog.FileName);
                MessageBox.Show(@"График сохранен!");
            }
        }
    }
}
