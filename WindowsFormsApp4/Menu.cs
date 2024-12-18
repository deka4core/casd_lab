using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp1
{
    public class NonZeroComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x > y) return 1;
            if (x == y) return 0;
            if (x < y) return -1;
            return -1;
        }
    }
    public partial class Menu : Form
    {
        
        bool firstCombox = false;
        bool secondCombox = false;
        bool genArrButton = false;
        Comparison<int> comparer = (x, y) => new NonZeroComparer().Compare(x, y);

        Test<int> test;
        public Menu()
        {
            test = new Test<int>();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!firstCombox || !secondCombox || !genArrButton) return;
            test.StartTest(comparer);
            Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            secondCombox = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            firstCombox = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (genArrButton || firstCombox != true || secondCombox != true) return;
            test.InitialTest(comboBox1.SelectedIndex, comboBox2.SelectedIndex, 0, 1000, comparer);
            genArrButton = true;
        }
    }
}
