using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SnakeWindowsForms
{
    public class Game
    {
        private Direction direction = Direction.Right;
        public Direction Direction
        {
            get
            {
                return direction;
            }
            set
            {
                if (value != direction)
                {
                    if (value == Direction.Right && direction != Direction.Left) direction = value;
                    if (value == Direction.Left && direction != Direction.Right) direction = value;
                    if (value == Direction.Up && direction != Direction.Down) direction = value;
                    if (value == Direction.Down && direction != Direction.Up) direction = value;
                }
                
            } 
        }
        private Form form;
        private Snake snake;
        private Food food;
        private List<WallSection> walls;
        private readonly int gameFieldSize;
        private readonly Random random = new Random();
        private readonly Timer snakeMovingTimer;
        private readonly int startXPosition = 8;
        private readonly int startYPosition = 8;
        private readonly int sectionSize = 30;
        private int score;

        public event EventHandler Lose;
        public event EventHandler<ScoreEventArgs> OnScoreChange;
        public Game(Form form, int gameFieldSize)
        {
            this.form = form;
            this.gameFieldSize = gameFieldSize;
            snakeMovingTimer = new Timer();
            snakeMovingTimer.Tick += Timer_Tick;
            DrawBorders();
            SetStartSettings();
        }

        public void Start()
        {
            snakeMovingTimer.Start();
        }

        private void SetStartSettings()
        {
            snakeMovingTimer.Interval = 250;
            score = 0;
            snake = new Snake(startXPosition, startYPosition, sectionSize, form);
            snake.Fail += Snake_Fail;
            food = CreateFood();

        }

        private void DrawBorders()
        {
            walls = new List<WallSection>();
            for (int i = 0; i < gameFieldSize; i++)
            {
                walls.Add(new WallSection(0, i, sectionSize, form));
                walls.Add(new WallSection(gameFieldSize - 1, i, sectionSize, form));
            }
            for (int i = 1; i < gameFieldSize - 1; i++)
            {
                walls.Add(new WallSection(i, 0, sectionSize, form));
                walls.Add(new WallSection(i, gameFieldSize - 1, sectionSize, form));
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (snake.Eat(food))
            {
                score++;
                OnScoreChange?.Invoke(this, new ScoreEventArgs(score));
                if (snakeMovingTimer.Interval >= 50)
                {
                    snakeMovingTimer.Interval -= 5;
                }
                form.Controls.Remove(food);
                snake.Grow(food);
                food = CreateFood();
            }
            if (snake.HitItself() || snake.HitBorder(walls))
            {
                snakeMovingTimer.Stop();
                return;
            }
            snake.MoveTo(Direction);
        }

        private Food CreateFood()
        {
            Food randomFood;
            do
            {
                var randomRow = random.Next(1, gameFieldSize - 1);
                var randomColumn = random.Next(1, gameFieldSize - 1);
                randomFood = new Food(randomRow, randomColumn, sectionSize, form);
                if (!snake.Contains(randomFood))
                {
                    break;
                }
                form.Controls.Remove(randomFood);
                randomFood.Dispose();
            }
            while (true);
            return randomFood;
        }

        private void Snake_Fail(object sender, EventArgs e)
        {
            GameFieldClear();
            Lose?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            snakeMovingTimer.Stop();
            snakeMovingTimer.Dispose();
            snake.Dispose();
            GameFieldClear();
        }

        private void GameFieldClear()
        {
            form.Controls.Remove(food);
            food.Dispose();
            foreach (var section in walls)
            {
                form.Controls.Remove(section);
                section.Dispose();
            }
        }
    }
}
