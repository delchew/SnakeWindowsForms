using SnakeWindowsForms.Properties;
using System.Windows.Forms;

namespace SnakeWindowsForms
{
     public class WallSection : Block
    {
        public WallSection(int row, int column, int size, Form form) : base(row, column, size, form)
        {
            Image = Resources.border;
        }
    }
}
