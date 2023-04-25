using BSolver.GameMech;
using BSolver.GameMech.Boards;
using BSolver.GameMech.Games;
using BSolver.GameMech.Think;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSolver.UI
{
    public partial class Form1 : Form
    {
        ClassicGame game;
        int tileSize = 40;
        int shapeBlockSize = 13;
        int boardX;
        int boardY;
        int shapeMenuX;
        int shapeMenuY;
        int shapeMenuRowSize = 8;
        int selectedShapesX;
        int selectedShapesY;

        Pen blackPen = new Pen(Color.Black, 1);
        Pen thickBlackPen = new Pen(Color.Black, 2);
        SolidBrush blueBrush = new SolidBrush(Color.Blue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush greyBrush = new SolidBrush(Color.Gainsboro);
        Font textFont = new Font("Arial", 20, FontStyle.Bold);
        StringFormat textFormat = new StringFormat();

        Graphics gr;
        List<ShapeButton> shapeButtons = new List<ShapeButton>();
        List<SelectShapeButton> selectShapeButtons = new List<SelectShapeButton>();

        ThinkResult thinkResult;
        Random random = new Random();

        const int SINGLE_BIT = 0b_0000_0001_0000_0000;

        class ShapeButton
        {
            public Shape Shape;
            public int x1;
            public int x2;
            public int y1;
            public int y2;
        }

        class SelectShapeButton
        {
            public int shapeIndex;
            public int x1;
            public int x2;
            public int y1;
            public int y2;
        }

        public Form1()
        {
            InitializeComponent();

            FlatBoard.Init();
            game = new ClassicGame();
            boardX = tileSize;
            boardY = tileSize;
            shapeMenuX = boardX + 10 * tileSize;
            shapeMenuY = boardY;
            selectedShapesX = boardX;
            selectedShapesY = boardY + 10 * tileSize;

            textFormat.Alignment = StringAlignment.Center;
            textFormat.LineAlignment = StringAlignment.Center;

            gr = this.CreateGraphics();

            CreateShapeButtons(game.Shapes, shapeMenuX, shapeMenuY, shapeMenuRowSize);

            this.WindowState = FormWindowState.Maximized;

            //string[] rowPatterns = new string[9];
            //rowPatterns[0] = "1111--11-";
            //rowPatterns[1] = "1--11111-";
            //rowPatterns[2] = "1111--1--";
            //rowPatterns[3] = "1-11--11-";
            //rowPatterns[4] = "---1--1--";
            //rowPatterns[5] = "1-11-1111";
            //rowPatterns[6] = "1111--1--";
            //rowPatterns[7] = "111------";
            //rowPatterns[8] = "--1---11-";
            //game.Board.RecreateBoard(rowPatterns);

            //game.Board.SelectedShapes.Add(game.Shapes.Where(s => s.Name == "t5c").FirstOrDefault());
            //game.Board.SelectedShapes.Add(game.Shapes.Where(s => s.Name == "i3v").FirstOrDefault());
            //game.Board.SelectedShapes.Add(game.Shapes.Where(s => s.Name == "l5d").FirstOrDefault());

            //game.Board.SelectedShapes.Add(game.Shapes.Where(s => s.Name == "x").FirstOrDefault());
            //game.Board.SelectedShapes.Add(game.Shapes.Where(s => s.Name == "i5h").FirstOrDefault());
            //game.Board.SelectedShapes.Add(game.Shapes.Where(s => s.Name == "i5v").FirstOrDefault());
            //this.Refresh();
        }

        

        private void DrawBoard(FlatBoard board)
        {
            // draw tiles
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    //Tile tile = board.Tiles[i, j];
                    bool color = (board.RowBitsColor[j] & (SINGLE_BIT >> i)) != 0;
                    bool thick = (board.RowBits[j] & (SINGLE_BIT >> i)) != 0;

                    Brush brush;
                    if (color)
                    { 
                        brush = redBrush; 
                    }
                    else if (thick)
                    {
                        brush = blueBrush;
                    }else if ((i / 3 + j / 3) % 2 == 0)
                    {
                        brush = whiteBrush;
                    }
                    else
                    {
                        brush = greyBrush;
                    }
                    
                    int tileX = boardX + i * tileSize;
                    int tileY = boardY + j * tileSize;
                    gr.FillRectangle(brush, tileX, tileY, tileSize, tileSize);

                    //if (tile.Thickness > 1)
                    //{
                    //    // write the thickness number
                    //    gr.DrawString(tile.Thickness.ToString(), textFont, whiteBrush, tileX + tileSize / 2, tileY + tileSize / 2, textFormat);
                    //}
                }
            }

            // draw vertical borders
            for (int i = 0; i <= 9; i++)
            {
                Pen pen = i % 3 == 0 ? thickBlackPen : blackPen;
                gr.DrawLine(pen, boardX + i * tileSize, boardY, boardX + i * tileSize, boardY + 9 * tileSize);
            }

            // draw horizontal borders
            for (int j = 0; j <= 9; j++)
            {
                Pen pen = j % 3 == 0 ? thickBlackPen : blackPen;
                gr.DrawLine(pen, boardX, boardY + j * tileSize, boardX + 9 * tileSize, boardY + j * tileSize);
            }
        }

        private void CreateShapeButtons(List<Shape> shapes, int collectionX, int collectionY, int collectionRowCount)
        {
            int x = 0;
            int y = 0;
            foreach (Shape shape in shapes)
            {
                int buttonSize = shapeBlockSize * 6;
                DrawShape(shape, collectionX + (x + 0.5) * buttonSize, collectionY + (y + 0.5) * buttonSize);

                int x1 = shapeMenuX + x * buttonSize;
                int y1 = shapeMenuY + y * buttonSize;
                gr.DrawRectangle(blackPen, x1, y1, buttonSize, buttonSize);


                shapeButtons.Add(new ShapeButton
                {
                    Shape = shape,
                    x1 = x1,
                    y1 = y1,
                    x2 = x1 + buttonSize,
                    y2 = y1 + buttonSize
                });


                x++;
                if (x >= collectionRowCount)
                {
                    x = 0;
                    y++;
                }

            }
        }

        private void DrawShapeCollection(List<Shape> shapes, int collectionX, int collectionY, int collectionRowCount)
        {
            //if (collectionX == selectedShapesX && game.Board.SelectedShapes.Count > 0)
            //{
            //    int a = 0;
            //}
            int x = 0;
            int y = 0;

            foreach(Shape shape in shapes)
            {
                int buttonSize = shapeBlockSize * 6;
                DrawShape(shape, collectionX + (x + 0.5) * buttonSize, collectionY + (y + 0.5) * buttonSize);

                int x1 = collectionX + x * buttonSize;
                int y1 = collectionY + y * buttonSize;
                gr.DrawRectangle(blackPen, x1, y1, buttonSize, buttonSize);

                x++;
                if (x >= collectionRowCount)
                {
                    x = 0;
                    y++;
                }

            }
        }

        private void DrawShape(Shape shape, double shapeCentreX, double shapeCentreY)
        {
            foreach (Tuple<double, double> pos in shape.BlockRelativePositions)
            {
                float x = (float)(shapeCentreX + (pos.Item1 - 0.5) * shapeBlockSize);
                float y = (float)(shapeCentreY + (pos.Item2 - 0.5) * shapeBlockSize);
                gr.FillRectangle(blueBrush, x, y, shapeBlockSize, shapeBlockSize);
                gr.DrawRectangle(blackPen, x, y, shapeBlockSize, shapeBlockSize);
            }
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawBoard(game.Board);
            DrawShapeCollection(game.Shapes, shapeMenuX, shapeMenuY, shapeMenuRowSize);
            DrawShapeCollection(game.Board.SelectedShapes, selectedShapesX, selectedShapesY, 3);
        }
        //List<BoardState> result;
        //int resultIndex = -1;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (game.Board.SelectedShapes.Count < 3)
            {
                ShapeButton clickedButton = shapeButtons.Where(s => s.x1 <= e.X && s.x2 > e.X && s.y1 <= e.Y && s.y2 > e.Y).FirstOrDefault();
                if (clickedButton != null)
                {
                    game.Board.SelectedShapes.Add(clickedButton.Shape);
                    this.Refresh();
                }
            }
            if (e.X >= boardX && e.X <= boardX + 9 * tileSize && e.Y >= boardY && e.Y <= boardY + 9 * tileSize)
            {
                int diffX = e.X - boardX;
                int diffY = e.Y - boardY;
                int gridX = (int)Math.Floor(diffX * 1.0 / tileSize);
                int gridY = (int)Math.Floor(diffY * 1.0 / tileSize);
                if (e.Button == MouseButtons.Left)
                {
                    //game.Board.Tiles[gridX, gridY].Thickness++;
                    game.Board.RowBits[gridY] |= (SINGLE_BIT >> gridX); 
                }
                else //if (game.Board.Tiles[gridX, gridY].Thickness > 0)
                {
                    //game.Board.Tiles[gridX, gridY].Thickness--;
                    game.Board.RowBits[gridY] &= ~(SINGLE_BIT >> gridX);
                }
                this.Refresh();
            }


            //else
            //{
            //    resultIndex++;
            //    this.Refresh();
            //    DrawBoard(result[resultIndex].Board);
            //}
        }



        private void btnAI_Click(object sender, EventArgs e)
        {
            //game.Board.Tiles[0, 0].Thickness = 3;
            if (thinkResult == null && game.Board.SelectedShapes.Count > 0)
            {
                BrainV1 brain = new BrainV1(10, -1, -2, -5);
                brain.SetGame(game);
                thinkResult = brain.Think();// ((int)numericUpDown1.Value);
            }
            if (thinkResult != null)
            {
                game.Board.CheckForEraser();
                game.Board.ClearColor();
                game.Board.PlaceShapeUiMode(thinkResult.Steps[0].Position.Item1,
                    thinkResult.Steps[0].Position.Item2,
                    thinkResult.Steps[0].Shape);
                thinkResult.Steps.RemoveAt(0);
                if (thinkResult.Steps.Count == 0)
                {
                    thinkResult = null;
                    game.Board.SelectedShapes.Clear();
                }
                this.Refresh();
            }
        }

        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            game.Board.SelectedShapes.Clear();
            this.Refresh();
        }

        private void btnRandomSelection_Click(object sender, EventArgs e)
        {
            game.PickRandomSelectedShapes(random);
            this.Refresh();
        }
    }
}
