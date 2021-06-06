using System.Windows;

namespace ApiConsumer.CustomCode
{
    /// <summary>
    /// Depracated.
    /// </summary>
    public interface INullChecker
    {
        public void NullCheck(object o)
        {
            if (o == null)
            {
                MessageBox.Show("No bueno");
                throw new NullCheckException();
            }
        }
    }
}
