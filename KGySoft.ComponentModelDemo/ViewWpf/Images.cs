﻿#region Usings

using System.Windows.Media;
using System.Windows.Media.Imaging;

#endregion

namespace KGySoft.ComponentModelDemo.ViewWpf
{
    internal static class Images
    {
        #region Fields

        private static ImageSource undo;
        private static ImageSource redo;
        private static ImageSource edit;
        private static ImageSource accept;
        private static ImageSource cancel;

        private static ImageSource error;
        private static ImageSource warning;
        private static ImageSource info;

        #endregion

        #region Properties

        public static ImageSource Undo => undo ?? (undo = BitmapFrame.Create(Properties.Resources.Undo));
        public static ImageSource Redo => redo ?? (redo = BitmapFrame.Create(Properties.Resources.Redo));
        public static ImageSource Edit => edit ?? (edit = BitmapFrame.Create(Properties.Resources.Edit));
        public static ImageSource Accept => accept ?? (accept = BitmapFrame.Create(Properties.Resources.Accept));
        public static ImageSource Cancel => cancel ?? (cancel = BitmapFrame.Create(Properties.Resources.Cancel));

        public static ImageSource Error => error ?? (error = BitmapFrame.Create(Properties.Resources.Error));
        public static ImageSource Warning => warning ?? (warning = BitmapFrame.Create(Properties.Resources.Warning));
        public static ImageSource Information => info ?? (info = BitmapFrame.Create(Properties.Resources.Information));

        #endregion
    }
}
