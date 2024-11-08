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
    public partial class Menu : Form
    {
        private bool _firstCombobox = false;
        private bool _secondCombobox = false;
        private bool _genArrButton = false;
        private readonly Test _test;
        public Menu()
        {
            _test = new Test();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!_firstCombobox || !_secondCombobox || !_genArrButton) return;
            _test.StartTest();
            Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _secondCombobox = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _firstCombobox = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_genArrButton != false || _firstCombobox != true || _secondCombobox != true) return;
            _test.InitialTest(comboBox1.SelectedIndex, comboBox2.SelectedIndex);
            _genArrButton = true;
        }
    }
}
