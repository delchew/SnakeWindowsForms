using System.Windows.Forms;

namespace SnakeWindowsForms
{
    public class UnfocusedButton : Button
    {
        public UnfocusedButton()
        {
            SetStyle(ControlStyles.Selectable, false);
        }
    }
}
