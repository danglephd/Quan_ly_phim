using MoviesCompaire.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoviesCompaire
{
    public partial class Form1 : Form
    {
        private int indexRun = 0;
        private int indexCompare = 0;
        private int index2 = 0;
        private List<Movies> lstImportMovies = null;
        private bool isCompare = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void gridControl1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                lstImportMovies = new List<Movies>();
                foreach (string filePath in files)
                {
                    Movies dto = new Movies();
                    dto.Name = ExtractMoviesName(filePath);
                    dto.Path = filePath;
                    Console.WriteLine(filePath);
                    lstImportMovies.Add(dto);
                }
                gCMovies.DataSource = lstImportMovies;
                button1.Enabled = true;
            }
        }

        private string ExtractMoviesName(string filePath)
        {
            string name;
            name = filePath.Substring(filePath.LastIndexOf("\\") + 1);
            name = name.Replace(".", " ");
            return name;
        }

        private void gridControl1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            indexCompare = 0;
            index2 = 1;
            updateMoviesNameCompare();
            isCompare = true;
            gCMovies.Enabled = false;
        }

        private void updateMoviesNameCompare()
        {
            lbMovieCompare.Text = getMovieNameAt(indexCompare);
            lbMovie2.Text = getMovieNameAt(index2);
        }

        private string getMovieNameAt(int indexCompare)
        {
            if (lstImportMovies != null)
            {
                return lstImportMovies[indexCompare].Name;
            }
            return "";
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (!isCompare)
            {
                return;
            }
            if (e.KeyCode == Keys.Up)
            {
                ProcessCompareMovies(1);
            }
            else if (e.KeyCode == Keys.Down)
            {
                ProcessCompareMovies(0);
            }
        }

        private void ProcessCompareMovies(int betterOrWorse)
        {
            if (betterOrWorse == 1)
            {
                indexCompare = index2;
            }
            index2++;
            if (index2 < lstImportMovies.Count)
            {
                updateMoviesNameCompare();
            }
            else
            {
                updateIndexCompare();
            }
        }

        private void updateIndexCompare()
        {
            Movies dto = new Movies();
            copyValue(dto, lstImportMovies[indexRun]);
            copyValue(lstImportMovies[indexRun], lstImportMovies[indexCompare]);
            copyValue(lstImportMovies[indexCompare], dto);
            indexRun++;
            indexCompare = indexRun;
            index2 = indexCompare + 1;
            if (indexCompare >= (lstImportMovies.Count - 1))
            {
                Console.WriteLine("Stop.");
                lbMovie2.Text = "";
                lbMovieCompare.Text = "";
                gCMovies.Enabled = true;
            }
            else
            {
                updateMoviesNameCompare();
            }
            gVMovies.RefreshData();
        }

        private void copyValue(Movies A, Movies B)
        {
            A.Id = B.Id;
            A.Name = B.Name;
            A.Path = B.Path;
            A.Level = B.Level;
        }
    }
}
