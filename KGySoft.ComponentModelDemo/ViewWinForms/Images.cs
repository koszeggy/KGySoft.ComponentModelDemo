using System.Drawing;
using BindingTest.Properties;

namespace BindingTest.ViewWinForms
{
    internal static class Images
    {
        private static Image undo;
        private static Image redo;
        private static Image edit;
        private static Image accept;
        private static Image cancel;

        private static Icon error;
        private static Icon warning;
        private static Icon info;

        public static Image Undo => undo ?? (undo = Image.FromStream(Resources.Undo));
        public static Image Redo => redo ?? (redo = Image.FromStream(Resources.Redo));
        public static Image Edit => edit ?? (edit = Image.FromStream(Resources.Edit));
        public static Image Accept => accept ?? (accept = Image.FromStream(Resources.Accept));
        public static Image Cancel => cancel ?? (cancel = Image.FromStream(Resources.Cancel));

        public static Icon Error => error ?? (error = new Icon(Resources.Error));
        public static Icon Warning => warning ?? (warning = new Icon(Resources.Warning));
        public static Icon Information => info ?? (info = new Icon(Resources.Information));
    }
}
