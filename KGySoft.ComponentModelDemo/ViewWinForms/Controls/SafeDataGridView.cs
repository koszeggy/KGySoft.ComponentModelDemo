using System;
using System.Windows.Forms;

namespace KGySoft.ComponentModelDemo.ViewWinForms.Controls
{
    /// <summary>
    /// Just a DataGridView, which catches the exceptions from its OnPaint to prevent killing the rendering (red X issue).
    /// Needed to demonstrate the issues of inconsistent binding states in a DataGridView.
    /// </summary>
    internal class SafeDataGridView : DataGridView
    {
        public SafeDataGridView()
        {
            AutoGenerateColumns = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                base.OnPaint(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unhandled exception has been detected in DataGridView.OnPaint. This would have killed the grid rendering in a regular application. Press Reset to update a possibly inconsistent binding.{Environment.NewLine}{Environment.NewLine}"
                    + $"The caught exception: {ex}", "Unhandled Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
