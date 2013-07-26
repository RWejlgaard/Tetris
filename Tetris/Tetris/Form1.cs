// Made by IzYZacee
//You must not take credit or in other ways steal this project
//If you wish to contribute you can PM me at http://reddit.com/u/izyzacee/
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Media;
using System.Runtime.InteropServices; 
namespace Tetris
{
    public class Form1 : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Panel drawingAreaCanvas;
        private System.ComponentModel.IContainer components;
 
       
        Timer timer;
        bool aBtnClicked;
        
        SoundPlayer soundEndGame = new SoundPlayer(@"soundEndGame.wav");
        SoundPlayer soundItemDropped = new SoundPlayer(@"soundItemDropped.wav");
        SoundPlayer soundMoveShape = new SoundPlayer(@"soundMoveShape.wav");
        SoundPlayer soundRotateShape = new SoundPlayer(@"soundRotateShape.wav");
 
 
        int drawingAreaWidth;
        int drawingAreaHeight;
 
        Rectangle rectangle;
        Rectangle[] rectangleShape;
        Shape aShape;
        private SolidBrush[][] arrayBrushes = new SolidBrush[30][];
        private SolidBrush[] brushColors = new SolidBrush[5];
        private int shapeType;
        private int nextShapeType;
        private Random randomShapeType = new Random();
        private bool isRowFull;
        private bool isDropped = false;
 
 
        
        private GameGrid gameGrid;
        private int numberOfRows;
        private int numberOfCols;
        private SolidBrush[][] gameGridBrushes;
        private SolidBrush[] theBrushColors;
        private System.Windows.Forms.Timer GameTimer;
        private Rectangle[][] rectangleGameGrid;
        private int dropRate;
        private int gameSpeed;
        private int bonusHeight;
        private Screen mainScreen;
        private Screen startScreen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblScore;
        private int bonusStep;
        private long score;
        private int level;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRows;
        private int levelRowsCompleted;
        private int totalRowsCompleted;
        private bool isGameOver;
        private bool isGamePaused;
        private bool dropBtnPressed = false;
        private Label label14;
        private ToolStripMenuItem menuFile;
        private ToolStripMenuItem menuNewGame;
        private ToolStripMenuItem menuResetGrid;
        private ToolStripMenuItem lavetAfToolStripMenuItem;
        private ToolStripMenuItem controlsToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem donateToolStripMenuItem;
        private bool startBtnPressed = false;
 
        public Form1()
        {
        
            try
            {
        
            }
            catch (Exception e)
            {
            }
 
 
            InitializeComponent();
        
            SetUpGame();
            
 
            aBtnClicked = false;
            timer = new Timer();
            timer.Interval = 300;
            timer.Tick += new EventHandler(ClockTick);
            timer.Enabled = true;
            
               
            GameTimer.Enabled = true;
            GameTimer.Interval = gameSpeed;
 
        }
   
        void ClockTick(Object sender, EventArgs e)
        {
            aBtnClicked = false;
        }
 
