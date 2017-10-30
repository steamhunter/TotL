using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PathFinder.AStar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

      



        private unsafe void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Random rnd = new Random();
                Bitmap gridBmp = new Bitmap(32, 32, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                AStar.PathNode[,] grid = new AStar.PathNode[gridBmp.Width, gridBmp.Height];
                SettlersEngine.ImagePixelLock locked = new SettlersEngine.ImagePixelLock(gridBmp, false);

                using (locked)
                {
                    int* pixels = locked.Pixels;

                    // setup grid with walls
                    for (int x = 0; x < gridBmp.Width; x++)
                    {
                        for (int y = 0; y < gridBmp.Height; y++)
                        {
                            Boolean isWall = false; //((y % 2) != 0) && (rnd.Next(0, 10) != 8);

                            if (isWall)
                                *pixels = unchecked((int)0xFF000000);
                            else
                                *pixels = unchecked((int)0xFFFFFFFF);

                            grid[x, y] = new AStar.PathNode()
                            {
                                IsWall = isWall,
                                X = x,
                                Y = y,
                            };

                            pixels++;
                        }
                    }
                }

                // compute and display path
                AStar.Solver<AStar.PathNode, Object> aStar = new AStar.Solver<AStar.PathNode, Object>(grid);
                IEnumerable<AStar.PathNode> path = aStar.Search(new Point(0, 0), new Point(gridBmp.Width - 2, gridBmp.Height - 2), null);

                System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

                watch.Start();
                {
                    aStar.Search(new Point(0, 0), new Point(gridBmp.Width - 2, gridBmp.Height - 2), null);
                }
                watch.Stop();

                MessageBox.Show("Pathfinding took " + watch.ElapsedMilliseconds + "ms to complete.");

                foreach (AStar.PathNode node in path)
                {
                    gridBmp.SetPixel(node.X, node.Y, Color.Red);
                }

                pictureBox1.Image = gridBmp;

                gridBmp.Save(".\\dump.png");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Unknown error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
