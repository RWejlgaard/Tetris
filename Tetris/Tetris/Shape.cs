// Made by IzYZacee
//You must not take credit or in other ways steal this project
//If you wish to contribute you can PM me at http://reddit.com/u/izyzacee/
using System;
using System.Drawing;
 
namespace Tetris
{
    /// <summary>
    
    /// </summary>
    public class Shape
    {
        
        public Point[] pntShape;
        public Rectangle[] rectangleShape;
        public bool shapeMoving;
 
        
        private int startingXCoordinate;
        private int currentShape = 1;
        private int currentShapePosition = 1;
        private int[] blockXPos = new int[4];
        private int[] blockYPos = new int[4];
        private int[] XPositions = new int[4];
        private int[] YPositions = new int[4];
        private int panelWidth;
        private int panelHeight;
        private bool isNextShape = false;
 
        public Shape(int shapeType, int screenWidth, int screenHeight, bool nextShape)
        {
            startingXCoordinate = ((screenWidth-1)/2);
            panelWidth = screenWidth;
            panelHeight = screenHeight;
            isNextShape = nextShape;
 
            
            currentShape = shapeType;
 
        
            SetShapeStart();
        }
        private void BuildShape(int intShapeType)
        {
           
            rectangleShape = new Rectangle[4];
            
            rectangleShape[0] = new Rectangle(blockXPos[0],blockYPos[0],10,10);
            rectangleShape[1] = new Rectangle(blockXPos[1],blockYPos[1],10,10);
            rectangleShape[2] = new Rectangle(blockXPos[2],blockYPos[2],10,10);
            rectangleShape[3] = new Rectangle(blockXPos[3],blockYPos[3],10,10);
        }
        public Rectangle[] moveShapeDown(int movePixels, Rectangle[][] rectangleGameGrid)
        {
            bool canMove = true;
 
         
            for (int j=0;j<4;j++)
            {
                if (((blockYPos[j] +10) + movePixels) > panelHeight-1)
                {
                    canMove = false;
                    shapeMoving = false;
                    break;
                }
            }
 
 
 
           
            if (canMove)
            {
                for (int k=0;k<4;k++)
                {
                    if (Decimal.Remainder(blockYPos[k],10) == 0 && blockYPos[k] >= 0)
                    {
                        if (blockYPos[k] == 260)
                        {
                            canMove = false;
                            shapeMoving = false;
                            break;
                        }
                        if (!rectangleGameGrid[(blockYPos[k]/10+1)][blockXPos[k]/10].IsEmpty)
                        {
                           
                            canMove = false;
                            shapeMoving = false;
                            break;
                        }
                    }
                }
            }
            if (canMove)
            {
                for (int i=0;i<4;i++)
                {
                    blockYPos[i] += movePixels;
                }
            }
            BuildShape(currentShape);
            return rectangleShape;
        }
        public Rectangle[] moveShapeLeft(int intMovePixels, Rectangle[][] rectangleGameGrid)
        {
            bool canMove = true;
            int[] YPosition = new int[4];
 
         
            int furthestX = panelWidth;
           
            for (int j=0;j<4;j++)
            {
                if (blockXPos[j] <= furthestX)
                {
                    furthestX = blockXPos[j];
                    YPosition[j] = blockYPos[j];
                    if (Decimal.Remainder(blockYPos[j],10) != 0)
                    {
                        YPosition[j] += 5;
                    }
                }
                if ((blockXPos[j] - intMovePixels) < 0)
                {
                    canMove = false;
                    break;
                }
            }
            if (canMove)
            {
                for (int i=0;i<4;i++)
                {
                    if (YPosition[i] >= 0)
                    {
                        if (!rectangleGameGrid[YPosition[i]/10][(furthestX-10)/10].IsEmpty)
                        {
                           
                            canMove = false;
                            break;
                        }
                    }
                }
            }
            if (canMove)
            {
                for (int i=0;i<4;i++)
                {
                    blockXPos[i] -= intMovePixels;
                }
            }
            BuildShape(currentShape);
            return rectangleShape;
        }
        public Rectangle[] moveShapeRight(int intMovePixels, Rectangle[][] rectangleGameGrid)
        {
            bool canMove = true;
            int[] YPosition = new int[4];
           
            int furthestX = 0;
            
            for (int j=0;j<4;j++)
            {
                if (blockXPos[j] >= furthestX)
                {
                    furthestX = blockXPos[j];
                    YPosition[j] = blockYPos[j];
                    if (Decimal.Remainder(blockYPos[j],10) != 0)
                    {
                        YPosition[j] += 5;
                    }
                }
                if ((blockXPos[j] + intMovePixels) + 10 >= panelWidth)
                {
                    canMove = false;
                    break;
                }
            }
            if (canMove)
            {
                for (int i=0;i<4;i++)
                {
                    if (YPosition[i] >= 0)
                    {
                        if (!rectangleGameGrid[YPosition[i]/10][(furthestX+10)/10].IsEmpty)
                        {
                            
                            canMove = false;
                            break;
                        }
                    }
                }
            }
            if (canMove)
            {
                for (int i=0;i<4;i++)
                {
                    blockXPos[i] += intMovePixels;
                }
            }
            BuildShape(currentShape);
            return rectangleShape;
        }
        public Rectangle[] FlipShape(string direction, Rectangle[][] rectangleGameGrid)
        {
            bool canShapeMove = true;
            if (direction == "right")
            {
                currentShapePosition++;
                if (currentShapePosition > 4)
                {
                    currentShapePosition = 1;
                }
            }
            if (direction == "left")
            {
                currentShapePosition--;
                if (currentShapePosition < 1)
                {
                    currentShapePosition = 4;
                }
            }
            SetShapePosition();
            BuildShape(currentShape);
           
            Rectangle recGameArea = new Rectangle(0,0,panelWidth,panelHeight);
            int[] YPosition = new int[4];
            for (int i=0;i<4;i++)
            {
                if (!recGameArea.Contains(rectangleShape[i]))
                {
                    canShapeMove = false;
                    break;
                }
              
                YPosition[i] = blockYPos[i];
                if (Decimal.Remainder(blockYPos[i],10) != 0)
                {
                    YPosition[i] += 5;
                }
                if (!rectangleGameGrid[YPositions[i]/10][blockXPos[i]/10].IsEmpty)
                {
                 
                    canShapeMove = false;
                    break;
                }
            }
            if (!canShapeMove)
            {
             
                if (direction == "right")
                {
                    currentShapePosition--;
                    if (currentShapePosition < 1)
                    {
                        currentShapePosition = 4;
                    }
                    SetShapePosition();
                    BuildShape(currentShape);
                }
                if (direction == "left")
                {
                    currentShapePosition++;
                    if (currentShapePosition > 4)
                    {
                        currentShapePosition = 1;
                    }
                    SetShapePosition();
                    BuildShape(currentShape);
                }
            }
            return rectangleShape;
        }
        private void SetShapeStart()
        {
            if (!isNextShape)
            {
                switch(currentShape)
                {
 
                    case 1:
                        blockXPos[0] = startingXCoordinate;
                        blockYPos[0] = -10;
                        blockXPos[1] = startingXCoordinate-10;
                        blockYPos[1] = -10;
                        blockXPos[2] = startingXCoordinate+10;
                        blockYPos[2] = -10;
                        blockXPos[3] = startingXCoordinate;
                        blockYPos[3] = -20;
                        break;
                    case 2:
                        blockXPos[0] = startingXCoordinate;
                        blockYPos[0] = -10;
                        blockXPos[1] = startingXCoordinate-10;
                        blockYPos[1] = -10;
                        blockXPos[2] = startingXCoordinate+10;
                        blockYPos[2] = -10;
                        blockXPos[3] = startingXCoordinate+10;
                        blockYPos[3] = -20;
                        break;
                    case 3:
                        blockXPos[0] = startingXCoordinate;
                        blockYPos[0] = -10;
                        blockXPos[1] = startingXCoordinate-10;
                        blockYPos[1] = -10;
                        blockXPos[2] = startingXCoordinate+10;
                        blockYPos[2] = -10;
                        blockXPos[3] = startingXCoordinate-10;
                        blockYPos[3] = -20;
                        break;
                    case 4:
                        blockXPos[0] = startingXCoordinate;
                        blockYPos[0] = -10;
                        blockXPos[1] = startingXCoordinate+10;
                        blockYPos[1] = -10;
                        blockXPos[2] = startingXCoordinate;
                        blockYPos[2] = -20;
                        blockXPos[3] = startingXCoordinate+10;
                        blockYPos[3] = -20;
                        break;
                    case 5:
                        blockXPos[0] = startingXCoordinate;
                        blockYPos[0] = -10;
                        blockXPos[1] = startingXCoordinate;
                        blockYPos[1] = -20;
                        blockXPos[2] = startingXCoordinate;
                        blockYPos[2] = -30;
                        blockXPos[3] = startingXCoordinate;
                        blockYPos[3] = -40;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch(currentShape)
                {
 
                    case 1:
                        blockXPos[0] = startingXCoordinate-5;
                        blockYPos[0] = 35;
                        blockXPos[1] = startingXCoordinate-15;
                        blockYPos[1] = 35;
                        blockXPos[2] = startingXCoordinate+5;
                        blockYPos[2] = 35;
                        blockXPos[3] = startingXCoordinate-5;
                        blockYPos[3] = 25;
                        break;
                    case 2:
                        blockXPos[0] = startingXCoordinate-5;
                        blockYPos[0] = 35;
                        blockXPos[1] = startingXCoordinate-15;
                        blockYPos[1] = 35;
                        blockXPos[2] = startingXCoordinate+5;
                        blockYPos[2] = 35;
                        blockXPos[3] = startingXCoordinate+5;
                        blockYPos[3] = 25;
                        break;
                    case 3:
                        blockXPos[0] = startingXCoordinate-5;
                        blockYPos[0] = 35;
                        blockXPos[1] = startingXCoordinate-15;
                        blockYPos[1] = 35;
                        blockXPos[2] = startingXCoordinate+5;
                        blockYPos[2] = 35;
                        blockXPos[3] = startingXCoordinate-15;
                        blockYPos[3] = 25;
                        break;
                    case 4:
                        blockXPos[0] = startingXCoordinate-10;
                        blockYPos[0] = 35;
                        blockXPos[1] = startingXCoordinate;
                        blockYPos[1] = 35;
                        blockXPos[2] = startingXCoordinate-10;
                        blockYPos[2] = 25;
                        blockXPos[3] = startingXCoordinate;
                        blockYPos[3] = 25;
                        break;
                    case 5:
                        blockXPos[0] = startingXCoordinate-5;
                        blockYPos[0] = 45;
                        blockXPos[1] = startingXCoordinate-5;
                        blockYPos[1] = 35;
                        blockXPos[2] = startingXCoordinate-5;
                        blockYPos[2] = 25;
                        blockXPos[3] = startingXCoordinate-5;
                        blockYPos[3] = 15;
                        break;
                    default:
                        break;
                }
                BuildShape(currentShape);
            }
        }
        public Rectangle[] GetShape()
        {
            return rectangleShape;
        }
        private void SetShapePosition()
        {
            switch(currentShape)
            {
                case 1:
                switch(currentShapePosition)
                {
                    case 1:
                        blockXPos[0] = blockXPos[0];
                        blockYPos[0] = blockYPos[0];
                        blockXPos[1] = blockXPos[0]-10;
                        blockYPos[1] = blockYPos[0];
                        blockXPos[2] = blockXPos[0]+10;
                        blockYPos[2] = blockYPos[0];
                        blockXPos[3] = blockXPos[0];
                        blockYPos[3] = blockYPos[0]-10;
                        break;
                    case 2:
                        blockXPos[0] = blockXPos[0];
                        blockYPos[0] = blockYPos[0];
                        blockXPos[1] = blockXPos[0];
                        blockYPos[1] = blockYPos[0]-10;
                        blockXPos[2] = blockXPos[0];
                        blockYPos[2] = blockYPos[0]+10;
                        blockXPos[3] = blockXPos[0]+10;
                        blockYPos[3] = blockYPos[0];
                        break;
                    case 3:
                        blockXPos[0] = blockXPos[0];
                        blockYPos[0] = blockYPos[0];
                        blockXPos[1] = blockXPos[0]+10;
                        blockYPos[1] = blockYPos[0];
                        blockXPos[2] = blockXPos[0]-10;
                        blockYPos[2] = blockYPos[0];
                        blockXPos[3] = blockXPos[0];
                        blockYPos[3] = blockYPos[0]+10;
                        break;
                    case 4:
                        blockXPos[0] = blockXPos[0];
                        blockYPos[0] = blockYPos[0];
                        blockXPos[1] = blockXPos[0];
                        blockYPos[1] = blockYPos[0]+10;
                        blockXPos[2] = blockXPos[0];
                        blockYPos[2] = blockYPos[0]-10;
                        blockXPos[3] = blockXPos[0]-10;
                        blockYPos[3] = blockYPos[0];
                        break;
                    default:
                        break;
                }
                    break;
                case 2:
                switch(currentShapePosition)
                {
                    case 1:
                        blockXPos[0] = blockXPos[0];
                        blockYPos[0] = blockYPos[0];
                        blockXPos[1] = blockXPos[0]-10;
                        blockYPos[1] = blockYPos[0];
                        blockXPos[2] = blockXPos[0]+10;
                        blockYPos[2] = blockYPos[0];
                        blockXPos[3] = blockXPos[0]+10;
                        blockYPos[3] = blockYPos[0]-10;
                        break;
                    case 2:
                        blockXPos[0] = blockXPos[0];
                        blockYPos[0] = blockYPos[0];
                        blockXPos[1] = blockXPos[0];
                        blockYPos[1] = blockYPos[0]-10;
                        blockXPos[2] = blockXPos[0];
                        blockYPos[2] = blockYPos[0]+10;
                        blockXPos[3] = blockXPos[0]+10;
                        blockYPos[3] = blockYPos[0]+10;
                        break;
                    case 3:
                        blockXPos[0] = blockXPos[0];
                        blockYPos[0] = blockYPos[0];
                        blockXPos[1] = blockXPos[0]+10;
                        blockYPos[1] = blockYPos[0];
                        blockXPos[2] = blockXPos[0]-10;
                        blockYPos[2] = blockYPos[0];
                        blockXPos[3] = blockXPos[0]-10;
                        blockYPos[3] = blockYPos[0]+10;
                        break;
                    case 4:
                        blockXPos[0] = blockXPos[0];
                        blockYPos[0] = blockYPos[0];
                        blockXPos[1] = blockXPos[0];
                        blockYPos[1] = blockYPos[0]+10;
                        blockXPos[2] = blockXPos[0];
                        blockYPos[2] = blockYPos[0]-10;
                        blockXPos[3] = blockXPos[0]-10;
                        blockYPos[3] = blockYPos[0]-10;
                        break;
                    default:
                        break;
                }
                    break;
                case 3:
                switch(currentShapePosition)
                {
                    case 1:
                        blockXPos[0] = blockXPos[0];
                        blockYPos[0] = blockYPos[0];
                        blockXPos[1] = blockXPos[0]-10;
                        blockYPos[1] = blockYPos[0];
                        blockXPos[2] = blockXPos[0]+10;
                        blockYPos[2] = blockYPos[0];
                        blockXPos[3] = blockXPos[0]-10;
                        blockYPos[3] = blockYPos[0]-10;
                        break;
                    case 2:
                        blockXPos[0] = blockXPos[0];
                        blockYPos[0] = blockYPos[0];
                        blockXPos[1] = blockXPos[0];
                        blockYPos[1] = blockYPos[0]-10;
                        blockXPos[2] = blockXPos[0];
                        blockYPos[2] = blockYPos[0]+10;
                        blockXPos[3] = blockXPos[0]+10;
                        blockYPos[3] = blockYPos[0]-10;
                        break;
                    case 3:
                        blockXPos[0] = blockXPos[0];
                        blockYPos[0] = blockYPos[0];
                        blockXPos[1] = blockXPos[0]+10;
                        blockYPos[1] = blockYPos[0];
                        blockXPos[2] = blockXPos[0]-10;
                        blockYPos[2] = blockYPos[0];
                        blockXPos[3] = blockXPos[0]+10;
                        blockYPos[3] = blockYPos[0]+10;
                        break;
                    case 4:
                        blockXPos[0] = blockXPos[0];
                        blockYPos[0] = blockYPos[0];
                        blockXPos[1] = blockXPos[0];
                        blockYPos[1] = blockYPos[0]+10;
                        blockXPos[2] = blockXPos[0];
                        blockYPos[2] = blockYPos[0]-10;
                        blockXPos[3] = blockXPos[0]-10;
                        blockYPos[3] = blockYPos[0]+10;
                        break;
                    default:
                        break;
                }
                    break;
                case 5:
                switch(currentShapePosition)
                {
                    case 1:
                        blockXPos[0] = blockXPos[0];
                        blockYPos[0] = blockYPos[0];
                        blockXPos[1] = blockXPos[0];
                        blockYPos[1] = blockYPos[0]-10;
                        blockXPos[2] = blockXPos[0];
                        blockYPos[2] = blockYPos[0]-20;
                        blockXPos[3] = blockXPos[0];
                        blockYPos[3] = blockYPos[0]-30;
                        break;
                    case 2:
                        blockXPos[0] = blockXPos[0];
                        blockYPos[0] = blockYPos[0];
                        blockXPos[1] = blockXPos[0]+10;
                        blockYPos[1] = blockYPos[0];
                        blockXPos[2] = blockXPos[0]+20;
                        blockYPos[2] = blockYPos[0];
                        blockXPos[3] = blockXPos[0]+30;
                        blockYPos[3] = blockYPos[0];
                        break;
                    case 3:
                        blockXPos[0] = blockXPos[0];
                        blockYPos[0] = blockYPos[0];
                        blockXPos[1] = blockXPos[0];
                        blockYPos[1] = blockYPos[0]+10;
                        blockXPos[2] = blockXPos[0];
                        blockYPos[2] = blockYPos[0]+20;
                        blockXPos[3] = blockXPos[0];
                        blockYPos[3] = blockYPos[0]+30;
                        break;
                    case 4:
                        blockXPos[0] = blockXPos[0];
                        blockYPos[0] = blockYPos[0];
                        blockXPos[1] = blockXPos[0]-10;
                        blockYPos[1] = blockYPos[0];
                        blockXPos[2] = blockXPos[0]-20;
                        blockYPos[2] = blockYPos[0];
                        blockXPos[3] = blockXPos[0]-30;
                        blockYPos[3] = blockYPos[0];
                        break;
                    default:
                        break;
                }
                    break;
                default:
                    break;
            }
        }
    }
}