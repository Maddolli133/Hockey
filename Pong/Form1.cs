using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Pong
    //Maddox Ollivier, 2022-05-04, This Air hockey game is to be summited for the summative
{
    public partial class Form1 : Form
    {
        //Sounds
        SoundPlayer bonk = new SoundPlayer(Properties.Resources.PlayerBonk);
        SoundPlayer wallHit = new SoundPlayer(Properties.Resources.WallHit);
        SoundPlayer goal = new SoundPlayer(Properties.Resources.Goal);
        SoundPlayer yay = new SoundPlayer(Properties.Resources.Yay);
        //Player and ball hit box
        Rectangle player1 = new Rectangle(5, 170, 10, 60);
        Rectangle player2 = new Rectangle(582, 170, 10, 60);
        Rectangle ball = new Rectangle(276, 195, 10, 10);
        //Center lines to seperate each side
        Rectangle centerLineRed = new Rectangle(290, 0, 3, 1000);
        Rectangle centerLineBlue = new Rectangle(287, 0, 3, 1000);
        //The boundary line
        Rectangle borderTopBlue = new Rectangle(0, 0, 290, 3);
        Rectangle borderTopRed = new Rectangle(290, 0, 350, 3);
        //The boundary line
        Rectangle borderBottomBlue = new Rectangle(0, 397, 290, 3);
        Rectangle borderBottomRed = new Rectangle(290, 397, 350, 3);
        //The scoring spot
        Rectangle blueScoreLine = new Rectangle(0, 150, 10, 100);
        Rectangle redScoreLine = new Rectangle(590, 150, 10, 100);
        //Players and ball value
        int player1Score = 0;
        int player2Score = 0;
        int playerSpeed = 4;
        int ballXSpeed = 0;
        int ballYSpeed = 0;
        //What the keys are set for player one
        bool wDown = false;
        bool aDown = false;
        bool sDown = false;
        bool dDown = false;
        //What the keys are set for player two
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;
        //Different brushes/pens
        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        Pen bluePen = new Pen(Color.Blue);
        Pen redPen = new Pen(Color.Red);
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) //Keys used in game
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }

        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)//Keys used in game
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }

        }
        private void gameEngine_Tick(object sender, EventArgs e)
        {
            //move ball 
            if (player1.IntersectsWith(ball) && ballXSpeed == 0)
            {
                 ballXSpeed = 6;
                 ballYSpeed = -6;
            }
            else if (player2.IntersectsWith(ball) && ballXSpeed == 0)
            {
                ballXSpeed = -6;
                ballYSpeed = 6;
            }
            ball.X += ballXSpeed;
            ball.Y += ballYSpeed;
            //move player 1 
            if (wDown == true && player1.Y > 0)
            {
                player1.Y -= playerSpeed;
            }
            else if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerSpeed;
            }
            else if (aDown == true && player1.X > 5)
            {
                player1.X -= playerSpeed;
            }
            else if (dDown == true && player1.X < 280)
            {
                player1.X += playerSpeed;
            }
            //move player 2 
            if (upArrowDown == true && player2.Y > 0)
            {
                player2.Y -= playerSpeed;
            }
            else if (downArrowDown == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += playerSpeed;
            }
            else if (leftArrowDown == true && player2.X > 290)
            {
                player2.X -= playerSpeed;
            }
            else if (rightArrowDown == true && player2.X < 582)
            {
                player2.X += playerSpeed;
            }
            //check if ball hit top or bottom wall and change direction if it does 
            if (ball.Y < 0 || ball.Y > this.Height - ball.Height)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed; 

                wallHit.Play();
            }
            else if (ball.X < 0 || ball.X > this.Width - ball.Width)
            {
                ballXSpeed *= -1;

                wallHit.Play();
            }
            //check if ball hits either player. If it does change the direction 
            //and place the ball in front of the player hit 
            if (player1.IntersectsWith(ball) && ballXSpeed != 0)
            {
                bonk.Play();
                if (ballXSpeed < 0)
                {
                    ball.X = player1.X + ball.Width;
                }
                else
                {
                    ball.X = player1.X - ball.Width;
                }
                ballXSpeed *= -1;
            }
            else if (player2.IntersectsWith(ball) && ballXSpeed != 0)
            {
                bonk.Play();
                if (ballXSpeed > 0)
                {
                    ball.X = player2.X - ball.Width;
                }
                else
                {
                    ball.X = player2.X + ball.Width;
                }
                ballXSpeed *= -1;
            }
            else if (player2.IntersectsWith(ball) && ballXSpeed != 0)
            {
                ballXSpeed *= -1;
                ball.X = player2.X - ball.Width;
            }
            //check if a player missed the ball and if true add 1 to score of other player  
            if (ball.IntersectsWith(blueScoreLine))
            {
                player2Score++;
                p2ScoreLabel.Text = $"{player2Score}";

                ball.X = 295;
                ball.Y = 195;

                ballXSpeed = 0;
                ballYSpeed = 0;
               
                yay.Play();
            }
            else if (ball.IntersectsWith(redScoreLine))
            {
                player1Score++;
                p1ScoreLabel.Text = $"{player1Score}";

                ball.X = 270;
                ball.Y = 195;

                ballXSpeed = 0;
                ballYSpeed = 0;

                yay.Play();
            }
            // check score and stop game if either player is at 3 
            if (player1Score == 3)
            {
                gameEngine.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 1 Wins!!";

                goal.Play();
            }
            else if (player2Score == 3)
            {
                gameEngine.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 2 Wins!!";

                goal.Play();
            }
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(blueBrush, player1);
            e.Graphics.FillRectangle(redBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, ball);
            e.Graphics.FillRectangle(redBrush, centerLineRed);
            e.Graphics.FillRectangle(blueBrush, centerLineBlue);
            e.Graphics.FillRectangle(blueBrush, borderTopBlue);
            e.Graphics.FillRectangle(redBrush, borderTopRed);
            e.Graphics.FillRectangle(blueBrush, borderBottomBlue);
            e.Graphics.FillRectangle(redBrush, borderBottomRed);
            e.Graphics.DrawArc(bluePen, -50, 150, 100, 100, -100, 200);
            e.Graphics.DrawArc(redPen, 550, 150, 100, 100, 440, 200);
        }

    }
}
