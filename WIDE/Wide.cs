﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using TUP.AsmResolver;
using Be.Windows.Forms;
using FastColoredTextBoxNS;
using GNIDA;

namespace WIDE
{
    public partial class MainForm : Form
    {
        GNIDA1 MyGNIDA;
        List<string> IncludedFiles = new List<string>();
        int VarLine = 0;
        string sss = "123";
        DynamicFileByteProvider dynamicFileByteProvider;
        TextStyle blueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        TextStyle funcStyle = new TextStyle(Brushes.LightSeaGreen, null, FontStyle.Regular);
        public MainForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        delegate void InsTextCallback(string text);
        private void InsText(string text)
        {
            fastColoredTextBox1.Navigate(VarLine);
            fastColoredTextBox1.InsertText(text);
            VarLine++;
        }

        public void AddVarEvent1(object sender, TVar Var)
        {
            if (this.fastColoredTextBox1.InvokeRequired)
            {
                try
                {
                    InsTextCallback d = new InsTextCallback(InsText);
                    this.Invoke(d, new object[] { Var.ToStr() + ";\n" });
                }
                catch (System.ObjectDisposedException) { }
            }
            else
            {
                AppendText(Var.FName);
            }
        }
        public void AddFuncEvent1(object sender, TFunc func)
        {
            ListViewItem itm1 = new ListViewItem();
            itm1.Text = func.FName;
            itm1.Tag = func;
            listView3.Items.Add(itm1);
        }
        public void OnLogEvent1(object sender, string LogStr)
        {
            Log.Items.Add(LogStr);
            //Log.SelectedIndex = Log.Items.Count-1;
        }
        public void OnFuncChanged1(object sender, TFunc Func)
        {
            foreach(ListViewItem itm in listView3.Items)
                if ((itm.Tag as TFunc).Equals(Func))
                {
                    fastColoredTextBox1.BeginUpdate();
                    fastColoredTextBox1.Text = fastColoredTextBox1.Text.Replace(itm.Text, Func.FName);
                    fastColoredTextBox1.EndUpdate();
                    itm.Text = Func.FName;
                }
        }
        private void AppendText(string text)
        {
            //some stuffs for best performance
            fastColoredTextBox1.BeginUpdate();
            fastColoredTextBox1.Selection.BeginUpdate();
            //remember user selection
            var userSelection = fastColoredTextBox1.Selection.Clone();
            //add text with predefined style
            fastColoredTextBox1.AppendText(text);
            //restore user selection
            if (!userSelection.IsEmpty || userSelection.Start.iLine < fastColoredTextBox1.LinesCount - 2)
            {
                fastColoredTextBox1.Selection.Start = userSelection.Start;
                fastColoredTextBox1.Selection.End = userSelection.End;
            }
            else
                fastColoredTextBox1.GoEnd();//scroll to end of the text
            //
            fastColoredTextBox1.Selection.EndUpdate();
            fastColoredTextBox1.EndUpdate();
        }
        delegate void SetTextCallback(string text);
        private void AddText(string text)
        {
            if (this.fastColoredTextBox1.InvokeRequired)
            {
                try
                {
                    SetTextCallback d = new SetTextCallback(AddText);
                    this.Invoke(d, new object[] { text });
                }
                catch (System.ObjectDisposedException) { }
            }
            else
            {
                AppendText(text);
            }
        }
        private void openFileDialog1_FileOk_1(object sender, CancelEventArgs e)
        {
            listView3.Clear();
            IncludedFiles.Clear();
            MyGNIDA = new GNIDA1(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\flirt.xml");
            MyGNIDA.OnLogEvent += OnLogEvent1;
            MyGNIDA.OnAddFunc += AddFuncEvent1;
            MyGNIDA.OnAddStr += AddText;
            MyGNIDA.OnVarEvent += AddVarEvent1;
            MyGNIDA.OnFuncChanged += OnFuncChanged1;
            MyGNIDA.LoadFile(openFileDialog1.FileName);

            dynamicFileByteProvider = new DynamicFileByteProvider(openFileDialog1.FileName, true);
            hexBox1.ByteProvider = dynamicFileByteProvider;

            fastColoredTextBox1.Clear();
            fastColoredTextBox1.Text = "//+---------------------------------------------------------------------+" +
                              "\n//| Dis file has bin generated by GNU Interactive Dissassembler (GNIDA) |" +
                              "\n//|       Copyleft (c) 2014 by me <na_derevnu@dedushke.ru>              |" +
                              "\n//|                          Free 4 use                                 |" +
                              "\n//|                         Writed AS IS                                |" +
                              "\n//+---------------------------------------------------------------------+\n";
            //if( & DynamicLoadedLibraryFile)


            if ((MyGNIDA.assembly.NTHeader.FileHeader.ExecutableFlags & ExecutableFlags.DynamicLoadedLibraryFile) != 0)
            { fastColoredTextBox1.Text += "#pragma option DLL\n"; }
            else {
                switch (MyGNIDA.assembly.NTHeader.OptionalHeader.SubSystem)
                    {
                        case SubSystem.WindowsGraphicalUI: fastColoredTextBox1.Text += "#pragma option W32\n"; break;
                        case SubSystem.WindowsConsoleUI: fastColoredTextBox1.Text += "#pragma option W32C\n"; break;
                        default: fastColoredTextBox1.Text += "#pragma option W32;//TO-DO!!!\n"; break;
                    }
                }
            IncludedFiles.Add("Windows.h");
            IncludedFiles.Add("msvcrt.h--");
            fastColoredTextBox1.Text += "#jumptomain NONE\n";
            foreach (string s in IncludedFiles)
            {
                fastColoredTextBox1.Text += "#include \"" + s + "\";\n";
            }
            VarLine = fastColoredTextBox1.LinesCount - 1;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 AboutForm = new AboutBox1();
            AboutForm.ShowDialog();
            AboutForm.Dispose();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            MyGNIDA.StopWork();
        }

        private void cmmListingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fastColoredTextBox1.Text.Length > 1)
            {
                fastColoredTextBox1.SaveToFile("cmm\\tmp.cmm", Encoding.ASCII);

                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = "cmm\\c--.exe";
                proc.StartInfo.Arguments = "cmm\\tmp.cmm";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.Start();
                do
                {
                    Log.Items.Add(proc.StandardOutput.ReadLine());
                } while (!proc.StandardOutput.EndOfStream);
                proc.WaitForExit();
            }
        }
        private void Funclist_DoubleClick(object sender, EventArgs e)
        {
            string sss = "";
            TFunc Func = (TFunc)listView3.SelectedItems[0].Tag;
            Console.WriteLine(Func.Addr.ToString("X8"));
        }