        private void LayoutForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            string strKeyPress = null;
            strKeyPress = e.KeyCode.ToString();
            if (!isGameOver)
            {
                switch (strKeyPress.ToUpper())
                {
                    case "A":
                        if (aShape.shapeMoving) rectangleShape = aShape.moveShapeLeft(10, gameGrid.GetGameGrid());
                   
                        break;
                    case "D":
                        if (aShape.shapeMoving) rectangleShape = aShape.moveShapeRight(10, gameGrid.GetGameGrid());
                    
                        break;
                    case "W":
                        if (aShape.shapeMoving) rectangleShape = aShape.FlipShape("right", gameGrid.GetGameGrid());
                    
                        break;
               
                    case "Q":
                        GameTimer.Stop();
                        SetUpGame();
                        DrawStart();
                        break;
                    case "E":
                        Application.Exit();
                        break;
                    case "ESCAPE":
                        if (!isGamePaused)
                        {
                           // DrawGamePaused();
                            GameTimer.Stop();
                            
                            isGamePaused = true;

                            Graphics gpause = startScreen.GetGraphics();
                            startScreen.erase();
                            gpause.DrawString("Pause", new Font("Comic Sans", 18), new SolidBrush(Color.Red), 46, 100);
                            startScreen.flip();
                        }
                        else
                        {
                            GameTimer.Start();
                            isGamePaused = false;
                        }
                        break;
                    case "R":
                        DrawGameOver();
                        break;
                    case "SPACE":
                        if (aShape.shapeMoving)
                        {
                      
                            GameTimer.Interval = dropRate;
                            isDropped = true;
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (strKeyPress.ToUpper())
                {
                    case "SPACE":
                     
                        SetUpGame();
                        shapeType = GetShapeType();
                        aShape = new Shape(shapeType, mainScreen.screenWidth, mainScreen.screenHeight, false);
                        nextShapeType = GetShapeType();
                       
                        aShape.shapeMoving = true;
                        isGameOver = false;
                        GameTimer.Interval = gameSpeed;
                        GameTimer.Enabled = true;
                        GameTimer.Start();
                        break;
                    default:
                        break;
                }
            }
        }
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if(components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }
 
        #region Windows Form Designer generated code
        /// <summary>
      
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            this.lblScore = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRows = new System.Windows.Forms.Label();
            this.drawingAreaCanvas = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.menuResetGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.lavetAfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.donateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GameTimer
            // 
            this.GameTimer.Tick += new System.EventHandler(this.GameTimer_Tick);
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblScore.Location = new System.Drawing.Point(50, 35);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(13, 13);
            this.lblScore.TabIndex = 3;
            this.lblScore.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(9, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Points:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(9, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Lines:";
            // 
            // lblRows
            // 
            this.lblRows.AutoSize = true;
            this.lblRows.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblRows.Location = new System.Drawing.Point(50, 54);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(13, 13);
            this.lblRows.TabIndex = 6;
            this.lblRows.Text = "0";
            // 
            // drawingAreaCanvas
            // 
            this.drawingAreaCanvas.BackColor = System.Drawing.SystemColors.Window;
            this.drawingAreaCanvas.Location = new System.Drawing.Point(12, 81);
            this.drawingAreaCanvas.Name = "drawingAreaCanvas";
            this.drawingAreaCanvas.Size = new System.Drawing.Size(168, 254);
            this.drawingAreaCanvas.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label14.Location = new System.Drawing.Point(9, 341);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 13);
            this.label14.TabIndex = 9;
            this.label14.Text = "Version: 1.0";
            // 
            // menuFile
            // 
            this.menuFile.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewGame,
            this.menuResetGrid,
            this.lavetAfToolStripMenuItem,
            this.donateToolStripMenuItem});
            this.menuFile.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(51, 20);
            this.menuFile.Text = "Menu";
            // 
            // menuNewGame
            // 
            this.menuNewGame.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menuNewGame.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.menuNewGame.Name = "menuNewGame";
            this.menuNewGame.Size = new System.Drawing.Size(152, 22);
            this.menuNewGame.Text = "New Game";
            this.menuNewGame.Click += new System.EventHandler(this.menuNewGame_Click);
            // 
            // menuResetGrid
            // 
            this.menuResetGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menuResetGrid.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.menuResetGrid.Name = "menuResetGrid";
            this.menuResetGrid.Size = new System.Drawing.Size(152, 22);
            this.menuResetGrid.Text = "Restart";
            this.menuResetGrid.Click += new System.EventHandler(this.menuResetGrid_Click);
            // 
            // lavetAfToolStripMenuItem
            // 
            this.lavetAfToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lavetAfToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lavetAfToolStripMenuItem.Name = "lavetAfToolStripMenuItem";
            this.lavetAfToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.lavetAfToolStripMenuItem.Text = "Made by?";
            this.lavetAfToolStripMenuItem.Click += new System.EventHandler(this.lavetAfToolStripMenuItem_Click);
            // 
            // controlsToolStripMenuItem
            // 
            this.controlsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.controlsToolStripMenuItem.Name = "controlsToolStripMenuItem";
            this.controlsToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.controlsToolStripMenuItem.Text = "Controls";
            this.controlsToolStripMenuItem.Click += new System.EventHandler(this.controlsToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.controlsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(192, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // donateToolStripMenuItem
            // 
            this.donateToolStripMenuItem.Name = "donateToolStripMenuItem";
            this.donateToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.donateToolStripMenuItem.Text = "Donate";
            this.donateToolStripMenuItem.Click += new System.EventHandler(this.donateToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(192, 360);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblRows);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.drawingAreaCanvas);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tetris";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.LayoutForm_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LayoutForm_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
 
        private void Form1_Load(object sender, System.EventArgs e)
        {
        }
        private void DrawScreen()
        {
            Graphics g = mainScreen.GetGraphics();
            gameGridBrushes = gameGrid.GetGameGridBrushes();
            theBrushColors = gameGrid.GetShapeColors();
            rectangleGameGrid = gameGrid.GetGameGrid();
            mainScreen.erase();
           
            for (int i=0;i<numberOfRows;i++)
            {
                for (int k=0;k<numberOfCols;k++)
                {
                    if (!gameGrid.IsGridLocationEmpty(i,k))
                    {
                        g.FillRectangle(gameGridBrushes[i][k],rectangleGameGrid[i][k]);
                        g.DrawRectangle(new Pen(Color.White,1),rectangleGameGrid[i][k]);
                    }
                }
            }
         
            for (int j=0;j<rectangleShape.Length;j++)
            {
                g.FillRectangle(theBrushColors[shapeType-1],rectangleShape[j]);
                g.DrawRectangle(new Pen(Color.White,1),rectangleShape[j]);
            }
 
 
            mainScreen.flip();
            bonusStep--;
 
 
        }
        private void LayoutForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            DrawStart();
        }
 
 
        private void GameTimer_Tick(object sender, System.EventArgs e)
        {
            if (isGamePaused == true) {
                Graphics gpause = startScreen.GetGraphics();
                startScreen.erase();
                gpause.DrawString("Pause", new Font("ComicSans", 18), new SolidBrush(Color.White), 5, 100);
                startScreen.flip();
            }
            if (aBtnClicked)
            {
            }
            if (startBtnPressed)
            {
                SetUpGame();
 
               
 
                shapeType = GetShapeType();
                aShape = new Shape(shapeType, mainScreen.screenWidth, mainScreen.screenHeight, false);
               // nextShapeType = GetShapeType();
                nextShapeType = 1;

                aShape.shapeMoving = true;
                isGameOver = false;
                GameTimer.Interval = gameSpeed;
                GameTimer.Enabled = true;
                GameTimer.Start();
                startBtnPressed = false;
                return;
            }
            if (aShape != null)
            {
                if (aShape.shapeMoving)
                {
                    if (dropBtnPressed)
                    {
                        GameTimer.Interval = dropRate;
                        dropBtnPressed = false;
                    
                    }
                    rectangleShape = aShape.moveShapeDown(dropRate, gameGrid.GetGameGrid());
                    DrawScreen();
 
                }
 
                else
                {
                    int XCoordinate;
                    int YCoordinate;
                  
                    for (int i = 0; i < 4; i++)
                    {
                        if (!rectangle.Contains(rectangleShape[i]))
                        {
                            isGameOver = true;
                            break;
                        }
                    }
                    if (!isGameOver)
                    {
                        int[] intYCoordinates = new int[4];
                        
                        for (int i = 0; i < 4; i++)
                        {
                            XCoordinate = rectangleShape[i].X;
                            YCoordinate = rectangleShape[i].Y;
                            intYCoordinates[i] = YCoordinate / 10;
                           
                            gameGrid.SetShapeLocation(YCoordinate / 10, XCoordinate / 10, rectangleShape[i], shapeType);
                        }
                      
                        Array.Sort(intYCoordinates);
                        for (int i = 0; i < 4; i++)
                        {
                            isRowFull = true;
                           
                            for (int j = 0; j < numberOfCols; j++)
                            {
                                if (gameGrid.IsGridLocationEmpty(intYCoordinates[i], j))
                                {
                                    isRowFull = false;
                                    break;
                               
                                }
                            }
                            if (isRowFull)
                            {
                                
                                for (int k = intYCoordinates[i]; k > 0; k--)
                                {
                                    
                                    for (int l = 0; l < numberOfCols; l++)
                                    {
                                        
                                        gameGrid.DropRowsDown(k, l);
                                    }
                                }
                               
                                gameGrid.SetTopRow();
                             
                                UpdateScore(intYCoordinates[i]);
                                bonusHeight = drawingAreaCanvas.Height - 1;
                                bonusStep = 5;
                            }
                        }
                        shapeType = nextShapeType;
                        aShape = new Shape(shapeType, mainScreen.screenWidth, mainScreen.screenHeight, false);
                        nextShapeType = GetShapeType();
                      
                        aShape.shapeMoving = true;
                        
                        GameTimer.Interval = gameSpeed;
                        isDropped = false;
                    }
                    else
                    {
                        GameTimer.Stop();
                      
                        DrawGameOver();
                      
                    }
                }
            }
           
        }
 
 
        private void DrawStart()
        {
            Graphics gStart = startScreen.GetGraphics();
            startScreen.erase();
 
 
            startScreen.flip();
        }
        private void UpdateScore(int intRowNum)
        {
            levelRowsCompleted++;
            totalRowsCompleted++;
            int reverseRow = 30 - intRowNum;
            score = score + (reverseRow * levelRowsCompleted * level * 10) + (bonusHeight * (level * levelRowsCompleted));
            lblScore.Text = score.ToString();
            if (levelRowsCompleted == 10)
            {
                UpdateLevel();
                levelRowsCompleted = 0;
            }
            lblRows.Text = totalRowsCompleted.ToString();
        }
        private void UpdateLevel()
        {
            level++;
            if (gameSpeed > 5) gameSpeed -= 20; 
        }
        private void SetUpGame()
        {
            
            gameSpeed = 100;
            bonusHeight = drawingAreaCanvas.Height-1;
            dropRate = 5;
            bonusStep = 5;
            score = 0;
            level = 1;
            levelRowsCompleted = 0;
            totalRowsCompleted = 0;
            isGameOver = true;
            isDropped = false;
 
            lblScore.Text = score.ToString();
            lblRows.Text = totalRowsCompleted.ToString();
 
         
            rectangle = new Rectangle(0,0,drawingAreaCanvas.Width, drawingAreaCanvas.Height);
            drawingAreaWidth = rectangle.Width;
            drawingAreaHeight = rectangle.Height;
            mainScreen = new Screen(drawingAreaCanvas, rectangle);
            
            rectangle = new Rectangle(0,0,drawingAreaCanvas.Width, drawingAreaCanvas.Height);
            startScreen = new Screen(drawingAreaCanvas, rectangle);
 
        
            numberOfRows = (drawingAreaCanvas.Height-1)/10;
            numberOfCols = (drawingAreaCanvas.Width-1)/10;
            gameGrid = new GameGrid(numberOfRows, numberOfCols);
 
        }
        private void DrawGameOver()
        {
            Graphics gOver = startScreen.GetGraphics();
            startScreen.erase();
            gOver.DrawString(lblScore.Text,new Font("ComicSans",10),new SolidBrush(Color.White),40,100);
            startScreen.flip();
        }

