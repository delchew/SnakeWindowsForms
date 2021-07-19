using System;
using System.Windows.Forms;
using System.Drawing;

namespace SnakeWindowsForms
{
    public partial class SnakeForm : Form
    {
        private Direction direction;
        private Game game;
        private readonly Timer waitStartTimer;
        private int startTimeWait;
        private UnfocusedButton startButton;
        private UnfocusedButton stopButton;
        private int controlsOffset = 5;
        private int gameFieldSize = 27;

        public SnakeForm()
        {
            InitializeComponent();
            startButton = new UnfocusedButton
            {
                Width = 100,
                Height = 25,
                Text = "Начать игру",
                Enabled = true
            };
            startButton.Location = new Point(ClientSize.Width - startButton.Width - controlsOffset, controlsOffset);
            startButton.Click += StartButton_Click;
            Controls.Add(startButton);

            stopButton = new UnfocusedButton
            {
                Width = 100,
                Height = 25,
                Text = "Завершить игру",
                Enabled = false
            };
            stopButton.Location = new Point(ClientSize.Width - stopButton.Width - controlsOffset, startButton.Bottom + controlsOffset);
            stopButton.Click += StopButton_Click; ;
            Controls.Add(stopButton);

            waitStartTimer = new Timer();
            waitStartTimer.Interval = 1000;
            waitStartTimer.Tick += WaitStartTimer_Tick;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            waitStartTimer.Stop();
            startWaitTimelabel.Text = string.Empty;
            game.Dispose();
            startButton.Enabled = true;
            stopButton.Enabled = false;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartInitialize();
            waitStartTimer.Start();
            startButton.Enabled = false;
            stopButton.Enabled = true;
        }

        private void Game_OnScoreChange(object sender, ScoreEventArgs e)
        {
            scoreLabel.Text = e.Score.ToString();
        }

        private void Game_Lose(object sender, EventArgs e)
        {
            StartInitialize();
            waitStartTimer.Start();
        }

        private void StartInitialize()
        {
            scoreLabel.Text = "0";
            startTimeWait = 3;
            startWaitTimelabel.Text = startTimeWait.ToString();
            game = new Game(this, gameFieldSize);
            game.Lose += Game_Lose;
            game.OnScoreChange += Game_OnScoreChange;
        }

        private void SnakeForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                direction = Direction.Left;
            }

            if (e.KeyCode == Keys.Right)
            {
                direction = Direction.Right;
            }

            if (e.KeyCode == Keys.Up)
            {
                direction = Direction.Up;
            }

            if (e.KeyCode == Keys.Down)
            {
                direction = Direction.Down;
            }
            game.Direction = direction;
        }

        private void WaitStartTimer_Tick(object sender, EventArgs e)
        {
            startTimeWait--;
            startWaitTimelabel.Text = startTimeWait.ToString();
            if (startTimeWait == 0)
            {
                waitStartTimer.Stop();
                startWaitTimelabel.Text = string.Empty;
                game.Start();
            }
        }
    }
}