        private void fastColoredTextBox1_ToolTipNeeded(object sender, FastColoredTextBoxNS.ToolTipNeededEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.HoveredWord))
            {
                e.ToolTipTitle = e.HoveredWord;
                e.ToolTipText = "This is tooltip for '" + e.HoveredWord + "'";
            }
        }

        private void fastColoredTextBox1_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(blueStyle);
            e.ChangedRange.SetStyle(blueStyle, @"Loc_[\dA-Fa-f]+");
            if(MyGNIDA!=null)
            foreach(KeyValuePair<long, TFunc> fnc in MyGNIDA.FullProcList)
            {
                e.ChangedRange.SetStyle(funcStyle, ' '+fnc.Value.FName);
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count == 1)
            {
                Form2 Fm = new Form2();
                Fm.NName = (listView3.SelectedItems[0].Tag as TFunc).FName;
                if (Fm.ShowDialog() == DialogResult.OK)
                    MyGNIDA.RenameFunction((listView3.SelectedItems[0].Tag as TFunc), Fm.NName);
            };
        }

        private void renameToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            TFunc f = GetSelectedFunction();
            if (f != null) 
            {
                Form2 Fm = new Form2();
                Fm.NName = f.FName;
                if (Fm.ShowDialog() == DialogResult.OK)
                    MyGNIDA.RenameFunction(f, Fm.NName);
            }
        }

        private void fastColoredTextBox1_DoubleClick(object sender, EventArgs e)
        {
            TFunc f = GetSelectedFunction();
            if (f!= null)
            {
                //foreach(Line L in fastColoredTextBox1.Lines)
                //Console.WriteLine(hoveredWord);
            }
        }

        private void fastColoredTextBox1_Click(object sender, EventArgs e)
        {
            TFunc f = GetSelectedFunction();
            if (f != null) renameToolStripMenuItem1.Text = f.FName;
            renameToolStripMenuItem1.Enabled = f!= null;
        }

        private TFunc GetSelectedFunction()
        {
            Place place = fastColoredTextBox1.Selection.Start;
            var r = new Range(fastColoredTextBox1, place, place);
            string hoveredWord = r.GetFragment("[_a-zA-Z0-9]").Text;
            ListViewItem itm = listView3.FindItemWithText(hoveredWord);
            if (itm != null) return (itm.Tag as TFunc);
            return null;
        }
        private void sendToFLIRTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 FForm = new Form3();
            FForm.flrt = MyGNIDA.flirt;
            FForm.Func = GetSelectedFunction();
            FForm.ShowDialog();
        }

        private void checkedListBox1_MouseClick(object sender, MouseEventArgs e)
        {
            /*
            listView3.Clear();
            foreach (KeyValuePair<long, TFunc> f in MyGNIDA.FullProcList)
            {
                if ((f.Value.type == 3) && (checkedListBox1.GetItemCheckState(0) == CheckState.Checked))
                {
                    ListViewItem itm1 = new ListViewItem();
                    itm1.Text = f.Value.FName;
                    itm1.Tag = f.Value;
                    listView3.Items.Add(itm1);
                }
            }
            */
        }

    }
}