    /*    private void DrawGamePaused() {
            Graphics gPaused = startScreen.GetGraphics();
            startScreen.erase();
            gPaused.DrawString("Paused",new Font("ComicSans",18), new SolidBrush(Color.Red),5,100);
        }*/
 
        private int GetShapeType()
        {
            int shapeType;
            do
            {
                shapeType = randomShapeType.Next(6);
            }while (shapeType == 0);
            return shapeType;
        }
 

     /*   private int GetShapeType()
        {
            int shapeType = 1;
            
        }*/

        private void menuNewGame_Click(object sender, EventArgs e)
        {
           
            SetUpGame();
            shapeType = GetShapeType();
            aShape = new Shape(shapeType, mainScreen.screenWidth, mainScreen.screenHeight, false);
            nextShapeType = GetShapeType();
          
            aShape.shapeMoving = true;
            isGameOver = false;
            GameTimer.Interval = gameSpeed;
            GameTimer.Enabled = true;
            GameTimer.Start();
        }
 
        private void menuResetGrid_Click(object sender, EventArgs e)
        {
            GameTimer.Stop();
            SetUpGame();
            DrawStart();
        }
 
        private void menuCloseApplication_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
 


        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void lavetAfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Made by IzYZacee");
        }

        private void controlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Space - Start \nW - Rotate (clockwise)\nA - Left\nD - Right\nSpace - Drop\nQ - Restart\nEsc - Pause");
        }

        private void button1_Click(object sender, EventArgs e)
        {
         
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
        
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
         
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://pitchinbox.com/widget/widget.swf?id=2479788740");
        }

        private void linkLabel1_LinkClicked_2(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void donateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://pitchinbox.com/widget/widget.swf?id=2479788740");
        }
    }
}