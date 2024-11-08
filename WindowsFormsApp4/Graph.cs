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
        private readonly ZedGraphControl _zedGraphControl2;
        private int _indexChapter = 0;
        private int _size = 0;
        private readonly List<string> _names = new List<string>();
        private readonly List<string> _chapters = new List<string>();
        private readonly Color[] _color = { Color.Red, Color.RoyalBlue, Color.DarkCyan, Color.Green, Color.Yellow, Color.Purple, Color.Black };
        private readonly Button _button;

        public Graph(int a, int b, int size, IReadOnlyList<double> x, IReadOnlyList<long[]> y)
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
                default:
                    break;
            }
            switch (b)
            {
                case 1:
                    _chapters.Add("Массивы случайных чисел по модулю 1000");
                    break;
                case 2:
                    _chapters.Add("Массивы, разбитые на несколько отсортированных подмассивов разного размера");
                    break;
                case 3:
                    _chapters.Add("Изначально отсортированные массивы случайных чисел с некоторым числом перестановок двух случайных элементов");
                    break;
                case 4:
                    _chapters.Add("Полностью отсортированные массивы в прямом порядке");
                    _chapters.Add("Полностью отсортированные массивы в обратном порядке");
                    _chapters.Add("Полностью отсортированные массивы с несколькими заменёнными элементами");
                    _chapters.Add("Полностью отсортированные массивы с большим количеством повторений одного элемента");
                    break;
                default:
                    _chapters.Add("Массивы случайных чисел по модулю 1000");
                    break;
            }

            _zedGraphControl2 = new ZedGraphControl { Dock = DockStyle.Fill };
            this.Controls.Add(_zedGraphControl2);
            var graphPane = _zedGraphControl2.GraphPane;
            if (_chapters != null)
            {
                graphPane.Title.Text = _chapters[0].ToString();
                _indexChapter++;
            }
            graphPane.XAxis.Title.Text = "Ось X";
            graphPane.YAxis.Title.Text = "Ось Y";

            for (var i = 0; i < y.Count; i++)
            {
                var points = new PointPairList();
                for(var j = 0; j != y[i].Length; j++)
                {
                    points.Add(x[j], y[i][j]);
                }
                var lineItem = graphPane.AddCurve(_names[i], points, _color[i], SymbolType.Circle);

                lineItem.Line.Width = 2.0f;
                lineItem.Line.SmoothTension = 0.5f;
                lineItem.Symbol.Size = 6;
            }
            
            InitializeComponent();
            _zedGraphControl2.AxisChange();
            _zedGraphControl2.Invalidate();
            _button = new Button();
            _button.Text = @"Save to file";
            this.Controls.Add(_button);
            _button.BringToFront();
            _button.Click += button1_MouseClick;


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