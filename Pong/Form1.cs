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

{
    public partial class Form1 : Form
    {
        SoundPlayer Bonk = new SoundPlayer(Properties.Resources.PlayerBonk);
        SoundPlayer WallHit = new SoundPlayer(Properties.Resources.WallHit);
        SoundPlayer Goal = new SoundPlayer(Properties.Resources.Goal);
        SoundPlayer Yay = new SoundPlayer(Properties.Resources.Yay);



        Rectangle player1 = new Rectangle(5, 170, 10, 60);
        Rectangle player2 = new Rectangle(582, 170, 10, 60);
        Rectangle ball = new Rectangle(276, 195, 10, 10);

        Rectangle centerLineRed = new Rectangle(290, 0, 3, 1000);
        Rectangle centerLineBlue = new Rectangle(287, 0, 3, 1000);

        Rectangle borderTopBlue = new Rectangle(0, 0, 290, 3);
        Rectangle borderTopRed = new Rectangle(290, 0, 350, 3);

        Rectangle borderBottomBlue = new Rectangle(0, 397, 290, 3);
        Rectangle borderBottomRed = new Rectangle(290, 397, 350, 3);

        Rectangle blueScoreLine = new Rectangle(0, 150, 10, 100);
        Rectangle redScoreLine = new Rectangle(590, 150, 10, 100);


            


        int player1Score = 0;
        int player2Score = 0;

        int playerSpeed = 4;
        int ballXSpeed = 0;
        int ballYSpeed = 0;

        bool wDown = false;
        bool aDown = false;
        bool sDown = false;
        bool dDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;

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
            switch (e.KeyCode)
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
            switch (e.KeyCode)
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

            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerSpeed;
            }

            if (aDown == true && player1.X > 5)
            {
                player1.X -= playerSpeed;
            }

            if (dDown == true && player1.X < 280)
            {
                player1.X += playerSpeed;
            }

           
            //move player 2 
            if (upArrowDown == true && player2.Y > 0)
            {
                player2.Y -= playerSpeed;
            }

            if (downArrowDown == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += playerSpeed;
            }

            if (leftArrowDown == true && player2.X > 290)
            {
                player2.X -= playerSpeed;
            }

            if (rightArrowDown == true && player2.X < 582)
            {
                player2.X += playerSpeed;
            }

           


            //check if ball hit top or bottom wall and change direction if it does 
            if (ball.Y < 0 || ball.Y > this.Height - ball.Height)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed; 

                WallHit.Play();
            }
            else if (ball.X < 0 || ball.X > this.Width - ball.Width)
            {
                ballXSpeed *= -1;

                WallHit.Play();
            }
            //check if ball hits either player. If it does change the direction 
            //and place the ball in front of the player hit 
            if (player1.IntersectsWith(ball) && ballXSpeed != 0)
            {
                Bonk.Play();
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
                Bonk.Play();
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
               
                Yay.Play();
            }
            else if (ball.IntersectsWith(redScoreLine))
            {
                player1Score++;
                p1ScoreLabel.Text = $"{player1Score}";

                ball.X = 270;
                ball.Y = 195;

                ballXSpeed = 0;
                ballYSpeed = 0;

                Yay.Play();
            }

            // check score and stop game if either player is at 3 
            if (player1Score == 3)
            {
                gameEngine.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 1 Wins!!";

                Goal.Play();
            }
            else if (player2Score == 3)
            {
                gameEngine.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 2 Wins!!";

                Goal.Play();
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


          // Ball Direction
          //make player 2 hit ball
          //sounds

          

        }

    }
}
