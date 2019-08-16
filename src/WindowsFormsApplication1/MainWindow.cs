using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class MainWindow : Form
    {
        private System.IO.FileStream fileStream;
        private int lastSearchSizeInBytes = 0;
        private Thread searchThread;
        SearchThread Worker;

        public MainWindow()
        {
            InitializeComponent();

            openFileDialog.Filter = "NES ROMs (*.nes)|*.nes|All files (*.*)|*.*";
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (searchThread != null && searchThread.IsAlive)
                searchThread.Abort();

            if (fileStream != null)
                fileStream.Close();
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (fileStream != null)
                fileStream.Close();

            this.Activate();
            openedFileName.Text = openFileDialog.FileName;

            if (editCheck.Checked)
            {
                results.ReadOnly = false;
                fileStream = new System.IO.FileStream(openFileDialog.FileName, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
            }
            else
            {
                results.ReadOnly = true;
                fileStream = new System.IO.FileStream(openFileDialog.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (fileStream == null)
            {
                MessageBox.Show("Before searching you have to open a file");
                return;
            }

            if (searchString.Text.Length == 0)
            {
                MessageBox.Show("Please enter search string");
                return;
            }

            results.Rows.Clear();
            resultsLabel.Text = "0";
            fileStream.Seek(0, System.IO.SeekOrigin.Begin);

            byte[] searchData;
            if (decSearch.Checked)
            {
                string[] bytes = searchString.Text.Split(new Char[] {','});
                searchData = new byte[bytes.Length];

                int tabPos = 0;
                foreach(string singleDEC in bytes)
                {
                    try
                    {
                        if (singleDEC.Length == 0 || Convert.ToInt32(singleDEC) > 255)
                        {
                            MessageBox.Show("One or more decimal numbers are not in 0-255 bounds");
                            return;
                        }
                    }
                    catch (System.FormatException)
                    {
                        MessageBox.Show("One or more values are not a proper number");
                        return;
                    }

                    searchData[tabPos++] = Convert.ToByte(singleDEC);
                }
            }
            else if (hexSearch.Checked)
            {
                if ((searchString.Text.Length % 2) != 0)
                {
                    MessageBox.Show("The length of search string must be even (each HEX value lenght must be 2)");
                    return;
                }
                searchData = new byte[(int)Math.Ceiling((double)searchString.Text.Length / 2)];

                int tabPos = 0;
                for (int i = 0; i < searchString.Text.Length; i += 2)
                {
                    try
                    {
                        searchData[tabPos++] = Convert.ToByte(searchString.Text.Substring(i, 2), 16);
                    }
                    catch (System.FormatException)
                    {
                        MessageBox.Show("One or more values are not porper HEX numbers");
                        return;
                    }
                }
            }
            else
            {
                searchData = new byte[searchString.Text.Length];

                for (int i = 0; i < searchString.Text.Length; i += 1)
                {
                    searchData[i] = (byte)searchString.Text[i];
                }
            }

            if (searchData.Length == 1 && relativeCheck.Checked)
                if (MessageBox.Show("If relative search is on, it's recomended to use minimum 2 values in search string. Otherwise program can stop for a longer time than usually.\nContinue anyway?", "Warning!: Working slow down", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                    return;

            //foreach (byte b in searchData)
            //    results.Rows.Add(b.ToString());

            lastSearchSizeInBytes = searchData.Length;

            List<object> list = new List<object>();
            list.Add(this);
            list.Add(relativeCheck.Checked);
            list.Add(searchData);
            list.Add(fileStream);

            Worker = new SearchThread();
            searchThread = new Thread(Worker.DoWork);
            searchThread.Start(list);

            searchButton.Visible = false;
            stopSearchButton.Visible = true;
            return;

            //Here is a code that do the same as a thread, but freezes window
            int resultCount = 0;
            string resultString = "";

            //NORMAL METHOD
            if (!relativeCheck.Checked)
            {
                foreach (byte data in searchData)
                {
                    if (Convert.ToString(data, 16).Length == 1)
                        resultString += '0';
                    resultString += Convert.ToString(data, 16).ToUpper();
                }

                int actualData = 0;
                while ((actualData = fileStream.ReadByte()) != -1)
                {
                    long pos = fileStream.Position;
                    if (actualData == searchData[0]) 
                    {
                        bool found = true;
                        for (int i = 1; i < searchData.Length; i++)
                        {
                            if (!(fileStream.ReadByte() == searchData[i]))
                            {
                                found = false;
                                break;
                            }
                        }

                        if (found)
                        {
                            results.Rows.Add(pos-1, resultString);
                            resultCount++;
                        }

                        fileStream.Seek(pos, System.IO.SeekOrigin.Begin);
                    }
                }
            }
            //RELATIVE METHOD
            else
            {
                for (int pos = 1; pos <= fileStream.Length - searchData.Length + 1; pos++)
                {
                    int beginingByte = fileStream.ReadByte();

                    resultString = "";
                    if (Convert.ToString(beginingByte, 16).Length == 1)
                        resultString += '0';
                    resultString += Convert.ToString(beginingByte, 16).ToUpper();

                    bool found = true;
                    for (int i = 1; i < searchData.Length; i++)
                    {
                        int comparisonByte = fileStream.ReadByte();
                        int dataDiff = beginingByte - comparisonByte;
                        int targetDiff = searchData[0] - searchData[i];

                        if (dataDiff != targetDiff)
                        {
                            found = false;
                            break;
                        }

                        if (Convert.ToString(comparisonByte, 16).Length == 1)
                            resultString += '0';
                        resultString += Convert.ToString(comparisonByte, 16).ToUpper();
                    }

                    if (found)
                    {
                        results.Rows.Add(pos-1, resultString);
                        resultCount++;
                    }

                    fileStream.Seek(pos, System.IO.SeekOrigin.Begin);
                }
            }

            resultsLabel.Text = Convert.ToString(resultCount);
        }

        private void results_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show("Cell edited. Row: " + e.RowIndex);

            DataGridViewRow applyData = results.Rows[e.RowIndex];

            //MessageBox.Show(applyData.Cells[1].Value.ToString());
            if ((applyData.Cells[1].Value.ToString().Length % 2) != 0)
            {
                MessageBox.Show("The length of search string must be even (each HEX value lenght must be 2)!\nFile will not be updated.", "Data have no even length", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            byte[] newData = new byte[(int)Math.Ceiling((double)applyData.Cells[1].Value.ToString().Length / 2)];

            int tabPos = 0;
            for (int i = 0; i < applyData.Cells[1].Value.ToString().Length; i += 2)
            {
                try
                {
                    newData[tabPos++] = Convert.ToByte(applyData.Cells[1].Value.ToString().Substring(i, 2), 16);
                }
                catch (System.FormatException)
                {
                    MessageBox.Show("One or more values are not porper HEX numbers!\nFile will not be updated.", "Wrong data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            if (newData.Length != lastSearchSizeInBytes)
            {
                MessageBox.Show("Edited value have to be the same length as searched!\nFile will not be updated.", "Wrong data length", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //UPDATING FILE
            //MessageBox.Show(applyData.Cells[0].Value.GetType().ToString());
            fileStream.Seek(Convert.ToInt64(applyData.Cells[0].Value), System.IO.SeekOrigin.Begin);

            
            fileStream.Write(newData, 0, newData.Length);
            fileStream.Flush();
        }

        private void stopSearch_Click(object sender, EventArgs e)
        {
            if (searchThread != null && searchThread.IsAlive)
            {
                Worker.RequestStop();

                stopSearchButton.Visible = false;
                searchButton.Visible = true;
            }
        }
    }
}
