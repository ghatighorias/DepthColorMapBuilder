using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DepthColorMap
{
    public partial class Form1 : Form
    {
        ColorMapper colorMapper;
        List<ColoredDepthData> rangeList;
        Bitmap coloredMap;
        Color rangeColor;

        public Form1()
        {
            InitializeComponent();
            colorMapper = new ColorMapper();
            colorMapper.SetImageZeroDistance(ref coloredMap);
            rangeColor = Color.Black;
            label4.Text = rangeColor.ToString();
        }

        private void CalculateColoredMap()
        {
            colorMapper.SetColorRange((int)numericUpDown1.Value, (int)numericUpDown2.Value, colorDialog1.Color, ref coloredMap);
            UpdateListviewWithRanges();
        }

        private void UpdateListviewWithRanges()
        {
            rangeList = colorMapper.ReadColorMap(coloredMap);
            pictureBox1.Image = coloredMap;

            listView1.Items.Clear();
            ListViewItem listViewItem;
            foreach (var item in rangeList)
            {
                listViewItem = new ListViewItem(item.StartOfRange.ToString());
                listViewItem.UseItemStyleForSubItems = false;
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(listViewItem, item.EndOfRange.ToString()));
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(listViewItem, item.RangeColor.ToString()));
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { BackColor = item.RangeColor });
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { BackColor = item.RangeColor });

                listView1.Items.Add(listViewItem);
            }


            if (numericUpDown2.Value < 8191)
            {
                numericUpDown1.Value = numericUpDown2.Value + 1;
                numericUpDown2.Value = 8191;
                numericUpDown2.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CalculateColoredMap();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (DialogResult.OK == colorDialog1.ShowDialog())
            {
                rangeColor  = colorDialog1.Color;
                label4.Text = rangeColor.ToString();
            }       
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if( saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                coloredMap.Save(saveFileDialog1.FileName);
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(ListView))
            {
                ListView LV = (ListView)sender;
                ListViewItem selectedItem= LV.SelectedItems[0];
                LV.Items.Remove(selectedItem);
            }
        }
    }
}
