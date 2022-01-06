
using System.Windows.Forms;

namespace Main
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.выбратьВесьТекстToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вырезатьВыделенныйФрагментToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.скопироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.форматироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openInNewTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьВТекущейВкладкеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьВНовомОкнеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openNewTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вТекущейВкладкеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openNewFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeCurrentTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllTabsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.цветПоляВводаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.FormColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.цветТекстаПриложенияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.таймерСохраненияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zeroSecInterval = new System.Windows.Forms.ToolStripMenuItem();
            this.twoSecInterval = new System.Windows.Forms.ToolStripMenuItem();
            this.fiveSecInterval = new System.Windows.Forms.ToolStripMenuItem();
            this.thirtySecInterval = new System.Windows.Forms.ToolStripMenuItem();
            this.sixtySecInterval = new System.Windows.Forms.ToolStripMenuItem();
            this.loggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выбратьВесьТекстToolStripMenuItem,
            this.вырезатьВыделенныйФрагментToolStripMenuItem,
            this.скопироватьToolStripMenuItem,
            this.вставитьToolStripMenuItem,
            this.форматироватьToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(264, 124);
            // 
            // выбратьВесьТекстToolStripMenuItem
            // 
            this.выбратьВесьТекстToolStripMenuItem.Name = "выбратьВесьТекстToolStripMenuItem";
            this.выбратьВесьТекстToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.выбратьВесьТекстToolStripMenuItem.Size = new System.Drawing.Size(263, 24);
            this.выбратьВесьТекстToolStripMenuItem.Text = "&Выбрать весь текст";
            this.выбратьВесьТекстToolStripMenuItem.Click += new System.EventHandler(this.SelectAllText);
            // 
            // вырезатьВыделенныйФрагментToolStripMenuItem
            // 
            this.вырезатьВыделенныйФрагментToolStripMenuItem.Name = "вырезатьВыделенныйФрагментToolStripMenuItem";
            this.вырезатьВыделенныйФрагментToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.вырезатьВыделенныйФрагментToolStripMenuItem.Size = new System.Drawing.Size(263, 24);
            this.вырезатьВыделенныйФрагментToolStripMenuItem.Text = "&Вырезать";
            this.вырезатьВыделенныйФрагментToolStripMenuItem.Click += new System.EventHandler(this.SetTextInClipBoard);
            // 
            // скопироватьToolStripMenuItem
            // 
            this.скопироватьToolStripMenuItem.Name = "скопироватьToolStripMenuItem";
            this.скопироватьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.скопироватьToolStripMenuItem.Size = new System.Drawing.Size(263, 24);
            this.скопироватьToolStripMenuItem.Text = "&Скопировать";
            this.скопироватьToolStripMenuItem.Click += new System.EventHandler(this.SetTextInClipBoard);
            // 
            // вставитьToolStripMenuItem
            // 
            this.вставитьToolStripMenuItem.Name = "вставитьToolStripMenuItem";
            this.вставитьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.вставитьToolStripMenuItem.Size = new System.Drawing.Size(263, 24);
            this.вставитьToolStripMenuItem.Text = "&Вставить";
            this.вставитьToolStripMenuItem.Click += new System.EventHandler(this.PasteTextFromClipBoard);
            // 
            // форматироватьToolStripMenuItem
            // 
            this.форматироватьToolStripMenuItem.Name = "форматироватьToolStripMenuItem";
            this.форматироватьToolStripMenuItem.Size = new System.Drawing.Size(263, 24);
            this.форматироватьToolStripMenuItem.Text = "&Форматировать";
            this.форматироватьToolStripMenuItem.Click += new System.EventHandler(this.ChangeSelectedTextFont);
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 28);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripMenuItem2,
            this.saveAsToolStripMenuItem,
            this.closeCurrentTabToolStripMenuItem,
            this.closeAllTabsToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.fileToolStripMenuItem.Text = "&Файл";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openInNewTabToolStripMenuItem,
            this.создатьВТекущейВкладкеToolStripMenuItem,
            this.создатьВНовомОкнеToolStripMenuItem});
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(272, 26);
            this.newToolStripMenuItem.Text = "&Новый";
            // 
            // openInNewTabToolStripMenuItem
            // 
            this.openInNewTabToolStripMenuItem.Name = "openInNewTabToolStripMenuItem";
            this.openInNewTabToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.openInNewTabToolStripMenuItem.Size = new System.Drawing.Size(318, 26);
            this.openInNewTabToolStripMenuItem.Text = "&Создать в новой вкладке";
            this.openInNewTabToolStripMenuItem.Click += new System.EventHandler(this.OpenInNewTab);
            // 
            // создатьВТекущейВкладкеToolStripMenuItem
            // 
            this.создатьВТекущейВкладкеToolStripMenuItem.Name = "создатьВТекущейВкладкеToolStripMenuItem";
            this.создатьВТекущейВкладкеToolStripMenuItem.Size = new System.Drawing.Size(318, 26);
            this.создатьВТекущейВкладкеToolStripMenuItem.Text = "&Создать в текущей вкладке";
            this.создатьВТекущейВкладкеToolStripMenuItem.Click += new System.EventHandler(this.FileInCurrentPage);
            // 
            // создатьВНовомОкнеToolStripMenuItem
            // 
            this.создатьВНовомОкнеToolStripMenuItem.Name = "создатьВНовомОкнеToolStripMenuItem";
            this.создатьВНовомОкнеToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.создатьВНовомОкнеToolStripMenuItem.Size = new System.Drawing.Size(318, 26);
            this.создатьВНовомОкнеToolStripMenuItem.Text = "&Создать в новом окне";
            this.создатьВНовомОкнеToolStripMenuItem.Click += new System.EventHandler(this.OpenInNewForm);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openNewTabToolStripMenuItem,
            this.вТекущейВкладкеToolStripMenuItem,
            this.openNewFormToolStripMenuItem});
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(272, 26);
            this.openToolStripMenuItem.Text = "&Открыть";
            // 
            // openNewTabToolStripMenuItem
            // 
            this.openNewTabToolStripMenuItem.Name = "openNewTabToolStripMenuItem";
            this.openNewTabToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.openNewTabToolStripMenuItem.Text = "&В новой вкладке";
            this.openNewTabToolStripMenuItem.Click += new System.EventHandler(this.OpenInNewTab);
            // 
            // вТекущейВкладкеToolStripMenuItem
            // 
            this.вТекущейВкладкеToolStripMenuItem.Name = "вТекущейВкладкеToolStripMenuItem";
            this.вТекущейВкладкеToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.вТекущейВкладкеToolStripMenuItem.Text = "&В текущей вкладке";
            this.вТекущейВкладкеToolStripMenuItem.Click += new System.EventHandler(this.FileInCurrentPage);
            // 
            // openNewFormToolStripMenuItem
            // 
            this.openNewFormToolStripMenuItem.Name = "openNewFormToolStripMenuItem";
            this.openNewFormToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.openNewFormToolStripMenuItem.Text = "&В новом окне";
            this.openNewFormToolStripMenuItem.Click += new System.EventHandler(this.OpenInNewForm);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(272, 26);
            this.saveToolStripMenuItem.Text = "&Сохранить";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveCurrentFile);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.toolStripMenuItem2.Size = new System.Drawing.Size(272, 26);
            this.toolStripMenuItem2.Text = "&Сохранить все";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.SaveAllFiles);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(272, 26);
            this.saveAsToolStripMenuItem.Text = "Сохранить &как";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsCurrentFile);
            // 
            // closeCurrentTabToolStripMenuItem
            // 
            this.closeCurrentTabToolStripMenuItem.Name = "closeCurrentTabToolStripMenuItem";
            this.closeCurrentTabToolStripMenuItem.Size = new System.Drawing.Size(272, 26);
            this.closeCurrentTabToolStripMenuItem.Text = "&Закрыть вкладку";
            this.closeCurrentTabToolStripMenuItem.Click += new System.EventHandler(this.CloseCurrentTab);
            // 
            // closeAllTabsToolStripMenuItem
            // 
            this.closeAllTabsToolStripMenuItem.Name = "closeAllTabsToolStripMenuItem";
            this.closeAllTabsToolStripMenuItem.Size = new System.Drawing.Size(272, 26);
            this.closeAllTabsToolStripMenuItem.Text = "&Закрыть все вкладки";
            this.closeAllTabsToolStripMenuItem.Click += new System.EventHandler(this.CloseAllTabs);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Z)));
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(272, 26);
            this.выходToolStripMenuItem.Text = "&Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.FormHotKeyToClose);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.selectAllToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.editToolStripMenuItem.Text = "&Правка";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("undoToolStripMenuItem.Image")));
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.undoToolStripMenuItem.Text = "&Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.UndoClick);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("redoToolStripMenuItem.Image")));
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.redoToolStripMenuItem.Text = "&Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.RedoClick);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
            this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.cutToolStripMenuItem.Text = "&Вырезать";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.SetTextInClipBoard);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.copyToolStripMenuItem.Text = "&Скопировать";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.SetTextInClipBoard);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.pasteToolStripMenuItem.Text = "&Вставить";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.PasteTextFromClipBoard);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.selectAllToolStripMenuItem.Text = "&Выбрать все";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.SelectAllText);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customizeToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
            this.toolsToolStripMenuItem.Text = "&Формат";
            // 
            // customizeToolStripMenuItem
            // 
            this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
            this.customizeToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.customizeToolStripMenuItem.Text = "&Настройка шрифта";
            this.customizeToolStripMenuItem.Click += new System.EventHandler(this.ChangeSelectedTextFont);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.цветПоляВводаToolStripMenuItem,
            this.toolStripMenuItem1,
            this.FormColorToolStripMenuItem,
            this.цветТекстаПриложенияToolStripMenuItem});
            this.optionsToolStripMenuItem.Image = global::Main.Properties.Resources.png_color_gradient_color_wheel_hsl_and_hsv_rgb_color_model_color_wheel_color_gradient_magenta_color_gradient_clipart_thumb;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.optionsToolStripMenuItem.Text = "&Настройка цвета";
            // 
            // цветПоляВводаToolStripMenuItem
            // 
            this.цветПоляВводаToolStripMenuItem.Name = "цветПоляВводаToolStripMenuItem";
            this.цветПоляВводаToolStripMenuItem.Size = new System.Drawing.Size(294, 26);
            this.цветПоляВводаToolStripMenuItem.Text = "&Цвет поля для ввода";
            this.цветПоляВводаToolStripMenuItem.Click += new System.EventHandler(this.ChangeTextBoxBackGround);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(294, 26);
            this.toolStripMenuItem1.Text = "&Цвет текста в поле для ввода";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.ChangeTextColor);
            // 
            // FormColorToolStripMenuItem
            // 
            this.FormColorToolStripMenuItem.Name = "FormColorToolStripMenuItem";
            this.FormColorToolStripMenuItem.Size = new System.Drawing.Size(294, 26);
            this.FormColorToolStripMenuItem.Text = "&Цвет окна приложения";
            this.FormColorToolStripMenuItem.Click += new System.EventHandler(this.ChangeBackColorOfTheForm);
            // 
            // цветТекстаПриложенияToolStripMenuItem
            // 
            this.цветТекстаПриложенияToolStripMenuItem.Name = "цветТекстаПриложенияToolStripMenuItem";
            this.цветТекстаПриложенияToolStripMenuItem.Size = new System.Drawing.Size(294, 26);
            this.цветТекстаПриложенияToolStripMenuItem.Text = "&Цвет текста приложения";
            this.цветТекстаПриложенияToolStripMenuItem.Click += new System.EventHandler(this.ChangeForeColorOfTheForm);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.таймерСохраненияToolStripMenuItem,
            this.loggingToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(98, 24);
            this.helpToolStripMenuItem.Text = "&Hастройки";
            // 
            // таймерСохраненияToolStripMenuItem
            // 
            this.таймерСохраненияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zeroSecInterval,
            this.twoSecInterval,
            this.fiveSecInterval,
            this.thirtySecInterval,
            this.sixtySecInterval});
            this.таймерСохраненияToolStripMenuItem.Image = global::Main.Properties.Resources.png_clipart_clock_computer_icons_timer_clock_timer_stopwatch;
            this.таймерСохраненияToolStripMenuItem.Name = "таймерСохраненияToolStripMenuItem";
            this.таймерСохраненияToolStripMenuItem.Size = new System.Drawing.Size(317, 26);
            this.таймерСохраненияToolStripMenuItem.Text = "&Таймер сохранения";
            // 
            // zeroSecInterval
            // 
            this.zeroSecInterval.Checked = true;
            this.zeroSecInterval.CheckOnClick = true;
            this.zeroSecInterval.CheckState = System.Windows.Forms.CheckState.Checked;
            this.zeroSecInterval.Name = "zeroSecInterval";
            this.zeroSecInterval.Size = new System.Drawing.Size(224, 26);
            this.zeroSecInterval.Text = "0 секунд";
            this.zeroSecInterval.Click += new System.EventHandler(this.ChangeIntervalTime);
            // 
            // twoSecInterval
            // 
            this.twoSecInterval.CheckOnClick = true;
            this.twoSecInterval.Name = "twoSecInterval";
            this.twoSecInterval.Size = new System.Drawing.Size(224, 26);
            this.twoSecInterval.Text = "2 секунды";
            this.twoSecInterval.Click += new System.EventHandler(this.ChangeIntervalTime);
            // 
            // fiveSecInterval
            // 
            this.fiveSecInterval.CheckOnClick = true;
            this.fiveSecInterval.Name = "fiveSecInterval";
            this.fiveSecInterval.Size = new System.Drawing.Size(224, 26);
            this.fiveSecInterval.Text = "5 секунд";
            this.fiveSecInterval.Click += new System.EventHandler(this.ChangeIntervalTime);
            // 
            // thirtySecInterval
            // 
            this.thirtySecInterval.CheckOnClick = true;
            this.thirtySecInterval.Name = "thirtySecInterval";
            this.thirtySecInterval.Size = new System.Drawing.Size(224, 26);
            this.thirtySecInterval.Text = "30 секунд";
            this.thirtySecInterval.Click += new System.EventHandler(this.ChangeIntervalTime);
            // 
            // sixtySecInterval
            // 
            this.sixtySecInterval.CheckOnClick = true;
            this.sixtySecInterval.Name = "sixtySecInterval";
            this.sixtySecInterval.Size = new System.Drawing.Size(224, 26);
            this.sixtySecInterval.Text = "60 секунд";
            this.sixtySecInterval.Click += new System.EventHandler(this.ChangeIntervalTime);
            // 
            // loggingToolStripMenuItem
            // 
            this.loggingToolStripMenuItem.Name = "loggingToolStripMenuItem";
            this.loggingToolStripMenuItem.Size = new System.Drawing.Size(317, 26);
            this.loggingToolStripMenuItem.Text = "&Сохранять предыдущую версию";
            this.loggingToolStripMenuItem.Click += new System.EventHandler(this.LogFilesSwitch);
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tabControl.Location = new System.Drawing.Point(0, 28);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 422);
            this.tabControl.TabIndex = 2;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1Closing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openInNewTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьВТекущейВкладкеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьВНовомОкнеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openNewTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вТекущейВкладкеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openNewFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeCurrentTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllTabsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem цветПоляВводаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem FormColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem цветТекстаПриложенияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem таймерСохраненияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zeroSecInterval;
        private System.Windows.Forms.ToolStripMenuItem twoSecInterval;
        private System.Windows.Forms.ToolStripMenuItem fiveSecInterval;
        private System.Windows.Forms.ToolStripMenuItem thirtySecInterval;
        private System.Windows.Forms.ToolStripMenuItem sixtySecInterval;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem выбратьВесьТекстToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вырезатьВыделенныйФрагментToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem скопироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem форматироватьToolStripMenuItem;
        private ToolStripMenuItem выходToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem loggingToolStripMenuItem;
    }
}

