using Lalalend_3.core;
using Lalalend_3.src.view;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lalalend_3
{
    public partial class Form1 : Form, IChartView
    {
        public event Action RequestedStatistics;
        public event Action<string> ChangedCommand;

        public Form1(IChartPresenter presenter)
        {
            InitializeComponent();
            presenter.View = this;
        }

        public void ShowAdditionalInfo(string info)
        {
            richTextBox1.Text = info;
        }

        public void ShowChart(List<Series> series)
        {
            chart1.Series.Clear();
            foreach(var tmp in series)
            {
                chart1.Series.Add(tmp);
            }
            chart1.ChartAreas[0].AxisY.IsStartedFromZero = false;
        }

        public void ShowGrid(List<string> columnsName, List<List<string>> rows)
        {

            dataGridView1.Columns.Clear();
            foreach (var column in columnsName)
            {
                var dataColumn = new DataGridViewColumn();
                dataColumn.HeaderText = column;
                dataColumn.Name = column;
                dataColumn.ReadOnly = true;
                dataColumn.CellTemplate = new DataGridViewTextBoxCell();
                dataGridView1.Columns.Add(dataColumn);
            }

            dataGridView1.Rows.Clear();
            for (int i = 0; i < rows.Count; i++)
            {
                dataGridView1.Rows.Add();
                for (int j = 0; j < columnsName.Count; j++)
                {
                    dataGridView1[columnsName[j], i].Value = rows[i][j];
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RequestedStatistics?.Invoke();
        }

        public void SetCommands(List<string> commands)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(commands.ToArray());
        }

        public string GetCSV()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader reader = new StreamReader(openFileDialog1.FileName))
                {
                    return reader.ReadToEnd();
                }
            }
            return "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedCommand(comboBox1.SelectedItem as string);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
