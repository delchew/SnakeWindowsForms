using SnakeWindowsForms.Properties;
using System.Windows.Forms;

namespace SnakeWindowsForms
{
    public class Food : Block
    {
        public Food(int row, int column, int size, Form form) : base(row, column, size, form)
        {
            Image = Resources.food;
        }
    }
}
