using SnakeWindowsForms.Properties;
using System.Windows.Forms;

namespace SnakeWindowsForms
{
    public class SnakeSection : Block
    {

        public SnakeSection(int row, int column, int size, Form form) : base(row, column, size, form)
        {
            Image = Resources.section;
        }
    }
}
