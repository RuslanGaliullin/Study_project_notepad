using System.Collections.Generic;
using System.Windows.Forms;


namespace Main
{
    /// <summary>
    /// The FileInUse data type used to assign appropriate properties to files that are open in the window.
    /// </summary>
    internal sealed class FileInUse
    {
        /// <summary>
        /// File name.
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Checking that the file has already been saved.
        /// </summary>
        public bool IsSaved { get; set; }
        /// <summary>
        /// The parameter that the file was created in the application and not saved to the computer.
        /// </summary>
        public bool IsNew { get; set; }
        /// <summary>
        /// The parameter that the file needs to keep past versions.
        /// </summary>
        public bool Log { get; set; }
        /// <summary>
        /// The tab that the file belongs to.
        /// </summary>
        public TabPage TabPage { get; init; }
        /// <summary>
        /// RichTextBox, which is linked to the file, i.e. it shows the contents of the file.
        /// </summary>
        public RichTextBox TextBox { get; private set; }

        // Constructor.
        public FileInUse(string fileName, RichTextBox text, TabPage tabPage, bool isNew)
        {

            this.FileName = fileName;
            this.IsSaved = true;
            this.IsNew = isNew;
            this.TextBox = text;
            this.TabPage = tabPage;
            this.Log = false;
        }
    }

    /// <summary>
    /// A static class for interacting with application windows.
    /// </summary>
    internal static class MyForms
    {
        // List of open forms.
        private static readonly List<Form1> s_windows = new();

        /// <summary>
        /// Method that returns the first open form other than the current one.
        /// </summary>
        /// <param name="curForm">The current main form.</param>
        /// <returns>A new open form, or null if it does not exist.</returns>
        public static Form1 GetOpened(Form curForm)
        {
            return s_windows.Find(x => x != curForm);
        }

        /// <summary>
        /// Method of deleting a form from open.
        /// </summary>
        /// <param name="form">The form to delete.</param>
        public static void Close(Form1 form)
        {
            s_windows.Remove(form);
        }

        /// <summary>
        /// Method of adding a form to the list of open forms.
        /// </summary>
        /// <param name="form">Form to add.</param>
        public static void Add(Form1 form)
        {
            s_windows.Add(form);
        }
    }
}
