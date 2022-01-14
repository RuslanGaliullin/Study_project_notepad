using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;

// I have one tab == one file, so I can replace the word tab and file. They are equivalent.
// Similarly, the word form == window, since each form is a separate window.

namespace Main
{
    public partial class Form1 : Form
    {
        // List of all files opened (created) in the window.
        private readonly List<FileInUse> files = new();

        // The currently open file(tab in the window.
        private FileInUse curFile;

        // Form number to track when a new form is created when opened, and when from another window.
        private readonly bool isFirst;

        /// <summary>
        /// Form constructor. Initialization of all components, launching the StartForm method.
        /// </summary>
        /// <param name="is First">The parameter that the form was created when the application was launched.</param>
        public Form1(bool isFirst)
        {
            InitializeComponent();
            this.Text = $"NotePad+";
            this.isFirst = isFirst;
            // Setting the basic general parameters of the form.
            StartForm();
        }

        /// <summary>
        /// Setting common parmeters for the form: filters for available files, restore if necessary
        /// settings of the previous launch, setting the timer, adding controls with tabs.
        /// </summary>
        private void StartForm()
        {
            timer.Interval = 1;
            timer.Enabled = false;
            timer.Tick += TimerTick;
            openFileDialog.Filter = "Text files(*.txt)|*.txt|Rich text files(*.rtf)|*.rtf|C#(*.cs)|*.cs|All files(*.*)|*.*";
            saveFileDialog.Filter = "Text files(*.txt)|*.txt|Rich text files(*.rtf)|*.rtf|C#(*.cs)|*.cs";
            saveFileDialog.DefaultExt = "*.rtf";
            if (isFirst)
            {
                SetSettingss();
            }
        }

        /// <summary>
        /// Event handler for button clicks: "Create in a new tab" or "Open in a new tab".
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void OpenInNewTab(object sender, EventArgs e)
        {
            // If you clicked on the "Create in a new tab" button.
            if (sender.ToString().Contains("&Создать"))
            {
                CreateTheTab(this, "", "Безымянный", true);
            }
            else
            {
                // If you clicked "Open in a new tab".
                openFileDialog.InitialDirectory = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory());
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                // Opening the file in which the error is being checked for catching.
                if (ShowText(openFileDialog.FileName, out string text))
                {
                    // If everything opens normally, then create a new tab.
                    CreateTheTab(this, text, openFileDialog.FileName, false);
                }
                else
                {
                    // If something went wrong, then we output an error message.
                    MessageBox.Show(text);
                }
            }
        }

