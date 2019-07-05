using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BindingTest.Properties;

namespace BindingTest.ViewWpf
{
    internal static class Images
    {
        private static ImageSource undo;
        private static ImageSource redo;
        private static ImageSource edit;
        private static ImageSource accept;
        private static ImageSource cancel;

        private static ImageSource error;
        private static ImageSource warning;
        private static ImageSource info;

        public static ImageSource Undo => undo ?? (undo = BitmapFrame.Create(Resources.Undo));
        public static ImageSource Redo => redo ?? (redo = BitmapFrame.Create(Resources.Redo));
        public static ImageSource Edit => edit ?? (edit = BitmapFrame.Create(Resources.Edit));
        public static ImageSource Accept => accept ?? (accept = BitmapFrame.Create(Resources.Accept));
        public static ImageSource Cancel => cancel ?? (cancel = BitmapFrame.Create(Resources.Cancel));

        public static ImageSource Error => error ?? (error = BitmapFrame.Create(Resources.Error));
        public static ImageSource Warning => warning ?? (warning = BitmapFrame.Create(Resources.Warning));
        public static ImageSource Information => info ?? (info = BitmapFrame.Create(Resources.Information));

    }
}
