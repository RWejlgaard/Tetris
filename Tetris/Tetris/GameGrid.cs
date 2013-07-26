// Made by IzYZacee
//You must not take credit or in other ways steal this project
//If you wish to contribute you can PM me at http://reddit.com/u/izyzacee/
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

 
namespace Tetris
{
    /// <summary>
    
    /// </summary>
    public class GameGrid
    {
        private Rectangle[][] rectangleGameGrid;
        private SolidBrush[][] gameGridBrushes;
        private SolidBrush[] theBrushColors;
 
        public GameGrid(int gameGridRows, int gameGridColumns)
        {
            rectangleGameGrid = new Rectangle [gameGridRows][];
            gameGridBrushes = new SolidBrush[gameGridRows][];
            theBrushColors    = new SolidBrush[5];
 
            
            for (int i=0;i<gameGridRows;i++)
            {
                rectangleGameGrid[i] = new Rectangle[gameGridColumns];
                gameGridBrushes[i] = new SolidBrush[gameGridColumns];
            }

       

            // #
            //###
            theBrushColors[0] = new SolidBrush(Color.Magenta);
            // #
            //###
            theBrushColors[1] = new SolidBrush(Color.Orange);
            //#
            //###
            theBrushColors[2] = new SolidBrush(Color.Blue);
            //##
            //##
            theBrushColors[3] = new SolidBrush(Color.DarkKhaki);
            //####
            theBrushColors[4] = new SolidBrush(Color.Cyan);

       
        }
        public Rectangle[][] GetGameGrid()
        {
            return rectangleGameGrid;
        }
        public SolidBrush[][] GetGameGridBrushes()
        {
            return gameGridBrushes;
        }
        public SolidBrush[] GetShapeColors()
        {
            return theBrushColors;
        }
        public bool IsGridLocationEmpty(int rowNumber, int colNumber)
        {
            if (rectangleGameGrid[rowNumber][colNumber].IsEmpty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void SetShapeLocation(int rowNumber, int colNumber, Rectangle square, int shapeType)
        {
            rectangleGameGrid[rowNumber][colNumber] = square;
            setShapeColorLocation(rowNumber, colNumber, shapeType);
        }
        public void setShapeColorLocation(int rowNumber, int colNumber, int shapeType)
        {
            gameGridBrushes[rowNumber][colNumber] = theBrushColors[shapeType-1];
        }
 
        public void DropRowsDown(int rowNumber, int colNumber)
        {
            if (!IsGridLocationEmpty(rowNumber-1, colNumber))
            {
                rectangleGameGrid[rowNumber][colNumber] = new Rectangle(rectangleGameGrid[rowNumber-1][colNumber].X,
                    rectangleGameGrid[rowNumber-1][colNumber].Y+10,10,10);
                gameGridBrushes[rowNumber][colNumber] = gameGridBrushes[rowNumber-1][colNumber];
            }
            else
            {
                rectangleGameGrid[rowNumber][colNumber] = rectangleGameGrid[rowNumber-1][colNumber];
            }
        }
        public void SetTopRow()
        {
            rectangleGameGrid[0] = new Rectangle[rectangleGameGrid[1].Length];
        }
    }
}