        /// <summary>
        /// Event handler for button clicks: "Create in a new window" or "Open in a new window".
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void OpenInNewForm(object sender, EventArgs e)
        {
            // If you want to create a file in a new window.
            if (sender.ToString().Contains("&Создать"))
            {
                Form1 newForm = new(false);
                // Adding to the general list of windows.
                MyForms.Add(newForm);
                CreateTheTab(newForm, "", "Безымянный", true);
                newForm.Show();
            }
            else
            {
                // Open an existing file in a new window
                openFileDialog.InitialDirectory = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory());
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                // Opening the file in which the error is being checked for catching.
                if (ShowText(openFileDialog.FileName, out string text))
                {
                    Form1 newForm = new(false);
                    // Adding a window to the general list of windows.
                    MyForms.Add(newForm);
                    // If everything opens normally, then create a new tab, but in the created window.
                    CreateTheTab(newForm, text, openFileDialog.FileName, false);
                    newForm.Show();
                }
                else
                {
                    // If something went wrong, then we output an error message.
                    MessageBox.Show(text);
                }
            }
        }

        /// <summary>
        /// Handler for clicking on the "Save to current tab" button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void FileInCurrentPage(object sender, EventArgs e)
        {
            // If there are no open tabs, then we execute as for the "Open in a new tab" event.
            if (tabControl.TabPages.Count == 0)
            {
                OpenInNewTab(openNewTabToolStripMenuItem, e);
                return;
            }
            // If there is, then close the current one and open a new one. If you cancel the opening of the file, the current one will close!
            DialogSaveToCloseCurrentTab(closeCurrentTabToolStripMenuItem, e);
            if (curFile.IsSaved)
            {
                files.Remove(curFile);
                tabControl.TabPages.Remove(tabControl.SelectedTab);
                // Calling the handler for clicking on the open button in a new tab.
                OpenInNewTab(openNewTabToolStripMenuItem, e);
            }
        }

        /// <summary>
        /// Method for creating a new tab in a specific window.
        /// </summary>
        /// <param name="form">The form where the new tab will be.</param>
        /// <param name="text">The text to be written to the RichTextBox.</param>
        /// <param name="fileName">A file that opens in a new tab.</param>
        /// <param name="isNew">The parameter that the file has just been created or already exists.</param>
        private void CreateTheTab(Form1 form, string text, string fileName, bool isNew)
        {
            try
            {
                // If the file is new, the tab will have the title "Unnamed", otherwise the file name.
                TabPage newTabPage = new(isNew ? "Безымянный" : Path.GetFileName(fileName));
                RichTextBox textBox = new();
                textBox.Dock = DockStyle.Fill;
                textBox.ContextMenuStrip = contextMenuStrip1;
                newTabPage.Controls.Add(textBox);
                // Open an existing file in rtf, if it has the appropriate permission.
                if (!isNew && Path.GetExtension(fileName) == ".rtf")
                {
                    textBox.LoadFile(fileName, RichTextBoxStreamType.RichText);
                }
                // Otherwise, we write the contents of the file in text format.
                else
                {
                    textBox.Text += text;
                }
                form.tabControl.TabPages.Add(newTabPage);
                // Changing the current tab in the window.
                form.tabControl.SelectedTab = newTabPage;
                // Changing the currently open file in the window.
                form.curFile = new FileInUse(fileName, textBox, newTabPage, isNew);
                // Add a new file to all open in the window.
                form.files.Add(form.curFile);
                // Adding a text change handler for RichTextBox to change the save status of the file.
                form.curFile.TextBox.TextChanged += new EventHandler(form.TextHasChanged);
            }
            catch (Exception e)
            {
                // Error message.
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Method for uploading the contents of a file in txt format>
        /// </summary>
        /// <param name="filename">File name.</param>
        /// <param name="text">The text of the file contents or an error message.</param>
        /// <returns>Information that the file opened without errors (true/false).</returns>
        private static bool ShowText(string filename, out string text)
        {
            try
            {
                text = File.ReadAllText(filename);
                return true;
            }
            catch (Exception e)
            {
                text = e.Message;
                return false;
            }
        }

        /// <summary>
        /// Event handler for clicking on the "Save" button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void SaveCurrentFile(object sender, EventArgs e)
        {
            // If a file is currently open, but it is created in the application (new).
            if (curFile != null && curFile.IsNew)
            {
                // // We call the event handler for clicking on the "Save as" button because the file is new.
                SaveAsCurrentFile(saveAsToolStripMenuItem, e);
            }
            // If some file is currently open that has not been saved yet
            else if (curFile != null && !curFile.IsSaved)
            {
                try
                {
                    // Saving the latest version of the file.
                    Logging();
                    // If the file resolution is .txt, then we save writing just the contents of the RichTextBox to a file.
                    if (Path.GetExtension(curFile.FileName) != ".rtf")
                    {
                        File.WriteAllText(curFile.FileName, curFile.TextBox.Text);
                    }
                    // Otherwise, we save it with formatting as rtf.
                    else
                    {
                        curFile.TextBox.SaveFile(curFile.FileName, RichTextBoxStreamType.RichText);
                    }
                    // Remove the asterisk from the tab header, which indicates that the file has not been saved.
                    curFile.TabPage.Text = Path.GetFileName(curFile.FileName);
                    curFile.IsSaved = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Event handler for clicking on the "Save as" button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void SaveAsCurrentFile(object sender, EventArgs e)
        {
            try
            {
                // Checking that there is a file that is being saved.
                if (curFile == null)
                {
                    return;
                }
                saveFileDialog.FileName = curFile.FileName;
                if (saveFileDialog.ShowDialog() == DialogResult.OK &&
                   saveFileDialog.FileName.Length > 0)
                {
                    // Saving the previous version.
                    Logging();
                    // .txt is saved by simply writing text to a file.
                    if (Path.GetExtension(saveFileDialog.FileName) != ".rtf")
                    {
                        File.WriteAllText(saveFileDialog.FileName, curFile.TextBox.Text);
                    }
                    // The rest of the files are in ce with the formatting preserved.
                    else
                    {
                        curFile.TextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
                    }
                    // Changing the tab header.
                    curFile.FileName = saveFileDialog.FileName;
                    curFile.TabPage.Text = Path.GetFileName(curFile.FileName);
                    curFile.IsSaved = true;
                    curFile.IsNew = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Handler for the text change event in RichTextBox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void TextHasChanged(object sender, EventArgs e)
        {
            if (curFile != null && curFile.IsSaved)
            {
                // Changing the file status to unsaved and adding an asterisk in the header.
                curFile.TabPage.Text = "*" + curFile.TabPage.Text;
                curFile.IsSaved = false;
            }
        }
        /// <summary>
        /// Adding a tab switch handler. When creating a form
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl.Selected += new TabControlEventHandler(ChangeSelectedTab);
        }
        /// <summary>
        /// The event handler for changing the current tab.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void ChangeSelectedTab(object sender, TabControlEventArgs e)
        {
            if (files.Count != 0)
            {
                // We are looking among the files for the one with the TabPage property corresponding to the open tab.
                curFile = files.Find(x => x.TabPage == tabControl.SelectedTab);
                // If we change the current file, we also change the settings for saving versions in accordance with the settings
                // for the file.
                if (curFile != null)
                {
                    loggingToolStripMenuItem.Checked = curFile.Log;
                }
                // If there is no open file or a new one has been created, then turn off logging by default.
                else
                {
                    loggingToolStripMenuItem.Checked = false;
                }
            }
        }

        /// <summary>
        /// Event handler for clicking on the "Close tab" button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void CloseCurrentTab(object sender, EventArgs e)
        {
            // Calling the method in order to understand what he wants to do with the file that will be closed
            DialogSaveToCloseCurrentTab(closeCurrentTabToolStripMenuItem, e);
            // If there is a file and all necessary saves have been performed.
            if (curFile != null && curFile.IsSaved)
            {
                // Cleaning the list of open files and tabs.
                files.Remove(curFile);
                tabControl.TabPages.Remove(tabControl.SelectedTab);
                if (files.Count != 0)
                {
                    // We are looking among the files for the one with the TabPage property corresponding to the open tab.
                    curFile = files.Find(x => x.TabPage == tabControl.SelectedTab);
                }
                else
                {
                    // Or assign it null otherwise.
                    curFile = null;
                    // If everything is closed, then you can uncheck logging.
                    loggingToolStripMenuItem.Checked = false;
                }
            }
        }

        /// <summary>
        /// Method for calling a dialog box with a request to understand if the user wants to close the CURRENT file.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void DialogSaveToCloseCurrentTab(object sender, EventArgs e)
        {
            if (curFile != null && !curFile.IsSaved)
            {
                DialogResult result = CloseDialogWindow($"Вы хотите сохранить" +
                    $" изменения в файле \"{curFile.FileName}\"?");
                switch (result)
                {
                    // The file needs to be saved.
                    case DialogResult.Yes:
                        SaveCurrentFile(sender, e);
                        return;
                    // The file does not need to be saved, therefore, it is saved with the original value.
                    case DialogResult.No:
                        curFile.IsSaved = true;
                        return;
                    case DialogResult.Cancel:
                        return;
                }
            }
        }

        /// <summary>
        /// A helpful method that calls MessageBox with a specific text and a selection of responses: yes, no, cancel.
        /// </summary>
        /// <param name="message">A message in the MessageBox.</param>
        /// <returns>The response selected by the user.</returns>
        private static DialogResult CloseDialogWindow(string message)
        {
            DialogResult result = MessageBox.Show(message,
                "Message",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1
                );
            return result;
        }

        /// <summary>
        /// Event handler for clicking on the "Close all" button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void CloseAllTabs(object sender, EventArgs e)
        {
            // Saving unsaved files or canceling closures.
            CloseAllTabsFunc(sender, e);
            // If there are no unsaved ones left, it means that the user did not cancel any save operation.
            if (!files.Exists(x => x.Issued == false))
            {
                // The number of open files and tabs.
                files.Clear();
                tabControl.TabPages.Clear();
                curFile = null;
                // If everything is closed, then you can uncheck logging.
                loggingToolStripMenuItem.Checked = false;
            }
        }

        /// <summary>
        /// Auxiliary method for processing a response from the user regarding saving files when closing tabs
        /// with subsequent saving of all window files, if necessary.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void CloseAllTabsFunc(object sender, EventArgs e)
        {
            int needSave = -1;
            // If there are unsaved files, a request for what to do with them.
            if (files.Exists(x => x.IsSaved == false))
            {
                switch (CloseDialogWindow($"Do you want to save changes to files?"))
                {
                    case DialogResult.Yes:
                        needSave = 1;
                        break;
                    case DialogResult.No:
                        needSave = -1;
                        break;
                    case DialogResult.Cancel:
                        needSave = 0;
                        break;
                }
            }
            // The user canceled the closing option.
            if (needSave == 0)
                return;
            // We go through all the open ones and save the changes or do not accept them.
            FileInUse[] copyFiles = new FileInUse[files.Count];
            files.CopyTo(copyFiles);
            foreach (var i in copyFiles)
            {
                curFile = i;
                if (needSave == -1)
                {
                    // If you don't need to save the files, we just assume that they are saved because you don't need to apply the changes.
                    curFile.IsSaved = true;
                }
                else
                {
                    // If you need to save files, then each one will be saved, but if it is not saved,
                    // it means there was a cancellation or an error and this file cannot be saved.
                    SaveCurrentFile(sender, e);
                    if (!curFile.IsSaved)
                    {
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Function for logging files after saving them.
        /// </summary>
        private void Logging()
        {
            // Checking that there is something to save and the button is pressed.
            if (curFile != null && loggingToolStripMenuItem.Checked)
            {
                // Creating a path to the folder with logs for a specific file..\NotePad+2.0\Logs\*fileName
                string path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\", "Logs", Path.GetFileName(curFile.FileName));
                if (!Directory.Exists(path))
                {
                    HideLogDirectory();
                    Directory.CreateDirectory(path);
                }
                // Only the latest 3 versions are stored. The oldest one is being deleted.
                if (Directory.GetFiles(path).Length == 3)
                {
                    // We sort them, since they are created with names that differ only in the dates and in the place of all the norms.
                    Array.Sort(Directory.GetFiles(path));
                    File.Delete(Directory.GetFiles(path)[0]);
                }
                // New Path: ..\NotePad+2.0\Logs\fileName\fileName Date Time
                string newFile = Path.GetFileName(curFile.FileName) + " " + DateTime.Now.ToString().Replace(':', '-')
                    + Path.GetExtension(curFile.FileName);
                File.Copy(curFile.FileName, Path.Combine(path, newFile));
            }

        }

        /// <summary>
        /// Auxiliary method so that if someone deleted the Logs folder, then when the Logging method gets it
        /// make it hidden again.
        /// </summary>
        private static void HideLogDirectory()
        {
            string dirLogPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\", "Logs");
            if (!Directory.Exists(dirLogPath))
            {
                DirectoryInfo dirLogInfo = Directory.CreateDirectory(dirLogPath);
                dirLogInfo.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
        }

        /// <summary>
        /// Handler for the window closing event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void Form1Closing(Object sender, FormClosingEventArgs e)
        {
            // Closing all windows. Like clicking on the "Close All" button.
            CloseAllTabsFunc(closeAllTabsToolStripMenuItem, e);
            // If all tabs are closed, that is, all are saved in the desired state.
            if (!files.Exists(x => x.issued == false))
            {
                // Changing the parent(main) form if there are still open forms besides the current one.
                if (MyForms.GetOpened(this) != null)
                {

                    // Changing the shape.
                    Program.s_context.MainForm = MyForms.GetOpened(this);
                    // Closing the current one.
                    MyForms.Close(this);
                    // Switch to the new main form.
                    Program.s_context.MainForm.Focus();
                }
                else
                {
                    // If the current form is the last one, then we save the settings.
                    SaveSettings();
                }
            }
            else
            {
                // The issue of applying saves was not resolved with all files => we do not close the form.
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Handler for clicking on one of the menu items "0 seconds", "1 second", "5 seconds", "30 seconds", "60 seconds".
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void ChangeIntervalTime(object sender, EventArgs e)
        {
            // Uncheck all punts.
            fiveSecInterval.Checked = false;
            zeroSecInterval.Checked = false;
            twoSecInterval.Checked = false;
            thirtySecInterval.Checked = false;
            sixtySecInterval.Checked = false;
            // If you clicked on "0 seconds", then you just need to turn off saving.
            if (sender.toString() == "0 seconds")
            {
                zeroSecInterval.Checked = true;
                timer.Enabled = false;
                return;
            }
            // Defining the control.
            ToolStripMenuItem chosen = (ToolStripMenuItem)sender;
            timer.Enabled = true;
            // Making it pressed.
            chosen.Checked = true;
            // Setting the time interval, i.e. taking the first number from the name of the menu item multiplied by 1000.
            timer.Interval = int.Parse(chosen.Text.Split()[0]) * 1000;
            return;

        }
        /// <summary>
        /// An action handler for each timer trigger.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void TimerTick(object sender, EventArgs e)
        {
            // Works only with non-text files, so as not to interfere with calling the dialog box
            if (curFile != null && !curFile.IsNew)
                SaveCurrentFile(sender, e);
        }
        /// <summary>
        /// Handler for the event of clicking on the "Input field color" button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void ChangeTextBoxBackGround(object sender, EventArgs e)
        {
            // If there is an open file and the user has not canceled the color selection.
            if (curFile != null && colorDialog.ShowDialog() == DialogResult.OK)
            {
                curFile.TextBox.BackColor = colorDialog.Color;
            }
        }
        /// <summary>
        /// Handler for the event of clicking on the "Text color in the input field" button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void ChangeTextColor(object sender, EventArgs e)
        {
            // If there is an open file and the user has not canceled the color selection.
            if (curFile != null && colorDialog.ShowDialog() == DialogResult.OK)
                // If nothing is selected, it is easy to change the input text.
                curFile.TextBox.SelectionColor = colorDialog.Color;
        }

        /// <summary>
        /// The handler of the "Copy" and "Cut" events.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void SetTextInClipBoard(object sender, EventArgs e)
        {
            if (curFile != null)
            {
                if (sender.ToString().Contains("Cut"))
                {
                    curFile.TextBox.Cut();
                }
                else
                {
                    Clipboard.SetDataObject(curFile.TextBox.SelectedText);
                }
            }
        }

        /// <summary>
        /// Handler of the "Insert" event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void PasteTextFromClipBoard(object sender, EventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            // Checking that the user wants to insert exactly the text.
            if (iData.GetDataPresent(DataFormats.Text))
            {
                curFile.TextBox.Paste();
            }
            else
            {
                MessageBox.Show("Only text can be inserted!");
            }
        }

        /// <summary>
        /// Handler for the event of clicking on the "Format" button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void ChangeSelectedTextFont(object sender, EventArgs e)
        {
            if (curFile != null && fontDialog.ShowDialog() == DialogResult.OK)
                curFile.TextBox.SelectionFont = fontDialog.Font;
        }

        /// <summary>
        /// Event handler for clicking on the "Select all" button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void SelectAllText(object sender, EventArgs e)
        {
            curFile.TextBox.SelectAll();
        }

        /// <summary>
        /// Handler for the "Undo" button click event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void UndoClick(object sender, EventArgs e)
        {
            if (curFile != null)
                curFile.TextBox.Undo();
        }

        /// <summary>
        /// Handler for the "Redo" button click event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void RedoClick(object sender, EventArgs e)
        {
            if (curFile != null)
                curFile.TextBox.Redo();
        }

        /// <summary>
        /// Handler for the event of clicking on the "Application window color" button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void ChangeBackColorOfTheForm(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Changing the window color.
                this.BackColor = colorDialog.Color;
                // Calling a method that paints all elements in the window color.
                SetFormColor();
            }
        }

        /// <summary>
        /// Handler for the event of clicking on the "Application window text color" button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void ChangeForeColorOfTheForm(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Changing the window color.
                this.ForeColor = colorDialog.Color;
                // Calling a method that paints all elements in the window color.
                SetFormColor();
            }
        }
        /// <summary>
        /// Auxiliary method for coloring all menu items in the window color (both text and background).
        /// </summary>
        private void SetFormColor()
        {
            for (int i = 0; i < menuStrip.Items.Count; i++)
                SetColor(menuStrip.Items[i] as ToolStripMenuItem, this.BackColor, this.ForeColor);
            menuStrip.BackColor = this.BackColor;
            menuStrip.ForeColor = this.ForeColor;
        }
        /// <summary>
        /// A recursive method of traversing all menu items, starting with item.
        /// </summary>
        /// <param name="item">The element that is being painted and which has subsections to look at.</param>
        /// <param name="newBackColor">New background color.</param>
        /// <param name="newForeColor">New text color.</param>
        private void SetColor(ToolStripMenuItem item, Color newBackColor, Color newForeColor)
        {
            item.BackColor = newBackColor;
            item.ForeColor = newForeColor;
            foreach (ToolStripMenuItem curItem in item.DropDownItems)
                SetColor(curItem, newBackColor, newForeColor);
        }
        /// <summary>
        /// Handler for clicking on the "Close all" button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void FormHotKeyToClose(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Handler for clicking on the "Save all" button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void SaveAllFiles(object sender, EventArgs e)
        {
            // We remember the current one so that we can "switch" to it again later.
            FileInUse rememberFile = curFile;
            // Bypass all files and save them.
            foreach (var file in files)
            {
                curFile = file;
                if (!curFile.IsSaved)
                {
                    SaveCurrentFile(saveToolStripMenuItem, e);
                }
            }
            curFile = rememberFile;
        }
        /// <summary>
        /// Method for saving the settings set in the last window and at the last open tab,
        /// if there was one.
        /// </summary>
        private void saveSettings()
        {
            // Collecting a list of files that were opened in the last window.
            List<string> addition = new();
            foreach (var i in files)
            {
                // Saving non-text files that were opened.
                if (!i.IsNew)
                {
                    addition.Add(i.FileName);
                }
                // We save the settings of each open tab so that only the settings of the last one remain.
                Properties.Settings.Default.textBoxBack = i.TextBox.BackColor;
                i.TextBox.Select(0, 1);
                Properties.Settings.Default.textInBoxColor = i.TextBox.SelectionColor;
                Properties.Settings.Default.textBoxFont = i.TextBox.SelectionFont;
                Properties.Settings.Default.logging = i.Log;
            }
            // Saving the color settings of the form.
            Properties.Settings.Default.formTextColor = this.ForeColor;
            Properties.Settings.Default.formBack = this.BackColor;
            // Adding a list of open, saved files.
            Properties.Settings.Default.files.AddRange(addition.ToArray());
            // Saving the timer status.
            Properties.Settings.Default.timerIntervalFlag = timer.Enabled;
            Properties.Settings.Default.timerIntervalVal = timer.Interval;
            Properties.Settings.Default.Save();
        }
        /// <summary>
        /// Method for restoring the settings left after closing.
        /// </summary>
        private void SetSettingss()
        {
            foreach (var file in Properties.Settings.Default.files)
            {
                // Trying to open files that were opened when closing.
                if (showText(file, out string text))
                {
                    // We print it in a separate tab.
                    CreateTheTab(this, text, file, false);
                    // Setting the RichTextBox settings.
                    curFile.TextBox.BackColor = Properties.Settings.Default.textBoxBack;
                    curFile.TextBox.SelectAll();
                    curFile.TextBox.SelectionColor = Properties.Settings.Default.textInBoxColor;
                    curFile.TextBox.SelectionFont = Properties.Settings.Default.textBoxFont;
                    curFile.IsSaved = true;
                    curFile.TabPage.Text = Path.GetFileName(curFile.FileName);
                    curFile.Log = Properties.Settings.Default.logging;
                    loggingToolStripMenuItem.Checked = Properties.Settings.Default.logging;
                }
                else
                {
                    MessageBox.Show(text);
                }
            }
            // Window and text color settings, setting timer parameters.
            this.BackColor = Properties.Settings.Default.formBack;
            this.ForeColor = Properties.Settings.Default.formTextColor;
            timer.Enabled = Properties.Settings.Default.timerIntervalFlag;
            timer.Interval = Properties.Settings.Default.timerIntervalVal;
            // Repainting the menu.
            SetCheckedInterval();
            // Check the appropriate time interval.
            SetFormColor();
            Properties.Settings.Default.files.Clear();
        }

        /// <summary>
        /// Auxiliary method to determine, by a given timer time interval, which element
        /// the menu with the choice of the frequency of saving needs to be ticked.
        /// </summary>
        private void SetCheckedInterval()
        {
            if (timer.Enabled)
            {
                foreach (var i in new List<ToolStripMenuItem>() {zeroSecInterval, twoSecInterval, fiveSecInterval,
                                                                  thirtySecInterval, sixtySecInterval})
                {
                    // We take a number from the name of the control and check for compliance with the specified interval.
                    i.Checked = i.Text.Split()[0] == (timer.Interval / 1000).ToString();
                }
            }
        }
        /// <summary>
        /// Handler for clicking on the "Save previous version" button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event parameters.</param>
        private void LogFilesSwitch(object sender, EventArgs e)
        {
            if (curFile != null && !curFile.IsNew)
            {
                // If there is an open non-text file, then change the "switch" to logging.
                loggingToolStripMenuItem.Checked = !loggingToolStripMenuItem.Checked;
                // We change the same for the open file
                curFile.Log = !curFile.Log;
            }
        }
    }
}
