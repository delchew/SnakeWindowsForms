using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SnakeWindowsForms
{
    public class Snake
    {
        private SnakeHead head;
        private int flashingCount = 12;
        private readonly List<SnakeSection> snakeBody;
        private readonly Form form;
        private readonly Timer missTimer;
        private readonly int sectionSize;

        public event EventHandler Fail;
        public Snake(int startRowPosition, int startColumnPosition, int sectionSize, Form form)
        {
            this.form = form;
            this.sectionSize = sectionSize;
            missTimer = new Timer();
            missTimer.Interval = 100;
            missTimer.Tick += MissTimer_Tick;
            snakeBody = new List<SnakeSection>();
            head = new SnakeHead(startColumnPosition, startRowPosition, sectionSize, form);
            snakeBody.Add(head);

            for (int i = 0; i < 2; i++)
            {
                var bodySection = new SnakeSection(head.Row, head.Column - (i + 1), sectionSize, form);
                snakeBody.Add(bodySection);
            }
        }

        public void MoveTo(Direction direction)
        {
            for (int i = snakeBody.Count - 1; i > 0; i--)
            {
                snakeBody[i].MoveToCell(snakeBody[i - 1]);
            }
            head.MoveTo(direction);
        }

        public bool Eat(Food food)
        {
            return head.Intersect(food);
        }

        public bool Contains(Block block)
        {
            foreach(var section in snakeBody)
            {
                if(section.Intersect(block))
                {
                    return true;
                }
            }
            return false;
        }

        public void Grow(Food food)
        {
            snakeBody.Add(new SnakeSection(snakeBody[snakeBody.Count - 1].Row, snakeBody[snakeBody.Count - 1].Column, sectionSize, form));
            head.MoveToCell(food);
        }

        public bool HitBorder(List<WallSection> walls)
        {
            foreach (var wall in walls)
            {
                if (head.Intersect(wall))
                {
                    Miss();
                    return true;
                }
            }
            return false;
        }

        public bool HitItself()
        {
            for (int i = 1; i < snakeBody.Count; i++)
            {
                if (head.Intersect(snakeBody[i]))
                {
                    Miss();
                    return true;
                }
            }
            return false;
        }

        private void MissTimer_Tick(object sender, EventArgs e)
        {
            foreach (var section in snakeBody)
            {
                section.Visible = !section.Visible;
            }
            flashingCount--;
            if (flashingCount == 0)
            {
                missTimer.Stop();
                Fail?.Invoke(this, EventArgs.Empty);
                Dispose();
            }
        }

        public void Dispose()
        {
            missTimer.Dispose();
            foreach(var section in snakeBody)
            {
                form.Controls.Remove(section);
                section.Dispose();
            }
        }

        private void Miss()
        {
            missTimer.Start();
        }
    }
}
