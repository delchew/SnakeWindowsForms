using SnakeWindowsForms.Properties;
using System.Windows.Forms;

namespace SnakeWindowsForms
{
    public class SnakeHead : SnakeSection
    {
        public SnakeHead(int row, int column, int size, Form form) : base(row, column, size, form)
        {
            Image = Resources.headRight;
        }

        public void MoveTo(Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    Column++;
                    Image = Resources.headRight;
                    break;
                case Direction.Left:
                    Column--;
                    Image = Resources.headLeft;
                    break;
                case Direction.Up:
                    Row--;
                    Image = Resources.headUp;
                    break;
                case Direction.Down:
                    Row++;
                    Image = Resources.headDown;
                    break;
            }
        }
    }
}
