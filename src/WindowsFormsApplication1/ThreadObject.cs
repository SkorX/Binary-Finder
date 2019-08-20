using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class SearchThread
    {
        private volatile bool _stopRequest = false;

        //Param list: Form1, relativeCheck, searchData, fileStream
        public void DoWork(object param)
        {
            List<object> prm;
            MainWindow Form_1;
            bool relativeCheck;
            byte[] searchData;
            FileStream fileStream;
            
            try
            {
                prm = (List<object>)param;
                Form_1 = prm[0] as MainWindow;
                relativeCheck = (bool)prm[1];
                searchData = (byte[])prm[2];
                fileStream = prm[3] as FileStream;
            }
            catch (InvalidCastException exc)
            {
                MessageBox.Show("One or more parameters of thread have invalid types.\nError: " + exc.Message, "Internal error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            catch (ArgumentOutOfRangeException exc)
            {
                MessageBox.Show("Paramether list is to small.\nError: " + exc.Message, "Internal error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            try
            {
                Form_1.Invoke(new MethodInvoker(() => Form_1.fileOpenButton.Enabled = false));
            }
            catch (ObjectDisposedException)
            {
                return;
            }
            
            int resultCount = 0;
            string resultString = "";

            try
            {
                //NORMAL METHOD
                if (!relativeCheck)
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
                        if (_stopRequest)
                        {
                            Form_1.Invoke(new MethodInvoker(() => Form_1.fileOpenButton.Enabled = true));
                            Form_1.Invoke(new MethodInvoker(() => Form_1.searchButton.Enabled = true));
                            Form_1.Invoke(new MethodInvoker(() => Form_1.stopSearchButton.Visible = false));
                            return;
                        }

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
                                Form_1.Invoke(new MethodInvoker(() => Form_1.results.Rows.Add(pos - 1, resultString)));
                                //Form_1.results.Rows.Add(pos - 1, resultString);
                                resultCount++;
                                Form_1.Invoke(new MethodInvoker(() => Form_1.resultsLabel.Text = resultCount.ToString()));
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
                        if (_stopRequest)
                        {
                            Form_1.Invoke(new MethodInvoker(() => Form_1.fileOpenButton.Enabled = true));
                            Form_1.Invoke(new MethodInvoker(() => Form_1.searchButton.Enabled = true));
                            Form_1.Invoke(new MethodInvoker(() => Form_1.stopSearchButton.Visible = false));
                            return;
                        }

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
                            Form_1.Invoke(new MethodInvoker(() => Form_1.results.Rows.Add(pos - 1, resultString)));
                            //Form_1.results.Rows.Add(pos - 1, resultString);
                            resultCount++;
                            Form_1.Invoke(new MethodInvoker(() => Form_1.resultsLabel.Text = resultCount.ToString()));
                        }

                        fileStream.Seek(pos, System.IO.SeekOrigin.Begin);
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                return;
            }

            try
            {
                Form_1.Invoke(new MethodInvoker(() => Form_1.fileOpenButton.Enabled = true));
                Form_1.Invoke(new MethodInvoker(() => Form_1.searchButton.Visible = true));
                Form_1.Invoke(new MethodInvoker(() => Form_1.stopSearchButton.Visible = false));
            }
            catch (ObjectDisposedException)
            {
                return;
            }
            //Form_1.resultsLabel.Text = Convert.ToString(resultCount);
        }


        public void RequestStop()
        {
            _stopRequest = true;
        }
    }
}
