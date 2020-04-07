using System.Drawing;

namespace KGySoft.ComponentModelDemo.ViewWinForms
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

        public static Image Undo => undo ??= Image.FromStream(Properties.Resources.Undo);
        public static Image Redo => redo ??= Image.FromStream(Properties.Resources.Redo);
        public static Image Edit => edit ??= Image.FromStream(Properties.Resources.Edit);
        public static Image Accept => accept ??= Image.FromStream(Properties.Resources.Accept);
        public static Image Cancel => cancel ??= Image.FromStream(Properties.Resources.Cancel);

        public static Icon Error => error ??= new Icon(Properties.Resources.Error);
        public static Icon Warning => warning ??= new Icon(Properties.Resources.Warning);
        public static Icon Information => info ??= new Icon(Properties.Resources.Information);
    }
}
